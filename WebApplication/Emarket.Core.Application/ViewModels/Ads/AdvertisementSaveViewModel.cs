using Emarket.Core.Application.ViewModels.Category;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.ViewModels
{
    public class AdvertisementSaveViewModel
    {
        public int AdvertisementId { get; set; }
        
        [Required(ErrorMessage = "Please, provide a name to reconize the product.")]
        public string ProductName { get; set; }
        
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "You must at least provide one image to the product")]
        public IFormFile FirstImage { get; set; }
        public IFormFile SecondImage { get; set; }
        public IFormFile ThirdImage { get; set; }
        public IFormFile FourthImage { get; set; }
        
        [Required(ErrorMessage = "You most provide a price.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public List<CategoryViewModel> Categories { get; set; }


    }

}
