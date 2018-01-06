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
    public class ClubsServices
    {
        public async Task<ResponseObject> GetClubs()
        {
            var wc = new AuthHttpClient();
            var response = await wc.GetAsync(new Uri(ApiAccess.ClubUrl));
            var reponse = GetResponseService.TraiteResponse(response, new ClubDAO(),true);
            
            return reponse;
            
        }
        public async Task<ResponseObject> AddClub(Club club)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(club).ToString());

            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.ClubUrl), postContent);

            return GetResponseService.TraiteResponse(response, new ClubDAO(), false);

        }
    }
}
