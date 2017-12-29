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
        public UsersServices()
        {
        }

        public async Task<ResponseObject> AddUser(User user)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(user).ToString());

            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.UsersUrl), postContent);

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

        public async Task<IEnumerable<User>> GetUsers()
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetStringAsync(new Uri(ApiAccess.UsersUrl));
            var rawUsers = JArray.Parse(reponse);
            var users = RawToUsers(rawUsers);

            return users;

        }
        public async Task<IEnumerable<User>> GetUsersWithAccount()
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetStringAsync(new Uri(ApiAccess.UsersAccountUrl));
            var rawUsers = JArray.Parse(reponse);
            var users = RawToUsers(rawUsers);
           
            return users;
        }
        public static IEnumerable<User> RawToUsers(JArray rawUsers)
        {
            var users = rawUsers.Children().Select(d => new User()
            {
                Id = d["id"].Value<int>(),
                Name = d["name"].Value<String>(),
                FirstName = d["firstName"].Value<string>(),
                Email = d["email"].Value<String>(),
                Phone = d["phone"].Value<string>(),
                Birthday = d["birthday"].Value<DateTime>(),
                Adresse = (d["adresse"].Value<Object>() != null) ? new Address()
                {
                    Street = (String)d.SelectToken("adresse.street"),
                    Box = (String)d.SelectToken("adresse.box"),
                    City = (String)d.SelectToken("adresse.city"),
                    //Country = (Country)d.SelectToken("adresse.street"),
                    Number = (String)d.SelectToken("adresse.number"),
                    Zipcode = (String)d.SelectToken("adresse.zipCode"),

                } : null

            });
            return users;
        }
    }
}
