using System;
using System.Collections.Generic;

namespace Elevens
{
    //================================================================================
    // **ElevensRules**
    //
    // A pure logic class that holds no state.
    // Its only job is to answer questions about the rules of Elevens.
    //================================================================================
    public class ElevensRules
    {
        // Gets the point value of a card for an 11-sum pair.
        // Face cards (J,Q,K) are 0, as they are not used in pairs.
        // - card: The card to check.
        // returns The point value (1-10) or 0.
        public int GetCardValue(Card? card)
        {
            if (card == null)
            {
                return 0;
            }

            if (card.Rank == Rank.Jack ||
                card.Rank == Rank.Queen ||
                card.Rank == Rank.King)
            {
                return 0;
            }

            return (int)card.Rank;
        }

        // Checks if a list of selected cards is a valid play.
        // A valid play is 2 cards that sum to 11, or 3 cards (J, Q, K).
        // - selectedCards: The list of cards the player selected.
        // returns True if the play is valid.
        public bool CheckIfValid(List<Card> selectedCards)
        {
            // Check for a 2-card pair
            if (selectedCards.Count == 2)
            {
                int value1 = GetCardValue(selectedCards[0]);
                int value2 = GetCardValue(selectedCards[1]);
                return value1 + value2 == 11;
            }

            // Check for a 3-card trio
            if (selectedCards.Count == 3)
            {
                bool hasJack = false;
                bool hasQueen = false;
                bool hasKing = false;

                foreach (Card card in selectedCards)
                {
                    if (card.Rank == Rank.Jack)
                    {
                        hasJack = true;
                    }
                    else if (card.Rank == Rank.Queen)
                    {
                        hasQueen = true;
                    }
                    else if (card.Rank == Rank.King)
                    {
                        hasKing = true;
                    }
                }
                
                return hasJack && hasQueen && hasKing;
            }

            return false;
        }

        // Checks the board for any possible moves.
        // This is the "loss condition" check.
        // - cardSlots: The list of all 9 card slots from the board.
        // returns True if NO moves are left, false if a move IS possible.
        public bool NoMovesAvailable(IReadOnlyList<Card?> cardSlots)
        {
            // Get a clean list of only non-null cards on the board
            List<Card> activeCards = new List<Card>();
            foreach (Card? card in cardSlots)
            {
                if (card != null)
                {
                    activeCards.Add(card);
                }
            }

            // Check for 11-sum pairs
            for (int i = 0; i < activeCards.Count; i++)
            {
                for (int j = i + 1; j < activeCards.Count; j++)
                {
                    if (GetCardValue(activeCards[i]) + GetCardValue(activeCards[j]) == 11)
                    {
                        return false; // Found a move
                    }
                }
            }

            // Check for J-Q-K sets
            bool hasJack = false;
            bool hasQueen = false;
            bool hasKing = false;
            foreach (Card card in activeCards)
            {
                if (card.Rank == Rank.Jack)
                {
                    hasJack = true;
                }
                else if (card.Rank == Rank.Queen)
                {
                    hasQueen = true;
                }
                else if (card.Rank == Rank.King)
                {
                    hasKing = true;
                }
            }
            
            if (hasJack && hasQueen && hasKing)
            {
                return false; // Found a move
            }

            // No pairs or trios were found.
            return true;
        }
    }
}