using System.Collections.Generic;
using _Script.ConditionalEffects;
using UnityEngine;
using UnityEngine.UI;

namespace _Script.PlayableCharacters
{
    public interface ICharacter
    {
        string CharacterName { get; set; }
        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        List<CharacterCard> SelectedCards { get; set; }
        Slider slider { get; set; }
        Hexagon currentHexPosition { get; set; }
        ClassType classType { get; set; }
        GameObject playableEntity { get; set; }
        GameObject HpSlider { get; set; }
        List<CharCondition> TurnStartConditionsList { get; set; }
        List<CharCondition> TurnEndConditionsList { get; set; }
        List<CharCondition> RoundEndConditionsList { get; set; }
        List<CharCondition> NeverEndingConditionsList { get; set; }
        List<CharCondition> TotalConditionList { get; set; }

        EntityControllerType entityControllerType { get; set; }
    
    }
}

