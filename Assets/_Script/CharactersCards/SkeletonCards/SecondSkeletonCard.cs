
    using UnityEngine;

    public class SecondSkeletonCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }


       public SecondSkeletonCard()
        {
            cardName = "Second Skeleton Card";
            initiative = 14;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 1, Attack 1");
            BottomCardAction = new CardAction(CardActionType.Shuffle, "Move 4");
        } 
    }
