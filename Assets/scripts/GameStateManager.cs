using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Xml2CSharp;

namespace Assets.scripts {
    public class GameStateManager {
        private int idx;
        private readonly Player player1 = new Player();
        private readonly Player player2 = new Player();

        public Player Player1 { get { return player1; } }
        public Player Player2 { get { return player2; } }

        private readonly List<Location> locations = new List<Location>();
        public List<Location> Locations { get { return locations; } }

        private List<Quest> possibleQuests = new List<Quest>();
        public List<Quest> PossibleQuests {
            get { return possibleQuests; }
            set { possibleQuests = value; }
        }

        private List<Choice> possibleChoices = new List<Choice>();
        public List<Choice> PossibleChoices {
            get { return possibleChoices; }
            set { possibleChoices = value; }
        }

        private List<string> globalHas = new List<string>();
        public List<string> GlobalHas {
            get { return globalHas; }
            set { globalHas = value; }
        }

        public List<string> Npcs { get; set; }

        public bool IsGrouped { get; set; }
        public bool WaitingForResponse { get; set; }

        public GameStateManager() {
            Player1.Manager = this;
            Player2.Manager = this;
            Locations.Add(Load("Assets/story/MagicianQuaters.xml"));
            Locations.Add(Load("Assets/story/Brothel.xml"));
            Locations.Add(Load("Assets/story/Temple.xml"));
        }

        public static Location Load(string fileName) {
            var serializer = new XmlSerializer(typeof(Location));
            return (Location) serializer.Deserialize(new XmlTextReader(fileName));
        }

        public bool HasPre(Global gHas) {
            return gHas.Has.Contains("!")
                ? !GlobalHas.Contains(gHas.Has.Substring(1))
                : GlobalHas.Contains(gHas.Has);
        }

        public void Goto(Location location) {
            if (IsGrouped) {
                return;
            }

            if (location != null && Player2.CurrentLocation != null
                && location.Name.Equals(Player2.CurrentLocation.Name)) {
                IsGrouped = true;
            }
        }

        private static void AddTextBoxToListInChoiceScene(string text) {
            var textBox = GameObject.FindGameObjectWithTag("Description");
            var template = textBox.transform.parent.GetChild(1);
            var templateCopy = Object.Instantiate(template.transform);
            templateCopy.GetComponent<Text>().text = text;
            templateCopy.parent = textBox.transform;
            templateCopy.gameObject.SetActive(true);
        }

        public static void AddChoiceDescriptionToUI(Choice choice) {
            AddTextBoxToListInChoiceScene(choice.Description.Replace("\r\n", "").Trim());
            if (choice.Results.Description != null) {
                AddTextBoxToListInChoiceScene(choice.Results.Description.Text.Replace("\r\n", "").Trim());
            }
        }

        public void SetQuestDescription() {
            AddTextBoxToListInChoiceScene(Player1.CurrentQuest.Dialogue.Replace("\r\n", "").Trim());
        }

        public static void UpdateChoiceUI() {
            GameObject.FindGameObjectWithTag("ChoiceFiller").GetComponent<ChoiceFiller>().UpdateSelection();
        }

        public void ResetPlayer() {
            Player1.Reset();
        }
    }
}
