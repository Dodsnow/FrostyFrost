using System.Collections.Generic;
using UnityEngine;

namespace _Script.GameCore.City.Buildings
{
    public interface IBuilding
    {
        string Name { get; set; }
        int Level { get; set; }
        int MaxLevel { get; set; }
        List<float> UpgradeBaseCost { get; set; }
        List<float> UpgrageCostPerLevel { get; set; }
    }
}