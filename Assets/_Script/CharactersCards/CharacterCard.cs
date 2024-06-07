using UnityEngine;

public interface CharacterCard
{
    string cardName { get; set; }
    int initiative { get; set; }
    CardAction TopCardAction { get; set; }

    CardAction BottomCardAction { get; set; }
    
    
}