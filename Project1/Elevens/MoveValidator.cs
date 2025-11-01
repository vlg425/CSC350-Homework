using System.Collections.Generic;
using System.Linq;

namespace Elevens
{
    // Contains methods to check if a player's move is valid.
    public class MoveValidator
    {
        // Checks if two selected cards are non-face cards that add up to 11.
        public bool IsValidPairSum11(Card a, Card b)
        {
            // Rule: A Jack, Queen, or King can NEVER be part of a sum-to-11 pair.
            if (a.Rank >= Rank.Jack || b.Rank >= Rank.Jack)
            {
                return false;
            }

            // If they are not face cards, check their sum. (Aces are 1).
            return ((int)a.Rank + (int)b.Rank) == 11;
        }

        // Checks if a list of three cards consists of one Jack, one Queen, and one King.
        public bool IsValidJQK(List<Card> cards)
        {
            if (cards.Count != 3) return false;

            // Use a HashSet to quickly check for the presence of each rank
            var ranks = cards.Select(c => c.Rank).ToHashSet();
            return ranks.Contains(Rank.Jack) &&
                   ranks.Contains(Rank.Queen) &&
                   ranks.Contains(Rank.King);
        }

        // Checks the entire board to see if any legal move exists.
        public bool HasAnyValidMove(List<Card> cards)
        {
            // Check all possible pairs for a sum of 11
            // (i starts at 0, j starts at i+1 to avoid checking the same pair twice)
            for (int i = 0; i < cards.Count; i++)
            {
                for (int j = i + 1; j < cards.Count; j++)
                {
                    // If we find even one valid pair, a move is possible
                    if (IsValidPairSum11(cards[i], cards[j]))
                    {
                        return true;
                    }
                }
            }

            // If no pairs were found, check if a J, Q, and K are all present
            var ranksOnBoard = cards.Select(c => c.Rank).ToHashSet();
            if (ranksOnBoard.Contains(Rank.Jack) &&
                ranksOnBoard.Contains(Rank.Queen) &&
                ranksOnBoard.Contains(Rank.King))
            {
                return true;
            }

            // If neither check passed, no moves are possible
            return false;
        }
    }
}