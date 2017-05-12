using System.Threading;
using Assets.Events;
using Assets.scripts;
using Network.Client.Handlers;
using Network.Shared;
using Network.Shared.Messages;

namespace Assets.Network.Client.Handlers {
    public class PlayerStateHandler : Handler<PlayerState> {
        private readonly Player me;
        private readonly Player player;
        private readonly GameStateManager manager;

        public PlayerStateHandler(Player me, Player player, GameStateManager manager) {
            this.me = me;
            this.player = player;
            this.manager = manager;
        }

        public Thread GetThread() {
            return new Thread(() => { });
        }

        public void Handle(InGoingMessages<PlayerState> obj) {
            var playerState = (PlayerState) obj;
            player.CurrentLocation = playerState.Location;
            if (manager.IsGrouped) {
                EventManager.CallEvent(Events.Events.Travelled);
            }
            manager.IsGrouped = Equals(player.CurrentLocation, me.CurrentLocation);
            if (manager.IsGrouped) {
                EventManager.CallEvent(Events.Events.QuestStarted);
            }
            player.CurrentQuest = playerState.Quest;
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<PlayerState>) obj);
        }
    }
}