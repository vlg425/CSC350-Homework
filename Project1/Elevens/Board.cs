
using System.Collections.Generic;

namespace Elevens
{
    public class Board
    {
        // Fields
        private List<Card> cardsOnBoard;
        private const int size = 9;

        // Board Constructor
        public Board()
        {
            cardsOnBoard = new List<Card>();
        }

        // Desired number of cards (9)
        public int Size { get { return size; } }

        // Number of cards currently on the board
        public int Count { get { return cardsOnBoard.Count; } }

        // Deals up to 9 cards from the deck
        public void DealInitial(Deck deck)
        {
            Refill(deck);
        }

        // Removes selected cards from the board
        public void Discard(List<Card> selected)
        {
            foreach (var card in selected)
            {
                cardsOnBoard.Remove(card);
            }
        }

        // Adds cards until board has 9 or deck is empty
        public void Refill(Deck deck)
        {
            while (cardsOnBoard.Count < size && !deck.Empty)
            {
                Card newCard = deck.TakeTopCard();
                if (newCard != null)
                {
                    cardsOnBoard.Add(newCard);
                }
            }
        }

        // Checks if the board currently has a given card
        public bool CheckForCard(Card card)
        {
            return cardsOnBoard.Contains(card);
        }

        // Empties board
        public void ClearBoard()
        {
            cardsOnBoard.Clear();
        }

        // Returns a list of all cards currently on the board
        public List<Card> GetCards()
        {
            return new List<Card>(cardsOnBoard);
        }
    }
}

