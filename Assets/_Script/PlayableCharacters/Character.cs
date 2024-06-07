using System;
using System.Collections.Generic;
using UnityEngine;

public interface Character
{
    string CharacterName { get; set; }
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
    List<CharacterCard> SelectedCards { get; set; }
 

    EntityControllerType entityControllerType { get; set; }
    
}

