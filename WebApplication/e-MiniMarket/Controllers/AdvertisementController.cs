using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emarket.Core.Application.Helpers;
using e_MiniMarket.Middleware;
using Emarket.Core.Application.ViewModels.Ads;
using Microsoft.AspNetCore.Http;

namespace e_MiniMarket.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly ICategoryService _categoryService;
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementController(ICategoryService categoryService, IAdvertisementService advertisementService, ValidateUserSession userSession)
        {
            this._categoryService = categoryService;
            this._validateUserSession = userSession;
            this._advertisementService = advertisementService;
        }

  
        public async Task<IActionResult> Index()
        {
            if(!_validateUserSession.HasUserLogged())
                    return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            List<AdvertisementViewModel> advertasiments = await _advertisementService.GetAllViewModelAsync();

            return View("Index", advertasiments);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            AdvertisementSaveViewModel saveViewModel = new AdvertisementSaveViewModel();
            saveViewModel.Categories = await _categoryService.GetAllViewModelAsync();

            return View("Save",  saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementSaveViewModel advertisementSave)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            if (!ModelState.IsValid)
            {
                return View("Save", advertisementSave);
            }

            var advertasiment = await _advertisementService.AddAsync(advertisementSave);

            //PROceso de anadir la imagen y update
            IFormFile[] fileForms = new IFormFile[] { advertisementSave.FirstImage, advertisementSave.SecondImage, advertisementSave.ThirdImage, advertisementSave.FourthImage };

            List<string> imgUrls = await ImageUpload.FileUpload(fileForms,advertasiment.AdvertisementId, "Advertisements");
            advertasiment.FirstImg = imgUrls[0];
            advertasiment.SecondImg = imgUrls[1];
            advertasiment.ThirdImg = imgUrls[2];
            advertasiment.FourthImg = imgUrls[3];

            await _advertisementService.UpdateAsync(advertasiment);


            return RedirectToRoute(new { controller="Advertisement", action = "Index" });
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            AdvertisementSaveViewModel saveViewModel = await _advertisementService.GetByIdSaveViewModelAsync(id);
            saveViewModel.Categories = await _categoryService.GetAllViewModelAsync();

            return View("Save", saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdvertisementSaveViewModel advertisementSave)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            if (!ModelState.IsValid)
            {
                return View("Save", advertisementSave);
            }

            //Aqui con esta nueva busqueda por ID traemos las imagenes con las URL ya seguras.
            var advertasiment = await _advertisementService.GetByIdSaveViewModelAsync(advertisementSave.AdvertisementId);

            //Proceso de anadir la imagen y update
            IFormFile[] fileForms = new IFormFile[] { advertisementSave.FirstImage, advertisementSave.SecondImage, advertisementSave.ThirdImage, advertisementSave.FourthImage };


            List<string> imgUrls = new List<string>() { advertasiment.FirstImg, advertasiment.SecondImg, advertasiment.ThirdImg, advertasiment.FourthImg };

            //Cuidado esta reaccionacion puede confundir en una futura revision :)
            //Proceso de reasignacion.
            imgUrls = await ImageUpload.FileUpload(fileForms, advertasiment.AdvertisementId, true, "Advertisements", imgUrls);

            //Mapping the new state.
            advertasiment.FirstImg = imgUrls[0]; // si estan vacias, quiere decir que no subieron nada por lo que mantengo el valor anterior.
            advertasiment.SecondImg = imgUrls[1];
            advertasiment.ThirdImg = imgUrls[2];
            advertasiment.FourthImg = imgUrls[3];
            advertasiment.ProductName = advertisementSave.ProductName;
            advertasiment.Description = advertisementSave.Description;
            advertasiment.CategoryId = advertisementSave.CategoryId;

            await _advertisementService.UpdateAsync(advertasiment);

            return RedirectToRoute(new { controller = "Advertisement", action = "Index" });
        }
        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            AdvertisementSaveViewModel saveViewModel = await _advertisementService.GetByIdSaveViewModelAsync(id);

            return View("Delete", saveViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(AdvertisementSaveViewModel advertisementSave)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            await _advertisementService.DeleteAsync(advertisementSave.AdvertisementId);

            ImageUpload.DeleteFile(advertisementSave.AdvertisementId, "Advertisements");

            return RedirectToRoute(new { controller = "Advertisement", action = "Index" });
        }

        #endregion


        #region Full Details

        public async Task<IActionResult> ProductDetails(int id)
        {
            if (!_validateUserSession.HasUserLogged())
                return RedirectToRoute(new { controller = "Authentication", action = "Index" });

            var productDetail = await _advertisementService.GetAdvertisementWithFullProperties(id);

            return View("FullViewAdvertisement", productDetail);
        }

        #endregion


    }
}
