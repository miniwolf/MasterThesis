using System.Threading;
using Assets.Network.Shared.Messages;

namespace Assets.Network.Client.Handlers {
    public interface Handler {
        Thread GetThread();

        void Handle(InGoingMessages obj);
    }

    public interface Handler<T> : Handler {
        void Handle(InGoingMessages<T> obj);
    }
}