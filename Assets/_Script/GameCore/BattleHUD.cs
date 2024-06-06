using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{

    public Button applyButton;
    public bool isConfirmed = false;
    public List<GameObject> displayCards = new List<GameObject>();
    public Dictionary<GameObject, CharacterCard> cardDictionary = new Dictionary<GameObject, CharacterCard>();
    public Dictionary<ClassType, GameObject> cardClassPrefab = new Dictionary<ClassType, GameObject>();


    public void DisplayCharacterHandDeck(PlayerCharacter playerCharacter)
    {

        for (int i = 0; i < playerCharacter.handDeck.Count; i++)
        {
            GameObject card = CreateCardVisual(cardClassPrefab[ClassType.Berserker], new Vector3(-3, 4, 10) + new Vector3(5 * i, 0, 0), playerCharacter.handDeck[i]);
            // card.transform.SetParent(GameObject.Find("Canvas").transform, false);
            // AnchorObject(card);
            cardDictionary.Add(card, playerCharacter.handDeck[i]);
            displayCards.Add(card);
        }

    }

    public void DisplayCharacterSelectedCards(PlayerCharacter playerCharacter)
    {
        for (int i = 0; i < playerCharacter.SelectedCards.Count; i++)
        {
            GameObject card = CreateCardVisual(cardClassPrefab[ClassType.Berserker], new Vector3(-3, 4, 10) + new Vector3(5 * i, 0, 0), playerCharacter.SelectedCards[i]);
            // card.transform.SetParent(GameObject.Find("Canvas").transform, false);
            // AnchorObject(card);
            cardDictionary.Add(card, playerCharacter.SelectedCards[i]);
            displayCards.Add(card);
        }
    }

    private GameObject CreateCardVisual(GameObject cardPrefab, Vector3 position, CharacterCard card)
    {
        GameObject cardUI = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        cardUI.transform.Find("CardName").GetComponent<TextMeshPro>().text = card.cardName;
        cardUI.transform.Find("TopAction").GetComponent<TextMeshPro>().text = card.TopCardAction.Discription;
        cardUI.transform.Find("BottomAction").GetComponent<TextMeshPro>().text = card.BottomCardAction.Discription;
        cardUI.transform.Find("Initiative").GetComponent<TextMeshPro>().text = card.initiative.ToString();
        cardUI.transform.position = position;
        return cardUI;

    }
    void AnchorObject(GameObject card)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        cardRectTransform.anchorMax = new Vector2(0, 1);
        cardRectTransform.anchorMin = new Vector2(0, 1);
        cardRectTransform.pivot = new Vector2(0, 1);

    }

    public void RemoveDisplayCards()
    {
        foreach (GameObject card in displayCards)
        {
            Destroy(card);
        }
        displayCards.Clear();
        cardDictionary.Clear();
        DisplayButton(false);
        isConfirmed = false;
    }
    public void DisplayButton(bool flag)
    {
        applyButton.gameObject.SetActive(flag);
    }

    public void ConfirmButton()
    {
        isConfirmed = !isConfirmed;
    }

    private void Start()
    {
        cardClassPrefab[ClassType.Berserker] = FindObjectOfType<CardReferences>()._cardPrefabs[0];

    }


}