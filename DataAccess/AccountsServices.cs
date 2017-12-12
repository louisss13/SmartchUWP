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
            String content = response.Content.ReadAsStringAsync().Result;
            ResponseObject contentResponse = new ResponseObject();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:

                    contentResponse.Content = JObject.Parse(content);
                    ApiAccess.Instance.Token = ((JObject)contentResponse.Content)["access_token"].Value<String>();

                    contentResponse.Success = true;
                    break;
                case HttpStatusCode.Unauthorized:

                    contentResponse.Content = null;
                    contentResponse.Success = false;
                    break;
                default:
                    contentResponse.Success = false;
                    break;
            }
            return contentResponse;

        }

        public async Task<ResponseObject> AddUser(Account user)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(user).ToString());

            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();


            var response = await wc.PostAsync(new Uri(ApiAccess.AccountUrl), postContent);

            ResponseObject contentResponse = new ResponseObject();
            String jstr = response.Content.ReadAsStringAsync().Result;



            if (response.StatusCode == HttpStatusCode.Created)
            {
                contentResponse.Content = JObject.Parse(jstr);
                contentResponse.Success = true;
            }
            else
            {
                contentResponse.Content = JArray.Parse(jstr);
                contentResponse.Success = false;
                
            }
            
            return contentResponse;
        }
    }
}
