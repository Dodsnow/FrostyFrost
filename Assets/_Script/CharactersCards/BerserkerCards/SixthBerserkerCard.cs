
    using UnityEngine;

    public class SixthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }


        public SixthBerserkerCard()  
        {
            cardName = "Sixth Berserker Card";
            initiative = 44;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 2");
            BottomCardAction = new CardAction(CardActionType.Discard, "Move 2");

        }
    }
