
    using System;
    using _Script.Characters.CharactersCards.Enum;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class SixthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        

        public SixthBerserkerCard()  
        {
            cardName = "Sixth Berserker Card";
            initiative = 44;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,"Attack 2", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,2,"AttackStab",null);
            BottomCardAction = new CardAction(CardDiscardActionType.Discard,"Move 10", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,10,0,0,"",null);

        }
    }
