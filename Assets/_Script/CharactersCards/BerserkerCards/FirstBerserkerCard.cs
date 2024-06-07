
    using UnityEngine;

    public class FirstBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }

        public FirstBerserkerCard()
        {
            cardName = "First Berserker Card";
            initiative = 20;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 3");
            BottomCardAction = new CardAction(CardActionType.Discard, "Move 3");
        }
        
        
    }
  
