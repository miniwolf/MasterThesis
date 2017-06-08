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
                manager.manager.Player1.Choose(manager.manager.Player1.HasChosen);
                manager.manager.AddChoiceDescriptionToUI(manager.manager.Player1.HasChosen, true);
                manager.manager.AddChoiceDescriptionToUI(manager.manager.Player2.HasChosen, false);
                manager.manager.AddGlobalPres(manager.manager.Player2.HasChosen);
            }
            manager.manager.WaitingForResponse = !manager.manager.WaitingForResponse;
        }
    }
}