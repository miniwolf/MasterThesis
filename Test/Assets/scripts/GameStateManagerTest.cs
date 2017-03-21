using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using Assets.scripts;
using UnityEngine.Apple.ReplayKit;
using Xunit;

namespace Test.Assets.scripts {
    public class GameStateManagerTest {
        private readonly GameStateManager manager;

        public GameStateManagerTest() {
            manager = new GameStateManager();
        }

        [Fact]
        public void FirstEncounterHasIndex1() {
            Assert.True("".Equals(manager.Player1.CurrentLocation.Name.value));
        }

        [Fact]
        public void HasBrothelAsPossibleLocation() {
            var contains = manager.Locations.Any(location => "Brothel".Equals(location.Name.value));
            Assert.True(contains);
        }

        [Fact]
        public void PlayerCanGoToBrothel() {
            var brothel = manager.Locations[1];
            manager.Player1.Goto(brothel);
            Assert.Equal(brothel, manager.Player1.CurrentLocation);
        }

        [Fact]
        public void PlayerStartsWithoutQUest() {
            Assert.Null(manager.Player1.CurrentQuest);
        }

        [Fact]
        public void PlayerCannotGoToTemple() {
            var temple = manager.Locations[2];
            manager.Player1.Goto(temple);
            Assert.NotEqual(temple, manager.Player1.CurrentLocation);
        }

        [Fact]
        public void GoingToBrothelCanStartQ11() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            Assert.Contains(q11, manager.PossibleQuests);
        }

        [Fact]
        public void CannotLeaveBrothelWhileOnQuest() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            var magicQuater = manager.Locations[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            manager.Player1.Goto(magicQuater);
            Assert.Equal(brothel, manager.Player1.CurrentLocation);
        }

        [Fact]
        public void CannotStartAQuestWhileOnOneAlready() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            var q21 = manager.Locations[0].Quests.RandomQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            manager.Player1.StartQuest(q21);
            Assert.Equal(q11, manager.Player1.CurrentQuest);
        }

        [Fact]
        public void HasTwoChoicesOnQ11() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            Assert.Equal(2, manager.PossibleChoices.Count);
        }

        [Fact]
        public void Q11HasC111AsOneChoice() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            Assert.Equal("C1.1.1", manager.PossibleChoices[0].name);
        }

        [Fact]
        public void ChoosingC111WillGiveWenchHappy() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            var c111 = manager.PossibleChoices[0];
            manager.Player1.Choose(c111);
            Assert.Contains("Wench Happy", manager.Player1.State);
        }

        [Fact]
        public void ChoosingC111WillMakeItPossibleToLeaveBrothel() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            var c111 = manager.PossibleChoices[0];
            manager.Player1.Choose(c111);
            manager.Player1.Goto(new location());
            Assert.Equal("", manager.Player1.CurrentLocation.Name.value);
        }

        [Fact]
        public void ChoosingC112WillGiveWenchAngryAnd3NewChoices() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            var c112 = manager.PossibleChoices[1];
            manager.Player1.Choose(c112);
            Assert.Contains("Wench Angry", manager.Player1.State);
            Assert.Equal(1, manager.PossibleChoices.Count);
        }

        [Fact]
        public void ChoosingC1121WillGiveTempleLocation() {
            manager.Player1.State.Add("Wizard Angry");
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            var c112 = manager.PossibleChoices[1];
            manager.Player1.Choose(c112);
            var c1121 = manager.PossibleChoices[0];
            Assert.Equal("C1.1.2.1", c1121.name);
            manager.Player1.Choose(c1121);
            Assert.Contains("Temple", manager.Player1.KnownLocation);
        }

        [Fact]
        public void ChoosingC1122MakesItNotPossibleToChooseItAgain() {
            var brothel = manager.Locations[1];
            manager.Player1.State.Add("Wizard Angry");
            manager.Player1.KnownLocation.Add("Temple");
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            var c112 = manager.PossibleChoices[1];
            manager.Player1.Choose(c112);
            var c1122 = manager.PossibleChoices[1];
            Assert.Equal("C1.1.2.2", c1122.name);
            manager.Player1.Choose(c1122);

            manager.Player1.StartQuest(q11);
            c112 = manager.PossibleChoices[1];
            manager.Player1.Choose(c112);
            c1122 = manager.PossibleChoices[1];
            Assert.NotEqual("C1.1.2.2", c1122.name);
        }

        [Fact]
        public void CannotChooseC1122WithoutTempleLocation() {
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            var c112 = manager.PossibleChoices[1];
            manager.Player1.Choose(c112);
            Assert.DoesNotContain("C1.1.2.2", manager.PossibleChoices.Select(choice => choice.name));
        }

        [Fact]
        public void CanGoToTempleAfterReceivingKnownLocation() {
            manager.Player1.State.Add("Wizard Angry");
            var brothel = manager.Locations[1];
            var q11 = brothel.Quests.RepeatableQuest[0];
            manager.Player1.Goto(brothel);
            manager.Player1.StartQuest(q11);
            var c112 = manager.PossibleChoices[1];
            manager.Player1.Choose(c112);
            var c1121 = manager.PossibleChoices[0];
            Assert.Equal("C1.1.2.1", c1121.name);
            manager.Player1.Choose(c1121);
            Assert.Contains("Temple", manager.Player1.KnownLocation);

            var temple = manager.Locations[2];
            Assert.Equal("Temple", temple.Name.value);
            manager.Player1.Goto(temple);
            Assert.Equal("Temple", manager.Player1.CurrentLocation.Name.value);
        }

        [Fact]
        public void CannotTakeQ23WithoutRevealedWench() {
            var magicianQuaters = manager.Locations[0];
            var q23 = magicianQuaters.Quests.OneshotQuest[0];
            Assert.Equal("Q2.3", q23.Name.value);

            manager.Player1.Goto(magicianQuaters);
            var quest = manager.PossibleQuests.FirstOrDefault(q => q.Name.value.Equals("Q2.3"));
            Assert.Null(quest);
        }

        [Fact]
        public void CanOnlyTakeQ23MoreThanOneTime() {
            var magicianQuaters = manager.Locations[0];
            var q23 = magicianQuaters.Quests.OneshotQuest[0];
            Assert.Equal("Q2.3", q23.Name.value);

            manager.Player1.Goto(magicianQuaters);
            manager.Player1.StartQuest(q23);
            var c231 = manager.PossibleChoices[0];
            manager.Player1.Choose(c231);

            var nQ23 = magicianQuaters.Quests.OneshotQuest[0];
            Assert.NotEqual("Q2.3", nQ23.Name.value);
        }
    }
}