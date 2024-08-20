
    using System;
    using System.Collections.Generic;
    using _Script.ConditionalEffects;
    using _Script.ConditionalEffects.Enum;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class SecondBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        

        public SecondBerserkerCard()
        {
            cardName = "Second Berserker Card";
            initiative = 12;
            TopCardAction = new CardAction(CardDiscardActionType.Discard, "Attack 1, Attack 2 " + ConditionTag.Bleed + ConditionTag.Stun, this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1, 1, "AttackChop", null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,2, "AttackSlice", new List<ApplicableConditions>(){ApplicableConditions.Bleed,ApplicableConditions.Stun});
            BottomCardAction = new CardAction(CardDiscardActionType.Discard,"Move 10, Heal 1", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,10,0,"",null);
            BottomCardAction.AddActionSequence(CharacterActionType.Heal,1,1,"",null);

        }
        
        
    }