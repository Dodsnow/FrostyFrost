  using UnityEngine;

  public class FirstSkeletonCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }


        public FirstSkeletonCard()
        {
            cardName = "Nothing Special";
            initiative = 20;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 3");
            BottomCardAction = new CardAction(CardActionType.Discard, "Move 3");
        }
    }
