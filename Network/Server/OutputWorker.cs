using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Network.Server {
    public class OutputWorker : Worker {
        private readonly NetworkStream networkStream;
        private readonly BinaryFormatter formatter = new BinaryFormatter();
        private readonly int ID;

        public OutputWorker(TcpClient tcpClient, int ID) {
            networkStream = tcpClient.GetStream();
            Response = new Queue<object>();
            this.ID = ID;
        }

        public Queue<object> Response { get; }

        public void Start() {
            while (running) {
                try {
                    SendResponses();
                } catch (IOException) {
                    Console.Out.WriteLine("Closing output worker");
                    running = false;
                }
                while (Response.Count == 0 && running) {
                    try {
                        Thread.Sleep(100);
                    } catch(IOException) {
                        Console.Out.WriteLine("Closing output worker");
                        running = false;
                    } catch (ThreadInterruptedException) {
                        Console.Out.WriteLine("Closing output worker");
                        running = false;
                    }
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
