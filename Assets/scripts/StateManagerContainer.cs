using Assets.Network.Client;
using Assets.Network.Shared;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class StateManagerContainer : MonoBehaviour {
        public readonly GameStateManager manager = new GameStateManager();
        public Client client;
        private string GotoPosition;

        private void Start() {
            DontDestroyOnLoad(gameObject.transform);
            client = FindObjectOfType<Client>();
        }

        private void Update() {
            if (GotoPosition.Length == 0) {
                return;
            }
            SceneManager.LoadScene(GotoPosition);
            GotoPosition = "";
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

            client.Communication.SendObject(location);
            if (client.Communication.GetNextResponse() is AllIsWell) {
                SceneManager.LoadScene(location == null ? "scenes/Locations" : "scenes/Npcs");
            }
        }

        public void StartQuest(Quest quest) {
            manager.Player1.StartQuest(quest);
            client.Communication.SendObject(quest);
            if (!(client.Communication.GetNextResponse() is AllIsWell)) {
                return;
            }
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
            if (client.Communication.GetNextResponse() is AllIsWell) {
                SceneManager.LoadScene(manager.Player1.CurrentQuest == null
                    ? "scenes/Quest"
                    : "scenes/Choice");
            }
        }

        public void BackToLocations() {
            SceneManager.LoadScene("scenes/Locations");
        }

        public void TalkTo(Npc npc) {
            manager.Player1.TalkTo(npc);
            client.Communication.SendObject(new TalkingTo(npc));
            if (!(client.Communication.GetNextResponse() is AllIsWell)) {
                return;
            }

            if (manager.IsGrouped && !manager.WaitingForResponse) {
                manager.WaitingForResponse = true;
                return;
            }

            Debug.Log("Changing");
            manager.WaitingForResponse = false;
            GotoPosition = "scenes/Quest";
        }
    }
}
