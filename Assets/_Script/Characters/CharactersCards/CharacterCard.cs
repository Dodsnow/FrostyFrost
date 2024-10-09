using _Script.Characters.CharactersCards.Enum;
using _Script.ConditionalEffects.Enum;
using _Script.PlayableCharacters;
using UnityEngine;
using UnityEngine.UI;

public interface CharacterCard
{
    string cardName { get; set; }
    int initiative { get; set; }
    CardAction TopCardAction { get; set; }

    CardAction BottomCardAction { get; set; }

    public void OnCardAction(ICharacter source, ICharacter target, CharacterActionType actionType)
    {
    }

    public int OnCardShieldValue(ICharacter source)
    {
        return 0;
    }

    public int OnCardMoveValue(ICharacter source, ICharacter target)
    {
        return 0;
    }

    public int OnCardAttackValue(ICharacter source, ICharacter target, CharacterActionType actionType)
    {
        return 0;
    }

    public int OnCardAttackRange(ICharacter source)
    {
        return 0;
    }

    public void OnCardActionEnd(ICharacter source, ICharacter target)
    {
    }
    public int OnCardMoveValue(ICharacter source)
    {
        return 0;
    }

    public int TriggerActiveDeckCard(ICharacter source, ICharacter target, CharacterActionType actionType)
    {
        return 0;
    }

    public void ReturnCardFromDeck(ICharacter source, ICharacter target)
    {
        
    }

    public bool CheckForCondition(ICharacter source, ICharacter target, ApplicableConditions conditions)
    {
        return false;
    }

    public int ActiveCardModifyRangeValue(ICharacter source)
    {
        return 0;
    }
    
    public void OnConditionApplied(ICharacter source, ICharacter target)
    {
    }
    public void OnDamageTaken(ICharacter source, ICharacter target, int resultValue)
    {
       
    }
    
    public void OnConditionRemoved(ICharacter target)
    {
    }
    
   
    public int OnExtraTarget(ICharacter source)
    {
        return 0;
    }
}