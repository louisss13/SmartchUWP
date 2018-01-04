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
        public async Task<IEnumerable<Tournament>> GetTournaments()
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetStringAsync(new Uri(ApiAccess.TournamentUrl));
            var rawClubs = JArray.Parse(reponse);
            var tournaments = rawClubs.Children().Select(d => new Tournament()
            {
                Id = d["id"].Value<int>(),
                Name = d["name"].Value<String>(),
                BeginDate = d["beginDate"].Value<DateTime>(),
                EndDate = d["endDate"].Value<DateTime>(),
                Etat = (TournamentState)d["etat"].Value<int>(),
                Participants = d.SelectToken("participantsId").Children().Select(l => new User() { Id = l.Value<int>()}).ToList(),
                Address = (d["address"].Value<Object>() != null) ? new Address()
                {
                    Street = (String)d.SelectToken("address.street"),
                    Box = (String)d.SelectToken("address.box"),
                    City = (String)d.SelectToken("address.city"),
                    //Country = (Country)d.SelectToken("adresse.street"),
                    Number = (String)d.SelectToken("address.number"),
                    Zipcode = (String)d.SelectToken("address.zipCode"),
                } : null,

            });
            return tournaments;

        }

        public async Task<Tournament> GetTournament(long idTournament)
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetStringAsync(new Uri(ApiAccess.TournamentUrl + "/" + idTournament ));
            var rawTournament = JObject.Parse(reponse);
            var Tournament = rawTournament.ToObject<TournamentDAO>();
            return Tournament.GetTournament();
        }

        public async Task<ResponseObject> AddTournamentAsync(Tournament tournament)
        {
            TournamentListDAO tournamentListDAO = new TournamentListDAO(tournament);
            HttpContent postContent = new StringContent(JObject.FromObject(tournamentListDAO).ToString());

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

        public async Task<ResponseObject> UpdateAsync(Tournament selectedTournament)
        {
            TournamentDAO tournamentDAO = new TournamentDAO(selectedTournament);
            HttpContent putContent = new StringContent(JObject.FromObject(tournamentDAO).ToString());

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            var response = await wc.PutAsync(new Uri(ApiAccess.TournamentUrl+"/"+selectedTournament.Id), putContent);

            ResponseObject contentResponse = new ResponseObject();
            String jstr = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
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

        public async Task<IEnumerable<User>> GetParticipants(long idTournament)
        {
            var wc = new AuthHttpClient();
            var reponse = await wc.GetStringAsync(new Uri(ApiAccess.TournamentUrl+"/"+ idTournament+"/participants"));
            var rawUsers = JArray.Parse(reponse);
            var users = UsersServices.RawToUsers(rawUsers);

            return users;

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
