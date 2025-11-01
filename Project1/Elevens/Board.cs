using System.Collections.Generic;

namespace Elevens
{
    // Represents the game board, which holds up to 9 cards.
    public class Board
    {
        // --- Fields ---

        // The list of cards currently visible on the board
        private List<Card> cardsOnBoard;
        
        // The target number of cards to have on the board
        private const int size = 9;

        // --- Constructor ---

        // Initializes a new, empty board.
        public Board()
        {
            cardsOnBoard = new List<Card>();
        }

        // --- Properties ---

        // The desired number of cards on the board (always 9).
        public int Size { get { return size; } }

        // The number of cards *currently* on the board.
        public int Count { get { return cardsOnBoard.Count; } }

        // --- Public Methods ---

        // Deals the initial set of cards to fill the board.
        public void DealInitial(Deck deck)
        {
            Refill(deck);
        }

        // Removes a list of selected cards from the board.
        public void Discard(List<Card> selected)
        {
            foreach (var card in selected)
            {
                cardsOnBoard.Remove(card);
            }
        }

        // Adds cards from the deck until the board has 9 cards or the deck is empty.
        public void Refill(Deck deck)
        {
            // Keep dealing while the board isn't full AND the deck isn't empty
            while (cardsOnBoard.Count < size && !deck.Empty)
            {
                Card? newCard = deck.TakeTopCard();
                if (newCard != null) // Check if TakeTopCard didn't return null
                {
                    cardsOnBoard.Add(newCard);
                }
            }
        }

        // Checks if a specific card is currently on the board.
        public bool CheckForCard(Card card)
        {
            return cardsOnBoard.Contains(card);
        }

        // Empties all cards from the board.
        public void ClearBoard()
        {
            cardsOnBoard.Clear();
        }

        // Returns a new list containing all cards currently on the board.
        public List<Card> GetCards()
        {
            // Returns a *copy* to prevent other classes from modifying the board's list
            return new List<Card>(cardsOnBoard);
        }
    }
}