using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets.scripts {
    public class FillGridWithLocations : MonoBehaviour {
        private GameObject grid;
        private StateManagerContainer manager;

        public GameObject ButtonTemplate;

        // Use this for initialization
        public void Start() {
            grid = GameObject.FindGameObjectWithTag("LevelGrid");
            Assert.IsNotNull(ButtonTemplate);
            manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();;
            FillGrid(manager.manager.Locations);
        }

        private void FillGrid(IEnumerable<location> locations) {
            foreach (var location in locations) {
                var buttonInstance = Instantiate(ButtonTemplate);
                buttonInstance.GetComponentInChildren<Text>().text = location.Name.Value;
                var button = buttonInstance.GetComponent<Button>();
                var locationCopy = location;
                button.onClick.AddListener(delegate { manager.Goto(locationCopy); });

                buttonInstance.transform.parent = grid.transform;
            }
        }
    }
}
