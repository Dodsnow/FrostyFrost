using System;
using System.Collections.Generic;
using _Script.ConditionalEffects;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;
using _Script.PlayableCharacters.Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{
    public Button applyButton;
    public Button skipActionButton;
    public Button inventorySwapButton;
    public bool skipAction = false;
    public bool isConfirmed = false;
    public List<GameObject> displayCards = new List<GameObject>();
    public Dictionary<GameObject, CharacterCard> CardDictionary = new Dictionary<GameObject, CharacterCard>();
    public Dictionary<ClassType, GameObject> CardClassPrefab = new Dictionary<ClassType, GameObject>();
    public Dictionary<CardAction, CharacterCard> CardActionDictionary = new Dictionary<CardAction, CharacterCard>();
    public GlowHighLight glowHighLight;
    public GameObject[] inventorySlots = new GameObject[10];
    public ItemSlotUI[] itemSlotUI = new ItemSlotUI[10];
    public GameObject[] bagSlots = new GameObject[6];
    public SelectionManager selectionManager;
    private PlayerCharacter _playerCharacter;
    private List<GameObject> _characterIconsList = new List<GameObject>();
    [SerializeField] GameObject _characterInitiativePanel;
    [SerializeField] private GameObject _characterIconPrefab;
    [SerializeField] private GameObject _characterPortrait;
    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private TextMeshProUGUI _characterHealth;
    [SerializeField] private TextMeshProUGUI _characterLevel;
    [SerializeField] private GameObject _enemyPortrait;
    [SerializeField] private TextMeshProUGUI _enemyName;
    [SerializeField] private TextMeshProUGUI _enemyHealth;
    [SerializeField] private TextMeshProUGUI _enemyLevel;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _characterCardCanvas;
    [SerializeField] private GameObject _consumablesInventory;
    [SerializeField] private CharacterConditionsDB _characterConditionsDB;
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
            // GameObject card = CreateCardVisual(CardClassPrefab[character.classType],
            //     new Vector3(0, 3.25f, 10) + new Vector3(3.5f * i, 0, 0), character.SelectedCards[i]);
            GameObject card = CreateCardVisual(CardClassPrefab[character.classType], _characterCardCanvas.transform.position + new Vector3(240 * i,0,0),
                character.SelectedCards[i]);

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
        if (playerCharacter.handDeck.Count <= 0)
        {
            Debug.LogWarning("Player is out of cards!");
        }

        for (int i = 0; i < playerCharacter.handDeck.Count; i++)
        {
            if (playerCharacter.handDeck[i] != null)
            {
                // GameObject card = CreateCardVisual(CardClassPrefab[playerCharacter.classType],
                //     new Vector3(0, 3.25f, 10) + new Vector3(3.5f * i, 0, 0), playerCharacter.handDeck[i]);
                GameObject card = CreateCardVisual(CardClassPrefab[playerCharacter.classType],
                    _characterCardCanvas.transform.position + new Vector3(240 * i,0,0), playerCharacter.handDeck[i]);
                CardDictionary.Add(card, playerCharacter.handDeck[i]);
                displayCards.Add(card);
            }
            else
            {
                Debug.LogWarning("Player is out of cards!");
            }
        }
    }

    private GameObject CreateCardVisual(GameObject cardPrefab, Vector3 position, CharacterCard card)
    {
        
        GameObject cardUI = Instantiate(cardPrefab,new Vector3(_characterCardCanvas.transform.position.x,_characterCardCanvas.transform.position.y,_characterCardCanvas.transform.position.z), Quaternion.identity);
        cardUI.transform.SetParent(GameObject.Find("Canvas").transform, false);


        cardUI.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = card.cardName;
        cardUI.transform.Find("TopAction").GetComponent<TextMeshProUGUI>().text = card.TopCardAction.Discription;
        if (card.TopCardAction.cardActionSequencesList.Find(x => x.Conditions != null) != null)
        {
            foreach (var condition in card.TopCardAction.cardActionSequencesList.Find(x => x.Conditions != null)
                         .Conditions)
            {
                for (int i = 0; i < card.TopCardAction.cardActionSequencesList.Count; i++)
                {

                    Debug.Log("Condition found: " + card.TopCardAction.cardActionSequencesList
                        .Find(x => x.Conditions != null).Conditions[i]);
                    cardUI.transform.Find("TopAction").transform.Find("Condition").gameObject.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            cardUI.transform.Find("TopAction").GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (card.BottomCardAction != null)
        {
            cardUI.transform.Find("BottomAction").GetComponent<TextMeshProUGUI>().text = card.BottomCardAction.Discription;
        }
        else
        {
            cardUI.transform.Find("BottomAction").GetComponent<TextMeshProUGUI>().text = "";
        }

        cardUI.transform.Find("Initiative").GetComponent<TextMeshProUGUI>().text = card.initiative.ToString();
        cardUI.transform.position = position;
        return cardUI;
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
                if (card.BottomCardAction != null)
                {
                    CardActionDictionary.Remove(card.BottomCardAction);
                }

                break;
            }
        }
    }
    public void ToggleCardsVisibility(bool flag)
    {
        foreach (GameObject card in displayCards)
        {
            card.SetActive(flag);
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
        BattleHUDReference.battleHUD = this;
    }

    private void Start()
    {
        CardReferences cardReferences = FindObjectOfType<CardReferences>();
        CardClassPrefab[ClassType.Berserker] = cardReferences._cardPrefabs[0];
        CardClassPrefab[ClassType.AISkeleton] = cardReferences._cardPrefabs[1];

        itemSlotUI[0] = new ItemSlotUI(inventorySlots[0], ItemType.Helm);
        itemSlotUI[1] = new ItemSlotUI(inventorySlots[1], ItemType.Armor);
        itemSlotUI[2] = new ItemSlotUI(inventorySlots[2], ItemType.Gloves);
        itemSlotUI[3] = new ItemSlotUI(inventorySlots[3], ItemType.MainHand);
        itemSlotUI[4] = new ItemSlotUI(inventorySlots[4], ItemType.OffHand);
        itemSlotUI[5] = new ItemSlotUI(inventorySlots[5], ItemType.Pants);
        itemSlotUI[6] = new ItemSlotUI(inventorySlots[6], ItemType.Boots);
        
    }


    public void SkipActionButton()
    {
        skipAction = true;
    }

    public GameObject DisplayConditionIcon(ICharacter character, GameObject conditionIconPrefab)
    {
        Slider slider = character.HpSlider.GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
        GameObject icon = Instantiate(conditionIconPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        icon.transform.SetParent(slider.transform, false);


        return icon;
    }

    public void ShowEquippedItems()
    {
        for (int x = 0; x < 6; x++)
        {
            foreach (var item in PlayerInventory.Inventory)
            {
                if (item.ItemType == itemSlotUI[x].ItemType)
                {
                    itemSlotUI[x].SetItemIcon(item.ItemIcon);
                    Debug.Log("ShowEquippedItems - type is found - " + x + " " + item.ItemName);
                }
            }
        }
    }

    public void ShowBagItems()
    {
        for (int x = 0; x < PlayerInventory.Bag.Count; x++)
        {
            if (PlayerInventory.Bag[x] != null)
            {
                bagSlots[x].GetComponent<Image>().sprite = PlayerInventory.Bag[x].ItemIcon;
                bagSlots[x].tag = "ConsumableItem";
                bagSlots[x].GetComponent<BagSlotUI>().item = PlayerInventory.Bag[x];
            }
           
        }
    }

    public void SortConditions(ICharacter character)
    {
        if (character.TotalConditionList.Count < 1)
        {
            return;
        }

        Slider slider = character.HpSlider.GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
        if (character.TotalConditionList.Count > 1)
        {
            float startingX;
            if (character.TotalConditionList.Count % 2 == 0)
            {
                startingX = character.TotalConditionList.Count / 2f * 0.35f * -1f;
            }
            else
            {
                startingX = Mathf.Ceil(character.TotalConditionList.Count / 2) * 0.7f * -1f;
            }

            character.TotalConditionList[0].Icon.transform.position =
                slider.transform.position + new Vector3(startingX, 1, 0);
            for (int x = 1; x < character.TotalConditionList.Count; x++)
            {
                character.TotalConditionList[x].Icon.transform.position =
                    character.TotalConditionList[x - 1].Icon.transform.position + new Vector3(0.7f, 0, 0);
            }
        }
        else
        {
            character.TotalConditionList[0].Icon.transform.position = slider.transform.position + new Vector3(0, 1, 0);
        }
    }

    public void SetInitiativeHud(ICharacter character)
    {
        GameObject initiativeIcon = Instantiate(_characterIconPrefab, _characterInitiativePanel.transform.position,
            Quaternion.identity);
        _characterIconsList.Add(initiativeIcon);
        initiativeIcon.GetComponentInChildren<Image>().sprite = character.CharacterIconSprite;
        initiativeIcon.transform.SetParent(_characterInitiativePanel.transform);
        initiativeIcon.GetComponentInChildren<RectTransform>().pivot.Set(0.5f, 0.5f);
    }
    public void RemoveObjectsFromInitiativeHud(ICharacter character)
    {
        foreach (GameObject icon in _characterIconsList)
        {
            if (icon.GetComponentInChildren<Image>().sprite == character.CharacterIconSprite)
            {
                Destroy(icon);
                _characterIconsList.Remove(icon);
                break;
            }
        }
    }
    
    public void SortInitiativeHud()
    {
        if (_characterIconsList.Count < 1)
        {
            return;
        }

        if (_characterIconsList.Count > 1)
        {
            float startingX;
            if (_characterIconsList.Count % 2 == 0)
            {
                startingX = _characterIconsList.Count / 2f * 50 * -1f;
            }
            else
            {
                startingX = Mathf.Ceil(_characterIconsList.Count / 2) * 100 * -1f;
            }

            _characterIconsList[0].transform.position =
                _characterInitiativePanel.transform.position + new Vector3(startingX, -100, 0);
            for (int x = 1; x < _characterIconsList.Count; x++)
            {
                _characterIconsList[x].transform.position =
                    _characterIconsList[x - 1].transform.position + new Vector3(100, 0, 0);
            }
        }
        else
        {
            _characterIconsList[0].transform.position =
                _characterInitiativePanel.transform.position + new Vector3(0, 0, 0);
        }
    }

    public void ChangeCharacterPortrait(PlayerCharacter character)
    {
        _characterPortrait.GetComponent<Image>().sprite = character.CharacterIconSprite;
        _characterName.text = character.CharacterName;
        _characterHealth.text = character.CurrentHealth + "/" + character.MaxHealth;
        _characterLevel.text = "Level " + character.Level;
    }

    public void ChangeEnemyPortrait(AiCharacter character)
    {
        _enemyPortrait.GetComponent<Image>().sprite = character.CharacterIconSprite;
        _enemyName.text = character.CharacterName;
        _enemyHealth.text = character.CurrentHealth + "/" + character.MaxHealth;
        _enemyLevel.text = "Level " + character.Level;
    }

    public void UpdatePortraitHp(ICharacter character)
    {
        if (character is PlayerCharacter)
        {
            _characterHealth.text = character.CurrentHealth + "/" + character.MaxHealth;
        }
        else
        {
            _enemyHealth.text = character.CurrentHealth + "/" + character.MaxHealth;
        }
    }

    public void InventorySwapButton()
    {
        _consumablesInventory.SetActive(!_consumablesInventory.gameObject.activeSelf);
        
    }
}

public static class BattleHUDReference
{
    public static BattleHUD battleHUD;
}