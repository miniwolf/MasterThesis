using Assets.scripts;
using UnityEngine;

namespace Assets.Actions {
    public class QuestResponse : MonoBehaviour {
        private StateManagerContainer manager;

        private void Start() {
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
        }

        public void StartQuest() {
            manager.manager.WaitingForResponse = true;
            manager.StartQuest(manager.manager.Player2.CurrentQuest);
        }

        public void Dont() {
            var savedLocation = manager.manager.Player1.CurrentLocation;
            manager.Stay();
            manager.Goto(savedLocation, true);
        }
    }
}
