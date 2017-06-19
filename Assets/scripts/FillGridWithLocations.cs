﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xml2CSharp;

namespace Assets.scripts {
    public class FillGridWithLocations : MonoBehaviour {
        private GameObject grid;
        private StateManagerContainer manager;

        public GameObject LevelTemplate;

        // Use this for initialization
        public void Start() {
            grid = GameObject.FindGameObjectWithTag("LevelGrid");
            manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();
            FillGrid(manager.manager.Locations);
        }

        private void FillGrid(IEnumerable<Location> locations) {
            foreach (var location in locations) {
                if (!manager.manager.Player1.HasPre(location.Pres, true, false)) {
                    continue;
                }
                var buttonInstance = Instantiate(LevelTemplate);
                var texts = buttonInstance.GetComponentsInChildren<Text>(true);
                texts[0].text = location.Name;
                foreach (Transform child in buttonInstance.transform) {
                    if (child.name.Equals(location.Name)) {
                        child.gameObject.SetActive(true);
                    }
                }
                if (manager.IsOtherPlayerAtThisLocation(location)) {
                    texts[2].text = "Player is Here";
                    texts[2].enabled = true;
                }
                var button = buttonInstance.GetComponentInChildren<Button>();
                var locationCopy = location;
                button.onClick.AddListener(delegate { manager.Goto(locationCopy, false, false); });

                buttonInstance.transform.SetParent(grid.transform);
            }
        }
    }
}
