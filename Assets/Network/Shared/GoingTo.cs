using System;
using Xml2CSharp;

namespace Assets.Network.Shared {
    [Serializable]
    public class GoingTo {
        public Location Location { get; private set; }

        public GoingTo(Location location) {
            Location = location;
        }
    }
}
