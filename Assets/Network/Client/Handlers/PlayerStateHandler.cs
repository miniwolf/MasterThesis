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
            manager.Player2.CurrentLocation = playerState.Location;
            if (manager.IsGrouped) {
                EventManager.CallEvent(Events.Events.Travelled);
            }
            manager.IsGrouped = Equals(manager.Player2.CurrentLocation, manager.Player1.CurrentLocation);
            manager.Player2.CurrentQuest = playerState.Quest;
            if (manager.IsGrouped) {
                EventManager.CallEvent(Events.Events.QuestStarted);
            }
            manager.Player2.TalkingTo = playerState.Npc;
            if (manager.IsGrouped) {
                EventManager.CallEvent(Events.Events.StartedTalking);
            }
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<PlayerState>) obj);
        }
    }
}