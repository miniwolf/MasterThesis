using System.Threading;

namespace Assets.Network.Client.Handlers {
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
                while (container.GetQueue().Count == 0 && running) {
                    try {
                        Thread.Sleep(100);
                    } catch (ThreadInterruptedException e) {
                        running = false;
                    }
                }
                handler.Handle(container.GetQueue().Dequeue());
            }
        }
    }
}