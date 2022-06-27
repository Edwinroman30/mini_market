using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emarket.Core.Application.Helpers;
using e_MiniMarket.Middleware;
using Emarket.Core.Application.ViewModels.Category;

namespace e_MiniMarket.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, ValidateUserSession userSession)
        {
            this._categoryService = categoryService;
            this._validateUserSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            List<CategoryViewModel> categoryView = await _categoryService.GetAllViewModelAsync();

            return View("Index", model: categoryView);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            return View("Save", model: new CategorySaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategorySaveViewModel saveViewModel)
        {
            
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            if (!ModelState.IsValid)
            {
                return View("Save", model: saveViewModel);
            }

            //I can verify if the process was success
            CategorySaveViewModel categoryView = await _categoryService.AddAsync(saveViewModel);

            return RedirectToRoute(new { action = "Index" });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            var category = await _categoryService.GetByIdSaveViewModelAsync(id);

            return View(viewName: "Save", model: category);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategorySaveViewModel categorySaveView)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            if(!ModelState.IsValid)
                return View(viewName: "Save", model: categorySaveView);

            await _categoryService.UpdateAsync(categorySaveView);

            return RedirectToRoute(new { action = "Index" });

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            var model = await _categoryService.GetByIdSaveViewModelAsync(id);

            return View("Delete", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategorySaveViewModel categorySave)
        {
            if (!_validateUserSession.HasUserLogged() || categorySave.CategoryId == 0) //The last validation does not make sense, but...
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            await _categoryService.DeleteAsync(categorySave.CategoryId);

            return RedirectToRoute(new { action = "Index" });
        }





    }

}
