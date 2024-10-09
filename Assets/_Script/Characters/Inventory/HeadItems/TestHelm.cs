
    using _Script.ConditionalEffects.Enum;
    using _Script.PlayableCharacters;
    using UnityEngine;


    public class TestHelm : GameItem
    {
        public override void UseItem(ICharacter source, ICharacter target)
        {
            CardActionManagerReference.cardActionManager.ApplyCondition(null ,target, ApplicableConditions.Shield);
        }
    }
