using CogAuth.services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogAuth.services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(string Photo, string Audio , string Name);

        Task<User> SignIn(string Photo, string Audio , string Name);
    }
}
