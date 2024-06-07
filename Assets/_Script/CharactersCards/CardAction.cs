
    public class CardAction 
    {
        public CardActionType ActionType { get; set; }
        public string Discription { get; set; }
        
        public CardAction(CardActionType actionType, string discription)
        {
            ActionType = actionType;
            Discription = discription;
        }
    }
