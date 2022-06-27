using Emarket.Core.Application.ViewModels.Category;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.ViewModels.Ads
{
    public class AdvertisementSaveViewModel
    {
        public int AdvertisementId { get; set; }
        
        [Required(ErrorMessage = "Please, provide a name to reconize the product.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please, provide a description to know more about it.")]
        public string Description { get; set; }

        // I dont set this required because I need to work with the views.
        [DataType(DataType.Upload)]
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
        
        // URL
        public string FirstImg { get; set; }
        public string SecondImg { get; set; }
        public string ThirdImg { get; set; }
        public string FourthImg { get; set; }


    }

}
