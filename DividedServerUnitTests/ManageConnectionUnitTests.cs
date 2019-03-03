
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServerLibrary;
using System.Net.Sockets;

namespace DividedServerUnitTests
{
    [TestClass]
    public class ManageConnectionUnitTests : BaseTest
    {
        #region GetIndexOfConnectedClient

        /// <summary>
        /// returns the index of the client by searching for the Ip Address
        /// </summary>
        [TestMethod]
        public void GetIndexOFConnectedClientByIpAddress_ThatExists()
        {
            ManageConnection.ConnectedClients.Clear();

            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", socket = new TcpClient() });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", socket = new TcpClient() });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", socket = new TcpClient() });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", socket = new TcpClient() });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", socket = new TcpClient() });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", socket = new TcpClient() });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", socket = new TcpClient() });//6

            Assert.AreEqual(5, ManageConnection.GetIndexOFConnectedClientByIpAddress("192.168.0.31"));
        }

        /// <summary>
        /// A return result of -1 means the list is empty
        /// </summary>
        [TestMethod]
        public void GetIndexOFConnectedClientByIpAddress_ThatDoesNotExist()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.2", id = 0, socket = new TcpClient() });
            Assert.AreEqual(-2, ManageConnection.GetIndexOFConnectedClientByIpAddress("192.168.0.1"));
        }

        /// <summary>
        /// a return value of -2 means the IpAddress Does not Exist in the list
        /// </summary>
        [TestMethod]
        public void GetIndexOFConnectedClientByIpAddress_WhenListEmpty()
        {
            ManageConnection.ConnectedClients.Clear();
            Assert.AreEqual(-1, ManageConnection.GetIndexOFConnectedClientByIpAddress("192.168.0.1"));
        }
        /// <summary>
        /// returns the index of the client by searching for the id
        /// </summary>
        [TestMethod]
        public void GetIndexOFConnectedClientById_ThatExists()
        {
            ManageConnection.ConnectedClients.Clear();

            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", id = 7, socket = new TcpClient() });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", id = 1, socket = new TcpClient() });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", id = 2, socket = new TcpClient() });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", id = 3, socket = new TcpClient() });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", id = 4, socket = new TcpClient() });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", id = 5, socket = new TcpClient() });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", id = 6, socket = new TcpClient() });//6

            Assert.AreEqual(3, ManageConnection.GetIndexOFConnectedClientById(3));
        }

        /// <summary>
        /// A return result of -1 means the list is empty
        /// </summary>
        [TestMethod]
        public void GetIndexOFConnectedClientById_ThatDoesNotExist()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.2", id = 0, socket = new TcpClient() });
            Assert.AreEqual(-2, ManageConnection.GetIndexOFConnectedClientById(1));
        }

        /// <summary>
        /// a return value of -2 means the IpAddress Does not Exist in the list
        /// </summary>
        [TestMethod]
        public void GetIndexOFConnectedClientById_WhenListEmpty()
        {
            ManageConnection.ConnectedClients.Clear();
            Assert.AreEqual(-1, ManageConnection.GetIndexOFConnectedClientById(1));
        }
        #endregion

        #region RemoveConnection

        [TestMethod]
        public void RemoveConnectionByIndex_WhenListEmpty()
        {
            ManageConnection.ConnectedClients.Clear();
            Assert.IsFalse(ManageConnection.RemoveConnectionByIndex(1));
        }

        [TestMethod]
        public void RemoveConnectionByIndex_ThatDoesNotExist()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", socket = new TcpClient() });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", socket = new TcpClient() });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", socket = new TcpClient() });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", socket = new TcpClient() });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", socket = new TcpClient() });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", socket = new TcpClient() });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", socket = new TcpClient() });//6

            Assert.IsFalse(ManageConnection.RemoveConnectionByIndex(7));

        }

        [TestMethod]
        public void RemoveConnectionByIndex_ThatExists()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", socket = new TcpClient() });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", socket = new TcpClient() });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", socket = new TcpClient() });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", socket = new TcpClient() });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", socket = new TcpClient() });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", socket = new TcpClient() });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", socket = new TcpClient() });//6

            Assert.IsTrue(ManageConnection.RemoveConnectionByIndex(1));

        }

        [TestMethod]
        public void RemoveConnectionById_WhenListEmpty()
        {
            ManageConnection.ConnectedClients.Clear();
            Assert.IsFalse(ManageConnection.RemoveConnectionById(1));

        }

        [TestMethod]
        public void RemoveConnectionById_ThatDoesNotExist()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", socket = new TcpClient(), id = 5 });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", socket = new TcpClient(), id = 1 });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", socket = new TcpClient(), id = 2 });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", socket = new TcpClient(), id = 3 });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", socket = new TcpClient(), id = 4 });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", socket = new TcpClient(), id = 7 });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", socket = new TcpClient(), id = 9 });//6
            Assert.IsFalse(ManageConnection.RemoveConnectionById(100));

        }

        [TestMethod]
        public void RemoveConnectionById_ThatExists()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", socket = new TcpClient(), id = 5 });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", socket = new TcpClient(), id = 1 });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", socket = new TcpClient(), id = 2 });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", socket = new TcpClient(), id = 3 });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", socket = new TcpClient(), id = 4 });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", socket = new TcpClient(), id = 7 });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", socket = new TcpClient(), id = 9 });//6
            Assert.IsTrue(ManageConnection.RemoveConnectionById(1));

        }

        [TestMethod]
        public void RemoveConnectionByIpAddress_WhenListEmpty()
        {
            ManageConnection.ConnectedClients.Clear();

            Assert.IsFalse(ManageConnection.RemoveConnectionByIpAddress("192.168.0.5"));

        }

        [TestMethod]
        public void RemoveConnectionByIpAddress_ThatDoesNotExist()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", socket = new TcpClient(), id = 5 });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", socket = new TcpClient(), id = 1 });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", socket = new TcpClient(), id = 2 });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", socket = new TcpClient(), id = 3 });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", socket = new TcpClient(), id = 4 });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", socket = new TcpClient(), id = 7 });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", socket = new TcpClient(), id = 9 });//6
            Assert.IsFalse(ManageConnection.RemoveConnectionByIpAddress("192.168.0.255"));

        }

        [TestMethod]
        public void RemoveConnectionByIpAddress_ThatExists()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.10", socket = new TcpClient(), id = 5 });//0
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.100", socket = new TcpClient(), id = 1 });//1
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.51", socket = new TcpClient(), id = 2 });//2
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.1", socket = new TcpClient(), id = 3 });//3
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.21", socket = new TcpClient(), id = 4 });//4
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.31", socket = new TcpClient(), id = 7 });//5
            ManageConnection.ConnectedClients.Add(new Client() { ip = "192.168.0.41", socket = new TcpClient(), id = 9 });//6
            Assert.IsTrue(ManageConnection.RemoveConnectionByIpAddress("192.168.0.100"));
        }
        #endregion

        #region AddConnection
        [TestMethod]
        public void AddNewClientToList()
        {
            ManageConnection.ConnectedClients.Clear();
            ManageConnection.ConnectedClients.Add(new Client() { ip="192.168.0.1", socket=new TcpClient(), id=1});
            ManageConnection.AddClient(new Client() { ip = "192.168.0.3", socket = new TcpClient(), id = 1 });
            Assert.AreEqual(2, ManageConnection.ConnectedClients.Count);
        }
        #endregion


    }
}
