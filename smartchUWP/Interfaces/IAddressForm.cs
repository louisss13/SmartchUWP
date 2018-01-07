using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Interfaces
{
    interface IAddressForm
    {
        Address Address { get; set; }
        Boolean IsErrorAdresse { get; set; }
        Boolean IsAddressRequiredCity { get; set; }
        Boolean IsAddressRequiredNumber { get; set; }
        Boolean IsAddressRequiredStreet { get; set; }
        Boolean IsAddressRequiredZipCode { get; set; }
    }
}
