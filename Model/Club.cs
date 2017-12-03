using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Club
    {
        public int Id { get; set;}
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public IEnumerable<User> Membres { get; set; }
        public IEnumerable<Account> Admins { get; set; }
        //public Address Address { get; set; }
        //public Tournois tournois { get; set; }
     }
}
