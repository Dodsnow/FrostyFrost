using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class ThirdOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public bool isImmortal { get; set; } = false;

        public ThirdOmenCard()
        {
            cardName = "Blood Rage";
            initiative = 85;
            TopCardAction = new CardAction(CardDiscardActionType.Lost, "Heal 4, 2 range, 2 target, Bleed", this);
            TopCardAction.AddActionSequence(CharacterActionType.Heal, 2, 2, 4, "", null);
            BottomCardAction = new CardAction(CardDiscardActionType.Active,
                "Persistent effect - If Bleeding and atleast 1 adjacent figure is bleeding, instead of suffering Hp, heal same amount. Doesnt heal bleed",
                this);
            BottomCardAction.AddActionSequence(CharacterActionType.BuffSelf, 0, 1, 0, "", null);
        }

        public void OnDamageTaken(ICharacter source, ICharacter target, int resultValue)
        {
            resultValue = source.SelectedCards[0].TopCardAction.cardActionSequencesList[0].ActionValue;
            if (target.ActiveDeck.Find(x => x.cardName == "ThirdOmenCard") != null)
            {
                for (int i = 0;
                     i <= AstarPathfinding.HexGrid.GetAdjacentTiles(source.currentHexPosition.hexPosition).Count;
                     i++)
                {
                    if (AstarPathfinding.HexGrid.GetAdjacentTiles(target.currentHexPosition.hexPosition)[i]
                        .GetComponent<ICharacter>().TotalConditionList.Exists(x =>
                            x.ApplicableCondition == ApplicableConditions.Bleed))
                    {
                        foreach (var condition in AstarPathfinding.HexGrid.GetAdjacentTiles(target.currentHexPosition
                                         .hexPosition)[i]
                                     .GetComponent<ICharacter>().TotalConditionList)
                        {
                            if (condition.ApplicableCondition == ApplicableConditions.Bleed)
                            {
                                CardActionManagerReference.cardActionManager.Heal(source, target, resultValue, "");
                            }
                            else
                            {
                                CardActionManagerReference.cardActionManager.Attack(source, target, resultValue, "",
                                    null);
                            }
                        }
                    }
                }
            }
        }


        // public bool CheckForCondition(ICharacter source, ICharacter target, ApplicableConditions conditions)
        // {
        //     if (source.TotalConditionList.Exists(x => x.ApplicableCondition == ApplicableConditions.Bleed))
        //     {
        //         List<ICharacter> adjacentFigures = new();
        //         for (int i = 0;
        //              i < AstarPathfinding.HexGrid.GetAdjacentTiles(source.currentHexPosition.hexPosition).Count;
        //              i++)
        //         {
        //             adjacentFigures.Add(
        //                 AstarPathfinding.HexGrid.GetAdjacentTiles(source.currentHexPosition.hexPosition)[i]
        //                     .GetComponent<ICharacter>());
        //         }
        //
        //         foreach (ICharacter figure in adjacentFigures)
        //         {
        //             if (figure.TotalConditionList.Exists(x =>
        //                     x.ApplicableCondition == ApplicableConditions.Bleed))
        //             {
        //                 source.CurrentHealth += source.SelectedCards[0].TopCardAction.cardActionSequencesList[0]
        //                     .ActionValue;
        //                 isImmortal = true;
        //             }
        //             else
        //             {
        //                 isImmortal = false;
        //             }
        //         }
        //     }
        //
        //     return isImmortal;
        // }
    }
}