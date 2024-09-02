
    using _Script.PlayableCharacters;
    using UnityEngine;

    public class GameItem : IItem
    {
        public int ItemCost { get; set; }
        public Sprite ItemIcon { get; set; }
        public string ItemName { get; set; }
        public ItemUsageType ItemUsageType { get; set; }
        public ItemType ItemType { get; set; }
        public string ItemDescription { get; set; }
        public bool isEquippable { get; set; }
        
        public virtual void UseItem(ICharacter source, ICharacter target)
        {
            Debug.Log("Gameitem has been used");
        }

        public virtual void EquipItem(ICharacter source)
        {
        
        }

        public virtual void OnAttackModifier(ICharacter source)
        {
            
        }
    }
