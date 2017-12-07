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
    public class TournamentsServices
    {
        public async Task<IEnumerable<Tournament>> GetTournaments()
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetStringAsync(new Uri(ApiAccess.TournamentUrl));
            var rawClubs = JArray.Parse(reponse);
            var tournaments = rawClubs.Children().Select(d => new Tournament()
            {
                Id = d["id"].Value<int>(),
                Name = d["name"].Value<String>(),
                BeginDate = d["begindate"].Value<DateTime>(),
                EndDate = d["enddate"].Value<DateTime>()
            });
            return tournaments;

        }

        public async Task<ResponseObject> AddTournamentAsync(Tournament tournament)
        {
            HttpContent postContent = new StringContent(JObject.FromObject(tournament).ToString());

            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.TournamentUrl), postContent);

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
