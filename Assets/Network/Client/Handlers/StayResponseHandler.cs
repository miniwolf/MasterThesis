using System.Threading;
using Assets.Events;
using Assets.Network.Shared.Actions;
using Assets.Network.Shared.Messages;

namespace Assets.Network.Client.Handlers {
    public class StayResponseHandler : Handler<StayResponse> {
        public void Handle(InGoingMessages<StayResponse> obj) {
            EventManager.CallEvent(Events.Events.Staying);
        }

        public Thread GetThread() {
            return new Thread(() => { });
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<StayResponse>) obj);
        }
    }
}