
    using UnityEngine;

    public class SecondBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GlowHighLight _highLight { get; set; }
        public GameObject cardPrefab { get; set; }


        public SecondBerserkerCard()
        {
            cardName = "Second Berserker Card";
            initiative = 12;
            TopCardAction = new CardAction(CardDiscardActionType.Discard, "Attack 1, Attack 2", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,"AttackChop");
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,2, "AttackSlice");
            BottomCardAction = new CardAction(CardDiscardActionType.Discard,"Move 1, Heal 1", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move,1,0,"");
            BottomCardAction.AddActionSequence(CharacterActionType.Heal,1,1,"");

        }
    }