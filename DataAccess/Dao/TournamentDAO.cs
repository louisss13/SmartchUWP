using DataAccess.Dao;
using DataAccess.Interface;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    class TournamentDAO : IDaoConvertible
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
            Matches = MatchDAO.GetListMatchDAO(tournament.Matches);

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
            if (Matches != null)
            {
                foreach (MatchDAO matchDao in Matches)
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
            }
            return listMatchphase;
        }

        public object ToObjectModel()
        {

            return GetTournament();
        }

        public IDaoConvertible ToObjectDao(JToken d)
        {
            Id = d["id"].Value<int>();
            Name = d["name"].Value<String>();
            BeginDate = d["beginDate"].Value<DateTime>();
            EndDate = d["endDate"].Value<DateTime>();
            Etat = (TournamentState)d["etat"].Value<int>();
            Participants = d.SelectToken("participants")?.Children().Select(l => (User)new UserDAO().ToObjectDao(l).ToObjectModel()).ToList();
            Matches = d.SelectToken("matches")?.Children().Select(l => (MatchDAO) new MatchDAO().ToObjectDao(l)).ToList();
            Address = (d["address"].Value<Object>() != null) ? new Address()
            {
                Street = (String)d.SelectToken("address.street"),
                Box = (String)d.SelectToken("address.box"),
                City = (String)d.SelectToken("address.city"),
                
                Number = (String)d.SelectToken("address.number"),
                Zipcode = (String)d.SelectToken("address.zipCode"),
            } : null;
            return this;
        }

      
    }
}