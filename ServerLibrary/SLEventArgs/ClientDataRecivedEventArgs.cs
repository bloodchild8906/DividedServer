using System;

namespace ServerLibrary.SLEventArgs
{
    //DataRecived
    public class ClientDataRecivedEventArgs : EventArgs
    {
        public Client Client { get; set; }
        public byte[] Data { get; set; }

        public ClientDataRecivedEventArgs(Client client, byte[] data)
        {
            Client = client;
            Data = data;
        }
        
    }
}
