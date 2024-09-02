using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Script.GameCore.Controllers
{
    public class KeyboardController : MonoBehaviour
    {
       [SerializeField] private BattleHUD _battleHUD;

       private void Update()
       {
           if (Input.GetKeyDown(KeyCode.Tab))
           {
               _battleHUD.ToggleCardsVisibility(!_battleHUD.displayCards[0].activeSelf);
           }
           
           
       }
    }
}