using System.Threading;
using Assets.Events;
using Assets.Network.Shared;
using Assets.Network.Shared.Messages;
using Assets.scripts;

namespace Assets.Network.Client.Handlers {
    public class PlayerStateHandler : Handler<PlayerState> {
        private readonly GameStateManager manager;

        public PlayerStateHandler(GameStateManager manager) {
            this.manager = manager;
        }

        public Thread GetThread() {
            return new Thread(() => { });
        }

        public void Handle(InGoingMessages<PlayerState> obj) {
            var playerState = (PlayerState) obj;
            manager.Player2.CurrentLocation = (Location) CheckReference(playerState.Location,
                manager.Player2.CurrentLocation, Events.Events.Travelled);
            manager.Player2.CurrentQuest = (Quest) CheckReference(playerState.Quest, manager.Player2.CurrentQuest,
                Events.Events.QuestStarted);
            manager.Player2.TalkingTo = (Npc) CheckReference(playerState.Npc, manager.Player2.TalkingTo,
                Events.Events.StartedTalking);
            manager.IsGrouped = Equals(manager.Player2.CurrentLocation, manager.Player1.CurrentLocation);
        }

        private object CheckReference(object A, object B, Events.Events e) {
            if ((A != null || B == null) && (A == null || B != null)) {
                return B;
            }
            if (manager.IsGrouped) {
                EventManager.CallEvent(e);
            }
            return A;
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<PlayerState>) obj);
        }
    }
}