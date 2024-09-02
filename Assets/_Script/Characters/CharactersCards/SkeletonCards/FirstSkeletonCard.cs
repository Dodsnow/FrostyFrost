  using System;
  using System.Collections.Generic;
  using _Script.ConditionalEffects.Enum;
  using UnityEngine;
  using UnityEngine.UI;
  [Serializable]
  public class FirstSkeletonCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        


        public FirstSkeletonCard()
        {
            cardName = "Nothing Special";
            initiative = 20;
            TopCardAction = new CardAction(CardDiscardActionType.Shuffle, "Move 3, Attack 3", this);
            TopCardAction.AddActionSequence(CharacterActionType.Move,3,0,"",null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,3,"AttackSlice", new List<ApplicableConditions>(){ApplicableConditions.Bleed});

        }
    }
