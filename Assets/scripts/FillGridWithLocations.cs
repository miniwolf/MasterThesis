using System.Collections.Generic;
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
            manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
            FillGrid(manager.manager.Locations);
        }

        private void FillGrid(IEnumerable<Location> locations) {
            foreach (var location in locations) {
                if (!manager.manager.Player1.HasPre(location.Pre)) {
                    continue;
                }
                var buttonInstance = Instantiate(LevelTemplate);
                var texts = buttonInstance.GetComponentsInChildren<Text>(true);
                texts[0].text = location.Name.Value;
                if (manager.IsOtherPlayerAtThisLocation(location)) {
                    texts[2].text = "Player is Here";
                    texts[2].enabled = true;
                }
                var button = buttonInstance.GetComponentInChildren<Button>();
                var locationCopy = location;
                button.onClick.AddListener(delegate { manager.Goto(locationCopy); });

                buttonInstance.transform.parent = grid.transform;
            }
        }
    }
}