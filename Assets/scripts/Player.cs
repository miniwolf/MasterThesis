using System.Collections.Generic;
using System.Linq;
using Xml2CSharp;

namespace Assets.scripts {
    public class Player {
        public Location CurrentLocation { get; set; }

        private List<string> state = new List<string>();

        public List<string> State {
            get { return state; }
            set { state = value; }
        }

        private List<string> knownLocation = new List<string>();

        public List<string> KnownLocation {
            get { return knownLocation; }
            set { knownLocation = value; }
        }

        public Quest CurrentQuest { get; set; }
        public GameStateManager Manager { get; set; }
        public string TalkingTo { get; set; }

        public bool Goto(Location location) {
            if (location.Pres != null && !HasPre(location.Pres)) {
                return false;
            }
            if (CurrentQuest != null) {
                CurrentQuest = null;
            }
            Manager.PossibleQuests = new List<Quest>();
            if (location.Quests != null) {
                // This might happen if misread from parser
                Manager.PossibleQuests = CollectQuests(location.Quests);
            }
            CurrentLocation = location;
            Manager.Npcs = location.Npcs.Npc;
            return true;
        }

        private List<Quest> CollectQuests(Quests locationQuests) {
            var list = new List<Quest>();
            if (locationQuests.OneshotQuest != null) {
                var ie = locationQuests.OneshotQuest.Where(quest => HasPre(quest.Pres));
                list.AddRange(ie.Cast<Quest>());
            }
            if (locationQuests.RandomQuest != null) {
                var ie = locationQuests.RandomQuest.Where(quest => HasPre(quest.Pres));
                list.AddRange(ie.Cast<Quest>());
            }
            if (locationQuests.RepeatableQuest != null) {
                var ie = locationQuests.RepeatableQuest.Where(quest => HasPre(quest.Pres));
                list.AddRange(ie.Cast<Quest>());
            }
            return list;
        }

        public void StartQuest(Quest quest) {
            if (CurrentQuest != null) {
                return;
            }
            CurrentQuest = quest;
            Manager.PossibleChoices.Clear();
            Manager.PossibleChoices.AddRange(CollectChoices(quest.Results.ChoicesResults.Choice));
        }

        private IEnumerable<Choice> CollectChoices(IEnumerable<string> choiceStrings) {
            return choiceStrings.Select(choiceString => 
                CurrentLocation.Choices.Choice.First(choice => 
                    choice.Name == choiceString)).ToList();
        }

        public void Choose(Choice choice) {
            var realChoice = FindChoice(choice.Name);
            var results = realChoice.Results;

            var location = Manager.Locations.First(loc => Equals(loc, CurrentLocation));
            location.Choices.Choice = location.Choices.Choice
                .Where(c => c.Name != choice.Name)
                .ToList();
            CurrentLocation = location;

            if (results.EffectResults != null) {
                State.AddRange(results.EffectResults.Effect);
            }

            if (results.LocationResults != null) {
                KnownLocation.Add(results.LocationResults.Location);
            }

            if (results.ChoicesResults == null) {
                if (CurrentQuest.GetType() == typeof(OneshotQuest)) {
                    location.Quests.OneshotQuest = location.Quests.OneshotQuest
                        .Where(q => q.Name != CurrentQuest.Name)
                        .ToList();
                }
                CurrentQuest = null;
                Goto(CurrentLocation);
                return;
            }

            Manager.PossibleChoices.Clear();
            foreach (var c in results.ChoicesResults.Choice) {
                var foundChoice = FindChoice(c);
                if (foundChoice != null && HasPre(foundChoice.Pres)) {
                    Manager.PossibleChoices.Add(foundChoice);
                }
            }
        }

        public bool HasPre(Pres pres) {
            //foreach (var at in choice.Pres.At)
            if (pres != null && pres.Effect != null
                && !pres.Effect.All(has => has.Contains("!")
                    ? !State.Contains(has.Substring(1))
                    : State.Contains(has))) {
                return false;
            }
            if (pres != null && pres.KnowsLocation != null
                && !KnownLocation.Contains(pres.KnowsLocation)) {
                return false;
            }
            return pres == null || pres.Global == null ||
                   Manager.HasPre(pres.Global);
        }

        private Choice FindChoice(string choiceName) {
            foreach (var choice in CurrentLocation.Choices.Choice) {
                if (choice.Name.Equals(choiceName)) {
                    return choice;
                }
            }
            return CurrentLocation.Choices.Choice.FirstOrDefault(choice =>
                choice.Name.Equals(choiceName));
        }

        public void TalkTo(string npc) {
        }
    }
}