using System.Collections.Generic;
using _Script.ConditionalEffects;
using _Script.PlayableCharacters;
using UnityEngine;
using UnityEngine.UI;

public class AiCharacter : MonoBehaviour, ICharacter
{
    public string CharacterName { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public MonsterType monsterType { get; set; }
    public List<CharacterCard> SelectedCards { get; set; }
    public Slider slider { get; set; }
    public Hexagon currentHexPosition { get; set; }
    public GameObject HpSlider { get; set; }
    public List<CharCondition> TurnStartConditionsList { get; set; } = new List<CharCondition>();
    public List<CharCondition> TurnEndConditionsList { get; set; } = new List<CharCondition>();
    public List<CharCondition> RoundEndConditionsList { get; set; } = new List<CharCondition>();
    public List<CharCondition> NeverEndingConditionsList { get; set; } = new List<CharCondition>();
    public List<CharCondition> TotalConditionList { get; set; } = new List<CharCondition>();
    public EntityControllerType entityControllerType { get; set; }
    public ClassType classType { get; set; }
    public GameObject playableEntity { get; set; }
    

    public List<CharacterCard> characterCards = new List<CharacterCard>();
    public List<CharacterCard> discardDeck = new List<CharacterCard>();

    private void FixedUpdate()
    {
        slider.transform.position = playableEntity.transform.position + new Vector3(0, 3, 0);
    }

}   
