
using System.Collections.Generic;

namespace Elevens
{
    public class MoveValidator
    {
        // IsValidPairSum11 – True if two cards add to 11
        public bool IsValidPairSum11(Card a, Card b)
        {
            return (GetCardValue(a) + GetCardValue(b)) == 11;
        }

        // IsValidJQK – True if 3 cards are J, Q, K
        public bool IsValidJQK(List<Card> triple)
        {
            bool hasJ = false, hasQ = false, hasK = false;
            foreach (var card in triple)
            {
                if (card.Rank == Rank.Jack) hasJ = true;
                else if (card.Rank == Rank.Queen) hasQ = true;
                else if (card.Rank == Rank.King) hasK = true;
            }
            return hasJ && hasQ && hasK;
        }

        // Returns true if any legal move exists on the board
        public bool HasAnyValidMove(List<Card> cards)
        {
            // check pairs for sum 11
            for (int i = 0; i < cards.Count; i++)
            {
                for (int j = i + 1; j < cards.Count; j++)
                {
                    if (IsValidPairSum11(cards[i], cards[j])) return true;
                }
            }

            // check triples for JQK
            int n = cards.Count;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        var triple = new List<Card> { cards[i], cards[j], cards[k] };
                        if (IsValidJQK(triple)) return true;
                    }
                }
            }

            return false;
        }

        // Helper method to get card value
        private int GetCardValue(Card card)
        {
            switch (card.Rank)
            {
                case Rank.Ace: return 1;
                case Rank.Two: return 2;
                case Rank.Three: return 3;
                case Rank.Four: return 4;
                case Rank.Five: return 5;
                case Rank.Six: return 6;
                case Rank.Seven: return 7;
                case Rank.Eight: return 8;
                case Rank.Nine: return 9;
                case Rank.Ten: return 10;
                default: return 0; // J, Q, K have no value for sum 11
            }
        }
    }
}