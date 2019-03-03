using System.Collections.Generic;
using System.Linq;

namespace ServerLibrary
{
    public static class ManageConnection
    {
        public static bool AddClient(Client client)
        {
            if (!ConnectedClients.Any(tmp_client => tmp_client.ip == client.ip))
            {
                ConnectedClients.Add(client);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Client> ConnectedClients = new List<Client>();

        #region GetIndexOfConnectedClient
        public static int GetIndexOFConnectedClientByIpAddress(string ipAddress)
        {
            //check if list empty
            if (ConnectedClients.Count == 0) return -1;
            //check if item does not exist
            if (!ConnectedClients.Any(client => client.ip == ipAddress)) return -2;
            //get index if item exists
            var index = ConnectedClients.FindIndex(client => client.ip == ipAddress);
            return index;
        }

        public static int GetIndexOFConnectedClientById(int id)
        {
            //check if list empty
            if (ConnectedClients.Count == 0) return -1;
            //check if item does not exist
            if (!ConnectedClients.Any(client => client.id == id)) return -2;
            //get index if item exists
            var index = ConnectedClients.FindIndex(client => client.id == id);
            return index;
        } 
        #endregion

        #region RemoveConnection
        public static bool RemoveConnectionByIndex(int index)
        {
            if (ConnectedClients.Count != 0 && (ConnectedClients.Count - 1) >= index)
            {
                ConnectedClients.RemoveAt(index);
                return true;
            }
            else if (ConnectedClients.Count == 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool RemoveConnectionById(int id)
        {
            if (ConnectedClients.Any(tmp_client => tmp_client.id == id))
            {
                ConnectedClients.RemoveAt(ConnectedClients.FindIndex(client => client.id == id));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RemoveConnectionByIpAddress(string ipAddress)
        {
            if (ConnectedClients.Any(client => client.ip == ipAddress))
            {
                ConnectedClients.RemoveAt(ConnectedClients.FindIndex(client => client.ip == ipAddress));
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion

    }
}
