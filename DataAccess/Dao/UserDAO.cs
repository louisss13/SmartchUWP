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
    class UserDAO : IDaoConvertible
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public Address Adresse { get; set; }

        public IDaoConvertible ToObjectDao(JToken d)
        {
            Id = d["id"].Value<long>();
            Name = d["name"].Value<String>();
            FirstName = d["firstName"].Value<string>();
            Email = d["email"].Value<String>();
            Phone = d["phone"].Value<string>();
            Birthday = d["birthday"].Value<DateTime>();
            Adresse = (d["adresse"].Value<Object>() != null) ? new Address()
            {
                Street = (String)d.SelectToken("adresse.street"),
                Box = (String)d.SelectToken("adresse.box"),
                City = (String)d.SelectToken("adresse.city"),
                //Country = (Country)d.SelectToken("adresse.street"),
                Number = (String)d.SelectToken("adresse.number"),
                Zipcode = (String)d.SelectToken("adresse.zipCode"),

            } : null;
            return this;
            
        }

        public object ToObjectModel()
        {
            return new User()
            {
                Id = Id,
                Name = Name,
                FirstName = FirstName,
                Email = Email,
                Phone = Phone,
                Birthday = Birthday,
                Adresse = Adresse

            };
        }
    }
}
