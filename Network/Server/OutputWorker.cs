using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Network.Server {
    public class OutputWorker {
        private bool running = true;
        private readonly NetworkStream networkStream;
        private readonly BinaryFormatter formatter = new BinaryFormatter();
        private readonly int ID;

        public OutputWorker(TcpClient tcpClient, int ID) {
            networkStream = tcpClient.GetStream();
            Response = new Queue<object>();
            this.ID = ID;
        }

        public Queue<object> Response { get; private set; }

        public void Start() {
            while (running) {
                SendResponses();
                while (Response.Count == 0 && running) {
                    Thread.Sleep(100);
                }
            }
            //"Logging out user " + myUserName;
            /*Data.userLogout(myUserName);
            if (Response.Count != 0) {
                try {
                    SendResponses();
                } catch (IOException ignored) { // User logged off we didn't manage to send response
                }
            }
            System.out.println("Closing output");*/
        }

        private void SendResponses() {
            while (Response.Count > 0) {
                Send(Response.Dequeue());
            }
        }

        private void Send(object obj) {
            Console.WriteLine("so: " + obj);
            formatter.Serialize(networkStream, obj);
            networkStream.Flush();
        }

        public int GetID() {
            return ID;
        }
    }
}
