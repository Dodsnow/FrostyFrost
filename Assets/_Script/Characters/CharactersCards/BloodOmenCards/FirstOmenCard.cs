using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class FirstOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }


        public FirstOmenCard()
        {
            cardName = "Ignore consequences";
            initiative = 86;
            TopCardAction = new CardAction(CardDiscardActionType.Discard, "Attack 4", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1, 1, 4, "AttackChop", null);
            BottomCardAction = new CardAction(CardDiscardActionType.Active, "Ignore Fatal damage, if bleeded", this);
            BottomCardAction.AddActionSequence(CharacterActionType.BuffSelf, 0, 1, 0, "",
                null);
        }


        public void OnConditionApplied(ICharacter source, ICharacter target)
        {
            if (target.ActiveDeck.Find(x => x.cardName == "FirstOmenCard") != null)
            {
                if (UtilsReference.utils.HasCondition(target, ApplicableConditions.Bleed))
                {
                    CardActionManagerReference.cardActionManager.ApplyCondition(source, target,
                        ApplicableConditions.Immortal);
                }
            }
        }

        public void OnConditionRemoved(ICharacter target)
        {
            if (target.ActiveDeck.Find(x => x.cardName == "FirstOmenCard") != null)
            {
                if (!UtilsReference.utils.HasCondition(target, ApplicableConditions.Bleed))
                {
                    CardActionManagerReference.cardActionManager.RemoveCondition(target, ApplicableConditions.Immortal);
                }
            }
        }
    }
}