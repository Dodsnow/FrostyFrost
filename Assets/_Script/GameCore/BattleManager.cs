using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using _Script.PlayableCharacters;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using Random = System.Random;


public class BattleManager : MonoBehaviour
{
    public BattleState state;
    private List<ICharacter> _characters = new List<ICharacter>();
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


    public void Start()
    {
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

    private void Update()
    {
        /*
        if (state == BattleState.CharacterTurnStart)
        {
            //TODO Make it work, Bitch!!!
            //Check for DOTS and buff/debuffs on character

            state = BattleState.CharacterTurn;
            CharacterTurn(selectionManager.lastSelectedCharacter);
            Debug.Log("update - state - CharacterTurnStart");
        }
        else if (state == BattleState.AiTurnStart)
        {
            //TODO Make it work, Bitch!!!

            // Check for DOTS and buff/debuff on AiCharacter

            state = BattleState.AiTurn;
            AiCharacterTurn(_currentAiCharacter);
            Debug.Log("update - state - AiTurnStart");
        }
        else if (state == BattleState.RoundEnd)
        {
            //TODO Make it work, Bitch!!!

            // End of the Round Logic
            
            state = BattleState.RoundStart;
            StartCoroutine(RoundStart());
            Debug.Log("update - state - RoundEnd");
        }*/
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
                battleHUD.DisplaySelectedCards(characterInitiativeList[i]);

                    if (characterInitiativeList[i].entityControllerType == EntityControllerType.Player)
                    {
                        Debug.Log("Round Start - Next entity is a player controlled");
                        currentActionSequenceIndex = 0;
                        selectionManager.lastSelectedCharacter = (PlayerCharacter)characterInitiativeList[i];
                        selectionManager.selectionMask = LayerMask.GetMask("ActionBar");
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
                Debug.Log("Round Start - Character Turn End");
            }

        roundNumber++;
        state = BattleState.RoundEnd;
        StartCoroutine(RoundStart());
        Debug.Log("Round Start - Round End");
    }

    private void CharacterTurn(PlayerCharacter playerCharacter)
    {
        selectionManager.selectionMask = LayerMask.GetMask("ActionBar");
        battleHUD.DisplaySelectedCards(playerCharacter);
    }

    private void AiCharacterTurn(AiCharacter aiCharacter)
    {
        battleHUD.DisplaySelectedCards(aiCharacter);
        StartCoroutine(_aiBehavior.AiActionPhase(aiCharacter));
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

            foreach (ICharacter character in _characters) { characterInitiativeList.Add(character); }

        characterInitiativeList = characterInitiativeList.OrderBy(x => x.SelectedCards[0].initiative).ToList();
        Debug.Log("Initiative list number is " + characterInitiativeList.Count);
    }
    
    public void CardActionEnd()
    {
        Debug.Log("card action sequence end - current index is " + currentActionSequenceIndex + " using a card " + selectionManager.lastSelectedCard.cardName + ". Total sequences number within action is " + selectionManager.lastSelectedCardAction.cardActionSequencesList.Count);
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
                Debug.Log("pick next card. Total selected cards number is " + selectionManager.lastSelectedCharacter.SelectedCards.Count);

                    switch (selectionManager.lastSelectedCardAction.DiscardActionType)
                    {
                        case CardDiscardActionType.Active:
                            selectionManager.lastSelectedCharacter.handDeck.Remove(selectionManager.lastSelectedCard);
                            selectionManager.lastSelectedCharacter.activeDeck.Add(selectionManager.lastSelectedCard);
                            Debug.Log("switch - active");
                            break;
                        case CardDiscardActionType.Discard:
                            selectionManager.lastSelectedCharacter.handDeck.Remove(selectionManager.lastSelectedCard);
                            selectionManager.lastSelectedCharacter.discardDeck.Add(selectionManager.lastSelectedCard);
                            Debug.Log("switch - discard");
                            break;
                        case CardDiscardActionType.Lost:
                            selectionManager.lastSelectedCharacter.handDeck.Remove(selectionManager.lastSelectedCard);
                            selectionManager.lastSelectedCharacter.lostDeck.Add(selectionManager.lastSelectedCard);
                            Debug.Log("switch - lost");
                            break;
                        case CardDiscardActionType.Shuffle:
                            //Shuffle the card deck back to the hand deck
                            Debug.Log("switch - shuffle");
                            break;
                    }

                    if (selectionManager.lastSelectedCharacter.SelectedCards.Count > 0)
                    {
                        Debug.Log("Select Top/Bot");
                        selectionManager.lastSelectedCard = selectionManager.lastSelectedCharacter.SelectedCards[0];
                        currentActionSequenceIndex = 0;
                        // battleHUD.DisplaySelectedCard(selectionManager.lastSelectedCharacter);
                            
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
    
}