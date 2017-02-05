using System;
using System.Linq;
using System.Xml;
using Assets.scripts;
using Assets.scripts.xml;
using Xunit;

namespace Test {
    public class Tests {
        private StoryContainer storyContainer;

        public Tests() {
            storyContainer = GameStateManager.Load("C:/Users/miniwolf/Documents/Prototype1Master/Assets/story/story.xml");
        }

        [Fact]
        public void TestGettingEncounter1() {
            Assert.True(storyContainer.Items.Length > 0);
        }

        [Fact]
        public void GettingDescriptionOfEncounter() {
            var textDescription = storyContainer.Items[0].Text.description;
            Assert.True(textDescription.StartsWith("You see a man"));
        }

        [Fact]
        public void GettingChoiceDescriptionsFromFirstEncounter() {
            var encounter = storyContainer.Items[0];
            Assert.True(encounter.Choices[0].description.StartsWith("Ignore"));
            Assert.True(encounter.Choices[1].description.StartsWith("Help"));
            Assert.True(encounter.Choices[2].description.StartsWith("Kill"));
        }

        [Fact]
        public void GettingChoicesEffectsFromFirstEncounter() {
            var encounter = storyContainer.Items[0];
            Assert.True(encounter.Choices[0].effect.StartsWith("R+"));
            Assert.True(encounter.Choices[1].effect.StartsWith("R-"));
            Assert.True(encounter.Choices[2].effect.StartsWith("R++"));
        }
    }
}