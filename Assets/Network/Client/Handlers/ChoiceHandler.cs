using System.Threading;
using Assets.Events;
using Assets.Network.Shared;
using Assets.Network.Shared.Actions;
using Assets.Network.Shared.Messages;
using Assets.scripts;

namespace Assets.Network.Client.Handlers {
    public class ChoiceHandler : Handler<OtherHasChosen> {
        private readonly GameStateManager manager;

        public ChoiceHandler(GameStateManager manager) {
            this.manager = manager;
        }

        public Thread GetThread() {
            return new Thread(() => { });
        }

        public void Handle(InGoingMessages<OtherHasChosen> obj) {
            manager.Player2.HasChosen = obj.GetAccess().GetData().Choice;
            EventManager.CallEvent(Events.Events.OtherHasChosen);
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<OtherHasChosen>) obj);
        }
    }
}