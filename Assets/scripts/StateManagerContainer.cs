using Assets.Network.Client;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class StateManagerContainer : MonoBehaviour {
        public GameStateManager manager = new GameStateManager();
        public Client client;

        private void Start() {
            DontDestroyOnLoad(gameObject.transform);
        }

        public void Goto(location location) {
            if (!manager.Player1.Goto(location)) {
                return;
            }

            client.Communication.SendObject(location);
            SceneManager.LoadScene("scenes/Quest");
        }

        public void StartQuest(Quest quest) {
            manager.Player1.StartQuest(quest);
            client.Communication.SendObject(quest);
            SceneManager.LoadScene("scenes/Choice");
        }

        public void Choose(choicesChoice choiceCopy) {
            manager.Player1.Choose(choiceCopy);
            client.Communication.SendObject(choiceCopy);
            SceneManager.LoadScene(manager.Player1.CurrentQuest == null
                ? "scenes/Quest"
                : "scenes/Choice");
        }

        public void BackToLocations() {
            SceneManager.LoadScene("scenes/Locations");
        }
    }
}