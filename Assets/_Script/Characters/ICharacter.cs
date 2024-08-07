using System.Collections.Generic;
using _Script.ConditionalEffects;
using UnityEditor;
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
        List<GameItem> BackpackInventory { get; set; }
        List<GameItem> EquippedInventory { get; set; }
        Sprite CharacterIconSprite { get; set; }
        bool isDead { get; set; }
        int Level { get; set; }
        GameObject classCardPrefab { get; set; }

        EntityControllerType entityControllerType { get; set; }

        public void CharacterDeath()
        {
            foreach (CharCondition condition in TotalConditionList)
            {
                GameObject.Destroy(condition.Icon);
            }
            GameObject.Destroy(HpSlider);
           
            playableEntity.GetComponent<Animator>().SetTrigger("isDead"); 
            GameObject.Destroy(playableEntity);
            currentHexPosition.isOccupied = false;
            
        }

        public void ModifyHealth(int amount,bool overHeal = false)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth && !overHeal)
            {
                CurrentHealth = MaxHealth;
            }

            slider.value = CurrentHealth;
        }
    }
}