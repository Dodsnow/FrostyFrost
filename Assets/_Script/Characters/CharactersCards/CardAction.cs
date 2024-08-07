using System;
using System.Collections.Generic;
using _Script.ConditionalEffects.Enum;
using UnityEngine;
[Serializable]
public class CardAction
{
    public CardDiscardActionType DiscardActionType { get; set; }
    public string Discription { get; set; }
    public List<CardActionSequence> cardActionSequencesList;
    public CharacterCard CharacterCard { get; set; }
    


    public CardAction(CardDiscardActionType discardActionType, string discription, CharacterCard characterCard)
    {
        DiscardActionType = discardActionType;
        Discription = discription;
        CharacterCard = characterCard;
        cardActionSequencesList = new List<CardActionSequence>();
    }
    

   

    public void AddActionSequence(CharacterActionType characterActionType, int actionRange, int actionValue,string animProp, List<ApplicableConditions> applicableConditionsList)
    {
        CardActionSequence cardActionSequence = new CardActionSequence(characterActionType, actionRange, actionValue, animProp);
        cardActionSequencesList.Add(cardActionSequence);
        if (applicableConditionsList != null)
        {
            cardActionSequence.Conditions.AddRange(applicableConditionsList);
        }
    }
    
   
}