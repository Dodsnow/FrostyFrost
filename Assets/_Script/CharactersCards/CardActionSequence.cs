    using UnityEngine;

    public class CardActionSequence
    {
        public CharacterActionType CharacterActionType { get; set; }
        public int ActionRange { get; set; }
        public int ActionValue { get; set; }
        public string AnimProp { get; set; }
        
        public CardActionSequence(CharacterActionType characterActionType, int actionRange, int actionValue,string animProp)
        {
            CharacterActionType = characterActionType;
            ActionRange = actionRange;
            ActionValue = actionValue;
            AnimProp = animProp;
        }
        
    }
