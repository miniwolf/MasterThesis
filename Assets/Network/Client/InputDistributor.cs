using System;
using System.Collections.Generic;
using Network.Shared.Messages;

namespace Network.Client {
    public class InputDistributor {
        private readonly Dictionary<Type, Container.Container> containers;

        private readonly Queue<InGoingMessages<object>> messages =
            new Queue<InGoingMessages<object>>();

        private bool running = true;

        public InputDistributor(Dictionary<Type, Container.Container> containers) {
            this.containers = containers;
        }

        public void Start() {
            while (running) {
                var input =messages.Dequeue();
                var container = containers[input.GetType()];
                if (container == null) {
                    messages.Enqueue(input);
                    continue;
                }
                container.AddObject(input);
            }
        }

        public void AddMessage(InGoingMessages<object> msg) {
            messages.Enqueue(msg);
        }

        public void Stop() {
            running = false;
        }
    }
}
