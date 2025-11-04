//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

namespace Elevens
{
    public class StatusManager
    {
        // --- RENAMED to GetMessage ---
        // It now also takes the GameState to decide on game over messages
        public string GetStatus(StatusText status, GameState currentState, List<Card>? cards = null)
        {
            // --- Game State has top priority ---
            if (currentState == GameState.Win)
            {
                return "CONGRATULATIONS! YOU WIN! Play again? (Y/N)";
            }
            if (currentState == GameState.Loss)
            {
                return "No more moves! GAME OVER! Play again? (Y/N)";
            }

            // --- If not game over, check the move result ---
            switch (status)
            {
                case StatusText.Welcome:
                    return "Rules: Valid Pair = 11, Valid Trio = J, Q, K.";

                case StatusText.ValidPair:
                    if (cards == null || cards.Count < 2) return "Error: Card data missing.";
                    return $"Valid Pair! {cards[0].Symbol} + {cards[1].Symbol} = 11.";

                case StatusText.InvalidPairSum:
                    if (cards == null || cards.Count < 2) return "Error: Card data missing.";
                    int sum = (int)cards[0].Rank + (int)cards[1].Rank;
                    return $"Invalid Pair! {cards[0].Symbol} + {cards[1].Symbol} = {sum}.";

                case StatusText.ValidTrio:
                    if (cards == null || cards.Count < 3) return "Error: Card data missing.";
                    var j = cards.First(c => c.Rank == Rank.Jack);
                    var q = cards.First(c => c.Rank == Rank.Queen);
                    var k = cards.First(c => c.Rank == Rank.King);
                    return $"Valid Trio! {j.Symbol}, {q.Symbol}, {k.Symbol}.";

                case StatusText.InvalidTrioMove:
                    return "Invalid Trio! A J, Q, and K must be selected.";

                case StatusText.InvalidSelection:
                    return "Invalid Move! Pair must = 11, Trio must = J, Q, K.";

                case StatusText.NoCardSelected:
                    return "You must select cards to play.";

                // Win/Loss are handled by the GameState check above
                case StatusText.Win:
                case StatusText.Loss:
                case StatusText.PlayAgain:
                case StatusText.None:
                default:
                    return ""; // An empty string will be "blank" in the footer
            }
        }
    }
}
