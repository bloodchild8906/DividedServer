using ServerLibrary.Helpers;
using System.Linq;

namespace ServerLibrary
{
    public static class ServerSendData
    {
        public static void SendDataToIp(string ipAddress, byte[] data)
        {
            HandleBuffer buffer = new HandleBuffer();
            buffer.WriteBytes(data);
            ManageConnection
                .ConnectedClients[ManageConnection.GetIndexOFConnectedClientByIpAddress(ipAddress)]
                .stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
        }

        public static void SendDataToId(int id, byte[] data)
        {
            HandleBuffer buffer = new HandleBuffer();
            buffer.WriteBytes(data);
            ManageConnection
                .ConnectedClients[ManageConnection.GetIndexOFConnectedClientById(id)]
                .stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
        }

        public static void SendDataToIndex(int index, byte[] data)
        {
            HandleBuffer buffer = new HandleBuffer();
            buffer.WriteBytes(data);
            ManageConnection
                .ConnectedClients[index]
                .stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
        }

        public static void SendToAll(byte[] data)
        {
            HandleBuffer buffer = new HandleBuffer();
            buffer.WriteBytes(data);
            ManageConnection
                .ConnectedClients.ForEach(client => client.stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null));
        }

        public static void SendToRangeOfIpAdresses(string[] ipRange, byte[] data)
        {
            HandleBuffer buffer = new HandleBuffer();
            buffer.WriteBytes(data);
            ipRange.ToList().ForEach(ipAddress => ManageConnection
                .ConnectedClients[ManageConnection.GetIndexOFConnectedClientByIpAddress(ipAddress)]
                .stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null));

        }

        public static void SendToRangeOfIds(int[] idRange, byte[] data)
        {
            HandleBuffer buffer = new HandleBuffer();
            buffer.WriteBytes(data);
            idRange.ToList().ForEach(id => ManageConnection
                .ConnectedClients[ManageConnection.GetIndexOFConnectedClientById(id)]
                .stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null));
        }

        public static void SendToRangeOfIndexes(int[] indexRange, byte[] data)
        {
            HandleBuffer buffer = new HandleBuffer();
            buffer.WriteBytes(data);
            indexRange.ToList().ForEach(index=> ManageConnection.ConnectedClients[index]
                .stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null));
            ;
        }

    }
}
