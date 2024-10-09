using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using _Script.Characters.CharactersCards.BloodOmenCards;
using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public LayerMask selectionMask;
    public BattleManager battleManager;
    public HexGrid hexGrid;
    private List<Hexagon> _lastSelectedHexes = new List<Hexagon>();
    public Hexagon _finalHexagon;
    public BattleHUD battleHud;
    public PlayerCharacter lastSelectedCharacter;
    public CharacterCard lastSelectedCard;
    public WaitUntil waitUntilConfirmed;
    private PlayerTurnState _playerTurnState;
    public CardActionManager _cardActionManager;
    public CardAction lastSelectedCardAction;
    public GameObject _finalTarget;
    public GameObject _lastSelectedObject;
    public SkipActionButton _skipActionButton;
    private CharacterActionType _characterActionType;
    private int numberOfTargets;


    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        SelectionManagerReference.selectionManager = this;
    }


    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;
        if (SelectionCharacter(mousePosition, out result) && result.gameObject.CompareTag("PlayerCharacter"))
        {
            battleHud.ChangeCharacterPortrait(result.GetComponent<PlayerCharacter>());
        }
        else if (SelectionCharacter(mousePosition, out result) && result.gameObject.CompareTag("Monster"))
        {
            battleHud.ChangeEnemyPortrait(result.GetComponent<AiCharacter>());
        }

        if (FindTarget(mousePosition, out result))
        {
            if (battleManager.state == BattleState.CharacterTurn)
            {
                if (selectionMask.value == LayerMask.GetMask("hex"))
                {
                    _cardActionManager.OnHexClick(result);
                }
                else if (selectionMask.value == LayerMask.GetMask("Monster") ||
                         selectionMask.value == LayerMask.GetMask("Character"))
                {
                    _cardActionManager.OnEntityClick(result);
                }
            }
        }
    }


    public IEnumerator CardActionChoose(CardAction cardAction)
    {
        lastSelectedCardAction = UtilsReference.utils.CopyCardAction(cardAction);
        lastSelectedCard = cardAction.CharacterCard;
        battleHud.skipActionButton.gameObject.SetActive(true);
        battleHud.ToggleCardsVisibility(false);
        if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
            CharacterActionType.Move) // For Move card abilities
        {
            if (lastSelectedCharacter.TotalConditionList.Exists(x =>
                    x.ApplicableCondition == ApplicableConditions.Immobilize))
            {
                battleHud.skipAction = true;
            }

            int bonusMove = lastSelectedCard.OnCardMoveValue(lastSelectedCharacter, null);
            _finalHexagon = null;
            selectionMask = LayerMask.GetMask("hex");
            List<Hexagon> radius = hexGrid.GetTileInRadius(lastSelectedCharacter.currentHexPosition,
                cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange + bonusMove);
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
                 CharacterActionType.Attack) // For Attack card abilities
        {
            List<ICharacter> targets = new List<ICharacter>();
            int extraTargets = lastSelectedCard.OnExtraTarget(lastSelectedCharacter);
            numberOfTargets = cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex]
                .NumberOfTargets + extraTargets;
            if (lastSelectedCharacter.TotalConditionList.Exists(x =>
                    x.ApplicableCondition == ApplicableConditions.Disarm))
            {
                battleHud.skipAction = true;
            }


            selectionMask = LayerMask.GetMask("Monster");
            List<Hexagon> radius = hexGrid.GetTileInRadius(lastSelectedCharacter.currentHexPosition,
                cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange);
            foreach (Hexagon hex in radius)
            {
                hex.EnableHighLight();
            }

//
            do
            {
                yield return new WaitUntil(() => _finalTarget != null || battleHud.skipAction);
                if (_finalTarget != null)
                {
                    targets.Add(_finalTarget.GetComponent<ICharacter>());
                }

                _finalTarget = null;
                _skipActionButton.ResetSkipActionButton();

                foreach (Hexagon hex in radius)
                {
                    hex.DisableHighLight();
                }
            } while (targets.Count >= numberOfTargets || battleHud.skipAction);
        }
        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.AttackSelf) // For Damage self card abilities 
        {
            selectionMask = LayerMask.GetMask("Character");
            List<Hexagon> radius = hexGrid.GetTileInRadius(lastSelectedCharacter.currentHexPosition,
                cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].ActionRange);
            foreach (Hexagon hex in radius)
            {
                hex.EnableHighLight();
            }

            battleHud.applyButton.gameObject.SetActive(true);
            if (battleHud.applyButton)
            {
                _finalTarget = lastSelectedCharacter.gameObject;
            }

            yield return new WaitUntil(() => _finalTarget != null || battleHud.skipAction);
            _finalTarget = null;
            _skipActionButton.ResetSkipActionButton();

            foreach (Hexagon hex in radius)
            {
                hex.DisableHighLight();
            }
        }
        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.Shield) // Shield card abilities
        {
            selectionMask = LayerMask.GetMask("Character");
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
        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.Heal) // Heal card abilities
        {
            selectionMask = LayerMask.GetMask("Character");
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
        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.Retaliate) // Retaliate card abilities
        {
            selectionMask = LayerMask.GetMask("Character");
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

        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.Buff)
        {
            List<LayerMask> layerMasks = new List<LayerMask>();
            layerMasks.Add(LayerMask.GetMask("Monster"));
            layerMasks.Add(LayerMask.GetMask("Character"));
            // selectionMask = LayerMask.GetMask("Character");
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
        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.BuffSelf)
        {
            _finalTarget = lastSelectedCharacter.gameObject;
            battleHud.DisplayButton(true);
            yield return new WaitUntil(() => battleHud.isConfirmed || battleHud.skipAction);
            battleHud.DisplayButton(false);
            yield return new WaitUntil(() => _finalTarget != null || battleHud.skipAction);
            _finalTarget = null;
            _skipActionButton.ResetSkipActionButton();
        }
        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.DebuffSelf)
        {
            _finalTarget = lastSelectedCharacter.gameObject;
            battleHud.DisplayButton(true);
            yield return new WaitUntil(() => battleHud.isConfirmed || battleHud.skipAction);
            battleHud.DisplayButton(false);
            yield return new WaitUntil(() => _finalTarget != null || battleHud.skipAction);
            _finalTarget = null;
            _skipActionButton.ResetSkipActionButton();
        }

        else if (cardAction.cardActionSequencesList[battleManager.currentActionSequenceIndex].CharacterActionType ==
                 CharacterActionType.Debuff)
        {
            List<LayerMask> layerMasks = new List<LayerMask>();
            layerMasks.Add(LayerMask.GetMask("Monster"));
            layerMasks.Add(LayerMask.GetMask("Character"));
            // selectionMask = LayerMask.GetMask("Character");
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

// Global function for card Selection from any deck. That uses ICharacter interface, GameObject and DeckType enum as parameters
    public void SelectCard(ICharacter character, GameObject displayedCard, DeckType deckType)
    {
        CharacterCard card = null;
        battleHud.CardDictionary.TryGetValue(displayedCard, out card);
        if (card != null)
        {
            switch (deckType)
            {
                case DeckType.Selected:
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

                    break;
                case DeckType.Active:
                    if (character.ActiveDeck.Exists(x => x == card))
                    {
                        character.ActiveDeck.Remove(card);
                        StopCoroutine(WaifForConfirmation());
                        battleHud.DisplayButton(false);
                    }
                    else if (character.ActiveDeck.Count < 2)
                    {
                        character.ActiveDeck.Add(card);

                        if (character.ActiveDeck.Count == 2)
                        {
                            StartCoroutine(WaifForConfirmation());
                            battleHud.DisplayButton(true);
                        }
                    }

                    break;
                case DeckType.Character:
                    if (character.CharacterGlobalDeck.Exists(x => x == card))
                    {
                        character.CharacterGlobalDeck.Remove(card);
                        StopCoroutine(WaifForConfirmation());
                        battleHud.DisplayButton(false);
                    }
                    else if (character.CharacterGlobalDeck.Count < 2)
                    {
                        character.CharacterGlobalDeck.Add(card);

                        if (character.CharacterGlobalDeck.Count == 2)
                        {
                            StartCoroutine(WaifForConfirmation());
                            battleHud.DisplayButton(true);
                        }
                    }

                    break;
                case DeckType.Discard:
                    if (character.DiscardDeck.Exists(x => x == card))
                    {
                        character.DiscardDeck.Remove(card);
                        StopCoroutine(WaifForConfirmation());
                        battleHud.DisplayButton(false);
                    }
                    else if (character.DiscardDeck.Count < 2)
                    {
                        character.DiscardDeck.Add(card);

                        if (character.DiscardDeck.Count == 2)
                        {
                            StartCoroutine(WaifForConfirmation());
                            battleHud.DisplayButton(true);
                        }
                    }

                    break;
                case DeckType.Lost:
                    if (character.LostDeck.Exists(x => x == card))
                    {
                        character.LostDeck.Remove(card);
                        StopCoroutine(WaifForConfirmation());
                        battleHud.DisplayButton(false);
                    }
                    else if (character.LostDeck.Count < 2)
                    {
                        character.LostDeck.Add(card);

                        if (character.LostDeck.Count == 2)
                        {
                            StartCoroutine(WaifForConfirmation());
                            battleHud.DisplayButton(true);
                        }
                    }

                    break;
                case DeckType.Hand:
                    if (character.HandDeck.Exists(x => x == card))
                    {
                        character.HandDeck.Remove(card);
                        StopCoroutine(WaifForConfirmation());
                        battleHud.DisplayButton(false);
                    }
                    else if (character.HandDeck.Count < 2)
                    {
                        character.HandDeck.Add(card);

                        if (character.HandDeck.Count == 2)
                        {
                            StartCoroutine(WaifForConfirmation());
                            battleHud.DisplayButton(true);
                        }
                    }

                    break;
            }
            // if (_playerTurnState == PlayerTurnState.CardHandSelection ||
            //     _playerTurnState == PlayerTurnState.CardRoundSelection)
            // {
            //     if (character.SelectedCards.Exists(x => x == card))
            //     {
            //         character.SelectedCards.Remove(card);
            //         StopCoroutine(WaifForConfirmation());
            //         battleHud.DisplayButton(false);
            //     }
            //     else if (character.SelectedCards.Count < 2)
            //     {
            //         character.SelectedCards.Add(card);
            //
            //         if (character.SelectedCards.Count == 2)
            //         {
            //             StartCoroutine(WaifForConfirmation());
            //             battleHud.DisplayButton(true);
            //         }
            //     }
            // }
        }
    }
    // Function that returns Selected Card from selected Deck. It uses ICharacter interface and DeckType enum as parameters.
    // Function returns selected CharacterCard object.
    public CharacterCard GetSelectedCard(ICharacter source, DeckType deckType)
    {
        
        switch (deckType)
        {
            case DeckType.Selected:
                for (int i = 0; i < source.SelectedCards.Count; i++)
                {
                    
                }
                return source.SelectedCards[0];
            case DeckType.Active:
                return source.ActiveDeck[0];
            case DeckType.Character:
                return source.CharacterGlobalDeck[0];
            case DeckType.Discard:
                return source.DiscardDeck[0];
            case DeckType.Lost:
                return source.LostDeck[0];
            case DeckType.Hand:
                return source.HandDeck[0];
            default:
                return null;
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

        battleHud.DisplayCards(character, DeckType.Hand);
    }


    IEnumerator HighlightHex()
    {
        for (int i = 0; i < _lastSelectedHexes.Count; i++)
        {
            _lastSelectedHexes[i].EnableHighLight();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
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

    private bool SelectionCharacter(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Character")) ||
            Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Monster")))
        {
            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}


public static class SelectionManagerReference
{
    public static SelectionManager selectionManager;
}