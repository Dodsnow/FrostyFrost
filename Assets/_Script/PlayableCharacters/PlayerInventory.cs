

    using System.Collections.Generic;
    // CharactersCards;

    public static class PlayerInventory
    {
        public static List<Item> inventory = new List<Item>();
        public static Dictionary<ClassType, List<CharacterCard>> cards = new Dictionary<ClassType, List<CharacterCard>>();
        public static Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

      static PlayerInventory()
        {
            List<CharacterCard> _berserker_cards = new List<CharacterCard>();
            _berserker_cards.Add(new FirstBerserkerCard());
            _berserker_cards.Add(new SecondBerserkerCard());
            _berserker_cards.Add(new ThirdBerserkerCard());
            _berserker_cards.Add(new FourthBerserkerCard());
            _berserker_cards.Add(new FifthBerserkerCard());
            _berserker_cards.Add(new SixthBerserkerCard());
            cards.Add(ClassType.Berserker, _berserker_cards);
        }
        
    }
