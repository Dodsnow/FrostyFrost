using System;
using System.Collections.Generic;
using _Script.ConditionalEffects;
using _Script.ConditionalEffects.Enum;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Script.ConditionalEffects
{
    [CreateAssetMenu(fileName = "CharacterConditionsDB", menuName = "ScriptableObjects/CharacterConditionsDB", order = 2)]

    public class CharacterConditionsDB : ScriptableObject
    {
       public List<ConditionTemplate> characterConditions = new List<ConditionTemplate>(); 
    }

    [Serializable]
    public class ConditionTemplate
    {
        public string ConditionID;
        public string ConditionName;
        public GameObject ConditionIconPrefab;
        public Sprite ConditionIcon;
        public ApplicableConditions ConditionType;
        public int ConditionValue;
        public bool isPositive;
        

    }
}

