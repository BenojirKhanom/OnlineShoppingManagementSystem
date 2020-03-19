using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreProject.Models;

namespace AspNetCoreProject.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<ApplicationUser> _signInManager;
        UserManager<ApplicationUser> _userManager;
        RoleManager<ApplicationRole> _rollManager;
        ApplicationDbContext _applicationDbContext;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> rollManager, ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _rollManager = rollManager;
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RollMappingToUserList()
        {


            List<DropDownListValue> ddlv = _userManager.Users.ToList().Select(s => new DropDownListValue { Id = s.Email, Name = s.Email }).ToList();

            var ViewDropDown = new ViewDropDown()
            {
                DropDownListValues = ddlv,
                DropdownName = "MyDropDownl"
            };


            return View(ViewDropDown);
        }

        [HttpPost]
        public IActionResult RollMappingToUserList(string UserName)
        {
            string url = "RollMappingToUser?UserName=" + UserName;

            return Redirect(url);

        }

        public IActionResult RollMappingToUser(string UserName)
        {
            ViewData["UserName"] = UserName;
            List<UserRollMapping> RollList = new List<UserRollMapping>();
            foreach (var r in _rollManager.Roles.ToList())
            {
                RollList.Add(new UserRollMapping() { Role = r, IsChecked = true });
            }


            return View(RollList);
        }


        [HttpPost]
        public async Task<IActionResult> RollMappingToUser(IList<UserRollMapping> applicationRoles, string UserName)
        {

            try
            {
                var user = _userManager.Users.Where(w => w.Email == UserName).FirstOrDefault();

                foreach (var k in applicationRoles)
                {
                    if (k.IsChecked == true)
                    {
                        try
                        {
                            await _userManager.AddToRoleAsync(user, k.Role.Name);
                        }
                        catch(Exception )
                        {

                        }
                        
                    }

                }
                return RedirectToAction("Index", "Home");

            }
            catch (Exception )
            {
                return View();
            }




        }


        public IActionResult Roll()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Roll(string RollName)
        {

            ApplicationRole role = new ApplicationRole()
            {
                Name = RollName,
                NormalizedName = RollName
            };

            IdentityResult result = await _rollManager.CreateAsync(role);

            if (result.Succeeded)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }


        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(string FirstName, string LastName, string Email, string Password, int registerId)
        {

            var StudentModel = _applicationDbContext.StudentModel.Where(w => w.Id == registerId).FirstOrDefault();

            ApplicationUser user = new ApplicationUser()
            {
                Email = Email,
                UserName = Email,
                FirstName = FirstName,
                LastName = LastName
            };
            IdentityResult result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }


        }


        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string ReturnUrl)
        {
            var userAll = _userManager.Users.ToList();

            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");

            }

            var UserInstance = _userManager.Users.Where(w => w.Email == userName).FirstOrDefault();
            IList<string> rolesList = await _userManager.GetRolesAsync(UserInstance);

            

            bool yesFound = await _userManager.CheckPasswordAsync(UserInstance, password);

            if (!yesFound)
            {
                return RedirectToAction("Index", "Home");
            }


            var customClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserInstance.UserName)
            };

            foreach (string s in rolesList)
            {
                customClaims.Add(new Claim(ClaimTypes.Role, s));
            }

            var claimsIdentity = new ClaimsIdentity(customClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

         

            await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme,
                claimsPrincipal, new AuthenticationProperties { IsPersistent = false });


            return RedirectToAction("Index", "Home");

        }

        public IActionResult Denied()
        {

            return RedirectToAction("Login");
        }

        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.Context.SignOutAsync();


            return RedirectToAction("Login");
        }

        [HttpGet]

        [ActionName("Logout")]
        public async Task<IActionResult> LogoutGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete("YourAppCookieName");

            return RedirectToAction("Login");
        }
    }
}