namespace Elevens
{
    //================================================================================
    // **Card**
    //
    // Represents a single playing card.
    // knows its own display symbols.
    //================================================================================
    public class Card
    {
        // --- Properties ---
        public Suit Suit { get; }
        public Rank Rank { get; }

        // Display properties (e.g., "K", "♥", "K♥")
        public string RankSymbol { get; }
        public string SuitSymbol { get; }
        public string Symbols { get; }

        // --- Constructor ---
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;

            // Generate the display symbols once
            RankSymbol = GetRankSymbol(rank);
            SuitSymbol = GetSuitSymbol(suit);
            Symbols = $"{RankSymbol}{SuitSymbol}";
        }
        
        // Override for easier debugging (e.g., "King of Spades")
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        // --- Private Static Helpers ---

        // Translates a Suit enum to its display character
        private static string GetSuitSymbol(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts:
                    return "♥";
                case Suit.Diamonds:
                    return "♦";
                case Suit.Clubs:
                    return "♣";
                case Suit.Spades:
                    return "♠";
                default:
                    return "?";
            }
        }

        // Translates a Rank enum to its display string
        private static string GetRankSymbol(Rank rank)
        {
            switch (rank)
            {
                case Rank.Ace:
                    return "A";
                case Rank.King:
                    return "K";
                case Rank.Queen:
                    return "Q";
                case Rank.Jack:
                    return "J";
                default:
                    // For 2-10, just use the number
                    return ((int)rank).ToString();
            }
        }
    }
}