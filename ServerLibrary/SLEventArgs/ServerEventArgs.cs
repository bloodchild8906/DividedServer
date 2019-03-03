using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    public class ServerEventArgs : EventArgs
    {
        public ServerEventArgs(string message = "")
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
