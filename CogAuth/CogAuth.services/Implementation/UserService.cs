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
        public async Task<bool> RegisterUser(string Photo, string Audio , string name)
        {
            var regDic = new List<KeyValuePair<string, string>>();
            regDic.Add(new KeyValuePair<string, string>("name", name));
            regDic.Add(new KeyValuePair<string, string>("imageData", Photo));
            regDic.Add(new KeyValuePair<string, string>("audioData", Audio));

            var content = "";
            foreach (var keyvalue in regDic)
            {
                content += keyvalue.Key + "=" + keyvalue.Value + "&";
            }
            content = content.Remove(content.Length - 1, 1);

            HttpContent contentstr = new StringContent(content.ToString());
            contentstr.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

            var client = new HttpClient();
            var res = await client.PostAsync("http://jpmeyer.xyz/faceapi/register/", contentstr);
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User> SignIn(string Photo, string Audio , string name)
        {
            var regDic = new List<KeyValuePair<string, string>>();
            regDic.Add(new KeyValuePair<string, string>("imageData", Photo));
            regDic.Add(new KeyValuePair<string, string>("audioData", Audio));

            var content = "";
            foreach (var keyvalue in regDic)
            {
                content += keyvalue.Key + "=" + keyvalue.Value + "&";
            }
            content = content.Remove(content.Length - 1, 1);

            HttpContent contentstr = new StringContent(content.ToString());
            contentstr.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

            var client = new HttpClient();
            var res = await client.PostAsync("http://jpmeyer.xyz/faceapi/signin/", contentstr);

            var responseMessage = await res.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<User>(responseMessage);

            

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
