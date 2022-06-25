using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel>
                                        where SaveViewModel : class
                                        where ViewModel : class
    {
        Task<SaveViewModel> AddAsync(SaveViewModel vm);

        Task UpdateAsync(SaveViewModel vm);

        Task DeleteAsync(int id);

        Task<SaveViewModel> GetByIdSaveViewModelAsync(int id);

        Task<List<ViewModel>> GetAllViewModelAsync();
    }

}
