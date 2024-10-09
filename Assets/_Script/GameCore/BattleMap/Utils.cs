
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using _Script.ConditionalEffects;
    using _Script.ConditionalEffects.Enum;
    using _Script.PlayableCharacters;
    using UnityEngine;

    public class Utils : MonoBehaviour
    {
        public BattleManager battleManager;
        
        public void Awake()
        {
            UtilsReference.utils = this;
        }
        public int NumberOfEnemies(Vector3Int startPosition,int radius, EntityControllerType entityType)
        {
        int amount = 0;
        foreach (ICharacter character in battleManager._characters)
        {
            if (character.entityControllerType == entityType && AstarPathfinding.GetDistance(character.currentHexPosition.hexPosition, startPosition) <= radius)
            {
                amount++;
            }
        }

        return amount;
        }
        
        public int NumberOfEnemiesUnderCondition(Vector3Int startPosition,int radius, EntityControllerType entityType,ApplicableConditions condition)
        {
            int amount = 0;
            foreach (ICharacter character in battleManager._characters)
            {
                if (character.entityControllerType == entityType && AstarPathfinding.GetDistance(character.currentHexPosition.hexPosition, startPosition) <= radius)
                {
                    if (character.TotalConditionList.Exists(x => x.ApplicableCondition == condition))
                    {
                        amount++;
                    }
                    
                }
            }

            return amount;
        }

        public CardAction CopyCardAction(CardAction cardAction)
        {
            CardAction tempCardAction = new CardAction(cardAction.DiscardActionType, cardAction.Discription, cardAction.CharacterCard);
            tempCardAction.cardActionSequencesList = new List<CardActionSequence>();
            for (int x = 0; x < cardAction.cardActionSequencesList.Count; x++)
            {
                CardActionSequence tempCardActionSequence = new CardActionSequence(cardAction.cardActionSequencesList[x].CharacterActionType, cardAction.cardActionSequencesList[x].ActionRange,cardAction.cardActionSequencesList[x].NumberOfTargets ,cardAction.cardActionSequencesList[x].ActionValue, cardAction.cardActionSequencesList[x].AnimProp);
                tempCardAction.cardActionSequencesList.Add(tempCardActionSequence);
            }

            return tempCardAction;
        }
        
        public bool HasCondition(ICharacter character, ApplicableConditions condition)
        {
            foreach (CharCondition tempCondition in character.TotalConditionList)
            {
                if (tempCondition.ApplicableCondition == condition)
                {
                    return true;
                }
            }

            return false;
        }
    }
    
    public static class UtilsReference
    {
        public static Utils utils;
    }
