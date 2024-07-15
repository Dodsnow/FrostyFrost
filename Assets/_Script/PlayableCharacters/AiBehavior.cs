using System.Collections.Generic;
using System.Collections;
using _Script.ConditionalEffects.Enum;
using UnityEngine;

public class AiBehavior : MonoBehaviour
{
    [SerializeField] private CardActionManager _cardActionManager;
    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private SpawnManager _spawnManager;
    


    public IEnumerator AiActionPhase(AiCharacter aiCharacter)
    {
        Debug.Log("Ai action phase start");
        CardActionSequence currentSequence;

            for (int i = 0; i < aiCharacter.SelectedCards[0].TopCardAction.cardActionSequencesList.Count; i++)
            {
                Debug.Log("Ai action phase - start sequence " + i);
                yield return new WaitForSeconds(1.0f);
                currentSequence = aiCharacter.SelectedCards[0].TopCardAction.cardActionSequencesList[i];

                    if (currentSequence.CharacterActionType == CharacterActionType.Move)
                    {
                        Debug.Log("Ai action phase - move sequence");

                            if (AstarPathfinding.GetDistance(aiCharacter.currentHexPosition.hexPosition,
                                    _spawnManager.playerCharacters[0].currentHexPosition.hexPosition) > 1)
                            {
                                Debug.Log("Ai action phase - have to move");
                                int movementPoints = currentSequence.ActionRange;
                                List<Hexagon> aiCharacterPath = AstarPathfinding.FindPath(aiCharacter.currentHexPosition, _spawnManager.playerCharacters[0].currentHexPosition);

                                if (movementPoints > aiCharacterPath.Count) { movementPoints = aiCharacterPath.Count; } 
                                _cardActionManager.Move(aiCharacter, aiCharacterPath[movementPoints - 1]);
                            }
                            else
                            {
                                _cardActionManager.cardActionEnd = true;
                                Debug.Log("Ai action phase - move card action end");
                            }
                            
                    }
                    else if (currentSequence.CharacterActionType == CharacterActionType.Attack)
                    {
                        Debug.Log("Ai action phase - attack sequence");

                            if (AstarPathfinding.GetDistance(aiCharacter.currentHexPosition.hexPosition,_spawnManager.playerCharacters[0].currentHexPosition.hexPosition) <= currentSequence.ActionRange)
                            {
                                Debug.Log("Ai action phase - have to attack");
                                _cardActionManager.Attack(aiCharacter, _spawnManager.playerCharacters[0],
                                    currentSequence.ActionValue,currentSequence.AnimProp,currentSequence.Conditions);
                            }
                            else
                            {
                                _cardActionManager.cardActionEnd = true;
                                Debug.Log("Ai action phase - attack card action end");
                            }
                    }

                yield return new WaitUntil(() => _cardActionManager.cardActionEnd);
                Debug.Log("Ai action phase - card action end");
                _cardActionManager.cardActionEnd = false;
            }

        Debug.Log("Ai action phase - " + aiCharacter.SelectedCards[0].cardName + " Monster card is removed");
            
                
            if (aiCharacter.SelectedCards[0].TopCardAction.DiscardActionType == CardDiscardActionType.Shuffle)
            {
                 Debug.Log("Ai action phase - Monster Deck is shuffled");
                 aiCharacter.characterCards.AddRange(aiCharacter.discardDeck);
                 aiCharacter.discardDeck.Clear();
            }
            else if (aiCharacter.SelectedCards[0].TopCardAction.DiscardActionType == CardDiscardActionType.Discard)
            {
                aiCharacter.discardDeck.Add(aiCharacter.SelectedCards[0]);
                Debug.Log("Ai action phase - total selected cards number " + aiCharacter.SelectedCards.Count);
                Debug.Log("Ai action phase - total discard cards number " + aiCharacter.discardDeck.Count);
            }

        aiCharacter.SelectedCards.Remove(aiCharacter.SelectedCards[0]);

        _battleManager.characterTurnEnd = true;
        Debug.Log("Ai action phase - Ai character turn end");
    }
}