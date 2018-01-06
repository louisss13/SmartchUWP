using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Club
    {
        public long ClubId { get; set;}
        public String Name { get; set; }
        public String ContactMail { get; set; }
        public String Phone { get; set; }
        public IEnumerable<User> Members { get; set; } = new List<User>();
        public IEnumerable<Account> Admins { get; set; } = new List<Account>();
        public Address Adresse { get; set; } = new Address();
        //public Tournois tournois { get; set; }
     }
}
