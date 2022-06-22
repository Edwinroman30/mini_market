
using Emarket.Core.Domain.Entities;
using Emarket.Infrastructure.Persistency.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emarket.Core.Application.Interfaces.Repositories;

namespace Emarket.Infrastructure.Persistency.Repositories
{
    public class CategoryRepositoryDB : GenericRepositoryDB<Category>, ICategoryRepository
    {
        private readonly ApplicationContext _dbContext;
        public CategoryRepositoryDB(ApplicationContext applicationContext)
            :base(applicationContext)
        {
            _dbContext = applicationContext;
        }
    }
}
