using Emarket.Core.Application.Interfaces;
using Emarket.Infrastructure.Persistency.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistency.Repositories
{
    public class GenericRepositoryDB<Entity> : IGenericRepositoryDB<Entity> where Entity : class
    {
        private ApplicationContext _dbContext { get; set; }

        public GenericRepositoryDB(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            try
            {
                await _dbContext.Set<Entity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task UpdateAsync(Entity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _dbContext
                 .Set<Entity>()
                 .AsNoTracking()
                 .ToListAsync();
        }

        //WIth n. property Include or SQL Join
        //Hacer uno con un ThenInclude()
        public virtual async Task<List<Entity>> GetAllWithPropertyAsync(List<string> navigationsProperties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (var property in navigationsProperties)
            {
                query = query.Include(property);
            }

            return await query.AsNoTracking().ToListAsync();
        }


        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext
                 .Set<Entity>()
                 .FindAsync(id);
        }



    }

}
