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
                try {
                    while (container.GetQueue().Count == 0) {
                        Thread.Sleep(100);
                    }
                    handler.Handle(container.GetQueue().Dequeue());
                } catch (ThreadInterruptedException e) {
                    running = false;
                }
            }
        }
    }
}