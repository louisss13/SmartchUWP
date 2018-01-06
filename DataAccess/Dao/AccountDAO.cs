using DataAccess.Interface;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    class AccountDAO : IDaoConvertible
    {
        public IDaoConvertible ToObjectDao(JToken d)
        {
            return this;
        }

        public object ToObjectModel()
        {
            return new Account() { };
        }
    }
}
