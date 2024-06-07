
    using UnityEngine;

    public class FifthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }


        public FifthBerserkerCard()
        {
            cardName = "Fifth Berserker Card";
            initiative = 65;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 4");
            BottomCardAction = new CardAction(CardActionType.Discard, "Move 4");

        }
    }
