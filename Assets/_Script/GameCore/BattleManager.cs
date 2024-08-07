using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using _Script.ConditionalEffects;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using Random = System.Random;


public class BattleManager : MonoBehaviour
{
    public BattleState state;
    public List<ICharacter> _characters = new List<ICharacter>();
    private int roundNumber = 1;
    public BattleHUD battleHUD;
    public HexGrid grid;
    public SpawnManager spawnManager;
    public SelectionManager selectionManager;
    [SerializeField] private AiBehavior _aiBehavior;
    private AiCharacter _currentAiCharacter;
    private List<ICharacter> characterInitiativeList = new List<ICharacter>();
    public bool characterTurnEnd = false;
    private bool _cardLocked = false;
    public int currentActionSequenceIndex = 0;
    public CardActionManager cardActionManager;
    [SerializeField] private GameObject _cameraEye;
    


    public void Start()
    {
        BattleManagerReference.BattleManager = this;
        grid.HexMapInit();
        StartBattle();
    }

    public void StartBattle()
    {
        state = BattleState.BattleMapSetup;
        roundNumber = 1;
        Hexagon hexagon;
        Random random = new Random();
        List<GameObject> tempHexagon = grid.HexagonTilesetMap.Values.ToList();
        do
        {
            hexagon = tempHexagon[random.Next(0, tempHexagon.Count)].GetComponent<Hexagon>();
            if (hexagon._terrainType == TerrainType.Normal && hexagon.isOccupied == false)
            {
                spawnManager.SpawnPlayerCharacter(0, hexagon);
                _cameraEye.transform.position = spawnManager.playerCharacters[0].transform.position;
                break;
            }
        } while (true);

        do
        {
            hexagon = tempHexagon[random.Next(0, tempHexagon.Count)].GetComponent<Hexagon>();
            if (hexagon._terrainType == TerrainType.Normal && hexagon.isOccupied == false)
            {
                spawnManager.SpawnAICharacter(0, hexagon);
                break;
            }
        } while (true);

        _characters.AddRange(spawnManager.playerCharacters);
        _characters.AddRange(spawnManager.aiCharacters);
        state = BattleState.RoundStart;

        StartCoroutine(RoundStart());
    }


    IEnumerator RoundStart()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("================================");
        Debug.Log("Round Start");
        state = BattleState.RoundStart;
        int currentCharacterIndex = 0;

        foreach (PlayerCharacter playerCharacter in spawnManager.playerCharacters)
        {
            selectionManager.SelectCharacter(playerCharacter);
            yield return new WaitUntil(() => _cardLocked);
            Debug.Log("Round Start - Cards Locked");
            _cardLocked = false;
        }

        battleHUD.DisplayButton(true);
        RandomAiCardSelection();
        yield return new WaitUntil(() => battleHUD.isConfirmed);
        Debug.Log("Round Start - Confirm Button is clicked");
        BattleInitiativeOrder();


        for (int i = 0; i < characterInitiativeList.Count; i++)
        {
            Debug.Log("Round Start - current character index is " + i);
            battleHUD.RemoveDisplayCards();
            characterTurnEnd = false;
            TriggerCharacterTurnConditions(characterInitiativeList[i]);
            battleHUD.DisplaySelectedCards(characterInitiativeList[i]);

            if (characterInitiativeList[i].entityControllerType == EntityControllerType.Player)
            {
                Debug.Log("Round Start - Next entity is a player controlled");
                currentActionSequenceIndex = 0;
                selectionManager.lastSelectedCharacter = (PlayerCharacter)characterInitiativeList[i];
                selectionManager.selectionMask = LayerMask.GetMask("ActionBar");
                battleHUD.ShowEquippedItems();
                //battleHUD.DisplaySelectedCards(selectionManager.lastSelectedCharacter);
                state = BattleState.CharacterTurn;
                //state = BattleState.CharacterTurnStart;
            }

            else if (characterInitiativeList[i].entityControllerType == EntityControllerType.AI)
            {
                Debug.Log("Round Start - Next entity is an AI controlled");
                _currentAiCharacter = (AiCharacter)characterInitiativeList[i];
                //battleHUD.DisplaySelectedCards(_currentAiCharacter);
                StartCoroutine(_aiBehavior.AiActionPhase(_currentAiCharacter));
                state = BattleState.AiTurn;
                //state = BattleState.AiTurnStart;
            }

            yield return new WaitUntil(() => characterTurnEnd);
            battleHUD.RemoveDisplayCards();
            TriggerCharacterEndTurnConditions(characterInitiativeList[i]);
            cardActionManager.RemoveCondition(characterInitiativeList[i], ApplicableConditions.Stun);
            battleHUD.SortConditions(characterInitiativeList[i]);

            Debug.Log("Round Start - Character Turn End");
        }

        roundNumber++;
        foreach (ICharacter character in characterInitiativeList)
        {
            TriggerRoundEndConditions(character);
            battleHUD.RemoveObjectsFromInitiativeHud(character);

        }

        state = BattleState.RoundEnd;
        StartCoroutine(RoundStart());
        Debug.Log("Round Start - Round End");
    }


    private void RandomAiCardSelection()
    {
        CharacterCard selectedCard;

        foreach (AiCharacter aiCharacter in spawnManager.aiCharacters)
        {
            selectedCard = aiCharacter.characterCards[UnityEngine.Random.Range(0, aiCharacter.characterCards.Count)];
            aiCharacter.SelectedCards.Add(selectedCard);
            aiCharacter.characterCards.Remove(selectedCard);
            battleHUD.DisplaySelectedCards(aiCharacter);
            Debug.Log(aiCharacter.CharacterName + " has picked " + selectedCard.cardName);
        }

        Debug.Log("every ai picked it cards");
    }

    public void CardConfirmation()
    {
        if (selectionManager.lastSelectedCharacter.SelectedCards.Count >= 2)
        {
            battleHUD.RemoveDisplayCards();
            _cardLocked = true;
            selectionManager.selectionMask = LayerMask.GetMask("");
        }
        else
        {
            Console.WriteLine("Select more cards");
        }
    }

    private void BattleInitiativeOrder()
    {
        characterInitiativeList.Clear();

        foreach (ICharacter character in _characters)
        {
            characterInitiativeList.Add(character);
        }

        characterInitiativeList = characterInitiativeList.OrderBy(x => x.SelectedCards[0].initiative).ToList();
        foreach (ICharacter character in characterInitiativeList)
        {
            battleHUD.SetInitiativeHud(character);
        }
        battleHUD.SortInitiativeHud();
        Debug.Log("Initiative list number is " + characterInitiativeList.Count);
    }

    public void SkipPlayerTurn(PlayerCharacter playerCharacter)
    {
        playerCharacter.discardDeck.AddRange(playerCharacter.SelectedCards);
        playerCharacter.SelectedCards.Clear();
    }
    public void CardActionEnd()
    {
        Debug.Log("card action sequence end - current index is " + currentActionSequenceIndex + " using a card " +
                  selectionManager.lastSelectedCard.cardName + ". Total sequences number within action is " +
                  selectionManager.lastSelectedCardAction.cardActionSequencesList.Count);
        currentActionSequenceIndex++;
        // battleHUD.RemoveDisplayCards();

        if (currentActionSequenceIndex < selectionManager.lastSelectedCardAction.cardActionSequencesList.Count)
        {
            // battleHUD.DisplaySelectedCard(selectionManager.lastSelectedCharacter);
            StartCoroutine(selectionManager.CardActionChoose(selectionManager.lastSelectedCardAction));
            Debug.Log("Start next card action sequence");
        }
        else
        {
            selectionManager.lastSelectedCharacter.SelectedCards.Remove(selectionManager.lastSelectedCard);
            // battleHUD.RemoveDisplayCard(selectionManager.lastSelectedCard);
            Debug.Log("pick next card. Total selected cards number is " +
                      selectionManager.lastSelectedCharacter.SelectedCards.Count);

            switch (selectionManager.lastSelectedCardAction.DiscardActionType)
            {
                case CardDiscardActionType.Active:
                    selectionManager.lastSelectedCharacter.activeDeck.Add(selectionManager.lastSelectedCard);
                    selectionManager.lastSelectedCharacter.handDeck.Remove(selectionManager.lastSelectedCard);
                    Debug.Log("switch - active");
                    break;
                case CardDiscardActionType.Discard:
                    selectionManager.lastSelectedCharacter.discardDeck.Add(selectionManager.lastSelectedCard);
                    selectionManager.lastSelectedCharacter.handDeck.Remove(selectionManager.lastSelectedCard);
                    Debug.Log("switch - discard");
                    break;
                case CardDiscardActionType.Lost:
                    selectionManager.lastSelectedCharacter.lostDeck.Add(selectionManager.lastSelectedCard);
                    selectionManager.lastSelectedCharacter.handDeck.Remove(selectionManager.lastSelectedCard);
                    Debug.Log("switch - lost");
                    break;
                case CardDiscardActionType.Shuffle:
                    selectionManager.lastSelectedCharacter.handDeck.AddRange(selectionManager.lastSelectedCharacter
                        .discardDeck);
                    selectionManager.lastSelectedCharacter.discardDeck.Clear();
                    Debug.Log("switch - shuffle");
                    break;
            }

            if (selectionManager.lastSelectedCharacter.SelectedCards.Count > 0)
            {
                Debug.Log("Select Top/Bot");
                selectionManager.lastSelectedCard = selectionManager.lastSelectedCharacter.SelectedCards[0];
                currentActionSequenceIndex = 0;
                // battleHUD.DisplaySelectedCard(selectionManager.lastSelectedCharacter);
                battleHUD.ToggleCardsVisibility(true);

                if (selectionManager._lastSelectedObject.CompareTag("TopCardAction"))
                {
                    Debug.Log("Next is Bottom action");
                    selectionManager.lastSelectedCardAction = selectionManager.lastSelectedCard.BottomCardAction;
                }
                else if (selectionManager._lastSelectedObject.CompareTag("BottomCardAction"))
                {
                    Debug.Log("Next is Top action");
                    selectionManager.lastSelectedCardAction = selectionManager.lastSelectedCard.TopCardAction;
                }

                StartCoroutine(selectionManager.CardActionChoose(selectionManager.lastSelectedCardAction));
            }
            else
            {
                Debug.Log("Character turn end");
                characterTurnEnd = true;
            }
        }
    }

    private void TriggerCharacterTurnConditions(ICharacter character)
    {
        foreach (CharCondition condition in character.TurnStartConditionsList)
        {
            switch (condition.ApplicableCondition)
            {
                case ApplicableConditions.Bleed:
                    character.CurrentHealth -= condition.ConditionValue;
                    character.slider.value = character.CurrentHealth;
                    if (character.CurrentHealth <= 0)
                    {
                        character.CharacterDeath();
                    }

                    Debug.Log("Bleed condition applied");
                    break;
                case ApplicableConditions.Poison:
                    character.CurrentHealth -= condition.ConditionValue;
                    character.slider.value = character.CurrentHealth;
                    if (character.CurrentHealth <= 0)
                    {
                        character.CharacterDeath();
                    }

                    Debug.Log("Poison condition applied");
                    break;
                case ApplicableConditions.Stun:
                    if (character.entityControllerType == EntityControllerType.Player)
                    {
                        SkipPlayerTurn((PlayerCharacter)character);
                    }
                    characterTurnEnd = true;
                    Debug.Log("Stun condition applied");
                    break;
                case ApplicableConditions.Recovery:
                    character.CurrentHealth += condition.ConditionValue;
                    if (character.CurrentHealth >= character.MaxHealth)
                    {
                        character.CurrentHealth = character.MaxHealth;
                        
                    }
                    character.slider.value = character.CurrentHealth;

                    Debug.Log("Recovery condition applied");
                    break;
            }
        }
    }

    private void TriggerCharacterEndTurnConditions(ICharacter character)
    {
        character.TurnEndConditionsList.Clear();
        battleHUD.SortConditions(character);
    }

    private void TriggerRoundEndConditions(ICharacter character)
    {
        
        character.RoundEndConditionsList.Clear();
        battleHUD.SortConditions(character);
    }
}

public static class BattleManagerReference
{
    public static BattleManager BattleManager;
}