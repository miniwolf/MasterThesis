using System.Collections.Generic;
using System.Linq;

namespace Assets.scripts {
    public class Player {
        public location CurrentLocation { get; set; } = new location();
        public Quest CurrentQuest { get; set; }
        public GameStateManager Manager { get; set; }
        public List<string> State { get; set; } = new List<string>();
        public List<string> KnownLocation { get; set; } = new List<string>();

        public void Goto(location location) {
            if (CurrentQuest != null) {
                return;
            }
            if (location.Name.value.Equals("Temple")) {
                return;
            }
            if (location.Quests != null) {
                Manager.PossibleQuests = CollectQuests(location.Quests);
            }
            CurrentLocation = location;
        }

        private static List<Quest> CollectQuests(locationQuests locationQuests) {
            var list = new List<Quest>();
            if (locationQuests.OneshotQuest != null)
                list.AddRange(locationQuests.OneshotQuest);
            if (locationQuests.RandomQuest != null)
                list.AddRange(locationQuests.RandomQuest);
            if (locationQuests.RepeatableQuest != null)
                list.AddRange(locationQuests.RepeatableQuest);

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
                    .Where(c => c.name.value != choice.name)
                    .ToArray();
                CurrentLocation = location;
            }

            if (results.effectResults != null) {
                State.AddRange(results.effectResults.Effect.Select(effect => effect.value));
            }

            if (results.locationResults != null) {
                KnownLocation.AddRange(results.locationResults.Select(location => location.value));
            }

            if (results.choicesResults == null) {
                CurrentQuest = null;
                return;
            }

            Manager.PossibleChoices.Clear();
            foreach (var c in results.choicesResults.choice) {
                var foundChoice = FindChoice(c.name);
                if (foundChoice != null && HasPre(foundChoice)) {
                    Manager.PossibleChoices.Add(c);
                }
            }
        }

        private bool HasPre(Choice choice) {
            var pres = choice.Pres;
            //foreach (var at in choice.Pres.At)
            if (choice.Pres?.Has != null
                && !choice.Pres.Has.All(has => State.Contains(has.value))) {
                return false;
            }
            if (choice.Pres?.KnowsLocations != null
                && !choice.Pres.KnowsLocations.All(knows => KnownLocation.Contains(knows.value))) {
                return false;
            }
            return true;
        }

        private Choice FindChoice(string choiceName) {
            foreach (var choice in CurrentLocation.Choices.repeatChoice) {
                if (choice.name.value.Equals(choiceName)) {
                    return choice;
                }
            }
            return CurrentLocation.Choices.onceChoice.FirstOrDefault(choice =>
                choice.name.value.Equals(choiceName));
        }
    }
}