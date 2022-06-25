using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please, you must provide a name.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, you must provide a Username.")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please, you must provide an email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, you must provide a password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Both passwords must be the same.")]
        [Required(ErrorMessage = "Please, you must provide the same password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please, you must provide a Phone Number.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

    }

}
