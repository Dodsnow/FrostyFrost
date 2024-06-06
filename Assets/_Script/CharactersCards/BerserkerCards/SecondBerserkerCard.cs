
    using UnityEngine;

    public class SecondBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }


        public SecondBerserkerCard()
        {
            cardName = "Second Berserker Card";
            initiative = 12;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 1, Attack 2");
            BottomCardAction = new CardAction(CardActionType.Discard, "Move 1, Heal 1");

        }
    }