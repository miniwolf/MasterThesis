using System.Collections.Generic;
using System.Threading;
using Network.Shared.Messages;

namespace Network.Client.Handlers.Container {
    public class DefaultContainer : Container {
        private readonly Queue<InGoingMessages> queue = new Queue<InGoingMessages>();
        private Thread runnable;

        public void AddObject(InGoingMessages obj) {
            queue.Enqueue(obj);
            Monitor.Pulse(runnable);
        }

        public Queue<InGoingMessages> GetQueue() {
            return queue;
        }

        public void SetThread(Thread thread) {
            this.runnable = thread;
        }
    }
}