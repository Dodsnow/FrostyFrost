using UnityEngine;

namespace _Script.GameCore.Buttons
{
    public class InventorySwapButton : MonoBehaviour
    {
        public void ToggleConsumablesInventory()
        {
            BattleHUDReference.battleHUD.InventorySwapButton();
        }
    }
}