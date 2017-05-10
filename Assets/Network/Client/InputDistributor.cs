using System;
using System.Collections.Generic;
using System.Threading;
using Network.Client.Handlers.Container;
using Network.Shared.Messages;

namespace Network.Client {
    public class InputDistributor {
        private readonly Dictionary<Type, Container> containers;

        private readonly Queue<InGoingMessages> messages =
            new Queue<InGoingMessages>();

        private bool running = true;

        public InputDistributor(Dictionary<Type, Container> containers) {
            this.containers = containers;
        }

        public void Start() {
            while (running) {
                while (messages.Count == 0) {
                    Thread.Sleep(100);
                }
                InGoingMessages input;
                lock (messages) {
                    input = messages.Dequeue();
                }
                Container container;
                containers.TryGetValue(input.GetType(), out container);
                if (container == null) {
                    lock (messages) {
                        messages.Enqueue(input);
                    }
                    continue;
                }
                container.AddObject(input);
            }
        }

        public void AddMessage(InGoingMessages msg) {
            lock (messages) {
                messages.Enqueue(msg);
            }
        }

        public void Stop() {
            running = false;
        }
    }
}
