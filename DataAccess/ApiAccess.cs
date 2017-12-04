using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public sealed class ApiAccess
    {
        private static readonly String Url = "http://localhost:49391/api/";
        public static readonly String UsersUrl = Url + "users";
        public static readonly String AccountUrl = Url + "account";
        public static readonly String LogInUrl = Url + "jwt";
        public static readonly string ClubUrl = Url + "clubs";

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
