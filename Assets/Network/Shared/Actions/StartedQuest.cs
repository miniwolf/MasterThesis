using System;
using Assets.Network.Shared.Messages;
using Xml2CSharp;

namespace Assets.Network.Shared.Actions {
    [Serializable]
    public class StartedQuest : InGoingMessages<Quest>, Access<Quest> {
        private readonly Quest quest;

        public StartedQuest(Quest quest) {
            this.quest = quest;
        }

        public Access<Quest> GetAccess() {
            return this;
        }

        public Quest GetData() {
            return quest;
        }
    }
}
