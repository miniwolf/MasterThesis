using UnityEngine;

namespace Assets.Events.Handlers {
    public class TravelEventHandler : MonoBehaviour, EventHandler {
        private GameObject joinButton;
        private GameObject dontJoinButton;

        private void Start() {
            EventManager.SubscribeToEvent(Events.Travelled, this);
            joinButton = GameObject.FindGameObjectWithTag("Join");
            dontJoinButton = GameObject.FindGameObjectWithTag("DontJoin");
        }

        public void Action() {
            joinButton.SetActive(true);
            dontJoinButton.SetActive(true);
        }
    }
}
