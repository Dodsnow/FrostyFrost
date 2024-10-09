
    using _Script.ConditionalEffects.Enum;
    using _Script.PlayableCharacters;
    using UnityEngine;

    public class TestAxe : GameItem
    {
        public override void OnAttackModifier(ICharacter source)
        {
            if(UtilsReference.utils.NumberOfEnemiesUnderCondition(source.currentHexPosition.hexPosition, 1, EntityControllerType.AI, ApplicableConditions.Bleed) > 1)
            {
                SelectionManagerReference.selectionManager.lastSelectedCardAction.cardActionSequencesList[BattleManagerReference.BattleManager.currentActionSequenceIndex].ActionValue += 1;
            }
        }
    }
