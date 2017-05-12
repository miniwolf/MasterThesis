using System.Collections.Generic;
using System.Threading;
using Assets.Network.Shared.Messages;

namespace Assets.Network.Client.Handlers.Container {
    public class DefaultContainer : Container {
        private readonly Queue<InGoingMessages> queue = new Queue<InGoingMessages>();
        private Thread runnable;

        public void AddObject(InGoingMessages obj) {
            queue.Enqueue(obj);
            lock (runnable) {
                Monitor.Pulse(runnable);
            }
        }

        public Queue<InGoingMessages> GetQueue() {
            return queue;
        }

        public void SetThread(Thread thread) {
            runnable = thread;
        }
    }
}