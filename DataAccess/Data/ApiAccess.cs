using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace DataAccess
{
    public static class ApiAccess
    {
        public enum URL {
            USERS,
            USER_ACCOUNT,
            ACCOUNT,
            LOGIN,
            CLUBS,
            TOURNAMENTS,
            MATCHS,
            POINTS, 
            PARTICIPANTS
        }

        private static String UrlApi;
        private static String Users;
        private static String UsersAccount;
        private static String Account;
        private static String Login;
        private static String Clubs;
        private static String Tournaments;
        private static String Matchs;
        private static String Points;
        private static String Participants;
       
        public static async Task<Uri> GetRessource(URL ressource, long id=0)
        {
            if (UrlApi == null)
                await SetStaticVar();
            string address = UrlApi;
            switch (ressource)
            {
                case URL.ACCOUNT:
                    address += Account;
                    break;
                case URL.CLUBS:
                    address += Clubs;
                    break;
                case URL.LOGIN:
                    address += Login;
                    break;
                case URL.TOURNAMENTS:
                    address += Tournaments;
                    break;
                case URL.USERS:
                    address += Users;
                    break;
                case URL.USER_ACCOUNT:
                    address += UsersAccount;
                    break;
                case URL.MATCHS:
                case URL.POINTS:
                case URL.PARTICIPANTS:
                    int id2 = 0;
                    return await GetRessource(ressource, id, id2);
                default:
                    return null;
            }
            if(id > 0)
            {
                address += "/" + id;
            }
            return new Uri(address);
        }
        public static async Task<Uri> GetRessource(URL ressource, long id1, long id2)
        {
            if (UrlApi == null)
                await SetStaticVar();
            string address = UrlApi;
            switch (ressource)
            {
                case URL.MATCHS:
                    address += Matchs;
                    break;
                case URL.POINTS:
                    address += Clubs;
                    break;
                case URL.PARTICIPANTS:
                    address += Participants;
                    break;
                default:
                    return null;
            }
            address = address.Replace("{id1}", id1.ToString());
            if (id2 > 0)
            {
                address += "/" + id2;
            }
            return new Uri(address);
        }
        public static async Task<Uri> GetRessource(URL ressource, long id1, Model.EJoueurs id2 = 0)
        {
            if (UrlApi == null)
                await SetStaticVar();
            string address = UrlApi;
            switch (ressource)
            {
                case URL.POINTS:
                    address += Clubs;
                    break;
                default:
                    return null;
            }
            address = address.Replace("{id1}", id1.ToString());
            if (id2 > 0)
            {
                address += "/" + id2;
            }
            return new Uri(address);
        }
        private async static Task SetStaticVar()
        {
            StorageFile file;
            #if DEBUG            
                file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/ApiAccessDebug.xml"));
            #else
                file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/ApiAccess.xml"));
            #endif

            
            var xmlfile = await XmlDocument.LoadFromFileAsync(file);
            UrlApi = xmlfile.DocumentElement.SelectSingleNode("url").InnerText;
            Users = xmlfile.DocumentElement.SelectSingleNode("ressources/users").InnerText;
            UsersAccount = xmlfile.DocumentElement.SelectSingleNode("ressources/user_accounts").InnerText;
            Account = xmlfile.DocumentElement.SelectSingleNode("./ressources/account").InnerText;
            Login = xmlfile.DocumentElement.SelectSingleNode("ressources/login").InnerText;
            Clubs = xmlfile.DocumentElement.SelectSingleNode("./ressources/clubs").InnerText;
            Tournaments = xmlfile.DocumentElement.SelectSingleNode("ressources/tournaments").InnerText;
            Matchs = xmlfile.DocumentElement.SelectSingleNode("./ressources/matchs").InnerText;
            Points = xmlfile.DocumentElement.SelectSingleNode("ressources/points").InnerText;
            Participants = xmlfile.DocumentElement.SelectSingleNode("./ressources/participants").InnerText;
        }
       
    }
}
