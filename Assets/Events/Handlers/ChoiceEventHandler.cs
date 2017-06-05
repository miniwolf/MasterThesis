using Assets.scripts;
using UnityEngine;
using Xml2CSharp;

namespace Assets.Events.Handlers {
    public class ChoiceEventHandler : MonoBehaviour, EventHandler {
        private StateManagerContainer manager;

        private void Start() {
            EventManager.SubscribeToEvent(Events.OtherHasChosen, this);
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
        }
        
        private void OnDestroy() {
            EventManager.UnsubscribeToEvent(Events.OtherHasChosen, this);
        }
        
        public void Action() {
            if (manager.manager.WaitingForResponse) {
                manager.Choose(manager.manager.Player1.HasChosen);
                return;
            }
            manager.manager.WaitingForResponse = true;
        }
    }
}