using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Script.GameCore.City.Buildings
{
    
    public class GameBuilding : MonoBehaviour
    {
        public string buildingName  = "";
        public int level  = 1;
        public int maxLevel = 5;
        public List<float> upgradeBaseCost  = new List<float>();
        public List<float> upgrageCostPerLevel = new List<float>();
        public GameObject buildingWindowUI;
        [SerializeField] private TextMeshProUGUI[] _resourceTexts = new TextMeshProUGUI[3];
        
       
        public void UpgradeBuilding()
        {
            if (level < maxLevel)
            {
                level++;
            }
        }
        
        public void OnClick()
        {
            buildingWindowUI.SetActive(!buildingWindowUI.activeSelf);
            GameBuilding building = GetComponent<GameBuilding>();
            buildingWindowUI.transform.GetChild(0).transform.Find("Name").GetComponentInChildren<TextMeshProUGUI>().text = building.buildingName;
            buildingWindowUI.transform.GetChild(0).transform.Find("Level").GetComponentInChildren<TextMeshProUGUI>().text = building.level.ToString();
            for (int i = 0; i < upgradeBaseCost.Count - 1; i++)
            {
                _resourceTexts[i].text = CalculateUpgradeCost((ResourceType) i).ToString();
            }
            
           

        }
        
        private int CalculateUpgradeCost(ResourceType resourceType)
        {
            
            int upgradeCost;
            
                upgradeCost = (int)(upgradeBaseCost[(int)resourceType] + upgrageCostPerLevel[(int)resourceType] * level);
                
                Debug.Log(resourceType + " upgrade cost " + upgradeCost);


                return upgradeCost;
        }
        
    }
}