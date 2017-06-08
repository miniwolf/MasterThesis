using System;
using Assets.Network.Shared.Messages;
using Xml2CSharp;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class OtherHasChosen : InGoingMessages<Choice>, Access<Choice> {
        public Choice Choice { get; private set; }

        public OtherHasChosen(Choice choice) {
            Choice = choice;
        }

        public Access<Choice> GetAccess() {
            return this;
        }

        public Choice GetData() {
            return Choice;
        }
    }
}