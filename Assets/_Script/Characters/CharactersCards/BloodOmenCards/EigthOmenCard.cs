using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class EigthOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        private int _bleedCount = 0;

        public EigthOmenCard()
        {
            cardName = "Sap Life";
            initiative = 28;
            TopCardAction = new CardAction(CardDiscardActionType.Discard, "Bleed range 1, Heal 2 self, Bleed self",
                this);
            TopCardAction.AddActionSequence(CharacterActionType.Debuff, 1,1,0,"", new List<ApplicableConditions>()
                {
                    ApplicableConditions.Bleed
                });
            TopCardAction.AddActionSequence(CharacterActionType.Heal, 0, 0, 2, "", null);
            TopCardAction.AddActionSequence(CharacterActionType.DebuffSelf, 0, 1, 0, "",
                new List<ApplicableConditions>() { ApplicableConditions.Bleed });
            BottomCardAction = new CardAction(CardDiscardActionType.Discard,
                "Move X where  is the number of bleed self", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move, 0, 0,0,"", null);
        }

        public int OnCardMoveValue(ICharacter source, ICharacter target)
        {
            foreach (var condition in source.TotalConditionList)
            {
                if (condition.ApplicableCondition == ApplicableConditions.Bleed)
                {
                    _bleedCount++;
                }
            }
            return _bleedCount;
        }
    }
}