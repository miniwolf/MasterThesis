/* 
 Licensed under the Apache License, Version 2.0
 
 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
    [XmlRoot(ElementName="npcs")]
    [Serializable]
    public class Npcs {
        [XmlElement(ElementName="npc")]
        public List<string> Npc { get; set; }
    }

    [XmlRoot(ElementName="pres")]
    [Serializable]
    public class Pres {
        [XmlElement(ElementName="present")]
        public List<string> Present { get; set; }
        [XmlElement(ElementName="effect")]
        public List<string> Effect { get; set; }
        [XmlElement(ElementName="global")]
        public Global Global { get; set; }
        [XmlElement(ElementName="Coop")]
        public string Coop { get; set; }
        [XmlElement(ElementName="class")]
        public string Class { get; set; }
        [XmlElement(ElementName="KnowsLocation")]
        public string KnowsLocation { get; set; }
        [XmlElement(ElementName="results")]
        public Results Results { get; set; }
        [XmlElement(ElementName="pick")]
        public Pick Pick { get; set; }
    }

    [XmlRoot(ElementName = "pick")]
    [Serializable]
    public class Pick {
        [XmlElement(ElementName="class")]
        public string Class { get; set; }
    }

    [XmlRoot(ElementName="choices")]
    [Serializable]
    public class Choices {
        [XmlElement(ElementName="choice")]
        public List<Choice> Choice { get; set; }
    }

    public interface Quest {
        string Name { get; set; }
        Results Results { get; set; }
        string Description { get; set; }
        string Dialogue { get; set; }
    }

    [XmlRoot(ElementName="global")]
    [Serializable]
    public class Global {
        [XmlElement(ElementName="has")]
        public string Has { get; set; }
    }

    [XmlRoot(ElementName="oneshotQuest")]
    [Serializable]
    public class OneshotQuest : Quest {
        [XmlElement(ElementName="name")]
        public string Name { get; set; }
        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        [XmlElement(ElementName="pres")]
        public Pres Pres { get; set; }
        [XmlElement(ElementName="dialogue")]
        public string Dialogue { get; set; }
        [XmlElement(ElementName="results")]
        public Results Results { get; set; }
    }

    [XmlRoot(ElementName="extranpc")]
    [Serializable]
    public class Extranpc {
        [XmlElement(ElementName="npc")]
        public string Npc { get; set; }
    }

    [XmlRoot(ElementName="randomQuest")]
    [Serializable]
    public class RandomQuest : Quest {
        [XmlElement(ElementName="name")]
        public string Name { get; set; }
        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        [XmlElement(ElementName="extranpc")]
        public Extranpc Extranpc { get; set; }
        [XmlElement(ElementName="pres")]
        public Pres Pres { get; set; }
        [XmlElement(ElementName="dialogue")]
        public string Dialogue { get; set; }
        [XmlElement(ElementName="results")]
        public Results Results { get; set; }
    }

    [XmlRoot(ElementName="quests")]
    [Serializable]
    public class Quests {
        [XmlElement(ElementName="oneshotQuest")]
        public List<OneshotQuest> OneshotQuest { get; set; }
        [XmlElement(ElementName="randomQuest")]
        public List<RandomQuest> RandomQuest { get; set; }
    }

    [XmlRoot(ElementName="description")]
    [Serializable]
    public class Description {
        [XmlAttribute(AttributeName="priority")]
        public string Priority { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName="effectResults")]
    [Serializable]
    public class EffectResults {
        [XmlElement(ElementName="effect")]
        public List<string> Effect { get; set; }
        [XmlElement(ElementName="global")]
        public Global Global { get; set; }
    }

    [XmlRoot(ElementName="dialogueResult")]
    [Serializable]
    public class DialogueResult {
        [XmlElement(ElementName="dialogue")]
        public string Dialogue { get; set; }
    }

    [XmlRoot(ElementName="results")]
    [Serializable]
    public class Results {
        [XmlElement(ElementName="description")]
        public Description Description { get; set; }
        [XmlElement(ElementName="effectResults")]
        public EffectResults EffectResults { get; set; }
        [XmlElement(ElementName="dialogueResult")]
        public DialogueResult DialogueResult { get; set; }
        [XmlElement(ElementName="choicesResults")]
        public ChoicesResults ChoicesResults { get; set; }
        [XmlElement(ElementName="locationResults")]
        public LocationResults LocationResults { get; set; }
        [XmlElement(ElementName="wingame")]
        public string Wingame { get; set; }
        [XmlElement(ElementName="dialogue")]
        public List<Dialogue> Dialogue { get; set; }
        [XmlElement(ElementName="endQuest")]
        public string EndQuest { get; set; }
    }

    [XmlRoot(ElementName="choice")]
    [Serializable]
    public class Choice {
        [XmlElement(ElementName="name")]
        public string Name { get; set; }
        [XmlElement(ElementName="description")]
        public string Description { get; set; }
        [XmlElement(ElementName="pres")]
        public Pres Pres { get; set; }
        [XmlElement(ElementName="results")]
        public Results Results { get; set; }
    }

    [XmlRoot(ElementName="choicesResults")]
    [Serializable]
    public class ChoicesResults {
        [XmlElement(ElementName="choice")]
        public List<string> Choice { get; set; }
    }

    [XmlRoot(ElementName="locationResults")]
    [Serializable]
    public class LocationResults {
        [XmlElement(ElementName="location")]
        public string Location { get; set; }
    }

    [XmlRoot(ElementName="dialogue")]
    [Serializable]
    public class Dialogue {
        [XmlAttribute(AttributeName="class")]
        public string Class { get; set; }
        [XmlText]
        public string Text { get; set; }
        [XmlElement(ElementName="name")]
        public string Name { get; set; }
        [XmlElement(ElementName="pres")]
        public Pres Pres { get; set; }
        [XmlElement(ElementName="results")]
        public Results Results { get; set; }
    }

    [XmlRoot(ElementName="dialogues")]
    [Serializable]
    public class Dialogues {
        [XmlElement(ElementName="dialogue")]
        public List<Dialogue> Dialogue { get; set; }
    }

    [XmlRoot(ElementName="location")]
    [Serializable]
    public class Location {
        [XmlElement(ElementName="name")]
        public string Name { get; set; }
        [XmlElement(ElementName="npcs")]
        public Npcs Npcs { get; set; }
        [XmlElement(ElementName="pres")]
        public Pres Pres { get; set; }
        [XmlElement(ElementName="quests")]
        public Quests Quests { get; set; }
        [XmlElement(ElementName="choices")]
        public Choices Choices { get; set; }
        [XmlElement(ElementName="dialogues")]
        public Dialogues Dialogues { get; set; }
    }

}
