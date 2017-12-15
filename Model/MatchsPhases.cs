using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MatchsPhase
    {
        public int NumPhase { get; set; }
        public IEnumerable<Match> Matchs { get; set; }
    }
}
