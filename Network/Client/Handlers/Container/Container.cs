using System.Collections.Generic;
using System.Threading;
using Network.Shared.Messages;

namespace Network.Client.Container {
    public interface Container {
        void AddObject(InGoingMessages<object> obj);

        Queue<InGoingMessages<object>> GetQueue();

        void SetThread(Thread thread);
    }
}