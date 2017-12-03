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

        public AuthHttpClient() : base() {
            if(ApiAccess.Instance.Token != null)
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiAccess.Instance.Token);
        }
    }
}
