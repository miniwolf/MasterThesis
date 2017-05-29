using System;
using Assets.Network.Shared.Messages;
using Xml2CSharp;

namespace Assets.Network.Shared {
    [Serializable]
    public class PlayerState : InGoingMessages<PlayerState>, Access<PlayerState> {
        public int ID { get; set; }
        public Location Location { get; set; }
        public Quest Quest { get; set; }
        public string Npc { get; set; }

        public Access<PlayerState> GetAcces() {
            return this;
        }

        public PlayerState GetData() {
            return this;
        }
    }
}
