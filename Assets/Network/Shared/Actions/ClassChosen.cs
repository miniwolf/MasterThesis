using System;
using Assets.Network.Shared.Messages;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class ClassChosen : InGoingMessages<string>, Access<string> {
        private readonly string clazz;

        public ClassChosen(string clazz) {
            this.clazz = clazz;
        }

        public Access<string> GetAccess() {
            return this;
        }

        public string GetData() {
            return clazz;
        }
    }
}