using _Script.ConditionalEffects.Enum;
using UnityEngine;

namespace _Script.ConditionalEffects
{
    public class CharCondition 
    {
        public string ConditionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GameObject Icon { get; set; }
        public int Priority { get; set; }
        public bool isPositive { get; set; }
        public ApplicableConditions ApplicableCondition { get; set; }
        public int ConditionValue { get; set; }
    }
}