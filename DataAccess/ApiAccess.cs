using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public sealed class ApiAccess
    {
        private static readonly String Url = "http://smartch.azurewebsites.net/api/";
        public static readonly String UsersUrl = Url + "userinfo";
        public static readonly String UsersAccountUrl = Url + "userinfo/account";
        public static readonly String AccountUrl = Url + "account";
        public static readonly String LogInUrl = Url + "jwt";
        public static readonly string ClubUrl = Url + "clubs";
       // public static readonly string ClubUrl = Url + "clubs/user/1";
        public static readonly string TournamentUrl = Url + "tournaments";
        
        public static String GetMatchUrl(long tournamentId, long matchId)
        {
            if(matchId == 0)
                return Url + "tournaments/" + tournamentId + "/matchs/";
            return Url + "tournaments/" + tournamentId + "/matchs/" + matchId;
        }
        public static String GetMatchPointUrl( long matchId)
        {
            return Url + "matchs/" +  matchId+"/point";
        }

        public String Token { get; set; }
        
        //Singleton
        private static readonly Lazy<ApiAccess> _lazyInstance =  new Lazy<ApiAccess>(() => new ApiAccess());

        public static ApiAccess Instance
        {
            get
            {
                return _lazyInstance.Value;
            }
        }

        

        private ApiAccess()
        {
            Token = null;
        }

    }
}
