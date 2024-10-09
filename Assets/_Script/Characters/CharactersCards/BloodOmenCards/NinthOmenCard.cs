using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class NinthOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }

        public NinthOmenCard()
        {
            cardName = "Blood Shield";
            initiative = 12;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,
                "Shield 1 + X, where X is the number of bleed self, Attack 1 Bleed", this);
            TopCardAction.AddActionSequence(CharacterActionType.Shield, 0, 1, 1, "", null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1, 1, 1, "", new List<ApplicableConditions>()
            {
                ApplicableConditions.Bleed
            });
            BottomCardAction = new CardAction(CardDiscardActionType.Discard, "Move 2", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move, 2, 0, 0, "", null);
        }
        
        public int OnCardShieldValue(ICharacter source)
        {
            int bleedCount = 0;
            foreach (var condition in source.TotalConditionList)
            {
                if (condition.ApplicableCondition == ApplicableConditions.Bleed)
                {
                    bleedCount++;
                }
            }
            return bleedCount;
        }
    }
}