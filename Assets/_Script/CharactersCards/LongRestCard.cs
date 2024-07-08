// namespace _Script.CharactersCards
// {
//     public class LongRestCard : CharacterCard
//     {
//         public string cardName { get; set; }
//         public int initiative { get; set; }
//         public CardAction TopCardAction { get; set; }
//         public CardAction BottomCardAction { get; set; }
//         public GlowHighLight _highLight { get; set; }
//         
//         public LongRestCard()
//         {
//             cardName = "Long Rest";
//             initiative = 99;
//             TopCardAction = new CardAction(CardDiscardActionType.Shuffle, "Long Rest", this);
//             TopCardAction.AddActionSequence(CharacterActionType.LongRest,0,2,"");
//             BottomCardAction = new CardAction(CardDiscardActionType.Shuffle, "Long Rest", this);
//         }
//     }
//     
// }