using e_MiniMarket.Middleware;
using e_MiniMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace e_MiniMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(ILogger<HomeController> logger, ValidateUserSession validateUser)
        {
            _logger = logger;
            _validateUserSession = validateUser;

        }

        public IActionResult Index()
        {
            if(!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            return View();
        }

      

    }
}
