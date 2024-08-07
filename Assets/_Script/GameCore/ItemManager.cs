
using System;
using Unity.VisualScripting;
    using UnityEngine;
    using Random = System.Random;

    public class ItemManager : MonoBehaviour
    {
        public ItemDB itemDB;
        Random random = new Random();
        private void Awake()
        {
            ItemManagerReference.itemManager = this;
            var armor = CreateItem("TestArmor");
            if (armor != null)
            {
                Debug.Log("Item created: " + armor.ItemName);
                PlayerInventory.Inventory.Add(armor);
            }

            var weapon = CreateItem("TestAxe");
            if (weapon != null)
            {
                Debug.Log("Item created: " + weapon.ItemName);
                PlayerInventory.Inventory.Add(weapon);
            }

            var helm = CreateItem("TestHelm");
            if (helm != null)
            {
                Debug.Log("Item created: " + helm.ItemName);
                PlayerInventory.Inventory.Add(helm);
            }
            
            var potion = CreateItem("TestPotion") as TestHealingPotion;
            if (potion != null)
            {
                Debug.Log("Item created: " + potion.ItemName);
                PlayerInventory.Bag.Add(potion);
            }
           
        }
        public GameItem CreateItem(string itemID)
        {
            ItemTemplate itemTemplate = itemDB.items.Find(x => x.itemID == itemID);
            GameItem game = new GameItem
            {
                ItemCost = itemTemplate.itemCost,
                ItemIcon = itemTemplate.itemIcon,
                ItemName = itemTemplate.itemName,
                ItemType = itemTemplate.ItemType,
                ItemUsageType = itemTemplate.itemUsageType,
                ItemDescription = itemTemplate.itemDescription,
                isEquippable = itemTemplate.isEquippable
            };


                //Debug.Log("Item Created - " + itemTemplate.itemName + " / "+ item.ItemName);
                //Debug.Log("Desc - " + itemTemplate.itemDescription + " / "+ item.ItemDescription);

            return game;
        }
        
    }

    public static class ItemManagerReference
    {
        public static ItemManager itemManager;
    }
