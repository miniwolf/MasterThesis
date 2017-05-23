using Assets.scripts;
using UnityEngine;

namespace Assets.Events.Handlers {
    public class TalkingEventHandler : MonoBehaviour, EventHandler {
        private GameObject joinButton;
        private GameObject dontJoinButton;
        private bool setup;
        private StateManagerContainer manager;

        private void Start() {
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
            EventManager.SubscribeToEvent(Events.StartedTalking, this);
            foreach (Transform button in GameObject.FindGameObjectWithTag("JoiningButtons").transform) {
                if (button.tag.Equals("Join")) {
                    joinButton = button.gameObject;
                } else {
                    dontJoinButton = button.gameObject;
                }
            }
        }

        private void Update() {
            if (!setup) {
                return;
            }
            joinButton.SetActive(true);
            dontJoinButton.SetActive(true);
            setup = false;
        }

        public void Action() {
            if (manager.manager.WaitingForResponse) {
                manager.TalkTo(manager.manager.Player2.TalkingTo);
                return;
            }
            setup = true;
        }
    }
}