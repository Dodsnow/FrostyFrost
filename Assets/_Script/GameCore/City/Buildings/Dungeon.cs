using System.Collections.Generic;
using UnityEngine;

namespace _Script.GameCore.City.Buildings
{
    public class Dungeon : MonoBehaviour, IBuilding
    {
        public string Name { get; set; } = "Dungeon";
        public int Level { get; set; } = 1;
        public int MaxLevel { get; set; } = 10;
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