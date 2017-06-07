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
            CheckGroup();
            if (CheckReference(playerState.Location, manager.Player2.CurrentLocation)) {
                manager.Player2.CurrentLocation = playerState.Location;
                if (manager.IsGrouped || manager.WaitingForResponse) {
                    EventManager.CallEvent(Events.Events.Travelled);
                }
            }
            if (CheckReference(playerState.Quest, manager.Player2.CurrentQuest)) {
                manager.Player2.CurrentQuest = playerState.Quest;
                if (manager.IsGrouped || manager.WaitingForResponse) {
                    EventManager.CallEvent(Events.Events.QuestStarted);
                }
            }
            if (CheckReference(playerState.Npc, manager.Player2.TalkingTo)) {
                manager.Player2.TalkingTo = playerState.Npc;
                if (manager.IsGrouped || manager.WaitingForResponse) {
                    EventManager.CallEvent(Events.Events.StartedTalking);
                }
            }
            CheckGroup();
        }

        private void CheckGroup() {
            manager.IsGrouped = manager.Player1.CurrentLocation == null && manager.Player2.CurrentLocation == null 
                                || manager.Player1.CurrentLocation != null && manager.Player2.CurrentLocation != null 
                                && manager.Player1.CurrentLocation.Name.Equals(manager.Player2.CurrentLocation.Name);
        }

        private static bool CheckReference(object A, object B) {
            return (A == null && B != null) || (A != null && B == null);
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<PlayerState>) obj);
        }
    }
}