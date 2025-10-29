using System.Text;

namespace Elevens
{
    public class ConsoleDisplay
    {
        // The main method to draw the entire board with corrected alignment
        public void DrawBoard(Board board)
        {
            var cards = board.GetCards();
            if (cards.Count == 0)
            {
                Console.WriteLine("The board is empty!");
                return;
            }

            // Use StringBuilder for efficient string building
            var top = new StringBuilder();    // For the top border line: " ┌─────┐"
            var middle = new StringBuilder(); // For the rank/suit line:  " │ A ♥ │"
            var bottom = new StringBuilder(); // For the bottom border:   " └─────┘"
            var indices = new StringBuilder();// For the index display:   "   [0]  "

            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                string rank = GetRankSymbol(card.Rank);
                string suit = GetSuitSymbol(card.Suit);

                // Append the pieces for each card to their respective lines
                top.Append(" ┌───┐");

                // Conditional padding to align all cards perfectly
                if (rank == "10")
                {
                    // No extra padding needed for the two-character "10"
                    middle.Append($" │{rank}{suit}│");
                }
                else
                {
                    // Add extra space for single-character ranks to maintain width
                    middle.Append($" │{rank} {suit}│");
                }
                
                bottom.Append(" └───┘");
                indices.Append($"  [{i}] ");
            }

            // Print each fully assembled line to the console
            Console.WriteLine(top.ToString());
            Console.WriteLine(middle.ToString());
            Console.WriteLine(bottom.ToString());
            Console.WriteLine(indices.ToString());
        }

        // Helper method to get the character for a suit
        private string GetSuitSymbol(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts:   return "♥";
                case Suit.Diamonds: return "♦";
                case Suit.Clubs:    return "♣";
                case Suit.Spades:   return "♠";
                default:            return "?";
            }
        }

        // Helper method to get the string for a rank
        private string GetRankSymbol(Rank rank)
        {
            switch (rank)
            {
                case Rank.Ace:   return "A";
                case Rank.King:  return "K";
                case Rank.Queen: return "Q";
                case Rank.Jack:  return "J";
                case Rank.Ten:   return "10";
                default:         return ((int)rank).ToString();
            }
        }
    }
}