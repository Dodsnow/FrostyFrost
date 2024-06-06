
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using Random = System.Random;


public class BattleManager : MonoBehaviour
{
    public BattleState state;
    private List<Character> _characters = new List<Character>();
    private int roundNumber = 1;
    public BattleHUD battleHUD;
    public HexGrid grid;
    public SpawnManager spawnManager;
    public SelectionManager selectionManager;
    private List<Character> characterInitiativeList = new List<Character>();
    public bool characterTurnEnd = false;






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
        state = BattleState.PreSelection;
        Debug.Log(state);
    }

    private void Update()
    {
        if (state == BattleState.PreSelection)
        {
            SelectCharacter();


            StartCoroutine(RoundStart());
        }


        else if (state == BattleState.RoundEnd)
        {
            // StartCoroutine(EndRound());
        }
    }
    private void SelectCharacter()
    {
        selectionManager.selectionMask = LayerMask.GetMask("Character");

        WaitUntil waitUntilConfirmed = new WaitUntil(() => battleHUD.isConfirmed);
            state = BattleState.CardSelection;
    }

    private void SelectCard(PlayerCharacter playerCharacter)
    {
        selectionManager.selectionMask = LayerMask.GetMask("Card");

        battleHUD.DisplayCharacterHandDeck(playerCharacter);
    }

    IEnumerator RoundStart()
    {
        int currentCharacterIndex = 0;
        foreach (PlayerCharacter playerCharacter in spawnManager.playerCharacters)
        {
            SelectCard(playerCharacter);
            yield return new WaitUntil(() => battleHUD.isConfirmed);
        }
        RandomAiCardSelection();
        BattleInitiativeOrder();
        for (int i = 0; i < characterInitiativeList.Count; i++)
        {
            if (characterInitiativeList[i].entityControllerType == EntityControllerType.Player)
            {
                selectionManager.selectionMask = LayerMask.GetMask("Card");
                battleHUD.DisplayCharacterSelectedCards(characterInitiativeList[currentCharacterIndex] as PlayerCharacter);
                yield return new WaitUntil(() => characterTurnEnd);

            }
            
        }

        yield return true;

    }

    private void CharacterTurn(PlayerCharacter playerCharacter)
    {
        if (playerCharacter.SelectedCards.Count <= 0) characterTurnEnd = true;
        else
        {
            state = BattleState.CharacterTurn;
            
        }
    }
    private void RandomAiCardSelection()
    {
        foreach (AiCharacter aiCharacter in spawnManager.aiCharacters)
        {
            aiCharacter.SelectedCards.Add(aiCharacter.characterCards[UnityEngine.Random.Range(0, aiCharacter.characterCards.Count)]);

        }
    }
    private void BattleInitiativeOrder()
    {
        foreach (Character character in _characters)
        {
            characterInitiativeList.Add(character);

        }
        characterInitiativeList = characterInitiativeList.OrderBy(x => x.SelectedCards[0].initiative).ToList();

    }

}
