using DataAccess.Dao;
using Model;
using Model.ModelException;
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
        public async Task<bool> LogIn(String username, String password)
        {
            LogInDAO infoLogin = new LogInDAO(username, password);

            HttpContent postContent = new StringContent(JObject.FromObject(infoLogin).ToString());
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PostAsync(new Uri(ApiAccess.LogInUrl), postContent);
                LoginResponseDAO responseO = (LoginResponseDAO)GetResponseService.TraiteResponse(response, new LoginResponseDAO(), false);
                ApiAccess.Instance.Token = responseO.AccessToken;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
          
            return true;
            
        }

        public async Task<bool> AddUser(Account user)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(user).ToString());
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PostAsync(new Uri(ApiAccess.AccountUrl), postContent);
                GetResponseService.TraiteResponse(response, new AccountDAO(), false);
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
            return true;
        }
    }
}
