using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emarket.Core.Application.Helpers;
using Emarket.Core.Application.ViewModels.User;

namespace e_MiniMarket.Middleware
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContext;

        public ValidateUserSession(IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
        }

        public bool HasUserLogged()
        {
            UserViewModel user = _httpContext.HttpContext.Session.Get<UserViewModel>("user_session");

            if (user == null)
                return false;

            return true;
        }

    }
}
