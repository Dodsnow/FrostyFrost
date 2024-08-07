using _Script.PlayableCharacters;
using UnityEngine;


    public class TestHealingPotion : GameItem
    {
 
        
        public void UseItem(ICharacter source, ICharacter target)
        {
            Debug.Log("Healing Potion");
         target.ModifyHealth(5);
        }
    }
