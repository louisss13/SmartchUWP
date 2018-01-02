using DataAccess.Dao;
using Model;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class TournamentDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Club Club { get; set; }
        public Address Address { get; set; }

        public DateTime BeginDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public TournamentState Etat { get; set; }

        public ICollection<User> Participants { get; set; } = new List<User>();
        public ICollection<Account> Admins { get; set; } = new List<Account>();

        public ICollection<MatchDAO> Matches
        {
            get;
            set;
        }
        public TournamentDAO() { }
        public TournamentDAO(Tournament tournament)
        {
            Id = tournament.Id;
            Name = tournament.Name;
            Club = tournament.Club;
            Address = tournament.Address;
            BeginDate = tournament.BeginDate;
            EndDate = tournament.EndDate;
            Etat = tournament.Etat;
            Participants = tournament.Participants;
            Admins = tournament.Admins;
            Matches = MatchDAO.getListMatchDAO(tournament.Matches);

        }

        public Tournament GetTournament()
        {
            return new Tournament()
            {
                Id = Id,
                Name = Name,
                Club = Club,
                Address = Address,
                BeginDate = BeginDate,
                EndDate = EndDate,
                Etat = Etat,
                Participants = Participants,
                Admins = Admins,
                Matches = MatcshDAOToMatchs()
            };
        }

        private ICollection<MatchsPhase> MatcshDAOToMatchs()
        {
            List<MatchsPhase> listMatchphase = new List<MatchsPhase>();
            foreach(MatchDAO matchDao in Matches)
            {
                MatchsPhase matchsPhase = listMatchphase.Find(m => m.NumPhase == matchDao.Phase);
                if (matchsPhase != null)
                {
                    matchsPhase.Matchs.Add(matchDao.GetMatch());
                }
                else
                {
                    matchsPhase = new MatchsPhase()
                    {
                        NumPhase = matchDao.Phase,
                        Matchs = new List<Match>() { matchDao.GetMatch() }
                    };
                    listMatchphase.Add(matchsPhase);
                }
                
            }
            return listMatchphase;
        }
    }
}