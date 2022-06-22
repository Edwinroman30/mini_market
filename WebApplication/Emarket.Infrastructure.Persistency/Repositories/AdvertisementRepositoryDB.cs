using Emarket.Core.Application.Interfaces.Repositories;
using Emarket.Core.Domain.Entities;
using Emarket.Infrastructure.Persistency.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistency.Repositories
{
    public class AdvertisementRepositoryDB : GenericRepositoryDB<Advertisement> , IAdvertisementRepository
    {
        private readonly ApplicationContext _dbContext; 
        public AdvertisementRepositoryDB(ApplicationContext applicationContext)
            :base(applicationContext)
        {
            _dbContext = applicationContext;
        }

    }
}
