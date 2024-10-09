

    using System.Collections.Generic;
    using _Script.Characters.CharactersCards.BloodOmenCards;


    public static class PlayerInventory
    {
        public static List<GameItem> Inventory = new();
        public static List<GameItem> Bag = new();
        public static Dictionary<ResourceType, int> Resources = new Dictionary<ResourceType, int>();
        public static ClassType _ClassType;
       

      static PlayerInventory()
      {
          
            Resources.Add(ResourceType.Metal, 0);
            Resources.Add(ResourceType.Gold, 0);
            Resources.Add(ResourceType.Wood, 0);
            Resources.Add(ResourceType.Hide, 0);
            
        }
        
    }
