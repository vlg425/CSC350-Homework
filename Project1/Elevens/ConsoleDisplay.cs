using System.Text;

namespace Elevens
{
    // Handles all logic for drawing the game state to the console.
    public class ConsoleDisplay
    {
        // Draws the entire board of cards as ASCII art.
        public void DrawBoard(Board board)
        {
            var cards = board.GetCards();
            if (cards.Count == 0)
            {
                Console.WriteLine("The board is empty!");
                return;
            }

            // Use StringBuilder for efficient string building
            // This is much faster than adding strings together in a loop
            var top = new StringBuilder();    // For the top border line: " ┌───┐"
            var middle = new StringBuilder(); // For the rank/suit line:  " │A ♥│"
            var bottom = new StringBuilder(); // For the bottom border:   " └───┘"
            var indices = new StringBuilder();// For the index display:   "  [0] "

            // Loop through each card on the board
            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                string rank = GetRankSymbol(card.Rank);
                string suit = GetSuitSymbol(card.Suit);

                // Append the pieces for this card to their respective lines
                top.Append(" ┌───┐");

                // Handle the "10" rank, which is two characters
                if (rank == "10")
                {
                    // No extra padding needed
                    middle.Append($" │{rank}{suit}│");
                }
                else
                {
                    // Add extra space for single-character ranks (A, 2-9, J, Q, K)
                    middle.Append($" │{rank} {suit}│");
                }
                
                bottom.Append(" └───┘");
                indices.Append($"  [{i}] "); // Display the card's index
            }

            // Print each fully assembled line to the console
            Console.WriteLine(top.ToString());
            Console.WriteLine(middle.ToString());
            Console.WriteLine(bottom.ToString());
            Console.WriteLine(indices.ToString());
        }

        // Helper method to get the Unicode symbol for a suit.
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

        // Helper method to get the string representation for a rank.
        private string GetRankSymbol(Rank rank)
        {
            switch (rank)
            {
                case Rank.Ace:   return "A";
                case Rank.King:  return "K";
                case Rank.Queen: return "Q";
                case Rank.Jack:  return "J";
                case Rank.Ten:   return "10";
                default:         return ((int)rank).ToString(); // For 2-9
            }
        }
    }
}