    using System.Collections.Generic;
    using _Script.Characters.CharactersCards.Enum;
    using _Script.ConditionalEffects;
    using _Script.ConditionalEffects.Enum;
    using UnityEngine;

    public class CardActionSequence
    {
        public CharacterActionType CharacterActionType { get; set; }
        public int ActionRange { get; set; }
        public int ActionValue { get; set; }
        public int NumberOfTargets { get; set; }
        public string AnimProp { get; set; }
        public List<ApplicableConditions> Conditions { get; set; }
       
        
        public CardActionSequence(CharacterActionType characterActionType, int actionRange,int numberOfTargets, int actionValue, string animProp)
        {
            CharacterActionType = characterActionType;
            ActionRange = actionRange;
            NumberOfTargets = numberOfTargets;
            ActionValue = actionValue;
            AnimProp = animProp;
            Conditions = new List<ApplicableConditions>();
            
            
        }
        
    }
