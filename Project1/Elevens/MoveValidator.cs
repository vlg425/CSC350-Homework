using System.Collections.Generic;
using System.Linq;

namespace Elevens
{
    public class MoveValidator
    {
        // Renamed and fixed: Checks if two NON-FACE cards add up to 11
        public bool IsValidPairSum11(Card a, Card b)
        {
            // Rule: A Jack, Queen, or King can NEVER be part of a sum-to-11 pair.
            if (a.Rank >= Rank.Jack || b.Rank >= Rank.Jack)
            {
                return false;
            }

            // If they are not face cards, check their sum.
            return ((int)a.Rank + (int)b.Rank) == 11;
        }

        // Checks if three cards are a Jack, Queen, and King
        public bool IsValidJQK(List<Card> cards)
        {
            if (cards.Count != 3) return false;

            var ranks = cards.Select(c => c.Rank).ToHashSet();
            return ranks.Contains(Rank.Jack) &&
                   ranks.Contains(Rank.Queen) &&
                   ranks.Contains(Rank.King);
        }

        // Checks if any legal move exists on the board
        public bool HasAnyValidMove(List<Card> cards)
        {
            // Check all pairs for a sum of 11
            for (int i = 0; i < cards.Count; i++)
            {
                for (int j = i + 1; j < cards.Count; j++)
                {
                    if (IsValidPairSum11(cards[i], cards[j]))
                    {
                        return true;
                    }
                }
            }

            // Check if a J, Q, and K are all present
            var ranksOnBoard = cards.Select(c => c.Rank).ToHashSet();
            if (ranksOnBoard.Contains(Rank.Jack) &&
                ranksOnBoard.Contains(Rank.Queen) &&
                ranksOnBoard.Contains(Rank.King))
            {
                return true;
            }

            return false;
        }
    }
}