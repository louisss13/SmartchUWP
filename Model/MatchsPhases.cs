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
        public ICollection<Match> Matchs { get; set; } = new List<Match>();

        public override bool Equals(object obj)
        {
            if (obj is MatchsPhase && obj != null)
                return (obj as MatchsPhase).NumPhase == NumPhase;
            return false;
        }

        public override int GetHashCode()
        {
            return -47539090 + NumPhase.GetHashCode();
        }
    }
}
