using System.Collections.Generic;
using UnityEngine;

public class AiCharacter : MonoBehaviour, Character
{
    public string CharacterName { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public MonsterType monsterType { get; set; }
    public List<CharacterCard> SelectedCards { get; set; }
    public EntityControllerType entityControllerType { get; set; }
    

    public List<CharacterCard> characterCards = new List<CharacterCard>();
    public List<CharacterCard> discardDeck = new List<CharacterCard>();


}
