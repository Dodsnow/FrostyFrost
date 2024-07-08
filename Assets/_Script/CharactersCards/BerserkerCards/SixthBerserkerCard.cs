
    using UnityEngine;

    public class SixthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GlowHighLight _highLight { get; set; }
        public GameObject cardPrefab { get; set; }


        public SixthBerserkerCard()  
        {
            cardName = "Sixth Berserker Card";
            initiative = 44;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,"Attack 2", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,2,"AttackStab");
            BottomCardAction = new CardAction(CardDiscardActionType.Discard,"Move 2", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,2,0,"");

        }
    }
