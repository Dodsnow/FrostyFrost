using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;
using UnityEngine;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class SixthOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        int _bleedCount = 0;

        public SixthOmenCard()
        {
            cardName = "Hand Axes";
            initiative = 45;
            
            TopCardAction = new CardAction(CardDiscardActionType.Discard,
                "Attack 2 Range 3 target 1 + X (X is bleed self count) Bleed, Bleed self", this);
            
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 3, 1, 2, "", new List<ApplicableConditions>()
                {
                    ApplicableConditions.Bleed
                });
            TopCardAction.AddActionSequence(CharacterActionType.Debuff, 0, 1, 0, "", new List<ApplicableConditions>()
                {
                    ApplicableConditions.Bleed
                });
            BottomCardAction = new CardAction(CardDiscardActionType.Discard, "Move 2, bleed self and adjacent figure", this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move, 2, 0, 0, "", null);
            BottomCardAction.AddActionSequence(CharacterActionType.DebuffSelf, 1,1,0, "", new List<ApplicableConditions>()
                {
                    ApplicableConditions.Bleed
                });
        }
        
       

        public int OnExtraTarget(ICharacter source)
        {
            int numberOfTargets = 0;
            foreach (var condition in source.TotalConditionList)
            {
                if (condition.ApplicableCondition == ApplicableConditions.Bleed)
                {
                    numberOfTargets++;
                }
            }
            return numberOfTargets;
        }
        
         public void OnCardActionEnd(ICharacter source, ICharacter target)
         {
             List<Hexagon> hexes =
                 AstarPathfinding.HexGrid.GetTileInRadius(source.currentHexPosition, 1);

             foreach (ICharacter entity in hexes)
             {
                    if (entity != null)
                    {
                        CardActionManagerReference.cardActionManager.ApplyCondition(source, entity, ApplicableConditions.Bleed);
                        break;
                    }
             }
             
             
         }
    }
}