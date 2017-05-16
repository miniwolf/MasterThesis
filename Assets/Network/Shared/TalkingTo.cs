using System;

namespace Assets.Network.Shared {
    [Serializable]
    public class TalkingTo {
        public readonly Npc npc;

        public TalkingTo(Npc npc) {
            this.npc = npc;
        }
    }
}
