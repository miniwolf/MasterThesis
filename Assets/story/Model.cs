﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5485
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Name {
    private string val = "";

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute("value")]
    public string Value { get { return val; } set { val = value; } }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Npc {
    private string name = "";
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Name { get { return name; } set { name = value; } }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Pre {
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("At", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public preAT[] At { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Has")]
    public Has[] Has { get; set; }

    [System.Xml.Serialization.XmlElementAttribute("KnowsLocation")]
    public KnowsLocation[] KnowsLocations { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("global")]
    public global[] global { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class preAT {
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string location { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Has {
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class KnowsLocation {
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class global {
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Has")]
    public Has[] Has { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("effect")]
    public Effect[] effect { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Effect {
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Choices {
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("choice", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ChoicesChoice[] choice { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("repeatChoice", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ChoicesRepeatChoice[] repeatChoice { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("onceChoice", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ChoicesOnceChoice[] onceChoice { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class ChoicesChoice {
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name { get; set; }
}

[Serializable]
public class Choice {
    private Name name = new Name();
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("name")]
    public Name Name { get { return name; } set { name = value; } }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("pre")]
    public Pre Pres { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("results")]
    public Results results { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class ChoicesRepeatChoice : Choice {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class ChoicesOnceChoice : Choice {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Results {
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string wingame { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("effectResults", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ResultsEffectResults effectResults { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("name", typeof(Name), IsNullable=false)]
    public Name[] locationResults { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("choices")]
    public Choices choicesResults { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class ResultsEffectResults {
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("global")]
    public global[] global { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("effect")]
    public Effect[] Effect { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Location {
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("choices", typeof(Choices))]
    public Choices Choices { get; set; }

    [System.Xml.Serialization.XmlElementAttribute("pre", typeof(Pre))]
    public Pre Pre { get; set; }

    private Name name = new Name();

    [System.Xml.Serialization.XmlElementAttribute("name", typeof(Name))]
    public Name Name { get { return name; } set { name = value; } }

    [System.Xml.Serialization.XmlElementAttribute("npcs", typeof(LocationNpcs),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public LocationNpcs[] Npcs { get; set; }

    [System.Xml.Serialization.XmlElementAttribute("quests", typeof(locationQuests), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public locationQuests Quests { get; set; }

    public override bool Equals(object obj) {
        if (obj is Location) {
            var other = obj as Location;
            return other.name.Value.Equals(name.Value);
        }
        return false;
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class LocationNpcs {
    private Npc npc = new Npc();
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("npc")]
    public Npc Npc { get { return npc; } set { npc = value; } }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class locationQuests {
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("repeatableQuest", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public LocationQuestsRepeatableQuest[] RepeatableQuest { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("oneshotQuest", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public LocationQuestsOneshotQuests[] OneshotQuest { get; set; }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("randomQuest", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public LocationQuestsRandomQuests[] RandomQuest { get; set; }
}

[Serializable]
public class Quest {
    private Name name = new Name();
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("name")]
    public Name Name { get { return name; } set { name = value; } }
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("pre")]
    public Pre Pres { get; set; }
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("choices")]
    public Choices Choices { get; set; }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class LocationQuestsRepeatableQuest : Quest {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class LocationQuestsOneshotQuests : Quest {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class LocationQuestsRandomQuests : Quest {
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("npc", typeof(Npc), IsNullable=false)]
    public Npc[] Npc { get; set; }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Backgrounds {

    private BackgroundsBackground[] itemsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Background", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BackgroundsBackground[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class BackgroundsBackground {

    private string nameField;

    private string descriptionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string description {
        get {
            return this.descriptionField;
        }
        set {
            this.descriptionField = value;
        }
    }
}