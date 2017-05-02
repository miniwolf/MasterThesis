using System.Threading;
using Network.Shared;
using Network.Shared.Messages;
using UnityEngine;

namespace Network.Client.Handlers {
    public class PlayerStateHandler : Handler<PlayerState> {
        public Thread GetThread() {
            return new Thread(() => { });
        }
        public void Handle(InGoingMessages<PlayerState> obj) {
            var playerState = (PlayerState) obj;
            Debug.Log(playerState);
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<PlayerState>) obj);
        }
    }
}