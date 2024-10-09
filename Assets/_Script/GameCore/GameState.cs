using System;

namespace _Script.GameCore
{
    [Serializable]
    public class GameState
    {
        public int metal;
        public int gold;
        public int wood;
        public int hide;
        
        public void UpdateGameState()
        {
            metal = PlayerInventory.Resources[ResourceType.Metal];
            gold = PlayerInventory.Resources[ResourceType.Gold];
            wood = PlayerInventory.Resources[ResourceType.Wood];
            hide = PlayerInventory.Resources[ResourceType.Hide];
           
        }
    }
}