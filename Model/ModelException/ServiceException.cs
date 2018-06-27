using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelException
{
    public class ServiceException : Exception
    {
        public string Description { get; set; }

        public ServiceException() : base("Erreur inconnue")
        {
        }

        public ServiceException(string message) : base(message)
        {
        }
        public ServiceException(string message, string description) : base(message)
        {
            Description = description;
        }
    }
}
