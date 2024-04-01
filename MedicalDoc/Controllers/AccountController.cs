using MedicalDoc.DAL.Services;
using MedicalDoc.Helpers;
using MedicalDoc.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using MedicalDoc.Models;
using MedicalDoc.Security;

namespace MedicalDoc.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }

        [AllowAnonymous]

        public IActionResult Login()
        {
           UserViewModel model = new UserViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _service.CheckAccount(model.Username, model.Password);
            if (!user.success)
            {
                model.success = false;
                model.message = user.message;
                return View(model);
            }

            try
            {


                var setLoginResult = await SetLoginAsync(user.MRObject);
                if (setLoginResult.success)
                    return RedirectToAction("Index", "Patients");
                else
                {
                    model.success = false;
                    model.message = setLoginResult.message;
                }
            }
            catch (Exception ex)
            {
                model.success = false;
                model.message = ex.Message;
            }
            return View(model);

        }

        public async Task<AppResult> SetLoginAsync(Account user)
        {
            AppResult result = new AppResult();
            try
            {
                this.SetSessionValue(SessionConstants.UserId, user.Id.ToString());
                this.SetSessionValue(SessionConstants.Username, user.Name);

                

                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;


                var claims = new List<Claim>{
                                    new Claim(ClaimTypes.Name, user.Name),
                                    new Claim(ClaimTypes.GivenName, user.Username)
                    
                };

    
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            if (HttpContext.Session.IsAvailable)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Login", "Account");
        }
    }

}