//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

namespace Elevens
{
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public string RankSymbol { get; }
        public string SuitSymbol { get; }
        public string Symbol { get; }
        // ---Consstructor---
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;

            RankSymbol = GetRankSymbol(rank);
            SuitSymbol = GetSuitSymbol(suit);
            Symbol = $"{RankSymbol}{SuitSymbol}";
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        private static string GetSuitSymbol(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts: return "♥";
                case Suit.Diamonds: return "♦";
                case Suit.Clubs: return "♣";
                case Suit.Spades: return "♠";
                default: return "?";
            }
        }

        private static string GetRankSymbol(Rank rank)
        {
            switch (rank)
            {
                case Rank.Ace: return "A";
                case Rank.King: return "K";
                case Rank.Queen: return "Q";
                case Rank.Jack: return "J";
                case Rank.Ten: return "10";
                default: return ((int)rank).ToString();
            }
        }


    }
}
