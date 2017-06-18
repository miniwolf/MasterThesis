using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts {
    public class NpcFiller : MonoBehaviour {
        private GameObject grid;
        private StateManagerContainer manager;
        public GameObject NpcsTemplate;

        public void Start() {
            grid = GameObject.FindGameObjectWithTag("LevelGrid");
            manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();
            FillGrid(manager.manager.Npcs);
        }

        private void FillGrid(IEnumerable<string> npcs) {
            foreach (var npc in npcs) {
                var npcInstance = Instantiate(NpcsTemplate);
                var texts = npcInstance.GetComponentsInChildren<Text>(true);
                texts[0].text = npc;
				foreach (Transform transform in npcInstance.transform) {
					if (transform.name.Equals(npc)) {
						transform.gameObject.SetActive(true);
					}
				}
                var button = npcInstance.GetComponentInChildren<Button>();
                var npcCopy = npc;
                button.onClick.AddListener(delegate { manager.TalkTo(npcCopy); });
                if (manager.IsOtherPlayerTalkingTo(npc)) {
                    texts[2].text = "Player is Here";
                    texts[2].enabled = true;
                }

                npcInstance.transform.SetParent(grid.transform);
            }
        }
    }
}