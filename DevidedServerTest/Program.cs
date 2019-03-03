using ServerLibrary;
using System;
using System.Threading;

namespace DevidedServerTest
{
    class Program
    {

        static void Main(string[] args)
        {
            var server = new Server(new ServerConfig() { Port = 5001 });
            server.OnServerMessageEvent += Server_OnServerEvent;
            server.OnServerConnectionEvent += Server_OnServerEvent;
            server.OnServerDataRecieved += Server_OnServerEvent;
            server.Start();
            while (true) {
                handleCommands(server);
            }
        }

        private static void handleCommands(Server server)
        {
            var command = Console.ReadLine().ToString();
            if (command.ToCharArray()[0] == '.')
            {
                server.SendCommand(command.Substring(1), true, true);
                return;
            }
            if (command.ToCharArray()[0] == '$')
            {
                var ipAndCom = command.Substring(1).Split('-');
                server.SendCommand(ipAndCom[1], true, false, ipAndCom[0]);
                return;
            }
            switch (command)
            {
                case "exit":
                    server.SendCommand("quit");
                    break;
                case "quit":
                    server.SendCommand("quit");
                    break;
                case "list":
                    server.SendCommand("list_connections");
                    break;

            }
        }

        private static void Server_OnServerEvent(ServerEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
