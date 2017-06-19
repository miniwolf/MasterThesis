namespace Assets.scripts {
    public enum ClassChoice {
        Me,
        You,
        NPC
    }

    public class DialogueWrapper {
        public string Description { get; set; }
        public ClassChoice Who { get; set; }

        public DialogueWrapper(string description, ClassChoice who) {
            Description = description;
            Who = who;
        }
    }
}
