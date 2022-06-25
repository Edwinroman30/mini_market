using Emarket.Core.Application.Interfaces.Repositories;
using Emarket.Core.Application.Interfaces.Services;
using Emarket.Core.Application.ViewModels.Category;
using Emarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            this._categoryRepository = categoryRepository;
            this._userRepository = userRepository;
        }

        public async Task<CategorySaveViewModel> AddAsync(CategorySaveViewModel vm)
        {
            Category category = new Category();
            category.CategoryId = vm.CategoryId;
            category.CategoryName = vm.CategoryName;
            category.Description = vm.Description;

            category = await _categoryRepository.AddAsync(category);

            return new CategorySaveViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<CategoryViewModel>> GetAllViewModelAsync()
        {
            List<Category> categories = await _categoryRepository.GetAllWithPropertyAsync(new List<string>() { "Advertisement" });
            //List<User> users = await _userRepository.GetAllAsync();

            return categories.Select(categories => new CategoryViewModel()
            {
                CategoryId = categories.CategoryId,
                CategoryName = categories.CategoryName,
                Description = categories.Description,
                ProductCount = categories.Advertisements.Count(),
                UserCount = categories.Advertisements.Where(ad => ad.CategoryId == categories.CategoryId).GroupBy(ad => ad.UserId).Count()
            }).ToList();
            
        }

        public async Task<CategorySaveViewModel> GetByIdSaveViewModelAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            return new CategorySaveViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

        }

        public async Task UpdateAsync(CategorySaveViewModel vm)
        {

            Category category = new Category()
            {
                CategoryId = vm.CategoryId,
                CategoryName = vm.CategoryName,
                Description = vm.Description
            };

            await _categoryRepository.UpdateAsync(category);

        }


    }
}
