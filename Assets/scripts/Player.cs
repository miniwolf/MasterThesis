using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        public Choice HasChosen { get; set; }
        public string TalkingTo { get; set; }
        public string ClassString { get; set; }

        public Player() {
            ClassString = "";
        }

        public bool Goto(Location location, bool overridePres) {
            if (!overridePres && location.Pres != null && !HasPre(location.Pres)) {
                return false;
            }
            if (CurrentQuest != null) {
                CurrentQuest = null;
            }

            // Reset
            Manager.PossibleChoices.Clear();
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
            var list = new List<Choice>();
            foreach (var choiceString in choiceStrings) {
                foreach (var choice in CurrentLocation.Choices.Choice) {
                    Debug.Log(choice);
                }
                var chosen = CurrentLocation.Choices.Choice.FirstOrDefault(choice => choice.Name == choiceString);
                if (chosen == null) {
                    continue;
                }
                if (HasPre(chosen.Pres)) {
                    list.Add(chosen);
                }
            }
            return list;
        }

        public void Choose(Choice choice) {
            var results = choice.Results;

            if (results.EndQuest != null) { // Will not do anything basically back button.
                if (CurrentQuest.GetType() == typeof(OneshotQuest) &&
                    CollectChoices(CurrentQuest.Results.ChoicesResults.Choice).Count() == 1) {
                    CurrentLocation.Quests.OneshotQuest = CurrentLocation.Quests.OneshotQuest
                        .Where(q => q.Name != CurrentQuest.Name)
                        .ToList();
                }
                CurrentQuest = null;
                Goto(CurrentLocation, false);
                return;
            }

            CurrentLocation.Choices.Choice = CurrentLocation.Choices.Choice
                .Where(c => c.Name != choice.Name)
                .ToList();

            if (results.EffectResults != null) {
                AddEffects(results.EffectResults.Effect);
            }

            if (results.LocationResults != null) {
                KnownLocation.Add(results.LocationResults.Location);
            }

            Manager.PossibleChoices.Remove(choice);
            if (results.ChoicesResults == null) {
                GameStateManager.UpdateChoiceUI();
                return;
            }

            foreach (var c in results.ChoicesResults.Choice) {
                var foundChoice = FindChoice(c);
                if (foundChoice == null || !HasPre(foundChoice.Pres)) {
                    continue;
                }
                Manager.PossibleChoices.Add(foundChoice);
            }
            GameStateManager.UpdateChoiceUI();
        }

        private void AddEffects(IEnumerable<string> effects) {
            foreach (var effect in effects) {
                if (State.Contains(effect)) {
                    continue;
                }
                if (effect.Contains("Angry") && State.Contains(effect.Replace("Angry", "Happy"))) {
                    State.Remove(effect.Replace("Angry", "Happy"));
                } else if (effect.Contains("Happy") && State.Contains(effect.Replace("Happy", "Angry"))) {
                    State.Remove(effect.Replace("Happy", "Angry"));
                } else if (effect.Contains("!") && State.Contains(effect.Replace("!", ""))) {
                    State.Remove(effect.Replace("!", ""));
                    continue;
                }
                State.Add(effect);
            }
        }

        public bool HasPre(Pres pres) {
            if (pres == null) {
                return true;
            }

            if (pres.Effect != null
                && !pres.Effect.All(has => has.Contains("!")
                    ? !State.Contains(has.Substring(1))
                    : State.Contains(has))) {
                return false;
            }

            if (pres.KnowsLocation != null 
                && (pres.KnowsLocation.Contains("!") 
                    ? KnownLocation.Contains(pres.KnowsLocation.Substring(1))
                    : !KnownLocation.Contains(pres.KnowsLocation))) {
                return false;
            }

            if (pres.Present != null && !pres.Present.All(has => ClassString.Equals(has))
                || pres.Class != null && !pres.Class.Equals(ClassString)) {
                return false;
            }
            return pres.Global == null ||
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

        public void Reset() {
            CurrentLocation = null;
            CurrentQuest = null;
        }
    }
}