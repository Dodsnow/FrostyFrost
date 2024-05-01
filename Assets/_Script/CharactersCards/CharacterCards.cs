public interface CharacterCards
{
    string cardName { get; set; }
    string TopActionDiscription { get; set; }
    string BottomActionDiscription { get; set; }
    void TopAction();
    void BottomAction();
    int initiative { get; set; }
    ActionType TopActionType { get; set; }
    ActionType BottomActionType { get; set; }
    
}