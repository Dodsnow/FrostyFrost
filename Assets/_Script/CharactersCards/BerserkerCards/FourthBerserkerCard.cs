
    using UnityEngine;

    public class FourthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GlowHighLight _highLight { get; set; }
        public GameObject cardPrefab { get; set; }


        public FourthBerserkerCard()
        {
            cardName = "Fourth Berserker Card";
            initiative = 86;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,"Attack 3, Attack 1", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,3, "AttackSlice");
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,"AttackStab");
            BottomCardAction = new CardAction(CardDiscardActionType.Lost,"Move 1, Move 4", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,1,0, "");
            BottomCardAction.AddActionSequence(CharacterActionType.Move,4,0, "");
            

        }
    }
