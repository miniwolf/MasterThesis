using System;
using Assets.Network.Shared.Messages;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class TalkingTo : InGoingMessages<string>, Access<string> {
        public readonly string npc;

        public TalkingTo(string npc) {
            this.npc = npc;
        }

        public Access<string> GetAccess() {
            return this;
        }

        public string GetData() {
            return npc;
        }
    }
}
