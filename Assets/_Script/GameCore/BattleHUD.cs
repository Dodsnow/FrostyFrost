using System;
using System.Collections.Generic;
using _Script.PlayableCharacters;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{
    public Button applyButton;
    public Button skipActionButton;
    public bool skipAction = false;
    public bool isConfirmed = false;
    public List<GameObject> displayCards = new List<GameObject>();
    public Dictionary<GameObject, CharacterCard> CardDictionary = new Dictionary<GameObject, CharacterCard>();
    public Dictionary<ClassType, GameObject> CardClassPrefab = new Dictionary<ClassType, GameObject>();
    public Dictionary<CardAction,CharacterCard > CardActionDictionary = new Dictionary<CardAction,CharacterCard>();
    public GlowHighLight glowHighLight;
    // [SerializeField] private SelectionManager _selectionManager;


    //TODO MAKE IT WORK, BITCH!!!!!
    // public void DisplaySelectedCard(ICharacter character)
    // {
    //     GameObject card = CreateCardVisual(CardClassPrefab[character.classType],
    //         new Vector3(-3, 4, 10) + new Vector3(5, 0, 0), _selectionManager.lastSelectedCard);
    //     CardDictionary.Add(card, _selectionManager.lastSelectedCard);
    //     CardActionDictionary.Add(_selectionManager.lastSelectedCard.TopCardAction, _selectionManager.lastSelectedCard);
    //     if (_selectionManager.lastSelectedCard.BottomCardAction != null)
    //     {
    //         CardActionDictionary.Add(_selectionManager.lastSelectedCard.BottomCardAction, _selectionManager.lastSelectedCard);
    //
    //     }
    //
    //     displayCards.Add(card);
    // }
    public void DisplaySelectedCards(ICharacter character)
    {
        for (int i = 0; i < character.SelectedCards.Count; i++)
        {
            GameObject card = CreateCardVisual(CardClassPrefab[character.classType], new Vector3(-3, 4, 10) + new Vector3(5 * i, 0, 0), character.SelectedCards[i]);
            
            CardDictionary.Add(card, character.SelectedCards[i]);
            CardActionDictionary.Add(character.SelectedCards[i].TopCardAction, character.SelectedCards[i]);
                
                if (character.SelectedCards[i].BottomCardAction != null)
                {
                    CardActionDictionary.Add(character.SelectedCards[i].BottomCardAction, character.SelectedCards[i]);
                }

            displayCards.Add(card);
        }
    }


    public void DisplayCharacterHandDeck(PlayerCharacter playerCharacter)
    {

        if (playerCharacter.handDeck.Count <= 0) {
4            Debug.LogWarning("Player is out of cards!");
        }

        for (int i = 0; i < playerCharacter.handDeck.Count; i++)
        {
            if (playerCharacter.handDeck[i] != null) 
            {
                GameObject card = CreateCardVisual(CardClassPrefab[playerCharacter.classType], new Vector3(-3, 4, 10) + new Vector3(5 * i, 0, 0), playerCharacter.handDeck[i]);
                CardDictionary.Add(card, playerCharacter.handDeck[i]);
                displayCards.Add(card); 
            }
            else {
                Debug.LogWarning("Player is out of cards!");
            }
        }
    }

    private GameObject CreateCardVisual(GameObject cardPrefab, Vector3 position, CharacterCard card)
    {
        GameObject cardUI = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        cardUI.transform.Find("CardName").GetComponent<TextMeshPro>().text = card.cardName;
        cardUI.transform.Find("TopAction").GetComponent<TextMeshPro>().text = card.TopCardAction.Discription;

            if (card.BottomCardAction != null)
            {
                cardUI.transform.Find("BottomAction").GetComponent<TextMeshPro>().text = card.BottomCardAction.Discription;
            }
            else
            {
                cardUI.transform.Find("BottomAction").GetComponent<TextMeshPro>().text = "";
            }

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
        CardDictionary.Clear();
        CardActionDictionary.Clear();
        DisplayButton(false);
    }

    public void RemoveDisplayCard(CharacterCard card)
    {
        foreach (GameObject cardObject in CardDictionary.Keys)
        {
            
            Debug.Log(CardDictionary[cardObject].cardName);
            if (CardDictionary[cardObject] == card)
            {
                Destroy(cardObject);
                CardDictionary.Remove(cardObject);
                displayCards.Remove(cardObject);
                CardActionDictionary.Remove(card.TopCardAction);
                if(card.BottomCardAction != null)
                {
                    CardActionDictionary.Remove(card.BottomCardAction);
                }
                break;
            }
        }
        
    }
    
    public void DisplayButton(bool flag)
    {
        applyButton.gameObject.SetActive(flag);
        isConfirmed = false;
        Debug.Log("DisplayButton - Show");
    }

    public bool ConfirmButton()
    {
        isConfirmed = !isConfirmed;
        return isConfirmed;
    }

    private void Awake()
    {
        DisplayButton(false);
    }

    private void Start()
    {
        CardReferences cardReferences = FindObjectOfType<CardReferences>();
        CardClassPrefab[ClassType.Berserker] = cardReferences._cardPrefabs[0];
        CardClassPrefab[ClassType.AISkeleton] = cardReferences._cardPrefabs[1];
    }

    public void SkipActionButton()
    {
        skipAction = true;
    }
}