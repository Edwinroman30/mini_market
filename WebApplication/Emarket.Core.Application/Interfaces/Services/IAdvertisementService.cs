using Emarket.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Interfaces.Services
{
    public interface IAdvertisementService : IGenericService<AdvertisementSaveViewModel, AdvertisementViewModel >
    {
        Task<AdvertisementViewModel> GetAdvertisementWithFilter(AdvertisementFilterViewModel filterViewModel);

    }
}
