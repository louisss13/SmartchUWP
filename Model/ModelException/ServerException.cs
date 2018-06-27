using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelException
{
    public class ServerException : Exception 
    {
        public string Description { get; set; }

        public ServerException() : base("Erreur serveur")
        {
        }

        public ServerException(string message) : base(message)
        {
        }
        public ServerException(string message, string description) : base(message)
        {
            Description = description;
        }

    }
}
