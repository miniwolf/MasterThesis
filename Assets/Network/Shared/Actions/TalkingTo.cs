using System;
using Assets.Network.Shared.Messages;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class TalkingTo : InGoingMessages {
        public readonly string npc;

        public TalkingTo(string npc) {
            this.npc = npc;
        }
    }
}
