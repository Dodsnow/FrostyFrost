using _Script.PlayableCharacters;
using UnityEngine;

public interface IItem
{
    int ItemCost { get; set; }
    Sprite ItemIcon { get; set; }
    string ItemName { get; set; }
    ItemUsageType ItemUsageType { get; set; }
    ItemType ItemType { get; set; }
    string ItemDescription { get; set; }
    bool isEquippable { get; set; }
    
    public void UseItem(ICharacter source, ICharacter target)
    {
            
    }
    public void EquipItem(ICharacter source)
    {
       
    }

    public void OnAttackModifier(ICharacter source)
    {
        
    }
}