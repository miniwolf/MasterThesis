using System.Collections.Generic;
using Assets.Network.Client;
using Assets.Network.Shared;
using Assets.Network.Shared.Actions;
using Assets.Network.Shared.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xml2CSharp;
using AssemblyCSharp;

namespace Assets.scripts {
    public class StateManagerContainer : MonoBehaviour {
        public readonly GameStateManager manager = new GameStateManager();
        public Client client;
        private string gotoPosition = "";
		public static Queue<DialogueWrapper> TextToBoxListInChoiceScene = new Queue<DialogueWrapper>();

		private static Color coloOne = new Color(1f, 0.6f, 0.6f);
		private static Color coloTwo = new Color(0.7f, 0.7f, 1f);
		private static Color coloThree = new Color(0.65f, 1f, 0.65f);

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

		private static void AddTextBoxToListInChoiceScene(DialogueWrapper text) {
            var textBox = GameObject.FindGameObjectWithTag("Description");
            var template = textBox.transform.parent.GetChild(1);
            var templateCopy = Instantiate(template.transform);
			templateCopy.GetComponent<Text>().text = text.Description;
			switch(text.Who) {
			case ClassChoice.Me:
				templateCopy.GetComponent<Text>().color = coloOne;
				break;
			case ClassChoice.You:
				templateCopy.GetComponent<Text>().color = coloTwo;
				break;
			case ClassChoice.NPC:
				templateCopy.GetComponent<Text>().color = coloThree;
				break;
			}
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

        public void Goto(Location location, bool overridePres, bool overrideCoop) {
            if (!manager.Player1.Goto(location, overridePres, overrideCoop)) {
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
                if (manager.IsGrouped && manager.Player2.HasChosen != null && !manager.Player2.HasChosen.Name.Equals(choiceCopy.Name)) {
                    if (int.Parse(manager.Player1.HasChosen.Results.Description.Priority) <
                        int.Parse(manager.Player2.HasChosen.Results.Description.Priority)) {
                        Print(manager.Player1.HasChosen, true, manager.Player2.HasChosen, false);
                    } else {
                        Print(manager.Player2.HasChosen, false, manager.Player1.HasChosen, true);
                    }
                } else {
                    manager.AddChoiceDescriptionToUI(choiceCopy, true);
                }
                if (manager.Player2.HasChosen != null && !manager.Player2.HasChosen.Name.Equals(choiceCopy.Name)) {
                    manager.AddGlobalPres(manager.Player2.HasChosen);
                }
            }
        }
        
        private void Print(Choice first, bool firstBool, Choice second, bool secondBool) {
            manager.AddChoiceDescriptionToUI(first, firstBool);
            manager.AddChoiceDescriptionToUI(second, secondBool);
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
