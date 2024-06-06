using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public PlayerCharacterDB playerCharacter;
    public AiCharacterDB aiCharacter;
    public List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();
    public List<AiCharacter> aiCharacters = new List<AiCharacter>();



    public void SpawnPlayerCharacter(int playerCharacterID, Hexagon hex)
    {
        PlayerCharacterTemplate playerCharacterTemplate = playerCharacter.playerCharacters[playerCharacterID];
        GameObject character = Instantiate(playerCharacterTemplate.characterPrefab, hex.transform.position + new Vector3(0, 1, 0),
            Quaternion.identity);
        PlayerCharacter player = character.GetComponent<PlayerCharacter>();
        player.SelectedCards = new List<CharacterCard>();
        player.entityControllerType = EntityControllerType.Player;
        player.CharacterName = playerCharacterTemplate.characterName;
        player.MaxHealth = playerCharacterTemplate.maxHealth;
        player.CurrentHealth = player.MaxHealth;
        player.handSize = playerCharacterTemplate.handSize;
        playerCharacters.Add(player);
        hex.isOccupied = true;
    }

    public void SpawnAICharacter(int aiCharacterID, Hexagon hex)
    {
        AiCharacterTemplate aiCharacterTemplate = aiCharacter.aiCharacters[aiCharacterID];
        GameObject character = Instantiate(aiCharacterTemplate.characterPrefab, hex.transform.position + new Vector3(0, 1, 0),
            Quaternion.identity);
        AiCharacter ai = character.GetComponent<AiCharacter>();
        ai.SelectedCards = new List<CharacterCard>();
        ai.CharacterName = aiCharacterTemplate.characterName;
        ai.entityControllerType = EntityControllerType.AI;

        ai.MaxHealth = aiCharacterTemplate.maxHealth;
        ai.CurrentHealth = ai.MaxHealth;
        ai.monsterType = aiCharacterTemplate.monsterType;
        ai.characterCards = AiCardManager.aiCardsDictionary[ai.monsterType];
        aiCharacters.Add(ai);
        hex.isOccupied = true;




    }
}
