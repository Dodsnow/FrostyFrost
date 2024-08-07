

    using System.Collections.Generic;
    

    public static class PlayerInventory
    {
        public static List<GameItem> Inventory = new();
        public static List<GameItem> Bag = new();
        public static Dictionary<ClassType, List<CharacterCard>> Cards = new Dictionary<ClassType, List<CharacterCard>>();
        public static Dictionary<ResourceType, int> Resources = new Dictionary<ResourceType, int>();
       

      static PlayerInventory()
      {
         List<CharacterCard> berserkerCards = new List<CharacterCard>
            {
                new FirstBerserkerCard(),
                new SecondBerserkerCard(),
                new ThirdBerserkerCard(),
                new FourthBerserkerCard(),
                new FifthBerserkerCard(),
                new SixthBerserkerCard()
            };
            Cards.Add(ClassType.Berserker, berserkerCards);
        }
        
    }
