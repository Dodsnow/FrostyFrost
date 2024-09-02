using _Script.PlayableCharacters;
using UnityEngine;


    
    
    public class TestArmor : GameItem
    {
        
        public override void EquipItem(ICharacter source)
        {
            source.MaxHealth += 5;
            source.CurrentHealth += 5;
            Debug.LogWarning("Equip armor has been executed");
        }
    }
