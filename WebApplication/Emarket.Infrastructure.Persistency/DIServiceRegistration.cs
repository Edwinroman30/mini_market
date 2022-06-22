using Emarket.Core.Application.Interfaces;
using Emarket.Core.Application.Interfaces.Repositories;
using Emarket.Infrastructure.Persistency.Contexts;
using Emarket.Infrastructure.Persistency.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistency
{
    public static class DIServiceRegistration
    {
        //Extension method c#
        public static void AddInfrastructureLayerDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            

            #region DB Setting

                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    services.AddDbContext<ApplicationContext>(options =>
                                                              options.UseInMemoryDatabase("EmarketDB"));
                }
                else
                {
                    services.AddDbContext<ApplicationContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        migrations => migrations.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
                }

            #endregion


            #region ProductServices Registration
              services.AddTransient(typeof(IGenericRepositoryDB<>), typeof(GenericRepositoryDB<>));
              services.AddTransient<IAdvertisementRepository, AdvertisementRepositoryDB>();
              services.AddTransient<ICategoryRepository, CategoryRepositoryDB>();
              services.AddTransient<IUserRepository, UserRepositoryDB>();
            #endregion

        }


    }

}
