using Assets.scripts;
using UnityEngine;

namespace Assets.Actions {
    public class TravelResponse : MonoBehaviour {
        private StateManagerContainer manager;

        private void Start() {
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
        }

        public void Travel() {
            manager.manager.WaitingForResponse = true;
            manager.Goto(manager.manager.Player2.CurrentLocation);
        }
    }
}