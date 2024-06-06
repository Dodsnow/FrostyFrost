
    using UnityEngine;

    public class FourthBerserkerCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public GameObject cardPrefab { get; set; }


        public FourthBerserkerCard()
        {
            cardName = "Fourth Berserker Card";
            initiative = 86;
            TopCardAction = new CardAction(CardActionType.Discard, "Attack 3, Attack 1");
            BottomCardAction = new CardAction(CardActionType.Lost, "Move 1, Move 4");
            

        }
    }
