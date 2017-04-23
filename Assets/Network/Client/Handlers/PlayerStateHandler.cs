using System;
using System.Threading;
using Network.Shared;
using Network.Shared.Messages;

namespace Network.Client.Handlers {
    public class PlayerStateHandler : Handler<PlayerState> {
        public void Handle(InGoingMessages<PlayerState> obj) {
            var playerState = (PlayerState) obj;
            Console.Out.WriteLine(playerState);
        }

        public Thread GetThread() {
            return new Thread(() => { });
        }
    }
}