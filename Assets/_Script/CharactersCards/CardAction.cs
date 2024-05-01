
    public class CardAction 
    {
        ActionType ActionType { get; set; }
        string Discription { get; set; }
        
        public CardAction(ActionType actionType, string discription)
        {
            ActionType = actionType;
            Discription = discription;
        }
    }
