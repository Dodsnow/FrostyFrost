using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Script.GameCore.City.Buildings
{
    [Serializable]
    public class GameBuilding : MonoBehaviour
    {
        public string Name  = "";
        public int Level  = 1;
        public int MaxLevel = 5;
        public List<float> UpgradeBaseCost  = new List<float>();
        public List<float> UpgrageCostPerLevel = new List<float>();
        public GameObject buildingWindowUI;
       
        
        public void UpgradeBuilding()
        {
            if (Level < MaxLevel)
            {
                Level++;
            }
        }
        
        public void OnClick()
        {
            buildingWindowUI.SetActive(!buildingWindowUI.activeSelf);
        }
    }
}