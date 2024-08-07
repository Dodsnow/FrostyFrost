
    using _Script.ConditionalEffects.Enum;
    using _Script.PlayableCharacters;
    using UnityEngine;

    public class TestHelm : GameItem
    {
        public void UseItem(ICharacter source, ICharacter target)
        {
            CardActionManagerReference.cardActionManager.ApplyCondition(target, ApplicableConditions.Shield);
        }
    }
