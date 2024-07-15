
    using UnityEngine;

    public class ThirdBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GlowHighLight _highLight { get; set; }
        public GameObject cardPrefab { get; set; }


        public ThirdBerserkerCard()
        {
            cardName = "Third Berserker Card";
            initiative = 32;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,"Attack 3", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,3,"AttackChop",null);
            BottomCardAction = new CardAction(CardDiscardActionType.Shuffle,"Move 10", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,10,0,"",null);

        }
    }
