using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    interface IDaoConvertible
    {
        Object ToObjectModel();
        IDaoConvertible ToObjectDao(JToken d);
    }
}
