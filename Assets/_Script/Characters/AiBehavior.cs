using System.Collections.Generic;
using System.Collections;
using System.Linq;
using _Script.Characters.CharactersCards.BloodOmenCards;
using _Script.Characters.CharactersCards.Enum;
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
        if (aiCharacter.TurnStartConditionsList.Exists(x => x.ApplicableCondition == ApplicableConditions.Stun))
        {
            ResolveAiCard(aiCharacter);
            yield return null;
        }
        else
        {
            for (int i = 0; i < aiCharacter.SelectedCards[i].TopCardAction.cardActionSequencesList.Count; i++)
            {
                Debug.Log("Ai action phase - start sequence " + i);
                yield return new WaitForSeconds(1.0f);
                currentSequence = aiCharacter.SelectedCards[0].TopCardAction.cardActionSequencesList[i];
                if (_spawnManager.playerCharacters[0].TotalConditionList
                    .Exists(x => x.ApplicableCondition == ApplicableConditions.Invisible))
                {
                    ResolveAiCard(aiCharacter);
                    yield return null;
                }
                else if (currentSequence.CharacterActionType == CharacterActionType.Move)
                {
                    Debug.Log("Ai action phase - move sequence");

                    if (AstarPathfinding.GetDistance(aiCharacter.currentHexPosition.hexPosition,
                            _spawnManager.playerCharacters[0].currentHexPosition.hexPosition) > 1)
                    {
                        Debug.Log("Ai action phase - have to move");
                        int movementPoints = currentSequence.ActionRange;
                        List<Hexagon> aiCharacterPath = AstarPathfinding.FindPath(aiCharacter.currentHexPosition,
                            _spawnManager.playerCharacters[0].currentHexPosition, currentSequence.CharacterActionType);

                        if (movementPoints > aiCharacterPath.Count)
                        {
                            movementPoints = aiCharacterPath.Count;
                        }

                        Debug.Log("Ai has: " + (movementPoints - 1) + " movement points");
                        _cardActionManager.Move(aiCharacter, aiCharacterPath[movementPoints - 1], currentSequence.CharacterActionType);
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

                    if (AstarPathfinding.GetDistance(aiCharacter.currentHexPosition.hexPosition,
                            _spawnManager.playerCharacters[0].currentHexPosition.hexPosition) <=
                        currentSequence.ActionRange)
                    {
                        Debug.Log("Ai action phase - have to attack");
                        if (_spawnManager.playerCharacters[0].ActiveDeck.Exists(x => x == typeof(FirstOmenCard)))
                        {
                            FirstOmenCard firstOmenCard = (FirstOmenCard)_spawnManager.playerCharacters[0].ActiveDeck
                                .Find(x => x == typeof(FirstOmenCard));
                            if (_spawnManager.playerCharacters[0].TotalConditionList.Exists(x =>
                                    x.ApplicableCondition == ApplicableConditions.Bleed))
                            {
                                currentSequence.ActionValue = 0;
                            }
                        }
                        else if (_spawnManager.playerCharacters[0].ActiveDeck.Exists(x => x == typeof(ThirdOmenCard)))
                        {
                            ThirdOmenCard thirdOmenCard = (ThirdOmenCard)_spawnManager.playerCharacters[0].ActiveDeck
                                .Find(x => x == typeof(ThirdOmenCard));
                            if (thirdOmenCard.isImmortal)
                            {
                                currentSequence.ActionValue = 0;
                            }
                        }

                        _cardActionManager.Attack(aiCharacter, _spawnManager.playerCharacters[0],
                            currentSequence.ActionValue, currentSequence.AnimProp, currentSequence.Conditions);
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
                Debug.Log("Ai action phase - " + aiCharacter.SelectedCards[0].cardName + " Monster card is removed");
            }

            ResolveAiCard(aiCharacter);
        }


        _battleManager.characterTurnEnd = true;
        Debug.Log("Ai action phase - Ai character turn end");
    }

    private void ResolveAiCard(AiCharacter aiCharacter)
    {
        Debug.Log("Ai cards Resolved +++++++++++++++++++++++++++++++");
        if (aiCharacter.SelectedCards[0].TopCardAction.DiscardActionType == CardDiscardActionType.Shuffle)
        {
            aiCharacter.DiscardDeck.Add(aiCharacter.SelectedCards[0]);
            Debug.Log("Ai action phase - Monster Deck is shuffled");
            if (aiCharacter.DiscardDeck.Count > 1)
            {
                aiCharacter.CharacterGlobalDeck.AddRange(aiCharacter.DiscardDeck);
                Debug.Log("Ai character deck cards number: " + aiCharacter.CharacterGlobalDeck.Count);
            }
            else if (aiCharacter.DiscardDeck.Count == 1)
            {
                aiCharacter.CharacterGlobalDeck.Add(aiCharacter.DiscardDeck[0]);
                Debug.Log("Ai character deck cards number: " + aiCharacter.CharacterGlobalDeck.Count);
            }

            aiCharacter.DiscardDeck.Clear();
            Debug.Log("Ai discard deck cards number: " + aiCharacter.DiscardDeck.Count);
        }
        else if (aiCharacter.SelectedCards[0].TopCardAction.DiscardActionType == CardDiscardActionType.Discard)
        {
            aiCharacter.DiscardDeck.Add(aiCharacter.SelectedCards[0]);
            Debug.Log("Ai discard deck cards number: " + aiCharacter.DiscardDeck.Count);
            Debug.Log("Ai action phase - total selected cards number " + aiCharacter.SelectedCards.Count);
            Debug.Log("Ai action phase - total discard cards number " + aiCharacter.DiscardDeck.Count);
        }

        aiCharacter.SelectedCards.Remove(aiCharacter.SelectedCards[0]);
        Debug.Log("Ai character Selected deck cards number: " + aiCharacter.SelectedCards.Count);
    }
}