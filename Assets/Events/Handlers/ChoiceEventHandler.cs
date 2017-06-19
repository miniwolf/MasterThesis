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
            if (manager.manager.WaitingForResponse || !manager.manager.IsGrouped) {
                var gameStateManager = manager.manager;
                if (gameStateManager.IsGrouped) {
                    gameStateManager.AddGlobalPres(gameStateManager.Player2.HasChosen);
                    if (gameStateManager.Player1.HasChosen.Results.Description.Priority == null
                        || gameStateManager.Player2.HasChosen.Results.Description.Priority == null) {
                        Print(gameStateManager.Player1.HasChosen, true, gameStateManager.Player2.HasChosen, false);
                    } else if (int.Parse(gameStateManager.Player1.HasChosen.Results.Description.Priority) <
                        int.Parse(gameStateManager.Player2.HasChosen.Results.Description.Priority)) {
                        Print(gameStateManager.Player1.HasChosen, true, gameStateManager.Player2.HasChosen, false);
                    } else {
                        Print(gameStateManager.Player2.HasChosen, false, gameStateManager.Player1.HasChosen, true);
                    }
                } else {
                    gameStateManager.AddChoiceDescriptionToUI(gameStateManager.Player1.HasChosen, true);
                }
            }
            manager.manager.WaitingForResponse = !manager.manager.WaitingForResponse;
        }

        private void Print(Choice first, bool firstBool, Choice second, bool secondBool) {
            manager.manager.AddChoiceDescriptionToUI(first, firstBool);
            if (!manager.manager.Player1.HasChosen.Name.Equals(manager.manager.Player2.HasChosen.Name)) {
                manager.manager.AddChoiceDescriptionToUI(second, secondBool);
            }
        }
    }
}
