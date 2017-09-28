using CogAuth.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CogAuth.services.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace CogAuth.services.Implementation
{
    public class UserService : IUserService
    {
        public async Task<User> RegisterUser(string Photo, string Audio)
        {
            using (var client = new HttpClient())
            {
                string content = "?Photo=" + Photo + "Audio=" + Audio;

                var response = await client.GetStringAsync("URI" + content);
                var coverage = JsonConvert.DeserializeObject<UserData>(response);
                return coverage.data;
            }
        }

        public async Task<User> SignIn(string Photo, string Audio)
        {
            using (var client = new HttpClient())
            {
                string content = "?Photo=" + Photo + "Audio=" + Audio;

                var response = await client.GetStringAsync("URI" + content);
                var coverage = JsonConvert.DeserializeObject<UserData>(response);
                return coverage.data;
            }
        }
    }
}
