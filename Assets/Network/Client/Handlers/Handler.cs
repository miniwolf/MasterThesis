using System.Threading;
using Network.Shared.Messages;

namespace Network.Client.Handlers {
    public interface Handler<T> {
        void Handle(InGoingMessages<T> obj);

        Thread GetThread();
    }
}