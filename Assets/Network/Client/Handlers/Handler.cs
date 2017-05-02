using System.Threading;
using Network.Shared;
using Network.Shared.Messages;

namespace Network.Client.Handlers {
    public interface Handler {
        Thread GetThread();

        void Handle(InGoingMessages obj);
    }

    public interface Handler<T> : Handler {
        void Handle(InGoingMessages<T> obj);
    }
}