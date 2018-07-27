using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelException
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException() : base("Unauthorize")
        {
        }
    }
}
