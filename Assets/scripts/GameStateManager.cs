using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

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

        private List<ChoicesChoice> possibleChoices = new List<ChoicesChoice>();
        public List<ChoicesChoice> PossibleChoices {
            get { return possibleChoices; }
            set { possibleChoices = value; }
        }

        private List<Has> globalHas = new List<Has>();
        public List<Has> GlobalHas {
            get { return globalHas; }
            set { globalHas = value; }
        }

        public List<Npc> Npcs { get; set; }

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

        public bool HasPre(global gHas) {
            return gHas.Has.All(has => has.value.Contains("!")
                ? !GlobalHas.Contains(new Has {value = has.value.Substring(1)})
                : GlobalHas.Contains(has));
        }

        public void Goto(Location location) {
            if (IsGrouped) {
                return;
            }

            if (location != null && Player2.CurrentLocation != null
                && location.Name.Value.Equals(Player2.CurrentLocation.Name.Value)) {
                IsGrouped = true;
            }
        }
    }
}
