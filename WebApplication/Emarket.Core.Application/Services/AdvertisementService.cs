using Emarket.Core.Application.Interfaces.Repositories;
using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.Ads;
using Emarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emarket.Core.Application.Helpers;
using Emarket.Core.Application.ViewModels.User;

namespace Emarket.Core.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IHttpContextAccessor _httpContext;
        private UserViewModel _userViewModel; 

        public AdvertisementService(IAdvertisementRepository advertisementRepository, IHttpContextAccessor accessor)
        {
            this._advertisementRepository = advertisementRepository;
            this._httpContext = accessor;

            _userViewModel = _httpContext.HttpContext.Session.Get<UserViewModel>("user_session");
        }

        public async Task<AdvertisementSaveViewModel> AddAsync(AdvertisementSaveViewModel vm)
        {

            //From SAVEVM to Model
            Advertisement advertisement = new Advertisement()
            {
                AdvertisementId = vm.AdvertisementId,
                ProductName = vm.ProductName,
                CategoryId = vm.CategoryId,
                UserId = _userViewModel.UserId,  //MUST COMING FROM SESSION
                FirstImage = vm.FirstImg,
                SecondImage = vm.SecondImg,
                ThirdImage = vm.ThirdImg,
                FourthImage = vm.FourthImg,
                Price = vm.Price,
                Description = vm.Description
            };

            Advertisement advertisementReturned = await _advertisementRepository.AddAsync(advertisement);

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
                Price = advertisementReturned.Price,
                Description = advertisementReturned.Description
            };


        }

        public async Task DeleteAsync(int id)
        {
            Advertisement advertisement = await _advertisementRepository.GetByIdAsync(id);

            await _advertisementRepository.DeleteAsync(advertisement);

        }


        public async Task<List<AdvertisementViewModel>> GetAdvertisementWithFilter(AdvertisementFilterViewModel filterViewModel)
        {

            List<Advertisement> result = await _advertisementRepository.GetAllWithPropertyAsync( new List<string> { "Category", "User" } );

            List<AdvertisementViewModel> advertisementViewModels = 
                result
                .Where(ads => ads.UserId != _userViewModel.UserId)
                .Select( advertisements => new AdvertisementViewModel()
                {
                    AdvertisementId = advertisements.AdvertisementId,
                    ProductName = advertisements.ProductName,
                    CategoryId = advertisements.CategoryId,
                    UserId = advertisements.UserId,
                    FirstImage = advertisements.FirstImage,
                    SecondImage = advertisements.SecondImage,
                    ThirdImage = advertisements.ThirdImage,
                    FourthImage = advertisements.FourthImage,
                    Price = advertisements.Price,
                    CategoryName = advertisements.Category.CategoryName,
                    Description = advertisements.Description
                })
                .ToList();


            if(filterViewModel.CategoryId != 0 && filterViewModel != null)
            {
                advertisementViewModels = advertisementViewModels.Where(ads => ads.CategoryId == filterViewModel.CategoryId).ToList();
            }

            if (!string.IsNullOrEmpty(filterViewModel.ProductName) && filterViewModel != null)
            {
                advertisementViewModels = advertisementViewModels.Where(ads => ads.ProductName == filterViewModel.ProductName || ads.ProductName.Contains(filterViewModel.ProductName) ).ToList();
            }
            
            return advertisementViewModels;
        }

        public async Task<List<AdvertisementViewModel>> GetAllViewModelAsync()
        {
            List<Advertisement> result = await _advertisementRepository.GetAllWithPropertyAsync(new List<string> { "Category", "User" });

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
                    Price = advertisements.Price,
                    CategoryName = advertisements.Category.CategoryName,
                    Description = advertisements.Description
                })
                .Where(ads => ads.UserId == _userViewModel.UserId) // TODO: PUT the user session id
                .ToList();

            return advertisementViewModels;
        }

        public async Task<AdvertisementCardViewModel> GetAdvertisementWithFullProperties(int id)
        {
            List<Advertisement> result = await _advertisementRepository.GetAllWithPropertyAsync(new List<string> { "Category", "User" });

            AdvertisementCardViewModel advertisementFullProperties =
                result
                .Select(advertisement => new AdvertisementCardViewModel()
                {
                    AdvertisementId = advertisement.AdvertisementId,
                    ProductName = advertisement.ProductName,
                    CategoryId = advertisement.CategoryId,
                    UserId = advertisement.UserId,
                    FirstImage = advertisement.FirstImage,
                    SecondImage = advertisement.SecondImage,
                    ThirdImage = advertisement.ThirdImage,
                    FourthImage = advertisement.FourthImage,
                    Price = advertisement.Price,
                    CategoryName = advertisement.Category.CategoryName,
                    DateofCreating = advertisement.CreatedAt,
                    AdvertiserName = advertisement.User.Name,
                    AdvertiserEmail = advertisement.User.Email,
                    AdvertiserPhone = advertisement.User.Phone,
                    Description = advertisement.Description
                })
                 .FirstOrDefault(ads => ads.AdvertisementId == id);

            return advertisementFullProperties;
        }

        public async Task<AdvertisementSaveViewModel> GetByIdSaveViewModelAsync(int id)
        {
            Advertisement advertisement = await _advertisementRepository.GetByIdAsync(id);

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
                Price = advertisement.Price,
                Description = advertisement.Description
                
            };


        }

        public async Task UpdateAsync(AdvertisementSaveViewModel vm)
        {

            //From SAVEVM to Model
            Advertisement advertisement = await _advertisementRepository.GetByIdAsync(vm.AdvertisementId);

            advertisement.AdvertisementId = vm.AdvertisementId;
            advertisement.ProductName = vm.ProductName;
            advertisement.CategoryId = vm.CategoryId;
            advertisement.UserId = vm.UserId;
            advertisement.FirstImage = vm.FirstImg;
            advertisement.SecondImage = vm.SecondImg;
            advertisement.ThirdImage = vm.ThirdImg;
            advertisement.FourthImage = vm.FourthImg;
            advertisement.Price = vm.Price;
            advertisement.Description = vm.Description;

            await _advertisementRepository.UpdateAsync(advertisement);

        }




    }

}
