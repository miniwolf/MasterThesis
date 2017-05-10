using System.Threading;
using Assets.scripts;
using Network.Shared;
using Network.Shared.Messages;

namespace Network.Client.Handlers {
    public class PlayerStateHandler : Handler<PlayerState> {
        private readonly Player player;

        public PlayerStateHandler(Player player) {
            this.player = player;
        }

        public Thread GetThread() {
            return new Thread(() => { });
        }
        public void Handle(InGoingMessages<PlayerState> obj) {
            var playerState = (PlayerState) obj;
            player.CurrentLocation = playerState.Location;
            player.CurrentQuest = playerState.Quest;
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<PlayerState>) obj);
        }
    }
}