using System;
using Xml2CSharp;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class OtherHasChosen {
        public Choice Choice { get; private set; }

        public OtherHasChosen(Choice choice) {
            Choice = choice;
        }
    }
}