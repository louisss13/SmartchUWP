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
    public class ClubsServices
    {
        public async Task<IEnumerable<Club>> GetClubs()
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetStringAsync(new Uri(ApiAccess.ClubUrl));
            var rawClubs = JArray.Parse(reponse);
            var clubs = rawClubs.Children().Select(d => new Club()
            {
                ClubId = d["clubId"].Value<int>(),
                Name = d["name"].Value<String>(),
                Email = d["contactMail"].Value<String>(),
                Phone = d["phone"].Value<string>()
            });
            return clubs;
            
        }
        public async Task<ResponseObject> AddClub(Club club)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(club).ToString());

            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.ClubUrl), postContent);

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
