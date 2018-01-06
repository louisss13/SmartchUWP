using DataAccess.Dao;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountsServices
    {
        public async Task<ResponseObject> LogIn(String username, String password)
        {
            LogInDAO infoLogin = new LogInDAO(username, password);

            HttpContent postContent = new StringContent(JObject.FromObject(infoLogin).ToString());
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.LogInUrl), postContent);
            ResponseObject responseO = GetResponseService.TraiteResponse(response, new LoginResponseDAO(), false);
            if(responseO.Success)
                ApiAccess.Instance.Token = ((LoginResponseDAO)responseO.Content).AccessToken;
            return responseO;
            
        }

        public async Task<ResponseObject> AddUser(Account user)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(user).ToString());
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.AccountUrl), postContent);

            return GetResponseService.TraiteResponse(response, new AccountDAO(), false);
        }
    }
}
