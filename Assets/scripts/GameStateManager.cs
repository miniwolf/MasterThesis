using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.scripts {
    public class GameStateManager {
        private int idx;

        public Player Player1 { get; } = new Player();
        public Player Player2 { get; } = new Player();
        public List<location> Locations { get; } = new List<location>();
        public List<Quest> PossibleQuests { get; set; } = new List<Quest>();
        public List<choicesChoice> PossibleChoices { get; set; } = new List<choicesChoice>();

        public GameStateManager() {
            Player1.Manager = this;
            Player2.Manager = this;
            Locations.Add(Load("C:/Users/miniwolf/Documents/MasterThesis/Assets/story/MagicianQuaters.xml"));
            Locations.Add(Load("C:/Users/miniwolf/Documents/MasterThesis/Assets/story/Brothel.xml"));
            Locations.Add(Load("C:/Users/miniwolf/Documents/MasterThesis/Assets/story/Temple.xml"));
        }

        public static location Load(string fileName) {
            var serializer = new XmlSerializer(typeof(location));
            return (location) serializer.Deserialize(new XmlTextReader(fileName));
        }
    }
}
