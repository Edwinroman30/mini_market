using Emarket.Core.Application.Helpers;
using Emarket.Core.Application.Interfaces.Repositories;
using Emarket.Core.Application.ViewModels.User;
using Emarket.Core.Domain.Entities;
using Emarket.Infrastructure.Persistency.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistency.Repositories
{
    public class UserRepositoryDB : GenericRepositoryDB<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;
        public UserRepositoryDB(ApplicationContext applicationContext)
            :base(applicationContext)
        {
            _dbContext = applicationContext;
        }

        
        public async Task<User> GetAndUserNameValidationAsync(User givenUser)
        {

            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.UserName == givenUser.UserName);

            if (user != null)
                return user;

            return null;

        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordCompute.PasswordHashing(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }

        public async Task<User> LoginUserAsync(UserLoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordCompute.PasswordHashing(loginVm.Password);
            User user = await _dbContext.Set<User>()
                                    .FirstOrDefaultAsync(user => user.UserName == loginVm.UserName && user.Password == passwordEncrypt);
            return user;
        }

    }
}
