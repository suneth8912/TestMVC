using Common.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Refit.Interfaces
{
    public interface IUserApiController
    {
        [Post("/AuthenticateUser")]
        Task<LoginOperationResult> AuthenticateUser(UserDto User);

    }
}
