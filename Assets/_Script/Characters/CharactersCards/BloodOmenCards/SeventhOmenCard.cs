using System.Collections.Generic;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;

namespace _Script.Characters.CharactersCards.BloodOmenCards
{
    public class SeventhOmenCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }

        public SeventhOmenCard()
        {
            cardName = "Blood Bath";
            initiative = 13;
            TopCardAction = new CardAction(CardDiscardActionType.Discard,
                "Attack 3 bleed, return X cards from discard. X number of bleed on target", this);
            TopCardAction.AddActionSequence(CharacterActionType.Attack, 1, 1, 3, "", new List<ApplicableConditions>()
            {
                ApplicableConditions.Bleed
            });
            TopCardAction.AddActionSequence(CharacterActionType.ReturnCard, 0, 0, 0, "", null);
            BottomCardAction = new CardAction(CardDiscardActionType.Lost, "Move 4, bleed all adjacent figures range 2",
                this);
            BottomCardAction.AddActionSequence(CharacterActionType.Move, 4, 0, 0, "", null);
        }

        public void ReturnCardFromDeck(ICharacter source, ICharacter target)
        {
            int cardReturnAmount = 0;
            foreach (var condition in target.TotalConditionList)
            {
                if (condition.ApplicableCondition == ApplicableConditions.Bleed)
                {
                    cardReturnAmount++;
                }
            }

            CardActionManagerReference.cardActionManager.ReturnCard(source, DeckType.Discard, cardReturnAmount);
        }
        
        public void OnCardActionEnd(ICharacter source, ICharacter target)
        {
            List<Hexagon> hexes =
                AstarPathfinding.HexGrid.GetTileInRadius(source.currentHexPosition, 2);

            foreach (ICharacter entity in hexes)
            {
                if (entity != null)
                {
                    CardActionManagerReference.cardActionManager.ApplyCondition(source, entity, ApplicableConditions.Bleed);
                    
                }
            }
        }
    }
}