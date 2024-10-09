using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;
using UnityEngine;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class SecondOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        private int bleedCount = 0;


        public SecondOmenCard()
        {
            cardName = "Enrage";
            initiative = 71;
            TopCardAction = new CardAction(CardDiscardActionType.Active,
                "2 dmg to self, Bleed self, Permanent skill : for X adjacent figures bleeding - Enrage ( + X damage for melee attacks)",
                this);
            TopCardAction.AddActionSequence(CharacterActionType.AttackSelf, 0, 1, 2, "", null);
            TopCardAction.AddActionSequence(CharacterActionType.DebuffSelf, 0, 1, 0, "",
                new List<ApplicableConditions>() { ApplicableConditions.Bleed });
            BottomCardAction = new CardAction(CardDiscardActionType.Lost, "Teleport 4 + X where is bleed self", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Teleport, 4, 0, 0, "", null);
        }

        public int TriggerActiveDeckCard(ICharacter source, ICharacter target, CharacterActionType actionType)
        {
            foreach (GameObject figures in AstarPathfinding.HexGrid.GetAdjacentTiles(source.currentHexPosition
                         .hexPosition))
            {
                if (figures.GetComponent<ICharacter>().TotalConditionList
                    .Exists(x => x.ApplicableCondition == ApplicableConditions.Bleed))
                {
                    bleedCount++;
                }
            }

            return bleedCount;
        }

        public int OnCardMoveValue(ICharacter source)
        {
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