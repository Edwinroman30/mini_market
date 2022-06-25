using Emarket.Core.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Interfaces.Services
{
    public interface ICategoryService : IGenericService<CategorySaveViewModel, CategoryViewModel>
    {
        //FUTURA IMPLEMENTACION DE pero personalizada y con los ThenInclude
        //        public async Task<List<Entity>> GetAllWithPropertyAsync(List<string> navigationsProperties)



    }
}
