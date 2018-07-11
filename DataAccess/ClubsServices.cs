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
    public class ClubsServices
    {
        public async Task<List<Club>> GetClubs()
        {
            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.GetAsync(await ApiAccess.GetRessource(ApiAccess.URL.CLUBS));
                var clubsDao = GetResponseService.TraiteResponse(response, new ClubDAO(),true);
                return ((List<object>)clubsDao).Cast<Club>().ToList();
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
           
        }
        public async Task<bool> AddClub(Club club)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(club).ToString());

            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PostAsync(await ApiAccess.GetRessource(ApiAccess.URL.CLUBS), postContent);
                GetResponseService.TraiteResponse(response, new ClubDAO(), false);
                return true;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }
    }
}
