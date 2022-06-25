using Emarket.Core.Application.Interfaces.Repositories;
using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.User;
using Emarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<UserSaveViewModel> AddAsync(UserSaveViewModel vm)
        {
            User user = new User() { UserId = vm.UserId, Name = vm.Name, UserName = vm.UserName, Password = vm.Password, Email = vm.Email, Phone = vm.Phone };
            user = await _userRepository.AddAsync(user);

            //FROM Model to ViewModel
            return new UserSaveViewModel()
            {
                UserId = user.UserId,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };


        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<List<UserViewModel>> GetAllViewModelAsync()
        {
            var userList = await _userRepository.GetAllAsync();
            
            return userList.Select(user => new UserViewModel()
            {
                UserId = user.UserId,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password

            }).ToList();
        }

        public async Task<UserSaveViewModel> GetByIdSaveViewModelAsync(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);

            //From Model to UserViewModel
            return new UserSaveViewModel()
            {
                UserId = user.UserId,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };
        }

        
        public async Task<UserViewModel> GetAndUserNameValidationAsync(UserSaveViewModel userSaveViewModel)
        {

            User user = new User() { 
                UserId = userSaveViewModel.UserId, 
                Name = userSaveViewModel.Name, 
                UserName = userSaveViewModel.UserName,
                Password = userSaveViewModel.Password,
                Email = userSaveViewModel.Email,
                Phone = userSaveViewModel.Phone 
            };
            
            User validatedUser = await _userRepository.GetAndUserNameValidationAsync(user);

            if (validatedUser != null)
            {
                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = validatedUser.UserId,
                    UserName = validatedUser.UserName,
                    Name = validatedUser.Name,
                    Email = validatedUser.Email,
                    Phone = validatedUser.Phone,
                    Password = validatedUser.Password
                };

                return userViewModel;

            }

            return null;

        }


        public async Task<UserViewModel> LoginAsync(UserLoginViewModel loginViewModel)
        {
            var userLogged = await _userRepository.LoginUserAsync(loginViewModel);

            if(userLogged != null)
            {
                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = userLogged.UserId,
                    UserName = userLogged.UserName,
                    Name = userLogged.Name,
                    Email = userLogged.Email,
                    Phone = userLogged.Phone,
                    Password = userLogged.Password
                };

                return userViewModel;

            }

            return null;
        }

        public async Task UpdateAsync(UserSaveViewModel vm)
        {
            
            await _userRepository.UpdateAsync(new User() {
                UserId = vm.UserId,
                Name = vm.Name,
                UserName = vm.UserName,
                Password = vm.Password,
                Email = vm.Email,
                Phone = vm.Phone
            });
            
        }

    }
}
