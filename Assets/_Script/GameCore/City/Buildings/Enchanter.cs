using System.Collections.Generic;
using UnityEngine;

namespace _Script.GameCore.City.Buildings
{
    public class Enchanter : MonoBehaviour, IBuilding
    {
        public string Name { get; set; } = "Enchanter";
        public int Level { get; set; } = 1;
        public int MaxLevel { get; set; } = 5;
        public List<float> UpgradeBaseCost { get; set; } = new () {
            [(int)ResourceType.Gold] = 2,
            [(int)ResourceType.Wood] = 2,
            [(int)ResourceType.Metal] = 2,
            [(int)ResourceType.Hide] = 1
                
        };
        public List<float> UpgrageCostPerLevel { get; set; } = new () {
            [(int)ResourceType.Gold] = 2,
            [(int)ResourceType.Wood] = 1.5f,
            [(int)ResourceType.Metal] = 2,
            [(int)ResourceType.Hide] = 0.5f
                
        };
    }
}