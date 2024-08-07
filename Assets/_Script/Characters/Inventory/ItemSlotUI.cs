    using System.Net.Mime;
    using UnityEngine;
    using UnityEngine.UI;

    public class ItemSlotUI
    {
      
        public GameObject ItemIcon;
        public ItemType ItemType;
        
        public ItemSlotUI(GameObject itemIcon, ItemType itemType)
        {
            ItemIcon = itemIcon;
            ItemType = itemType;
        }
        
        public void SetItemIcon(Sprite icon)
        {
            ItemIcon.GetComponent<Image>().sprite = icon;
        }
    }
