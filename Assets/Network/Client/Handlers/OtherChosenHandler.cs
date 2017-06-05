using System.Threading;
using Assets.Events;
using Assets.Network.Shared.Messages;
using Xml2CSharp;

namespace Assets.Network.Client.Handlers {
    public class OtherChosenHandler : Handler<Choice> {
        public void Handle(InGoingMessages<Choice> obj) {
            EventManager.CallEvent(Events.Events.OtherHasChosen);
        }

        public Thread GetThread() {
            return new Thread(() => { });
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<Choice>) obj);
        }
    }
}