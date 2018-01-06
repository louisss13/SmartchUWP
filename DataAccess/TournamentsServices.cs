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
    public class TournamentsServices
    {
        public async Task<ResponseObject> GetTournaments()
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetAsync(new Uri(ApiAccess.TournamentUrl));
            return GetResponseService.TraiteResponse(reponse, new TournamentListDAO(), true);
            
        }

        public async Task<ResponseObject> GetTournament(long idTournament)
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetAsync(new Uri(ApiAccess.TournamentUrl + "/" + idTournament ));
            return GetResponseService.TraiteResponse(reponse, new TournamentDAO(), false);
        }

        public async Task<ResponseObject> AddTournamentAsync(Tournament tournament)
        {
            TournamentListDAO tournamentListDAO = new TournamentListDAO(tournament);
            HttpContent postContent = new StringContent(JObject.FromObject(tournamentListDAO).ToString());
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(new Uri(ApiAccess.TournamentUrl), postContent);

            return GetResponseService.TraiteResponse(response, new TournamentDAO(), false);
        }

        public async Task<ResponseObject> UpdateAsync(Tournament selectedTournament)
        {
            TournamentDAO tournamentDAO = new TournamentDAO(selectedTournament);
            HttpContent putContent = new StringContent(JObject.FromObject(tournamentDAO).ToString());
            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var wc = new AuthHttpClient();
            var response = await wc.PutAsync(new Uri(ApiAccess.TournamentUrl+"/"+selectedTournament.Id), putContent);

            return GetResponseService.TraiteResponse(response, new TournamentDAO(), false);
        }

        public async Task<ResponseObject> GetParticipants(long idTournament)
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetAsync(new Uri(ApiAccess.TournamentUrl+"/"+ idTournament+"/participants"));

            return GetResponseService.TraiteResponse(reponse, new UserDAO(), true);

            

        }

        public async Task<Boolean> UpdateMatch(Tournament tournament, Match match, int phase)
        {
            MatchDAO matchDAO = new MatchDAO(match, phase);
            HttpContent putContent = new StringContent(JObject.FromObject(matchDAO).ToString());

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PutAsync(ApiAccess.GetMatchUrl(tournament.Id, match.Id), putContent);

            String jstr = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<ResponseObject> AddMatch(Tournament tournament, Match match, int phase)
        {
            MatchDAO matchDAO = new MatchDAO(match, phase);
            HttpContent putContent = new StringContent(JObject.FromObject(matchDAO).ToString());

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(ApiAccess.GetMatchUrl(tournament.Id, match.Id), putContent);

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

        public async Task<ResponseObject> DelPointMatch(long id, Point point)
        {
            HttpContent putContent = new StringContent(JObject.FromObject(point).ToString());

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.DeleteAsync(ApiAccess.GetMatchPointUrl(id)+"/"+point.Joueur);

            ResponseObject contentResponse = new ResponseObject();
            String jstr = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.Accepted)
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

        public async Task<ResponseObject> AddPointMatch( long matchId, Point point)
        {

            HttpContent putContent = new StringContent(JObject.FromObject(point).ToString());

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PostAsync(ApiAccess.GetMatchPointUrl(matchId), putContent);

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
