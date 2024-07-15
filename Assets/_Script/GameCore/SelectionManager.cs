using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using _Script.PlayableCharacters;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public LayerMask selectionMask;
    public BattleManager battleManager;
    public HexGrid hexGrid;
    private List<Hexagon> _lastSelectedHexes = new List<Hexagon>();
    private Hexagon _finalHexagon;
    public BattleHUD battleHud;
    public PlayerCharacter lastSelectedCharacter;
    public CharacterCard lastSelectedCard;
    public WaitUntil waitUntilConfirmed;
    private PlayerTurnState _playerTurnState;
    public CardActionManager _cardActionManager;
    public CardAction lastSelectedCardAction;
    private GameObject _finalTarget;
    public GameObject _lastSelectedObject;
    public SkipActionButton _skipActionButton;


    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;

        if (FindTarget(mousePosition, out result))
        {
            Debug.Log(result.name);
            if (selectionMask.value == LayerMask.GetMask("Card"))
            {
                if (_playerTurnState == PlayerTurnState.CardRoundSelection)
                {
                    SelectCard(lastSelectedCharacter, result);
                }
            }
            else if (battleManager.state == BattleState.CharacterTurn)
            {
                if (selectionMask.value == LayerMask.GetMask("ActionBar"))
                {
                    GameObject card = result.transform.root.gameObject;
                    CharacterCard characterCard = battleHud.CardDictionary[card];
                    _lastSelectedObject = result;
                    // battleHud.RemoveDisplayCards();
                    if (result.CompareTag("TopCardAction"))
                    {
                        StartCoroutine(CardActionChoose(characterCard.TopCardAction));
                    }
                    else if (result.CompareTag("BottomCardAction"))
                    {
                        StartCoroutine(CardActionChoose(characterCard.BottomCardAction));
                    }
                }
                else if (selectionMask.value == LayerMask.GetMask("hex"))
                {
                    _finalHexagon = result.GetComponent<Hexagon>();
                    if (AstarPathfinding.GetDistance(lastSelectedCharacter.currentHexPosition.hexPosition,
                            result.GetComponent<Hexagon>().hexPosition) <= lastSelectedCardAction
                            .cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange &&
                        !result.GetComponent<Hexagon>().isOccupied)
                    {
                        _cardActionManager.Move(lastSelectedCharacter, _finalHexagon);
                    }
                    else
                    {
                        Debug.LogWarning("Out of range");
                    }
                }
                else if (selectionMask.value == LayerMask.GetMask("Monster"))
                {
                    Debug.Log("Start of attack");
                    _finalTarget = result;
                    if (AstarPathfinding.GetDistance(lastSelectedCharacter.currentHexPosition.hexPosition,
                            result.GetComponent<ICharacter>().currentHexPosition.hexPosition) <= lastSelectedCardAction
                            .cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange)
                    {
                        _cardActionManager.Attack(lastSelectedCharacter, result.GetComponent<ICharacter>(),
                            lastSelectedCardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex]
                                .ActionValue,
                            lastSelectedCardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex]
                                .AnimProp, lastSelectedCardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].Conditions);
                    }
                    else
                    {
                        Debug.LogWarning("Out of range");
                    }
                }
                else if (selectionMask.value == LayerMask.GetMask("Character"))
                {
                    if(AstarPathfinding.GetDistance(lastSelectedCharacter.currentHexPosition.hexPosition, result.GetComponent<ICharacter>().currentHexPosition.hexPosition) <= lastSelectedCardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange)
                    {
                       _cardActionManager.Heal(lastSelectedCharacter, result.GetComponent<ICharacter>(), lastSelectedCardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionValue, lastSelectedCardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].AnimProp);
                    }
                    else
                    {
                        Debug.LogWarning("Out of range");
                    }
                }
            }
        }
    }


    public IEnumerator CardActionChoose(CardAction cardAction)
    {
        lastSelectedCardAction = cardAction;
        lastSelectedCard = cardAction.CharacterCard;
        battleHud.skipActionButton.gameObject.SetActive(true);
            
            if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                CharacterActionType.Move)
            {
                _finalHexagon = null;
                selectionMask = LayerMask.GetMask("hex");
                List<Hexagon> radius = hexGrid.GetTileInRadius(lastSelectedCharacter.currentHexPosition,
                    cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange);
                foreach (Hexagon hex in radius)
                {
                    hex.EnableHighLight();
                }

                yield return new WaitUntil(() => _finalHexagon != null || battleHud.skipAction);
                selectionMask = LayerMask.GetMask("Default");
                _finalHexagon = null;
                _skipActionButton.ResetSkipActionButton();

                foreach (Hexagon hex in radius)
                {
                    hex.DisableHighLight();
                }
            }
            else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                    CharacterActionType.Attack)
            {
                selectionMask = LayerMask.GetMask("Monster");
                List<Hexagon> radius = hexGrid.GetTileInRadius(lastSelectedCharacter.currentHexPosition,
                    cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange);
                foreach (Hexagon hex in radius)
                {
                    hex.EnableHighLight();
                }

                yield return new WaitUntil(() => _finalTarget != null || battleHud.skipAction);
                _finalTarget = null;
                _skipActionButton.ResetSkipActionButton();

                foreach (Hexagon hex in radius)
                {
                    hex.DisableHighLight();
                }
            }

        yield return new WaitUntil(() => _cardActionManager.cardActionEnd);
        battleManager.CardActionEnd();
    }

    IEnumerator WaifForConfirmation()
    {
        waitUntilConfirmed = new WaitUntil(() => battleHud.isConfirmed);
        yield return waitUntilConfirmed;
        battleManager.CardConfirmation();
    }

    public void SelectCard(PlayerCharacter character, GameObject displayedCard)
    {
        CharacterCard card = null;
        battleHud.CardDictionary.TryGetValue(displayedCard, out card);
        if (card != null)
        {
            if (_playerTurnState == PlayerTurnState.CardHandSelection ||
                _playerTurnState == PlayerTurnState.CardRoundSelection)
            {
                if (character.SelectedCards.Exists(x => x == card))
                {
                    character.SelectedCards.Remove(card);
                    StopCoroutine(WaifForConfirmation());
                    battleHud.DisplayButton(false);
                }
                else if (character.SelectedCards.Count < 2)
                {
                    character.SelectedCards.Add(card);
                    if (character.SelectedCards.Count == 2)
                    {
                        StartCoroutine(WaifForConfirmation());
                        battleHud.DisplayButton(true);
                    }
                }
            }
        }
    }

    public void SelectCharacter(PlayerCharacter character)
    {
        lastSelectedCharacter = character;
        selectionMask = LayerMask.GetMask("Card");

            if (battleManager.state == BattleState.RoundStart)
            {
                _playerTurnState = PlayerTurnState.CardRoundSelection;
            }

        battleHud.DisplayCharacterHandDeck(character);
    }

    IEnumerator HighlightHex()
    {
        for (int i = 0; i < _lastSelectedHexes.Count; i++)
        {
            _lastSelectedHexes[i].EnableHighLight();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectionMask.value))
        {
            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}