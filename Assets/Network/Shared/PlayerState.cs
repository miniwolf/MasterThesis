using System;
using Network.Shared.Messages;

namespace Network.Shared {
    [Serializable]
    public class PlayerState : InGoingMessages<PlayerState>, Access<PlayerState> {
        public int ID { get; set; }
        public location Location { get; set; }
        public Quest Quest { get; set; }

        public Access<PlayerState> GetAcces() {
            return this;
        }

        public PlayerState GetData() {
            return this;
        }
    }
}
