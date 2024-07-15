
    using UnityEngine;

    public class SecondSkeletonCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GlowHighLight _highLight { get; set; }
        public GameObject cardPrefab { get; set; }


       public SecondSkeletonCard()
        {
            cardName = "Second Skeleton Card";
            initiative = 14;
            TopCardAction = new CardAction(CardDiscardActionType.Shuffle, "Move 4, Attack 1, Attack 1", this);
            TopCardAction.AddActionSequence(CharacterActionType.Move,4,0,"",null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,"AttackChop",null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,"AttackSlice",null);
           
        } 
    }
