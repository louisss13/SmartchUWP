using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    class LogInDAO
    {
        public String UserName { get; set; }
        public String Password { get; set; }

        public LogInDAO(string userName, String password) {
            UserName = userName;
            Password = password;
        }
    }
}
