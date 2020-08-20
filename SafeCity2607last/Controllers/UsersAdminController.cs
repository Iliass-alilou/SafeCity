using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SafeCity2607last.Models;
using Microsoft.AspNetCore.Authorization;
using SafeCity2607last.Data;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Microsoft.EntityFrameworkCore;
using System;

namespace SafeCity2607last.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
    {

        ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public UsersAdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHash)
        {

            _context = context;

            this.userManager = userManager;
            passwordHasher = passwordHash;
        }

        // Select by Function Admin
        public IActionResult Index()
        {
            var users = _context.Users.FromSql("SELECT * FROM AspNetUsers where City='Rabat'").ToList();

            return View(users);
        }




        public IActionResult Create()
        {
            return View(new ApplicationUser());

        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            await userManager.CreateAsync(user);
            return RedirectToAction("/ Account / Register");
        }










        // Update 
        public async Task<IActionResult> Update(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }



        [HttpPost]

        public async Task<IActionResult> Update(
            string profilePicture,
            string firstName,
            string lastName,
            string email,
            string city,
            string adresse,
            string cin,
            string phoneNumber,
            string phoneNumber2,
            string function,
            string id
            )
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {

                //----------------------

                if (!string.IsNullOrEmpty(firstName))
                    user.FirstName = firstName;
                else
                    ModelState.AddModelError("", "First Name cannot be empty");


                //----------------------

                if (!string.IsNullOrEmpty(lastName))
                    user.LastName = lastName;
                else
                    ModelState.AddModelError("", "Last Name cannot be empty");

                //-------------------------

                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");


                //-----------------------------

                if (!string.IsNullOrEmpty(city))
                    user.City = city;
                else
                    ModelState.AddModelError("", "Email cannot be empty");


                //-----------------------------

                if (!string.IsNullOrEmpty(adresse))
                    user.Adresse = adresse;
                else
                    ModelState.AddModelError("", "Adresse cannot be empty");

                //-----------------------------

                if (!string.IsNullOrEmpty(cin))
                    user.CIN = cin;
                else
                    ModelState.AddModelError("", "Adresse cannot be empty");


                //--------------------------------
                if (!string.IsNullOrEmpty(phoneNumber))
                    user.PhoneNumber = phoneNumber;
                else
                    ModelState.AddModelError("", "Phone Number1 cannot be empty");

                //-------------------------------------------
                if (!string.IsNullOrEmpty(phoneNumber2))
                    user.PhoneNumber2 = phoneNumber2;
                else
                    ModelState.AddModelError("", "Phone Number2 cannot be empty");


                //---------------------------------------------
                if (!string.IsNullOrEmpty(function))
                    user.Function = function;
                else
                    ModelState.AddModelError("", "Function cannot be empty");



                //---------------------------------------------------------------------------------

                if (!string.IsNullOrEmpty(email) /*&& !string.IsNullOrEmpty(password)*/)
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
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

    }
}





