using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Network.Shared;
using Xunit;

namespace Test.Server {
    public class ServerTest : IDisposable {
        private readonly Network.Server.Server server;
        private readonly TcpClient tcpClient;

        public ServerTest() {
            server = new Network.Server.Server(8001);
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, e) => {
                server.Start();
            };
            worker.RunWorkerAsync();
            tcpClient = new TcpClient();
            tcpClient.Connect("localhost", 8001);
        }

        public void Dispose() {
            server.Dispose();
        }

        [Fact]
        public void CanConnecToServer() {
            new TcpClient().Connect("localhost", 8001);
            Console.WriteLine("Connected");
            Assert.True(true, "Should reach this place");
        }

        [Fact]
        public void CanSendStringToServer() {
            Console.WriteLine("Connected");
            var formatter = new BinaryFormatter();
            var networkStream = tcpClient.GetStream();
            formatter.Serialize(networkStream, "String");
            Assert.True(true, "Should reach this place");
        }

        [Fact]
        public void SendingCurrentLocation_returnsAllIsWell() {
            var formatter = new BinaryFormatter();
            var networkStream = tcpClient.GetStream();
            var location = new location {Name = new name {Value = "Temple"}};
            formatter.Serialize(networkStream, location);

            var deserialize = formatter.Deserialize(networkStream);
            Assert.True(deserialize.GetType() == typeof(AllIsWell));
        }

        [Fact]
        public void SendingGetState_returnsState0ForFirstPlayerConnecting() {
            var formatter = new BinaryFormatter();
            var networkStream = tcpClient.GetStream();
            var getState = new GetState();
            formatter.Serialize(networkStream, getState);

            var deserialize = formatter.Deserialize(networkStream);
            Assert.True(deserialize.GetType() == typeof(PlayerState));
            Assert.Equal(0, ((PlayerState) deserialize).ID);
        }
    }
}
