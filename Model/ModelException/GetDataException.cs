using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelException
{
    public class GetDataException : Exception
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public GetDataException() : base("Problème lors de la récupération des données")
        {
        }

        public GetDataException(string message) : base(message)
        {
        }
        public GetDataException(string message, string description) : base(message)
        {
            Description = description;
        }
    }
}
