
    using UnityEngine;

    public class ThirdBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }


        public ThirdBerserkerCard()
        {
            cardName = "Third Berserker Card";
            initiative = 32;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 3,");
            BottomCardAction = new CardAction(CardActionType.Discard, "Move 3");

        }
    }
