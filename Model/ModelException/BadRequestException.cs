using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelException
{
    public class BadRequestException : Exception
    {
        public IEnumerable<Error> Errors { get; set; }

        public BadRequestException() : base("Bad Request Exception")
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }
        public BadRequestException(string message, IEnumerable<Error> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
