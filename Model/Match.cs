using System;

namespace Model
{
    public class Match
    {
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public Account Arbitre { get; set; }
        public string Emplacement { get; set; }
        public EMatchState State { get; set; }
        public Score Score { get; set; }
        public DateTime Time { get; set; }
    }
}