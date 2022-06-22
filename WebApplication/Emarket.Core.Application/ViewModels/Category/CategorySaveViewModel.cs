using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.ViewModels.Category
{
    public class CategorySaveViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please, provide a category name.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please, provide a category name.")]
        public string Description { get; set; }
    }
}
