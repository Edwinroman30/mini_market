using e_MiniMarket.Middleware;
using e_MiniMarket.Models;
using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.Ads;
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
        private readonly ValidateUserSession _validateUserSession;
        private readonly IAdvertisementService _advertisementService;
        private readonly ICategoryService _categoryService;

        public HomeController( ValidateUserSession validateUser, IAdvertisementService advertisement, ICategoryService categoryService)
        {
            _validateUserSession = validateUser;
            _advertisementService = advertisement;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AdvertisementFilterViewModel filterViewModel)
        {
            
            List<AdvertisementViewModel> advertisements = await _advertisementService.GetAdvertisementWithFilter(filterViewModel);
            ViewBag.Categories = await _categoryService.GetAllViewModelAsync();

            if(!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            return View(model : advertisements);
        }

      

    }
}
