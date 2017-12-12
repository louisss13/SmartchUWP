using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public String Email { get; set; }

        public String Name { get; set; }
        public String FirstName { get; set; }

        public String Phone { get; set; }
        public Address Adresse { get; set; } = new Address();
        public DateTime Birthday { get; set; }
        
       
    }
}
