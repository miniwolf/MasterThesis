using System.Threading;
using Assets.scripts;
using Network.Shared;
using Network.Shared.Messages;

namespace Network.Client.Handlers {
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
            manager.IsGrouped = Equals(player.CurrentLocation, me.CurrentLocation);
            player.CurrentQuest = playerState.Quest;
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<PlayerState>) obj);
        }
    }
}