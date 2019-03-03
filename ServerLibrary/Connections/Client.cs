using ServerLibrary.SLEventArgs;
using System;
using System.Net.Sockets;

namespace ServerLibrary
{
    public class Client
    {
        public int id;
        public string ip;
        public TcpClient socket;
        public NetworkStream stream;
        private byte[] _readBuffer;

        public delegate void ClientConnectionEventHandler(ClientEventArgs e);
        public event ClientConnectionEventHandler OnClientConnectionEvent;

        public delegate void ClientDataRecivedEventHandler(ClientDataRecivedEventArgs e);
        public event ClientDataRecivedEventHandler OnClientDataRecivedEvent;

        public void Start(int readBufferSize = 4096, int writeBufferSize = 4096)
        {
            ReportClientConnectionEvent($"Client Connected Successfully with ip: {ip}",true);
            socket.SendBufferSize = writeBufferSize;
            socket.ReceiveBufferSize = readBufferSize;
            stream = socket.GetStream();

            _readBuffer = new byte[socket.ReceiveBufferSize];
            Array.Resize(ref _readBuffer, socket.ReceiveBufferSize);
            stream.BeginRead(_readBuffer, 0, socket.ReceiveBufferSize, OnDataRecieve, null);
        }

        private void OnDataRecieve(IAsyncResult ar)
        {

            try
            {
                int readbytes = stream.EndRead(ar);
                byte[] bytes = new byte[readbytes];
                Buffer.BlockCopy(_readBuffer, 0, bytes, 0, readbytes);
                ReportClientDataRecivedEvent(bytes);
                stream.BeginRead(_readBuffer, 0, socket.ReceiveBufferSize, OnDataRecieve, null);

            }
            catch (Exception)
            {
                CloseConnection();
                return;
            }

        }

        private void CloseConnection()
        {
            ReportClientConnectionEvent($"Client with ip: {ip} has disconnected", false);
            socket.Close();
            socket = null;
        }

        protected virtual void ClientConnectionEvent(string message, bool connected) => OnClientConnectionEvent?.Invoke(new ClientEventArgs(this,message,connected));
        private void ReportClientConnectionEvent(string message,bool connected) => ClientConnectionEvent(message, connected);

        protected virtual void ClientDataRecivedEvent(byte[] data) => OnClientDataRecivedEvent?.Invoke(new ClientDataRecivedEventArgs(this,data));

        private void ReportClientDataRecivedEvent(byte[] data) => ClientDataRecivedEvent(data);
    }
}
