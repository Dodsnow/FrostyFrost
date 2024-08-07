using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemDB", menuName = "ScriptableObjects/ItemDB", order = 3)]
public class ItemDB : ScriptableObject
{
    public List<ItemTemplate> items = new List<ItemTemplate>();
}

[Serializable]
public class ItemTemplate
{
    public string itemID;
    public int itemCost;
    public Sprite itemIcon;
    public string itemName;
    public ItemUsageType itemUsageType;
    public ItemType ItemType;
    public string itemDescription;
    public bool isEquippable;
    
}