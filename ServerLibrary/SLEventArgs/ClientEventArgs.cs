using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.SLEventArgs
{
    public class ClientEventArgs : EventArgs
    {
        public ClientEventArgs(Client client,string message,bool connected)
        {
            Message = message;
            Client = client;
            Connected = connected;
        }

        public string Message { get; set; }
        public Client Client { get; set; }
        public bool Connected { get; set; }
    }
}
