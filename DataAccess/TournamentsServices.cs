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
    public class TournamentsServices
    {
        public async Task<List<Tournament>> GetTournaments()
        {
            var wc = new AuthHttpClient();
            try
            {
                var reponse = await wc.GetAsync(await ApiAccess.GetRessource(ApiAccess.URL.TOURNAMENTS));
                var tournamentDAO = GetResponseService.TraiteResponse(reponse, new TournamentListDAO(), true);
                return ((List<Object>)tournamentDAO).Cast<Tournament>().ToList();
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }

        public async Task<Tournament> GetTournament(long idTournament)
        {
            var wc = new AuthHttpClient();
            try
            {
                var reponse = await wc.GetAsync(await ApiAccess.GetRessource(ApiAccess.URL.TOURNAMENTS,idTournament ));
                return (Tournament) GetResponseService.TraiteResponse(reponse, new TournamentDAO(), false);
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }

        public async Task<bool> AddTournamentAsync(Tournament tournament)
        {
            TournamentListDAO tournamentListDAO = new TournamentListDAO(tournament);
            HttpContent postContent = new StringContent(JObject.FromObject(tournamentListDAO).ToString());
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PostAsync(await ApiAccess.GetRessource(ApiAccess.URL.TOURNAMENTS), postContent);
                GetResponseService.TraiteResponse(response, new TournamentDAO(), false);
                return true;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
            
        }

        public async Task<bool> UpdateAsync(Tournament selectedTournament)
        {
            TournamentDAO tournamentDAO = new TournamentDAO(selectedTournament);
            HttpContent putContent = new StringContent(JObject.FromObject(tournamentDAO).ToString());
            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PutAsync(await ApiAccess.GetRessource(ApiAccess.URL.TOURNAMENTS, selectedTournament.Id), putContent);
                GetResponseService.TraiteResponse(response, new TournamentDAO(), false);
                return true;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }

        public async Task<List<User>> GetParticipants(long idTournament)
        {
            var wc = new AuthHttpClient();
            try
            {
                var reponse = await wc.GetAsync(await ApiAccess.GetRessource(ApiAccess.URL.PARTICIPANTS, idTournament));
                var usersDao = GetResponseService.TraiteResponse(reponse, new UserDAO(), true);
                return ((List<Object>)usersDao).Cast<User>().ToList();
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }

        public async Task<Boolean> UpdateMatch(Tournament tournament, Match match, int phase)
        {
            MatchDAO matchDAO = new MatchDAO(match, phase);
            HttpContent putContent = new StringContent(JObject.FromObject(matchDAO).ToString());

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PutAsync(await ApiAccess.GetRessource(ApiAccess.URL.MATCHS,tournament.Id, match.Id), putContent);
                GetResponseService.TraiteResponse(response, null, false);
                return true;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }

        public async Task<bool> AddMatch(Tournament tournament, Match match, int phase)
        {
            if (phase <= 0)
                phase = 1;
            MatchDAO matchDAO = new MatchDAO(match, phase);
            HttpContent putContent = new StringContent(JObject.FromObject(matchDAO).ToString());

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PostAsync(await ApiAccess.GetRessource(ApiAccess.URL.MATCHS, tournament.Id, match.Id), putContent);
                GetResponseService.TraiteResponse(response, null, false);
                return true;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }

        public async Task<bool> DelPointMatch(long id, Point point)
        {
            HttpContent putContent = new StringContent(JObject.FromObject(point).ToString());
            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.DeleteAsync(await ApiAccess.GetRessource(ApiAccess.URL.POINTS,id ,point.Joueur));
                GetResponseService.TraiteResponse(response, null, false);
                return true;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }

        public async Task<bool> AddPointMatch( long matchId, Point point)
        {
            HttpContent putContent = new StringContent(JObject.FromObject(point).ToString());
            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var wc = new AuthHttpClient();
            try
            {
                var response = await wc.PostAsync(await ApiAccess.GetRessource(ApiAccess.URL.POINTS, matchId), putContent);
                GetResponseService.TraiteResponse(response, null, false);
                return true;
            }
            catch (HttpRequestException)
            {
                throw new GetDataException();
            }
        }
    }
}
