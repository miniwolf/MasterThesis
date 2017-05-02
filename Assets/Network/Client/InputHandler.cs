using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Network.Client;
using Network.Client.Handlers.Container;
using Network.Shared;
using Network.Shared.Messages;

namespace Assets.Network.Client {
    public class InputHandler {
        private readonly BinaryFormatter formatter;
        private readonly NetworkStream objIn;
        private readonly Queue<Response> inputs = new Queue<Response>();
        private static readonly Dictionary<Type, Container> containers =
            new Dictionary<Type, Container>();
        private bool running = true;
        private readonly InputDistributor distributor;
        private readonly Thread distributorThread;

        public InputHandler(BinaryFormatter formatter, NetworkStream objIn) {
            this.formatter = formatter;
            this.objIn = objIn;
            distributor = new InputDistributor(containers);
            distributorThread = new Thread(() => distributor.Start());
            distributorThread.Start();
        }

        public void Start() {
            object input = null;
            while (running) {
                while (input == null && running) {
                    input = formatter.Deserialize(objIn);
                    HandleInput(input);
                }
            }
        }

        private void HandleInput(object input) {
            var item = input as Response;
            if (item != null) {
                inputs.Enqueue(item);
                return;
            }
            distributor.AddMessage((InGoingMessages) input);
        }

        public Response ContainsResponse() {
            return inputs.Dequeue();
        }

        public static void Register(Type type, Container handler) {
            containers.Add(type, handler);
        }

        public static void Unregister(Type type) {
            containers.Remove(type);
        }

        public void Close() {
            running = false;
            distributorThread.Abort();
        }
    }
}
