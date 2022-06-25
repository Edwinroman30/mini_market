using Emarket.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserSaveViewModel, UserViewModel>
    {
        Task<UserViewModel> LoginAsync(UserLoginViewModel loginViewModel);
        Task<UserViewModel> GetAndUserNameValidationAsync(UserSaveViewModel userSaveViewModel); //En los controllers

    }
}
