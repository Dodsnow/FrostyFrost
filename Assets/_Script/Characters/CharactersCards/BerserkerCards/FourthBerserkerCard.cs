
    using System;
    using System.Collections.Generic;
    using _Script.ConditionalEffects;
    using _Script.ConditionalEffects.Enum;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class FourthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        

        public FourthBerserkerCard()
        {
            cardName = "Fourth Berserker Card";
            initiative = 86;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,"Attack 3, Attack 1", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,3, "AttackSlice",new List<ApplicableConditions>(){ApplicableConditions.Bleed});
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,"AttackStab",null);
            BottomCardAction = new CardAction(CardDiscardActionType.Lost,"Move 1, Move 10", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,1,0, "",null);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,10,0, "",null);
            

        }
        
        
    }
