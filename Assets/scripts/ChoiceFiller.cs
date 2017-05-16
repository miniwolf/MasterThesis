using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts {
    public class ChoiceFiller : MonoBehaviour {
        private GameObject grid;
        private StateManagerContainer manager;

        public GameObject ButtonTemplate;

        // Use this for initialization
        public void Start() {
            grid = GameObject.FindGameObjectWithTag("LevelGrid");
            manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();;
            FillGrid(manager.manager.PossibleChoices);
        }

        private void FillGrid(IEnumerable<ChoicesChoice> choices) {
            foreach (var choice in choices) {
                if (choice == null) {
                    continue; // TODO: Fucking fix this
                }
                var buttonInstance = Instantiate(ButtonTemplate);
                buttonInstance.GetComponentInChildren<Text>().text = choice.name;
                var button = buttonInstance.GetComponent<Button>();
                var choiceCopy = choice;
                button.onClick.AddListener(delegate { manager.Choose(choiceCopy); });

                buttonInstance.transform.SetParent(grid.transform);
            }
        }
    }
}
