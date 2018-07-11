using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuthHttpClient : HttpClient
    {
        public static string Token { get; set; }

        public AuthHttpClient() : base() {
            if(AuthHttpClient.Token != null)
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHttpClient.Token);
        }
    }
}
