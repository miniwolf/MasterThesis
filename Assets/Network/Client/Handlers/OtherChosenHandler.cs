using System.Threading;
using Assets.Events;
using Assets.Network.Shared.Messages;
using Assets.scripts;
using Xml2CSharp;

namespace Assets.Network.Client.Handlers {
    public class OtherChosenHandler : Handler<Choice> {
        private readonly GameStateManager manager;

        public OtherChosenHandler(GameStateManager manager) {
            this.manager = manager;
        }
        
        public void Handle(InGoingMessages<Choice> obj) {
            manager.Player2.HasChosen = obj.GetAccess().GetData();
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
