using Assets.scripts;
using UnityEngine;
using Xml2CSharp;

namespace Assets.Actions {
    public class DontResponse : MonoBehaviour {
        private GameObject joinButton;
        private GameObject dontJoinButton;
        private bool setup;
        private bool extra;
        private StateManagerContainer manager;

        private void Start() {
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
            foreach (Transform button in
                GameObject.FindGameObjectWithTag("JoiningButtons").transform) {
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
            joinButton.SetActive(false);
            dontJoinButton.SetActive(false);
            if (extra) {
                var savedLocation = manager.manager.Player1.CurrentLocation;
                manager.Stay();
                manager.Goto(savedLocation, true, false);
            } else {
                manager.Stay();
            }
            setup = false;
            extra = true;
        }

        public void Action(string which) {
            if (which != "") {
                extra = true;
            }
            setup = true;
        }
    }
}
