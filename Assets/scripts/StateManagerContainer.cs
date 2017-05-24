using Assets.Network.Client;
using Assets.Network.Shared;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class StateManagerContainer : MonoBehaviour {
        public readonly GameStateManager manager = new GameStateManager();
        public Client client;
        private string gotoPosition = "";

        private void Start() {
            DontDestroyOnLoad(gameObject.transform);
            client = FindObjectOfType<Client>();
        }

        private void Update() {
            if (gotoPosition.Length == 0) {
                return;
            }
            SceneManager.LoadScene(gotoPosition);
            gotoPosition = "";
        }

        public bool IsOtherPlayerAtThisLocation(Location location) {
            return manager.Player2.CurrentLocation != null &&
                   manager.Player2.CurrentLocation.Name.Value.Equals(location.Name.Value);
        }

        public bool IsOtherPlayerTalkingTo(Npc npc) {
            return manager.Player2.TalkingTo != null &&
                   npc.Name.Equals(manager.Player2.TalkingTo.Name);
        }

        public void Goto(Location location) {
            if (!manager.Player1.Goto(location)) {
                return;
            }
            manager.Goto(location);

            HandleAction(location, location == null ? "scenes/Locations" : "scenes/Npcs");
        }

        private void HandleAction(object obj, string scene) {
            client.Communication.SendObject(obj);
            if (!(client.Communication.GetNextResponse() is AllIsWell)) {
                return;
            }

            if (manager.IsGrouped && !manager.WaitingForResponse) {
                manager.WaitingForResponse = true;
                return;
            }
            
            Debug.Log("Choose");
            manager.WaitingForResponse = false;
            gotoPosition = scene;
        }

        public void StartQuest(Quest quest) {
            manager.Player1.StartQuest(quest);
            HandleAction(quest, "scenes/Choice");
        }
        

        public void Choose(ChoicesChoice choiceCopy) {
            manager.Player1.Choose(choiceCopy);
            
            HandleAction(choiceCopy, manager.Player1.CurrentQuest == null 
                ? "scenes/Quest"
                : "scenes/Choice");
        }

        public void TalkTo(Npc npc) {
            manager.Player1.TalkTo(npc);
            HandleAction(new TalkingTo(npc), "scenes/Quest");
        }

        public void BackToLocations() {
            SceneManager.LoadScene("scenes/Locations");
        }
    }
}
