using System;
using System.Diagnostics;
using System.Threading;

namespace Network.Client.Handlers {
    public class DefaultThreadHandler {
        private readonly Container.Container container;
        private readonly Handler handler;
        private bool running = true;

        public DefaultThreadHandler(Container.Container container, Handler handler) {
            this.container = container;
            this.handler = handler;
        }

        public void Start() {
            while (running) {
                while (container.GetQueue().Count != 0) {
                    handler.Handle(container.GetQueue().Dequeue());
                }
                try {
                    lock(this) {
                        Monitor.Wait(this);
                    }
                } catch (ThreadInterruptedException e) {
                    running = false;
                }
            }
        }
    }
}