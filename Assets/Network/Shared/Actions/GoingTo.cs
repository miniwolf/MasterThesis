using System;
using Assets.Network.Shared.Messages;
using Xml2CSharp;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class GoingTo : InGoingMessages<Location>, Access<Location> {
        private readonly Location location;

        public GoingTo(Location location) {
            this.location = location;
        }

        public Access<Location> GetAccess() {
            return this;
        }

        public Location GetData() {
            return location;
        }
    }
}
