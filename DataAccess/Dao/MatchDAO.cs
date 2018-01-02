using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class MatchDAO
    { 
        public long Id { get; set; }
        public User Joueur1 { get; set; }
        public User Joueur2 { get; set; }
        public int Joueur1Id { get; set; }
        public int Joueur2Id { get; set; }
        public Account Arbitre { get; set; }
        public string Emplacement { get; set; }
        public EMatchState State { get; set; }
        public Score Score { get; set; }
        public TimeSpan DebutPrevu { get; set; }
        public int Phase { get; set; }

        public MatchDAO() { }
        public MatchDAO(Match match, int phase)
        {
            Id = match.Id;
            Joueur1 = match.Player1;
            Joueur2 = match.Player2;
            if (match.Player1 != null)
                Joueur1Id = match.Player1.Id;
            if (match.Player2 != null)
                Joueur2Id = match.Player2.Id;
            Arbitre = match.Arbitre;
            Emplacement = match.Emplacement;
            State = match.State;
            Score = match.Score;
            DebutPrevu = match.Time;
            Phase = phase;
        }

        public Match GetMatch()
        {
            return new Match()
            {
                Id = Id,
                Player1 = (Joueur1!= null)?Joueur1: new User() { Id = Joueur1Id},
                Player2 = (Joueur2 != null) ? Joueur2 : new User() { Id = Joueur2Id },
                Arbitre = Arbitre,
                Emplacement = Emplacement,
                State = State,
                Score = Score,
                Time = DebutPrevu
            };
        }
        public static ICollection<MatchDAO> getListMatchDAO(ICollection<MatchsPhase> matchsPhases)
        {
            List<MatchDAO> matchs = new List<MatchDAO>();
            foreach(MatchsPhase phase in matchsPhases)
            {
                foreach(Match match in phase.Matchs)
                {
                    matchs.Add(new MatchDAO(match, phase.NumPhase) );
                }
            }

            return matchs;
        }


    }
}
