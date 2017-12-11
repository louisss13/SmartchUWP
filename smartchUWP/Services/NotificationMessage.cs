using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Services
{
    public class NotificationMessage
    {
        public NotificationMessageType VariableType { get; set; }

        public NotificationMessage(NotificationMessageType variable)
        {
            VariableType = variable;
        }
    }
}
