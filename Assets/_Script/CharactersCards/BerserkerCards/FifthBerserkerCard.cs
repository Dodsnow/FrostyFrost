
    using UnityEngine;

    public class FifthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GlowHighLight _highLight { get; set; }
        public GameObject cardPrefab { get; set; }


        public FifthBerserkerCard()
        {
            cardName = "Fifth Berserker Card";
            initiative = 65;
            TopCardAction = new CardAction(CardDiscardActionType.Discard, "Attack 4", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1,4, "AttackSlice");
            BottomCardAction = new CardAction(CardDiscardActionType.Discard,"Move 4", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move, 4,0, "");

        }
    }
