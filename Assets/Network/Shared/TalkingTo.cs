using System;

namespace Assets.Network.Shared {
    [Serializable]
    public class TalkingTo {
        public readonly string npc;

        public TalkingTo(string npc) {
            this.npc = npc;
        }
    }
}
