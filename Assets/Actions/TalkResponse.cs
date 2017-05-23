using Assets.scripts;
using UnityEngine;

namespace Assets.Actions {
    public class TalkResponse : MonoBehaviour {
        private StateManagerContainer manager;

        private void Start() {
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
        }

        public void Talk() {
            manager.manager.WaitingForResponse = true;
            manager.TalkTo(manager.manager.Player2.TalkingTo);
        }
    }
}