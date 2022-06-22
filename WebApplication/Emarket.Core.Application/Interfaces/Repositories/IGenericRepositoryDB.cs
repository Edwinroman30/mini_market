using Emarket.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Interfaces
{

    public interface IGenericRepositoryDB<Entity>  where Entity : class
    {
        Task<bool> AddAsync(Entity entity);

        Task UpdateAsync(Entity entity);

        Task DeleteAsync(Entity entity);

        Task<List<Entity>> GetAllAsync();

        Task<Entity> GetByIdAsync(int id);

        Task<List<Entity>> GetAllWithPropertyAsync(List<string> navigationsProperties);

    }

}
