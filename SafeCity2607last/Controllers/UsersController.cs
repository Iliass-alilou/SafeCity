using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SafeCity2607last.Models;
using Microsoft.AspNetCore.Authorization;

namespace SafeCity2607last.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        
        //UserManager<ApplicationUser> userManager;
        private UserManager<ApplicationUser> userManager;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public UsersController(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHash)
        {
            this.userManager = userManager;
            passwordHasher = passwordHash;
        }
        /// 
        /// Injecting Role Manager
        /// 
        /// 
        //public UsersController(UserManager<ApplicationUser> userManager)
        //{
        //    this.userManager = userManager;
        //}

        public IActionResult Index()
        {
            var users = userManager.Users.ToList();

            return View(users);
          
        }


        public IActionResult Create()
        {
            return View(new ApplicationUser());
            //            < li class="nav-item">
            //    <a class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Register">Register</a>
            //</li>
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            await userManager.CreateAsync(user);
            return RedirectToAction("/ Account / Register");
        }

          [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", userManager.Users);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }



        public async Task<IActionResult> Update(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }



        [HttpPost]
        //public async Task<IActionResult> Update(string id, string email, string password
                    public async Task<IActionResult> Update
            (string id,
            string email,
            string password,
            string FirstName,
            string LastName,
            string PhoneNumber)
       
        // string FirstName,
        //string LastName,
        //string Function,

        //string CIN,

        //string Adresse,

        //string City,

        //string PhoneNumber1,


        //string PhoneNumber2,

        //string photo,



        //string Lati,


        //string Long)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null) 
            {

                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }

                //if (!string.IsNullOrEmpty(FirstName))
                //    user.FirstName = FirstName;
                //else
                //    ModelState.AddModelError("", "FirstName cannot be empty");

                //if (!string.IsNullOrEmpty(LastName))
                //    user.LastName = LastName;
                //else
                //    ModelState.AddModelError("", "LastName cannot be empty");

                //if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                //{
                //    IdentityResult result = await userManager.UpdateAsync(user);
                //    if (result.Succeeded)
                //        return RedirectToAction("Index");
                //    else
                //        Errors(result);
                //}













            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        //private void Errors(IdentityResult result)
        //{
        //    foreach (IdentityError error in result.Errors)
        //        ModelState.AddModelError("", error.Description);
        //}
    }
}





//using System;
//using System.Threading.Tasks;
//using WebAppPLatForm2309100Ex.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;


//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebAppPLatForm2309100Ex.Controllers
//{


//    [Authorize(Roles="SuperAdmin")]
//    public class UsersController : Controller
//    {
//        private UserManager<ApplicationUser> userManager;
//        private IPasswordHasher<ApplicationUser> passwordHasher;

//        public UsersController(UserManager<ApplicationUser> usrMgr, IPasswordHasher<ApplicationUser> passwordHash)
//        {
//            userManager = usrMgr;
//            passwordHasher = passwordHash;
//        }

//        // GET: /<controller>/
//        public IActionResult Index()
//        {
//            return View(userManager.Users);
//        }
//        public async Task<IActionResult> Update(string id)
//        {
//            ApplicationUser user = await userManager.FindByIdAsync(id);
//            if (user != null)
//                return View(user);
//            else
//                return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public async Task<IActionResult> Update(string id, string email, string password)
//        {
//            ApplicationUser user = await userManager.FindByIdAsync(id);
//            if (user != null)
//            {
//                if (!string.IsNullOrEmpty(email))
//                    user.Email = email;
//                else
//                    ModelState.AddModelError("", "Email cannot be empty");
//                if (!string.IsNullOrEmpty(password))

//                    user.PasswordHash = passwordHasher.HashPassword(user, password);
//                else
//                    ModelState.AddModelError("", "Password cannot be empty");

//                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
//                {
//                    IdentityResult result = await userManager.UpdateAsync(user);
//                    if (result.Succeeded)
//                        return RedirectToAction("Index");
//                    else
//                        Errors(result);
//                }
//            }
//            else
//                ModelState.AddModelError("", "User Not Found");
//            return View(user);
//        }

//        private void Errors(IdentityResult result)
//        {
//            foreach (IdentityError error in result.Errors)
//                ModelState.AddModelError("", error.Description);
//        }
//        public async Task<IActionResult> Delete(string id)
//        {
//            ApplicationUser user = await userManager.FindByIdAsync(id);
//            if (user != null)
//            {
//                IdentityResult result = await userManager.DeleteAsync(user);
//                if (result.Succeeded)
//                    return RedirectToAction("Index");
//                else
//                    Errors(result);
//            }
//            else
//                ModelState.AddModelError("", "User Not Found");
//            return View("Index", userManager.Users);
//        }
//    }
//}
