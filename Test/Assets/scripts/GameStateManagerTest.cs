using System.Linq;
using Assets.scripts;
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
            Assert.Equal(3, manager.PossibleChoices.Count);
        }

        [Fact]
        public void ChoosingC1121WillGiveTempleLocation() {
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
    }
}