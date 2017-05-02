using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Network.Shared;

namespace Network.Server {
    public class InputWorker : Worker {
        private readonly int ID;
        private readonly OutputWorker output;
        private readonly NetworkStream objIn;
        private XmlSerializer xmlDeserializer = new XmlSerializer(typeof(string));
        private readonly BinaryFormatter formatter = new BinaryFormatter();

        public InputWorker(int ID, TcpClient tcpClient, OutputWorker output) {
            this.ID = ID;
            this.output = output;
            objIn = tcpClient.GetStream();
        }

        public void Start() {
            try {
                while (running) {
                    var input = formatter.Deserialize(objIn);
                    HandleInput(input);
                }
            } catch (IOException) {
                Data.RemoveUser(ID);
                running = false;
            }
        }

        private void HandleInput(object input) {
            if (input == null) {
                return;
            }
            var s = input as string;
            if (s != null) {
                Console.Out.WriteLine(s);
            } else if (input is UpdateCount) {
                Console.Out.WriteLine((UpdateCount) input);
            } else if (input is location) {
                var playerState = Data.GetUserState(ID);
                playerState.Location = (location) input;
                Data.UpdateState(ID, playerState);
                output.Response.Enqueue(new AllIsWell());
            } else if (input is Quest) {
                var playerState = Data.GetUserState(ID);
                playerState.Quest = (Quest) input;
                Data.UpdateState(ID, playerState);
                output.Response.Enqueue(new AllIsWell());
            } else if (input is GetState) {
                var playerState = Data.GetUserState(ID);
                output.Response.Enqueue(playerState);
            } else if (input is GetOtherStates) {
                var playerStates = Data.GetAllBut(ID);
                output.Response.Enqueue(playerStates);
            }
        }
    }

    public abstract class Worker {
        protected bool running = true;
    }
}