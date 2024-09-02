using System;
using System.Collections.Generic;
using _Script.GameCore.City.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Script.GameCore.City
{
    public class CityManager : MonoBehaviour
    {
        private Dictionary<ResourceType, int> _resources = new();
        private Dictionary<ResourceType, GameObject> _resourceUI = new();
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _woodText;
        [SerializeField] private TextMeshProUGUI _hideText;
        [SerializeField] private TextMeshProUGUI _metalText;

        private void Awake()
        {
            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                _resources.Add(resourceType, 0);
                
            }
            _resourceUI.Add(ResourceType.Gold, _goldText.gameObject);
            _resourceUI.Add(ResourceType.Wood, _woodText.gameObject);
            _resourceUI.Add(ResourceType.Hide, _hideText.gameObject);
            _resourceUI.Add(ResourceType.Metal, _metalText.gameObject);

        }
        
        public void AddResource(ResourceType resourceType, int amount)
        {
            _resources[resourceType] += amount;
            if (_resources[resourceType] < 0)
            {
                _resources[resourceType] = 0;
            }
            _resourceUI[resourceType].GetComponent<TextMeshProUGUI>().text = _resources[resourceType].ToString();
        }
    }
}