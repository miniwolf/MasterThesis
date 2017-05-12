using System.Collections.Generic;
using System.Threading;
using Assets.Network.Shared.Messages;

namespace Assets.Network.Client.Handlers.Container {
    public interface Container {
        void AddObject(InGoingMessages obj);

        Queue<InGoingMessages> GetQueue();

        void SetThread(Thread thread);
    }
}