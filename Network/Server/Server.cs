using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Assets.Network.Shared;

namespace Network.Server {
    public class Server {
        private int port;
        private static bool running = true;
        private static TcpListener listener;
        private static int ID;

        public Server(int port) {
            this.port = port;
        }

        public static void Main() {
            listener = new TcpListener(IPAddress.Any, 8001);
            listener.Start();

            while (running) {
                Console.Out.WriteLine("Waiting for client");
                WorkerCreation(listener);
            }
        }

        private static void WorkerCreation(TcpListener listener) {
            var tcpClient = listener.AcceptTcpClient();
            var worker = new OutputWorker(tcpClient, ID);
            var inputWorker = new InputWorker(ID, tcpClient, worker);
            var outputThread = new Thread(() => CreateOutputWorker(worker));
            var inputThread = new Thread(() => CreateInputWorker(inputWorker));
            Console.Out.WriteLine("Logging in ID: " + ID);
            outputThread.Start();
            inputThread.Start();
            var state = new PlayerState {ID = ID};
            Data.UpdateState(ID, state);
            Data.AddUser(ID++, worker, inputWorker);
        }

        private static void CreateInputWorker(InputWorker inputWorker) {
            inputWorker.Start();
        }

        private static void CreateOutputWorker(OutputWorker outputWorker) {
            outputWorker.Start();
        }

        public void Dispose() {
            running = false;
            listener.Stop();
        }
    }
}