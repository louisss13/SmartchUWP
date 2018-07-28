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
        public string NameTournament { get; set; }
        public Club Club { get; set; }
        public Address Address { get; set; } = new Address();

        public DateTime BeginDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public TournamentState Etat { get; set; }

        public ICollection<User> Participants { get; set; } = new List<User>();
        public ICollection<Account> Admins { get; set; } = new List<Account>();

        public ICollection<MatchsPhase> Matches { get; set; }



    }
}
