using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;
using UnityEditor;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class FifthOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        int bleedCount = 0;

        public FifthOmenCard()
        {
            cardName = "Cut veins";
            initiative = 14;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,
                "Attack X (Where X is the number of bleed you have), Bleed Self", this);

            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1, 1, 0, "", null);
            TopCardAction.AddActionSequence(CharacterActionType.DebuffSelf, 0, 1, 0, "",
                new List<ApplicableConditions>()
                {
                    ApplicableConditions.Bleed
                });
            BottomCardAction = new CardAction(CardDiscardActionType.ShuffleLost,
                "Move 1, Bleed all figures in range 1, Return Card From Lost, Lost", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move, 1, 0, 0, "", null);
            BottomCardAction.AddActionSequence(CharacterActionType.Debuff, 1, 7, 0, "", new List<ApplicableConditions>()
            {
                ApplicableConditions.Bleed
            });
        }

        public int OnCardAttackValue(ICharacter source, ICharacter target, CharacterActionType actionType)
        {
            for (int i = 0; i <= source.TotalConditionList.Count; i++)
            {
                if (source.TotalConditionList[i].ConditionID == "Bleed")
                {
                    bleedCount++;
                }
            }

            return bleedCount;
        }

        public void ReturnCardFromDeck(ICharacter source, ICharacter target)
        {
            CharacterCard cardToReturn = null;
            BattleHUDReference.battleHUD.DisplayCards(source, DeckType.Lost);
            // cardToReturn = SelectionManagerReference.selectionManager.SelectCard(source,  );
            CardActionManagerReference.cardActionManager.ReturnCard(source, DeckType.Lost, 1);
            
        }
    }
}