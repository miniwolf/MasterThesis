using System.Threading;
using Network.Shared.Messages;

namespace Network.Client.Handlers {
    public class QuestHandler : Handler<Quest> {
        private readonly GameStateManager manager;

        public QuestHandler(GameStateManager manager) {
            this.manager = manager;
        }

        public Thread GetThread() {
            return new Thread(() => {});
        }

        public void Handle(InGoingMessages obj) {
            Handle((InGoingMessages<Quest>) obj);
        }

        public void Handle(InGoingMessages<Quest> obj) {
            if (!manager.IsGrouped) {
                return;
            }
        }
    }
}