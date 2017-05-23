﻿using UnityEngine;

namespace Assets.Events.Handlers {
    public class QuestEventHandler : MonoBehaviour, EventHandler {
        private GameObject joinButton;
        private GameObject dontJoinButton;
        private bool setup;

        private void Start() {
            EventManager.SubscribeToEvent(Events.QuestStarted, this);
            foreach (Transform button in GameObject.FindGameObjectWithTag("JoiningButtons").transform) {
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
            joinButton.SetActive(true);
            dontJoinButton.SetActive(true);
            setup = false;
        }

        public void Action() {
            setup = true;
        }
    }
}
