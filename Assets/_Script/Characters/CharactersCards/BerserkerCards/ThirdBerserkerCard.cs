
    using System;
    using System.Collections.Generic;
    using _Script.Characters.CharactersCards.Enum;
    using _Script.ConditionalEffects.Enum;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class ThirdBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
       

        public ThirdBerserkerCard()
        {
            cardName = "Third Berserker Card";
            initiative = 32;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,"Attack 3", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,3,"AttackChop", new List<ApplicableConditions>()
                {
                    ApplicableConditions.Disarm});
            BottomCardAction = new CardAction(CardDiscardActionType.Shuffle,"Move 10", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,10,0,0,"",null);

        }
    }
