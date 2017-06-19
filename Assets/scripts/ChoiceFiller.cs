using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xml2CSharp;

namespace Assets.scripts {
    public class ChoiceFiller : MonoBehaviour {
        private GameObject grid;
        private StateManagerContainer manager;
        private bool updateSelection;

        public GameObject ButtonTemplate;

        // Use this for initialization
        public void Start() {
            grid = GameObject.FindGameObjectWithTag("LevelGrid");
            manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();
            var NPCname = GameObject.FindGameObjectWithTag("NPCName").GetComponent<Text>();
            NPCname.text = manager.manager.Player1.CurrentLocation.Npcs.Npc[0];
            FillGrid(manager.manager.PossibleChoices);
            manager.manager.SetQuestDescription();
        }

        public void Update() {
            if (!updateSelection) {
                return;
            }
            ClearSelection();
            FillGrid(manager.manager.PossibleChoices);
            updateSelection = false;
        }

        public void UpdateSelection() {
            updateSelection = true;
        }

        public void ClearSelection() {
            foreach (Transform element in grid.transform) {
                Destroy(element.gameObject);
            }
        }

        private void FillGridWithItem(Choice choice) {
            var buttonInstance = Instantiate(ButtonTemplate);
            buttonInstance.GetComponentInChildren<Text>().text =
                choice.Description.Replace("\r\n", "").Trim();
            var button = buttonInstance.GetComponent<Button>();
            var choiceCopy = choice;
            button.onClick.AddListener(delegate { manager.Choose(choiceCopy); });

            buttonInstance.transform.SetParent(grid.transform);
        }

        private void FillGrid(IEnumerable<Choice> choices) {
            foreach (var choice in choices) {
                if (choice == null) {
                    continue; // TODO: Fucking fix this
                }
                FillGridWithItem(choice);
            }
        }
    }
}
