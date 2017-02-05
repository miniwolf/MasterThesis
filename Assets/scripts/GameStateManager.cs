using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.scripts {
    public class GameStateManager : MonoBehaviour {
        private int idx = 0;

        private void Start() {
            Next();
        }

        public void Next() {
            //var parser = new XMLParser();
            var content = Load("Assets/story/story.xml");
            //var LevelXML = parser.Parse(content);
            Debug.Log(content);
            idx++;
        }

        public static StoryContainer Load(string fileName) {
            var serializer = new XmlSerializer(typeof(StoryContainer));
            return (StoryContainer) serializer.Deserialize(new XmlTextReader(fileName));
        }
    }
}