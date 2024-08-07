using _Script.PlayableCharacters;
using UnityEngine;


    
    
    public class TestArmor : GameItem
    {
        
        public void EquipItem(ICharacter source)
        {
            source.MaxHealth += 5;
        }
    }
