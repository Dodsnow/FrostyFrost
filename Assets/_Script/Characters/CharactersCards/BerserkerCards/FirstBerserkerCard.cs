
    using System;
    using System.Collections.Generic;
    using _Script.ConditionalEffects;
    using _Script.ConditionalEffects.Enum;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class FirstBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        

        public FirstBerserkerCard()
        {
            cardName = "First Berserker Card";
            initiative = 20;
            TopCardAction = new CardAction(CardDiscardActionType.Discard, "Attack 3", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,3,"AttackChop", new List<ApplicableConditions>(){ApplicableConditions.Bleed});
            BottomCardAction = new CardAction(CardDiscardActionType.Discard, "Move 10", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,10,0, "",null);
        }
        
        
    }
  
