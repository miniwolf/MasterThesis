using System;
using Xml2CSharp;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class HasChosen {
        public Choice Choice { get; private set; }

        public HasChosen(Choice choice) {
            Choice = choice;
        }
    }
}