using System;
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
                if (manager.manager.IsGrouped) {
                    manager.manager.AddGlobalPres(manager.manager.Player2.HasChosen);
                    if (int.Parse(manager.manager.Player1.HasChosen.Results.Description.Priority) <
                        int.Parse(manager.manager.Player2.HasChosen.Results.Description.Priority)) {
                        Print(manager.manager.Player1.HasChosen, true, manager.manager.Player2.HasChosen, false);
                    } else {
                        Print(manager.manager.Player2.HasChosen, false, manager.manager.Player1.HasChosen, true);
                    }
                } else {
                    manager.manager.AddChoiceDescriptionToUI(manager.manager.Player1.HasChosen, true);
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