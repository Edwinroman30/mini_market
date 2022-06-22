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
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, you must provide a Username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please, you must provide an email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, you must provide a password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please, you must provide a Phone Number.")]
        public string Phone { get; set; }

    }

}
