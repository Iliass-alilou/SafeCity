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

        //private void Errors(IdentityResult result)
        //{
        //    foreach (IdentityError error in result.Errors)
        //        ModelState.AddModelError("", error.Description);
        //}
    }
}





