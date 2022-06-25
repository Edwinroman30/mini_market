using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application
{
  
        public static class DIServiceRegistration
        {

            public static void AddApplicationLayerDependecies(this IServiceCollection services)
            {
                //services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>) ); to user with AUTOMAPPER
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<IAdvertisementService, AdvertisementService>();
                services.AddTransient<ICategoryService, CategoryService>();
            }

        }

}
