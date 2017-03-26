using System.Collections.Generic;
using System.Linq;

namespace Assets.scripts {
    public class Player {
        private location currentLocation = new location();

        public location CurrentLocation {
            get { return currentLocation; }
            set { currentLocation = value; }
        }

        private List<string> state = new List<string>();

        public List<string> State { get { return state; } set { state = value; } }

        private List<string> knownLocation = new List<string>();

        public List<string> KnownLocation {
            get { return knownLocation; }
            set { knownLocation = value; }
        }

        public Quest CurrentQuest { get; set; }
        public GameStateManager Manager { get; set; }

        public bool Goto(location location) {
            if (CurrentQuest != null) {
                return false;
            }
            if (location.Pre != null && !HasPre(location.Pre)) {
                return false;
            }
            if (location.Quests != null) {
                Manager.PossibleQuests = CollectQuests(location.Quests);
            }
            CurrentLocation = location;
            return true;
        }

        private List<Quest> CollectQuests(locationQuests locationQuests) {
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
            Manager.PossibleChoices.AddRange(quest.Choices.choice);
        }

        public void Choose(choicesChoice choice) {
            var realChoice = FindChoice(choice.name);
            var results = realChoice.results;

            if (realChoice.GetType() == typeof(choicesOnceChoice)) {
                var location = Manager.Locations.First(loc => loc == CurrentLocation);
                location.Choices.onceChoice = location.Choices.onceChoice
                    .Where(c => c.Name.Value != choice.name)
                    .ToArray();
                CurrentLocation = location;
            }

            if (results.effectResults != null) {
                State.AddRange(results.effectResults.Effect.Select(effect => effect.value));
            }

            if (results.locationResults != null) {
                KnownLocation.AddRange(results.locationResults.Select(location => location.Value));
            }

            if (results.choicesResults == null) {
                if (CurrentQuest.GetType() == typeof(locationQuestsOneshotQuests)) {
                    var location = Manager.Locations.First(loc => loc == CurrentLocation);
                    location.Quests.OneshotQuest = location.Quests.OneshotQuest
                        .Where(q => q.Name.Value != CurrentQuest.Name.Value)
                        .ToArray();
                }
                CurrentQuest = null;
                return;
            }

            Manager.PossibleChoices.Clear();
            foreach (var c in results.choicesResults.choice) {
                var foundChoice = FindChoice(c.name);
                if (foundChoice != null && HasPre(foundChoice.Pres)) {
                    Manager.PossibleChoices.Add(c);
                }
            }
        }

        private bool HasPre(pre Pres) {
            //foreach (var at in choice.Pres.At)
            if (Pres != null && Pres.Has != null
                && !Pres.Has.All(has => State.Contains(has.value))) {
                return false;
            }
            if (Pres != null && Pres.KnowsLocations != null
                && !Pres.KnowsLocations.All(knows => KnownLocation.Contains(knows.value))) {
                return false;
            }
            if (Pres != null && Pres.global != null
                && !Pres.global.All(gHas => Manager.HasPre(gHas))) {
                return false;
            }
            return true;
        }

        private Choice FindChoice(string choiceName) {
            foreach (var choice in CurrentLocation.Choices.repeatChoice) {
                if (choice.Name.Value.Equals(choiceName)) {
                    return choice;
                }
            }
            return CurrentLocation.Choices.onceChoice.FirstOrDefault(choice =>
                choice.Name.Value.Equals(choiceName));
        }
    }
}