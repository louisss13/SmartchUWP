using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using DataAccess.Dao;

namespace DataAccess
{
    public class UsersServices
    {

        public async Task<ResponseObject> LogIn(String username, String password) {
            LogInDAO infoLogin = new LogInDAO(username, password);

            HttpContent postContent = new StringContent(JObject.FromObject(infoLogin).ToString());
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.LogInUrl), postContent);
            String content = response.Content.ReadAsStringAsync().Result;
            ResponseObject contentResponse = new ResponseObject();
            switch (response.StatusCode) {
                case HttpStatusCode.OK : 
            
                    contentResponse.Content = JObject.Parse(content);
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
        public async Task<ResponseObject> AddUser(User user)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(user).ToString());
           
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();


            var response = await wc.PostAsync(new Uri(ApiAccess.AccountUrl), postContent);
            
            ResponseObject contentResponse = new ResponseObject();
            String jstr =  response.Content.ReadAsStringAsync().Result;
         
            

            if (response.StatusCode == HttpStatusCode.Created)
            {
                contentResponse.Content = JObject.Parse(jstr);
                contentResponse.Success = true;
            }
            else
            {
                contentResponse.Content = JArray.Parse(jstr);
                contentResponse.Success = false;
                //contentResponse.AddFirst(new JObject("Success", false));
            }
                //var RawUser = JObject.Parse(jUser);
                /*var forecast = rawWeather["list"].Children().Select(d => new WeatherForecast()
                {
                    Date = d["dt_txt"].Value<DateTime>(),
                    MinTemp = d["main"]["temp_min"].Value<double>(),
                    MaxTemp = d["main"]["temp_max"].Value<double>(),
                    WeatherDescription = d["weather"].First["description"].Value<String>(),
                    WindSpeed = d["wind"]["speed"].Value<double>()
                });*/
                return contentResponse;
            }
        }
}
