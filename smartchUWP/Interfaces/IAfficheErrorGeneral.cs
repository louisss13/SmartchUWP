using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Interfaces
{
    public interface IAfficheErrorGeneral
    {
        Boolean IsGeneralError { get; set; }
        Boolean IsGeneralErrorVisible { get; set; }
        String ErrorDescription { get; set; }
    }
}
