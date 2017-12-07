using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tournament
    {
        
            public long Id { get; set; }
            public string Name { get; set; }
            public Club Club { get; set; }
            public Address Address { get; set; }

            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }
            public int State { get; set; }

            public ICollection<User> Participants { get; set; }
            public ICollection<User> Admins { get; set; }

           // public ICollection<Match> Matches { get; set; }


        
    }
}
