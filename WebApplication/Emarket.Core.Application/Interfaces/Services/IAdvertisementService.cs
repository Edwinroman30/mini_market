﻿using Emarket.Core.Application.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Interfaces.Services
{
    public interface IAdvertisementService : IGenericService<AdvertisementSaveViewModel, AdvertisementViewModel >
    {
        Task<List<AdvertisementViewModel>> GetAdvertisementWithFilter(AdvertisementFilterViewModel filterViewModel);
        Task<AdvertisementCardViewModel> GetAdvertisementWithFullProperties(int id);
    }
}
