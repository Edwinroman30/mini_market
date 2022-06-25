using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.ViewModels.User
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "You must specify a username, please provide one valid.")]
        public string UserName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, you must provide a password.")]
        public string Password { get; set; }
    }
}
