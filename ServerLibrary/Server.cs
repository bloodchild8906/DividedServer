using Newtonsoft.Json;
using ServerLibrary.Helpers;
using ServerLibrary.SLEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerLibrary
{
    public class Server
    {


        static Thread _socketThread;
        private static bool consoleRunning;
        TcpListener _server;
        private static string Command = "";
        public Server(ServerConfig Config)
        {
            _server = new TcpListener(IPAddress.Any, Config.Port);
            _server.Start();
            _server.BeginAcceptTcpClient(OnClientConnect, null);
        }

        public void Start()
        {
            _socketThread = new Thread(new ThreadStart(MainThread));
            _socketThread.Start();
            ReportServerMessage("Server Started Successfully");
        }

        //todo: refactor this method more generically
        public void SendCommand(string command, bool toClient = false, bool broadcast = false, string ipAddress = "")
        {
            if (command.ToLower() == "quit" && !toClient)
            {
                Command = "quit";
            }
            if (toClient)
            {
                if (broadcast)
                {
                    ServerSendData.SendToAll(command.ToByteArray());
                }
                else if (ManageConnection.GetIndexOFConnectedClientByIpAddress(ipAddress) >= 0)
                {
                    ServerSendData.SendDataToIp(ipAddress, command.ToByteArray());
                }
            }
            if (command.ToLower() == "list_connections"&&!toClient)
                ReportServerMessage(JsonConvert.SerializeObject(ManageConnection.ConnectedClients));
            

        }

        private static void MainThread()
        {
            consoleRunning = true;

            while (consoleRunning)
            {
                switch (Command.ToLower())
                {
                    case "quit":
                        consoleRunning = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnClientConnect(IAsyncResult ar)
        {
            var tmp_range = new List<int>();
            TcpClient client = _server.EndAcceptTcpClient(ar);
            _server.BeginAcceptTcpClient(OnClientConnect, null);
            client.NoDelay = false;
            if (ManageConnection.ConnectedClients.Count > 0) ManageConnection.ConnectedClients.ForEach(_client => tmp_range.Add(_client.id));
            var clientObject = new Client()
            {
                socket = client,
                ip = client.Client.RemoteEndPoint.ToString(),
                id =Enumerable.Range(0, ManageConnection.ConnectedClients.Count).Except(tmp_range).ToList().Count!=0? 
                Enumerable.Range(0, ManageConnection.ConnectedClients.Count).Except(tmp_range).ToList().First():
                (ManageConnection.ConnectedClients.Count > 0 ? ManageConnection.ConnectedClients.Max(tmp_client => tmp_client.id) + 1 : 0)
            };
            clientObject.OnClientConnectionEvent += ClientObject_OnClientConnectionEvent;
            clientObject.OnClientDataRecivedEvent += ClientObject_OnClientDataRecivedEvent;
            clientObject.Start();

        }

        private void ClientObject_OnClientDataRecivedEvent(ClientDataRecivedEventArgs e)
        {
            ReportDataRecived(Encoding.Default.GetString(e.Data));
        }

        private void ClientObject_OnClientConnectionEvent(ClientEventArgs e)
        {
            if (e.Connected)
            {
                ManageConnection.ConnectedClients.Add(e.Client);
            }
            else
            {
                ManageConnection.ConnectedClients.Remove(e.Client);
            }
            ReportConnectionMessage(e.Message);
        }


        public delegate void ServerMessageEventHandler(ServerEventArgs e);
        public event ServerMessageEventHandler OnServerMessageEvent;

        public delegate void ServerConnectionEventHandler(ServerEventArgs e);
        public event ServerConnectionEventHandler OnServerConnectionEvent;

        public delegate void ServerDataRecievedEventHandler(ServerEventArgs e);
        public event ServerDataRecievedEventHandler OnServerDataRecieved;
        protected virtual void ServerMessageEvent(string message) => OnServerMessageEvent?.Invoke(new ServerEventArgs(message));
        private void ReportServerMessage(string message) => ServerMessageEvent(message);
        protected virtual void ServerConnectionEvent(string message) => OnServerConnectionEvent?.Invoke(new ServerEventArgs(message));
        private void ReportConnectionMessage(string message) => ServerConnectionEvent(message);
        protected virtual void ServerDataRecivedEvent(string message) => OnServerDataRecieved?.Invoke(new ServerEventArgs(message));
        private void ReportDataRecived(string message) => ServerDataRecivedEvent(message);

    }
}
