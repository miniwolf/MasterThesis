using Assets.Network.Client;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class StateManagerContainer : MonoBehaviour {
        public GameStateManager manager = new GameStateManager();
        public Client client;

        private void Start() {
            DontDestroyOnLoad(gameObject.transform);
            client = FindObjectOfType<Client>();
        }

        public bool IsOtherPlayerAtThisLocation(Location location) {
            return manager.Player2.CurrentLocation.Name.Value.Equals(location.Name.Value);
        }

        public void Goto(Location location) {
            if (!manager.Player1.Goto(location)) {
                return;
            }
            manager.IsGrouped = false;
            client.Communication.SendObject(location);
            SceneManager.LoadScene(location == null ? "scenes/Locations" : "scenes/Npcs");
        }

        public void StartQuest(Quest quest) {
            manager.Player1.StartQuest(quest);
            client.Communication.SendObject(quest);
            if (manager.IsGrouped) {
                // Wait until other person has chosen before move on, could be a notification system or it could
                // be a while. A while could lock the system, which
                return;
            }
            SceneManager.LoadScene("scenes/Choice");
        }

        public void Choose(ChoicesChoice choiceCopy) {
            manager.Player1.Choose(choiceCopy);
            client.Communication.SendObject(choiceCopy);
            SceneManager.LoadScene(manager.Player1.CurrentQuest == null
                ? "scenes/Quest"
                : "scenes/Choice");
        }

        public void BackToLocations() {
            SceneManager.LoadScene("scenes/Locations");
        }

        public void TalkTo(Npc npc) {
            manager.Player1.TalkTo(npc);
        }
    }
}