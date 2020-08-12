//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;

//namespace SafeCity2607last.Controllers
//{
//    //[Authorize(Roles = "Admin,SuperAdmin")]
//    public class RoleController : Controller
//    {

//        RoleManager<IdentityRole> roleManager;

//        /// 
//        /// Injecting Role Manager
//        /// 
//        /// 
//        public RoleController(RoleManager<IdentityRole> roleManager)
//        {
//            this.roleManager = roleManager;
//        }
//        //[Authorize(Roles = "Admin")]
//        [Authorize(Policy = "writepolicy")]
//        // [Authorize(Policy = "readpolicy")]
//        public IActionResult Index()
//        {
//            var roles = roleManager.Roles.ToList();
//            return View(roles);
//        }

//        [Authorize(Policy = "writepolicy")]
//        public IActionResult Create()
//        {
//            return View(new IdentityRole());
//        }
//        //[Authorize(Policy = "writepolicy")]
//        [HttpPost]
//        public async Task<IActionResult> Create(IdentityRole role)
//        {
//            await roleManager.CreateAsync(role);
//            return RedirectToAction("Index");
//        }
//        [Authorize(Policy = "writepolicy")]
//        [HttpPost]
//        public async Task<IActionResult> Delete(string id)
//        {
//            IdentityRole role = await roleManager.FindByIdAsync(id);
//            if (role != null)
//            {
//                IdentityResult result = await roleManager.DeleteAsync(role);
//                if (result.Succeeded)
//                    return RedirectToAction("Index");
//                else
//                    Errors(result);
//            }
//            else
//                ModelState.AddModelError("", "No role found");
//            return View("Index", roleManager.Roles);
//        }

//        private void Errors(IdentityResult result)
//        {
//            foreach (IdentityError error in result.Errors)
//                ModelState.AddModelError("", error.Description);
//        }

//    }
//}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SafeCity2607last.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafeCity2607last.Controllers
{
    public class RoleController : Controller
    {
        // GET: /<controller>/
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }
        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            foreach (ApplicationUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }

        public async Task<IActionResult> List(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            foreach (ApplicationUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        public ViewResult Index() => View(roleManager.Roles);

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [Authorize(Policy = "writepolicy")]
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }
        //[Authorize(Policy = "writepolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(string id)
        {
    
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }
    }
}