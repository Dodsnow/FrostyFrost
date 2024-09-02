
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
            TestArmor armor = CreateItem<TestArmor>("TestArmor");
            if (armor != null)
            {
                Debug.Log("Item created: " + armor.ItemName);
                PlayerInventory.Inventory.Add(armor);
            }

            TestAxe weapon = CreateItem<TestAxe>("TestAxe");
            if (weapon != null)
            {
                Debug.Log("Item created: " + weapon.ItemName);
                PlayerInventory.Inventory.Add(weapon);
            }

            TestHelm helm = CreateItem<TestHelm>("TestHelm");
            if (helm != null)
            {
                Debug.Log("Item created: " + helm.ItemName);
                PlayerInventory.Inventory.Add(helm);
            }
            
            TestHealingPotion potion = CreateItem<TestHealingPotion>("TestPotion");
            if (potion != null)
            {
                Debug.Log("Item created: " + potion.ItemName);
                PlayerInventory.Bag.Add(potion);
            }
           
        }
        
        public T CreateItem<T>(string itemID)
        {
            ItemTemplate itemTemplate = itemDB.items.Find(x => x.itemID == itemID);
            if (itemTemplate == null ) { return default; };
            T myNewItem = Activator.CreateInstance<T>();
            GameItem gameItem = myNewItem as GameItem;

                gameItem.ItemCost = itemTemplate.itemCost;
                gameItem.ItemIcon = itemTemplate.itemIcon;
                gameItem.ItemName = itemTemplate.itemName;
                gameItem.ItemType = itemTemplate.ItemType;
                gameItem.ItemUsageType = itemTemplate.itemUsageType;
                gameItem.ItemDescription = itemTemplate.itemDescription;
                gameItem.isEquippable = itemTemplate.isEquippable;
        
    
                //Debug.Log("Item Created - " + itemTemplate.itemName + " / "+ item.ItemName);
                //Debug.Log("Desc - " + itemTemplate.itemDescription + " / "+ item.ItemDescription);

            return myNewItem;
        }
        
    }

    public static class ItemManagerReference
    {
        public static ItemManager itemManager;
    }
