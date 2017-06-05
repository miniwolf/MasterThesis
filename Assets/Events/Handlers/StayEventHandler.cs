using Assets.scripts;
using UnityEngine;

namespace Assets.Events.Handlers {
    public class StayEventHandler : MonoBehaviour, EventHandler {
        private StateManagerContainer manager;

        private void Start() {
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
            EventManager.SubscribeToEvent(Events.Staying, this);
        }
        
        private void OnDestroy() {
            EventManager.UnsubscribeToEvent(Events.Staying, this);
        }

        public void Action() {
            manager.manager.WaitingForResponse = false;
        }
    }
}