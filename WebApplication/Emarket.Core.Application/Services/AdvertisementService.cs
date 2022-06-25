using Emarket.Core.Application.Interfaces.Repositories;
using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.Ads;
using Emarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementService;

        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            this._advertisementService = advertisementRepository;
            //USE A USERVIEWMODEL TODO
        }

        public async Task<AdvertisementSaveViewModel> AddAsync(AdvertisementSaveViewModel vm)
        {

            //From SAVEVM to Model
            Advertisement advertisement = new Advertisement()
            {
                AdvertisementId = vm.AdvertisementId,
                ProductName = vm.ProductName,
                CategoryId = vm.CategoryId,
                UserId = vm.UserId,  //MUST COMING FROM SESSION
                /// TODO
                FirstImage = vm.FirstImg,
                SecondImage = vm.SecondImg,
                ThirdImage = vm.ThirdImg,
                FourthImage = vm.FourthImg,
                Price = vm.Price
            };

            Advertisement advertisementReturned = await _advertisementService.AddAsync(advertisement);

            //From Model to SaveViewModel

            return new AdvertisementSaveViewModel()
            {
                AdvertisementId = advertisementReturned.AdvertisementId,
                ProductName = advertisementReturned.ProductName,
                CategoryId = advertisementReturned.CategoryId,
                UserId = advertisementReturned.UserId,
                FirstImg = advertisementReturned.FirstImage,
                SecondImg = advertisementReturned.SecondImage,
                ThirdImg = advertisementReturned.ThirdImage,
                FourthImg = advertisementReturned.FourthImage,
                Price = advertisementReturned.Price
            };


        }

        public async Task DeleteAsync(int id)
        {
            Advertisement advertisement = await _advertisementService.GetByIdAsync(id);

            await _advertisementService.DeleteAsync(advertisement);

        }


        public async Task<List<AdvertisementViewModel>> GetAdvertisementWithFilter(AdvertisementFilterViewModel filterViewModel)
        {

            List<Advertisement> result = await _advertisementService.GetAllWithPropertyAsync( new List<string> { "Category", "User" } );

            List<AdvertisementViewModel> advertisementViewModels = 
                result.Select( advertisements => new AdvertisementViewModel()
                {
                    AdvertisementId = advertisements.AdvertisementId,
                    ProductName = advertisements.ProductName,
                    CategoryId = advertisements.CategoryId,
                    UserId = advertisements.UserId,
                    FirstImage = advertisements.FirstImage,
                    SecondImage = advertisements.SecondImage,
                    ThirdImage = advertisements.ThirdImage,
                    FourthImage = advertisements.FourthImage,
                    Price = advertisements.Price
                })
                .Where(ads => ads.UserId != 2 ) // TODO: PUT the user session id
                .ToList();


            if(filterViewModel.CategoryId != 0 && filterViewModel != null)
            {
                advertisementViewModels = advertisementViewModels.Where(ads => ads.CategoryId == filterViewModel.CategoryId).ToList();
            }

            if (!string.IsNullOrEmpty(filterViewModel.ProductName) && filterViewModel != null)
            {
                advertisementViewModels = advertisementViewModels.Where(ads => ads.ProductName == filterViewModel.ProductName).ToList();
            }
            
            return advertisementViewModels;
        }

        public async Task<List<AdvertisementViewModel>> GetAllViewModelAsync()
        {
            List<Advertisement> result = await _advertisementService.GetAllWithPropertyAsync(new List<string> { "Category", "User" });

            List<AdvertisementViewModel> advertisementViewModels =
                result.Select(advertisements => new AdvertisementViewModel()
                {
                    AdvertisementId = advertisements.AdvertisementId,
                    ProductName = advertisements.ProductName,
                    CategoryId = advertisements.CategoryId,
                    UserId = advertisements.UserId,
                    FirstImage = advertisements.FirstImage,
                    SecondImage = advertisements.SecondImage,
                    ThirdImage = advertisements.ThirdImage,
                    FourthImage = advertisements.FourthImage,
                    Price = advertisements.Price
                })
                //.Where(ads => ads.UserId != 2) // TODO: PUT the user session id
                .ToList();

            return advertisementViewModels;
        }

        public async Task<AdvertisementSaveViewModel> GetByIdSaveViewModelAsync(int id)
        {
            Advertisement advertisement = await _advertisementService.GetByIdAsync(id);

            if (advertisement == null || advertisement.AdvertisementId == 0)
                return null;

            //From Model to SaveViewModel

            return new AdvertisementSaveViewModel()
            {
                AdvertisementId = advertisement.AdvertisementId,
                ProductName = advertisement.ProductName,
                CategoryId = advertisement.CategoryId,
                UserId = advertisement.UserId,
                FirstImg = advertisement.FirstImage,
                SecondImg = advertisement.SecondImage,
                ThirdImg = advertisement.ThirdImage,
                FourthImg = advertisement.FourthImage,
                Price = advertisement.Price
            };


        }

        public async Task UpdateAsync(AdvertisementSaveViewModel vm)
        {

            //From SAVEVM to Model
            Advertisement advertisement = new Advertisement()
            {
                AdvertisementId = vm.AdvertisementId,
                ProductName = vm.ProductName,
                CategoryId = vm.CategoryId,
                UserId = vm.UserId,  //MUST COMING FROM SESSION
                /// TODO
                FirstImage = vm.FirstImg,
                SecondImage = vm.SecondImg,
                ThirdImage = vm.ThirdImg,
                FourthImage = vm.FourthImg,
                Price = vm.Price
            };


            await _advertisementService.UpdateAsync(advertisement);

        }




    }

}
