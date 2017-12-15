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
        public Object Variable { get; set; }

        public NotificationMessage(NotificationMessageType variableType)
        {
            VariableType = variableType;
            Variable = null;
        }
        public NotificationMessage(NotificationMessageType variableType , Object variable)
        {
            VariableType = variableType;
            Variable = variable;
        }
    }
}
