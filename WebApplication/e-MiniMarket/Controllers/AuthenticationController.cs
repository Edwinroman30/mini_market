using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_MiniMarket.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        
        public AuthenticationController(IUserService userService)
        {
            this._userService = userService;
        }

        #region Login
        public IActionResult Index()
        {
            return View("Signin");
        }

        public IActionResult Signin()
        {
            return View("Signin");
        }

        [HttpPost]
        public async Task<IActionResult> Signin(UserLoginViewModel userLoginView)
        {
            if (!ModelState.IsValid)
            {
                return View("Signin", userLoginView);
            }
           

            var user = await _userService.LoginAsync(userLoginView);

            if (user != null)
            {
                //Set to the session
                return RedirectToRoute(new { controller = "Home", action = "Index" } );
            }
            else
            {
                ModelState.AddModelError("userValidation", "Invalid Data.");//change
                return View("Signin", userLoginView);
            }

        }
        #endregion


        public IActionResult Signup()
        {
            return View("Signup");
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserSaveViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
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


    }
}
