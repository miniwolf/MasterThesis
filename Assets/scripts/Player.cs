using System.Collections.Generic;
using System.Linq;

namespace Assets.scripts {
    public class Player {
        public location CurrentLocation { get; set; } = new location();
        public Quest CurrentQuest { get; set; }
        public GameStateManager Manager { get; set; }
        public List<string> State { get; set; } = new List<string>();

        public void Goto(location location) {
            if (CurrentQuest != null) {
                return;
            }
            if (location.Name.value.Equals("Temple")) {
                return;
            }
            Manager.PossibleQuests = CollectQuests(location.Quests);
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

        public void Choose(choicesChoice c111) {
            var results = FindChoice(c111.name).results;
            if (results.effectResults == null) {
                return;
            }

            foreach (var effect in results.effectResults.Effect) {
                State.Add(effect.value);
            }

            if (results.choicesResults == null) {
                return;
            }

            Manager.PossibleChoices.Clear();
            Manager.PossibleChoices.AddRange(results.choicesResults.choice);
        }

        private Choice FindChoice(string c111Name) {
            foreach (var choice in CurrentLocation.Choices.repeatChoice) {
                if (choice.name.value.Equals(c111Name)) {
                    return choice;
                }
            }
            return CurrentLocation.Choices.onceChoice.FirstOrDefault(choice =>
                choice.name.value.Equals(c111Name));
        }
    }
}