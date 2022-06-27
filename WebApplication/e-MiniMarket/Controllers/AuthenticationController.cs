using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emarket.Core.Application.Helpers;
using e_MiniMarket.Middleware;

namespace e_MiniMarket.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public AuthenticationController(IUserService userService, ValidateUserSession userSession)
        {
            this._userService = userService;
            this._validateUserSession = userSession;

        }

        #region Login
        public IActionResult Index()
        {
            if(_validateUserSession.HasUserLogged())
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
           
            return View("Signin");

        }

        public IActionResult Signin()
        {
            if (_validateUserSession.HasUserLogged())
                   return RedirectToRoute(new { controller = "Home", action = "Index" });
        
            return View("Signin");
        }

        [HttpPost]
        public async Task<IActionResult> Signin(UserLoginViewModel userLoginView)
        {
            if (!ModelState.IsValid)
            {
                return View("Signin", userLoginView);
            }
           

            UserViewModel user = await _userService.LoginAsync(userLoginView);
           
            if (user != null)
            {
                user.Password = "";

                //Stablishing the session.
                HttpContext.Session.Set<UserViewModel>("user_session", user);

                //Set to the session
                return RedirectToRoute(new { controller = "Home", action = "Index" } );
            }
            else
            {
                ModelState.AddModelError("loginValidation", "Invalid Data.");
                return View("Signin", userLoginView);
            }

        }
        #endregion

        #region SignUP
        public IActionResult Signup()
        {
            if (_validateUserSession.HasUserLogged())
                    return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View("Signup");
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserSaveViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Signup", saveViewModel);
            }

            var checkout_name = await _userService.GetAndUserNameValidationAsync(saveViewModel);

            if (checkout_name != null)
            {
                ModelState.AddModelError("SignError", "Username already exist. Please provide a new one.");
                return View("Signup", saveViewModel);
            }


            var user = await _userService.AddAsync(saveViewModel);

            if (user != null)
            {
                //Set to the session
                return RedirectToRoute(new { controller = "Authentication", action = "Signin" });
            }

            return View("Signup");

        }
        #endregion


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user_session");
            return RedirectToRoute(new { controller = "Authentication", action = "Index" });
        }


    }
}
