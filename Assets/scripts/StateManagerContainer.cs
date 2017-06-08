using System.Collections.Generic;
using Assets.Network.Client;
using Assets.Network.Shared;
using Assets.Network.Shared.Actions;
using Assets.Network.Shared.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xml2CSharp;

namespace Assets.scripts {
    public class StateManagerContainer : MonoBehaviour {
        public readonly GameStateManager manager = new GameStateManager();
        public Client client;
        private string gotoPosition = "";
        public static Queue<string> TextToBoxListInChoiceScene = new Queue<string>();

        private void Start() {
            gotoPosition = "";
            DontDestroyOnLoad(gameObject.transform);
            client = FindObjectOfType<Client>();
        }

        private void OnDestroy() {
            client.Close();
        }

        private void Update() {
            if (GameStateManager.UpdateUI) {
                GameObject.FindGameObjectWithTag("ChoiceFiller").GetComponent<ChoiceFiller>().UpdateSelection();
                GameStateManager.UpdateUI = false;
            }
            if (TextToBoxListInChoiceScene.Count != 0) {
                lock (TextToBoxListInChoiceScene) {
                    AddTextBoxToListInChoiceScene(TextToBoxListInChoiceScene.Dequeue());
                }
            }
            if (gotoPosition.Length != 0) {
                SceneManager.LoadScene(gotoPosition);
                gotoPosition = "";
            }
        }

        private static void AddTextBoxToListInChoiceScene(string text) {
            var textBox = GameObject.FindGameObjectWithTag("Description");
            var template = textBox.transform.parent.GetChild(1);
            var templateCopy = Instantiate(template.transform);
            templateCopy.GetComponent<Text>().text = text;
            templateCopy.parent = textBox.transform;
            templateCopy.gameObject.SetActive(true);
        }

        public bool IsOtherPlayerAtThisLocation(Location location) {
            return manager.Player2.CurrentLocation != null &&
                   manager.Player2.CurrentLocation.Name.Equals(location.Name);
        }

        public bool IsOtherPlayerTalkingTo(string npc) {
            return manager.Player2.TalkingTo != null &&
                   npc.Equals(manager.Player2.TalkingTo);
        }

        public void Goto(Location location, bool overridePres) {
            if (!manager.Player1.Goto(location, overridePres)) {
                return;
            }
            manager.Goto(location);

            HandleAction(new GoingTo(location), location == null ? "scenes/Locations" : "scenes/Npcs");
        }

        private void HandleAction(InGoingMessages obj, string scene) {
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

        private bool HandleActionBoolean(object obj) {
            client.Communication.SendObject(obj);

            if (!(client.Communication.GetNextResponse() is AllIsWell)) {
                return false;
            }

            if (manager.IsGrouped && !manager.WaitingForResponse) {
                manager.WaitingForResponse = true;
                return false;
            }

            manager.WaitingForResponse = false;
            return true;
        }

        public void StartQuest(Quest quest) {
            manager.Player1.StartQuest(quest);
            HandleAction(new StartedQuest(quest), "scenes/Choice");
        }

        public void Choose(Choice choiceCopy) {
            manager.Player1.Choose(choiceCopy);
            var message = new HasChosen(choiceCopy);

            if (manager.Player1.CurrentQuest == null) {
                HandleAction(message, "scenes/Quest");
            } else if (HandleActionBoolean(message) && !manager.WaitingForResponse) {
                manager.AddChoiceDescriptionToUI(manager.Player2.HasChosen, false);
                manager.AddChoiceDescriptionToUI(choiceCopy, true);
                manager.AddGlobalPres(manager.Player2.HasChosen);
            }
        }

        public void TalkTo(string npc) {
            manager.Player1.TalkTo(npc);
            HandleAction(new TalkingTo(npc), "scenes/Quest");
        }

        public void BackToLocations() {
            HandleActionBoolean(new GoingTo(null));
            client.Communication.SendObject(new TalkingTo(null));
            client.Communication.GetNextResponse();
            client.Communication.SendObject(new StartedQuest(null));
            client.Communication.GetNextResponse();
            manager.Player1.Reset();
            manager.Goto(null);
            SceneManager.LoadScene("scenes/Locations");
        }

        public void Stay() {
            client.Communication.SendObject(new StayResponse());
            client.Communication.GetNextResponse();
        }
    }
}
