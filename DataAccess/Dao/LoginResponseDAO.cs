using DataAccess.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    class LoginResponseDAO : IDaoConvertible
    {
        public String AccessToken { get; set; }
        public int ExpireIn { get; set; }

        public IDaoConvertible ToObjectDao(JToken d)
        {
            AccessToken = d["access_token"].Value<string>();
            ExpireIn = d["expires_in"].Value<int>();

            return this;
        }

        public object ToObjectModel()
        {
            return this;
        }
    }
}
