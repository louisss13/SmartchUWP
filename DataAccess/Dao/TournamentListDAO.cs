using DataAccess.Interface;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    class TournamentListDAO : IDaoConvertible
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ClubId { get; set; }
        public Address Address { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public TournamentState Etat { get; set; }

        public ICollection<User> Participants{ get; set; }
        public ICollection<long> ParticipantsId { get; set; }
        public ICollection<string> AdminsId { get; set; }

        public ICollection<long> MatchesId { get; set; }

        public TournamentListDAO() { }
        public TournamentListDAO(Tournament tournament )
        {
            Id = tournament.Id;
            Name = tournament.Name;
            if(tournament.Club != null)
            ClubId = tournament.Club.ClubId;

            Address = tournament.Address;
            BeginDate = tournament.BeginDate;
            EndDate = tournament.EndDate;
            Etat = tournament.Etat;
            List<long> users = new List<long>();
            foreach (User joueur in tournament.Participants)
            {
                users.Add(joueur.Id);
            }
            ParticipantsId = users;

            List<string> account = new List<string>();
            /*foreach (TournamentAdmin admin in tournament.Admins)
            {
                account.Add(admin.Account);
            }*/
            AdminsId = account;
        }

        public object ToObjectModel()
        {
            ICollection<User> users = new List<User>();
            if (ParticipantsId != null && (Participants == null || Participants.Count() <= 0)) { 
                
                foreach (long joueur in ParticipantsId)
                {
                    users.Add(new User() { Id = joueur });
                }
            }
            else
            {
                users = Participants;
            }
            return new Tournament()
            {
                Id = Id,
                Name = Name,
                BeginDate = BeginDate,
                EndDate = EndDate,
                Etat = Etat,
                Participants = users,
                Address = Address
            };
        
        }

        public IDaoConvertible ToObjectDao(JToken d)
        {
            Id = d["id"].Value<int>();
            Name = d["name"].Value<String>();
            BeginDate = d["beginDate"].Value<DateTime>();
            EndDate = d["endDate"].Value<DateTime>();
            Etat = (TournamentState)d["etat"].Value<int>();
            Participants = d.SelectToken("participantsId").Children().Select(l => new User() { Id = l.Value<int>() }).ToList();
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
