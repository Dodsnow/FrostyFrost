using System.Collections.Generic;
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

        EntityControllerType entityControllerType { get; set; }
    
    }
}

