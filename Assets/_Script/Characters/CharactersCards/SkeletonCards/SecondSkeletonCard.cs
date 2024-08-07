
    using System;
    using System.Collections.Generic;
    using _Script.ConditionalEffects.Enum;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class SecondSkeletonCard : CharacterCard
    {
        public string cardName { get; set; }
        public int initiative { get; set; }
        public CardAction TopCardAction { get; set; }
        public CardAction BottomCardAction { get; set; }
        public Image ConditionImage { get; set; }

        public SecondSkeletonCard()
        {
            cardName = "Second Skeleton Card";
            initiative = 14;
            TopCardAction = new CardAction(CardDiscardActionType.Shuffle, "Move 4, Attack 1, Attack 1", this);
            TopCardAction.AddActionSequence(CharacterActionType.Move,4,0,"",null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,"AttackChop",null);
            TopCardAction.AddActionSequence(CharacterActionType.Attack,1,1,"AttackSlice", new List<ApplicableConditions>()
                {
                    ApplicableConditions.Weaken
                });
           
        } 
    }
