using DataAccess.Interface;
using Newtonsoft.Json.Linq;
using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    class ClubDAO : IDaoConvertible
    {
        public long ClubId { get; set; }
        public string Name { get; set; }
        public string ContactMail { get; set; }
        public string Phone { get; set; }

        public ClubDAO()
        {
           
        }

        public IDaoConvertible ToObjectDao(JToken d)
        {
            ClubId = d["id"].Value<int>();
            Name = d["name"].Value<String>();
            ContactMail = d["contactMail"].Value<String>();
            Phone = d["phone"].Value<string>();
            return this;
        }
        

            public object ToObjectModel()
        {
            return new Club()
            {
                ClubId = ClubId,
                Name = Name,
                ContactMail = ContactMail,
                Phone = Phone
            };
        }
    }
}
