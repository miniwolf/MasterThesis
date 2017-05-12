using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts {
    public class NpcFiller : MonoBehaviour {
        private GameObject grid;
        private StateManagerContainer manager;
        public GameObject NpcsTemplate;

        public void Start() {
            grid = GameObject.FindGameObjectWithTag("PreGrid");
            manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();
            FillGrid(manager.manager.Npcs);
        }

        private void FillGrid(IEnumerable<Npc> npcs) {
            foreach (var npc in npcs) {
                var npcInstance = Instantiate(NpcsTemplate);
                var texts = npcInstance.GetComponentsInChildren<Text>();
                texts[0].text = npc.Name;

                var button = npcInstance.GetComponentInChildren<Button>();
                var npcCopy = npc;
                button.onClick.AddListener(delegate { manager.TalkTo(npcCopy); });

                npcInstance.transform.parent = grid.transform;
            }
        }
    }
}