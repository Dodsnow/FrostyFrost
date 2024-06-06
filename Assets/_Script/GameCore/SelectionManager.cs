using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public LayerMask selectionMask;
    public BattleManager battleManager;
    public HexGrid hexGrid;
    private List<Hexagon> _lastSelectedHexes = new List<Hexagon>();
    private GameObject _finalHex;
    private GameObject _startHex;
    public BattleHUD battleHud;
    private PlayerCharacter _lastSelectedCharacter;
    WaitUntil waitUntilConfirmed;
    private PlayerTurnState _playerTurnState;
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

            if (selectionMask == LayerMask.GetMask("Card"))
            {
                if (_playerTurnState == PlayerTurnState.CardSelection)
                {
                    Debug.Log("Card selection mask active");
                    result.name = result.name + " Selected";
                    SelectCard(_lastSelectedCharacter, result);
                    if (_lastSelectedCharacter.SelectedCards.Count >= 2)
                    {

                        battleHud.DisplayButton(true);
                        StartCoroutine(WaifForConfirmation());
                    }
                }
                else if (_playerTurnState == PlayerTurnState.CardAction)
                {
                    selectionMask = LayerMask.GetMask("CardAction");

                }

            }
            else if (selectionMask == LayerMask.GetMask("CardAction"))
            {
                StartCoroutine(CardActionChoose(result.GetComponent<CharacterCard>()));

            }

            else if (selectionMask == LayerMask.GetMask("hex"))
            {
                if (_startHex == null)
                {
                    _startHex = result;
                    _startHex.GetComponent<Hexagon>().EnableHighLight();
                }
                else if (_finalHex == null)
                {
                    _finalHex = result;
                    _finalHex.GetComponent<Hexagon>().EnableHighLight();

                    _lastSelectedHexes = AstarPathfinding.FindPath(_startHex.GetComponent<Hexagon>(),
                        _finalHex.GetComponent<Hexagon>());
                    // foreach (Hexagon hexes in _lastSelectedHexes)
                    // {

                    //     hexes.EnableHighLight();
                    // }
                    StartCoroutine(HighlightHex());
                }
                else
                {
                    if (_lastSelectedHexes != null)
                    {
                        foreach (Hexagon hexes in _lastSelectedHexes)
                        {
                            hexes.DisableHighLight();
                        }
                    }

                    _startHex = null;
                    _finalHex = null;
                }
            }

        }
    }

    private IEnumerator CardActionChoose(CharacterCard card)
    {
        GameObject result;
        if (FindTarget(Input.mousePosition, out result))
        {
            card = result.GetComponent<CharacterCard>();
            if (result.gameObject.CompareTag("TopAction"))
            {
                CardAction cardAction = card.TopCardAction;
                yield return waitUntilConfirmed;
            }
            else if (result.gameObject.CompareTag("BottomAction"))
            {
                yield return waitUntilConfirmed;
            }
        }
    }

    IEnumerator WaifForConfirmation()
    {
        waitUntilConfirmed = new WaitUntil(() => battleHud.isConfirmed);
        yield return waitUntilConfirmed;
        battleHud.RemoveDisplayCards();
        selectionMask = LayerMask.GetMask("Character");

    }
    public void SelectCard(PlayerCharacter character, GameObject displayedCard)
    {
        CharacterCard card = null;
        battleHud.cardDictionary.TryGetValue(displayedCard, out card);
        if (card != null)
        {
            if (character.SelectedCards.Exists(x => x == card))
            {
                character.SelectedCards.Remove(card);
                StopCoroutine(WaifForConfirmation());
                Debug.Log("Card removed");

            }
            else
            {
                character.SelectedCards.Add(card);
                Debug.Log("Card added");
                Debug.Log(card.cardName);
            }

        }




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

        if (Physics.Raycast(ray, out hit, selectionMask.value))
        {

            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}

