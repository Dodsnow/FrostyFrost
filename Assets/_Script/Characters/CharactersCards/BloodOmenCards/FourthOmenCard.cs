using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class FourthOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }

        public FourthOmenCard()
        {
            cardName = "Through Barricades";
            initiative = 16;
            TopCardAction = new CardAction(CardDiscardActionType.Discard, " Attack 1  Bleed, Move 3 , Attack 2", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1, 1, 1, "", new List<ApplicableConditions>()
                {
                    ApplicableConditions.Bleed
                });
            TopCardAction.AddActionSequence(CharacterActionType.Move, 3, 0,0, "", null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1,1 ,2, "", null);
            BottomCardAction = new CardAction(CardDiscardActionType.Discard, "Jump 4", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Jump, 4, 0, 0, "", null);
        }
    }
}