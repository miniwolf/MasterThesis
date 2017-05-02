﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets.scripts {
    public class FillGridWithLocations : MonoBehaviour {
        private GameObject grid;
        private StateManagerContainer manager;

        public GameObject LevelTemplate;

        // Use this for initialization
        public void Start() {
            grid = GameObject.FindGameObjectWithTag("LevelGrid");
            Assert.IsNotNull(LevelTemplate);
            manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();
            FillGrid(manager.manager.Locations);
        }

        private bool IsOtherPlayerAtThisLocation(location location) {
            return manager.manager.Player2.CurrentLocation.Equals(location);
        }

        private void FillGrid(IEnumerable<location> locations) {
            foreach (var location in locations) {
                if (!manager.manager.Player1.HasPre(location.Pre)) {
                    continue;
                }
                var buttonInstance = Instantiate(LevelTemplate);
                var texts = buttonInstance.GetComponentsInChildren<Text>();
                texts[0].text = location.Name.Value;
                if (IsOtherPlayerAtThisLocation(location)) {
                    texts[1].enabled = true;
                }
                var button = buttonInstance.GetComponentInChildren<Button>();
                var locationCopy = location;
                button.onClick.AddListener(delegate { manager.Goto(locationCopy); });

                buttonInstance.transform.parent = grid.transform;
            }
        }
    }
}
