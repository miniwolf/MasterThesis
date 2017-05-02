using System.Collections.Generic;
using System.Threading;
using Network.Shared.Messages;

namespace Network.Client.Handlers.Container {
    public interface Container {
        void AddObject(InGoingMessages obj);

        Queue<InGoingMessages> GetQueue();

        void SetThread(Thread thread);
    }
}