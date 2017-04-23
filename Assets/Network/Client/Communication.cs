using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Assets.Network.Client;
using Network.Shared;

namespace Network.Client {
    public class Communication {
        private readonly NetworkStream output;
        private readonly BinaryFormatter formatter;
        private readonly InputHandler inputHandler;
        private readonly Thread inputThread;

        public Communication(string host, int port) {
            var tcpClient = new TcpClient();
            tcpClient.Connect(host, port);
            output = tcpClient.GetStream();
            formatter = new BinaryFormatter();

            inputHandler = new InputHandler(new BinaryFormatter(), output);
            inputThread = new Thread(() => inputHandler.Start()) {IsBackground = true};
            inputThread.Start();
        }

        public void SendObject(object obj) {
            formatter.Serialize(output, obj);
        }

        public void Close() {
            inputHandler.Close();
            inputThread.Interrupt();
            output.Close();
        }

        public Response GetNextResponse() {
            Response response;
            while ((response = inputHandler.ContainsResponse()) == null) {}
            return response;
        }
    }
}