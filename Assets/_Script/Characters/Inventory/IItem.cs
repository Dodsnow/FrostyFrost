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
    
    public virtual void UseItem(ICharacter source, ICharacter target)
    {
            
    }
    public virtual void EquipItem(ICharacter source)
    {
       
    }

    public virtual void OnAttackModifier(ICharacter source)
    {
        
    }
}