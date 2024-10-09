using System.Collections.Generic;
using _Script.PlayableCharacters;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;


public class SpawnManager : MonoBehaviour
{
    public PlayerCharacterDB playerCharacter;
    public AiCharacterDB aiCharacter;
    public List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();
    public List<AiCharacter> aiCharacters = new List<AiCharacter>();
    public GameObject HpSliderPrefab;
    public BattleHUD battleHud;
    


    public void SpawnPlayerCharacter(int playerCharacterID, Hexagon hex)
    {
        PlayerCharacterTemplate playerCharacterTemplate = playerCharacter.playerCharacters[playerCharacterID];
        GameObject character = Instantiate(playerCharacterTemplate.characterPrefab, hex.transform.position + new Vector3(0, 1, 0),
            Quaternion.identity);
        PlayerCharacter player = character.GetComponent<PlayerCharacter>();
        player.currentHexPosition = hex;
        player.playableEntity = character;
        player.classType = playerCharacterTemplate.classType;
        player.SelectedCards = new List<CharacterCard>();
        player.entityControllerType = EntityControllerType.Player;
        player.CharacterName = playerCharacterTemplate.characterName;
        player.MaxHealth = playerCharacterTemplate.maxHealth;
        player.CurrentHealth = player.MaxHealth;        
        player.HpSlider = Instantiate(HpSliderPrefab, hex.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        player.slider = player.HpSlider.GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
        player.slider.maxValue = player.MaxHealth;
        player.HpSlider.GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>().value = player.CurrentHealth;
        player.HpSlider.GetComponentInChildren<Canvas>().renderMode = RenderMode.WorldSpace;
        player.HpSlider.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        player.handSize = playerCharacterTemplate.handSize;
        player.CharacterIconSprite = playerCharacterTemplate.characterIcon;
        player.HandDeck = playerCharacterTemplate.characterCards[playerCharacterTemplate.classType];
        // player.classCardPrefab = playerCharacterTemplate.characterCardPrefab;
        // IItem tempItem = PlayerInventory.Inventory[0];
        Debug.Log("Player inventory number of Items :" + PlayerInventory.Inventory.Count);
        // Debug.Log(tempItem.ItemName);
        foreach (GameItem item in PlayerInventory.Inventory)
        {
            
            Debug.Log("Equipping item" + item.ItemName);

            if (item.isEquippable)
            {
                player.EquippedInventory.Add(item);
                item.EquipItem(player);
            }
        }
        battleHud.ShowBagItems();
        // battleHud.ShowEquippedItems();
        playerCharacters.Add(player);
        hex.isOccupied = true;
        hex.characterOnHex = player;


    }

    public void SpawnAICharacter(int aiCharacterID, Hexagon hex)
    {
        AiCharacterTemplate aiCharacterTemplate = aiCharacter.aiCharacters[aiCharacterID];
        GameObject character = Instantiate(aiCharacterTemplate.characterPrefab, hex.transform.position + new Vector3(0, 1, 0),
            Quaternion.identity);
        AiCharacter ai = character.GetComponent<AiCharacter>();
        ai.classType = ClassType.AISkeleton;
        ai.playableEntity = character;
        ai.SelectedCards = new List<CharacterCard>();
        ai.currentHexPosition = hex;
        ai.CharacterName = aiCharacterTemplate.characterName;
        ai.entityControllerType = EntityControllerType.AI;
        ai.MaxHealth = aiCharacterTemplate.maxHealth;
        ai.CurrentHealth = ai.MaxHealth;
        ai.HpSlider = Instantiate(HpSliderPrefab, hex.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        ai.slider = ai.HpSlider.GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
        ai.slider.maxValue = ai.MaxHealth;
        ai.HpSlider.GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>().value = ai.CurrentHealth;
        ai.HpSlider.GetComponentInChildren<Canvas>().renderMode = RenderMode.WorldSpace;
        ai.HpSlider.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        ai.CharacterGlobalDeck = AiCardManager.aiCardsDictionary[ai.classType];
        aiCharacters.Add(ai);
        ai.CharacterIconSprite = aiCharacterTemplate.characterIcon;
        hex.isOccupied = true;
        hex.characterOnHex = ai;





    }
}
