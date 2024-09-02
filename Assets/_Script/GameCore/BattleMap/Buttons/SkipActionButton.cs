using UnityEngine;
    
    public class SkipActionButton : MonoBehaviour
    { 
        public BattleHUD _battleHUD;
        
        public void SkipActionButtonOnClick()
        {
            _battleHUD.SkipActionButton();
        }

        public void ResetSkipActionButton()
        {
            _battleHUD.skipAction = false;
            _battleHUD.skipActionButton.gameObject.SetActive(false);
        }
    }
