using System;
using System.Collections.Generic;

namespace Elevens
{
    //================================================================================
    // **Board**
    //
    // It holds the 9 card slots.
    // It does not contain any game logic, it just holds data for
    // the ElevensGame class to manage.
    //================================================================================
    public class Board
    {
        // --- Fields ---
        private readonly Card?[] _cardSlots; // An array of 9 nullable Cards

        // --- Properties ---

        // How many slots are on this board (e.g., 9)
        public int BoardSize
        {
            get { return _cardSlots.Length; }
        }

        // How many slots currently have a card in them
        public int ActiveCardCount
        {
            get
            {
                int count = 0;
                foreach (Card? card in _cardSlots)
                {
                    if (card != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        // A read-only list of the cards, for the View to draw
        public IReadOnlyList<Card?> CardsOnBoard
        {
            get { return Array.AsReadOnly(_cardSlots); }
        }

        // --- Constructor ---
        public Board(int size)
        {
            _cardSlots = new Card?[size];
        }
        
        // --- Public Methods ---
        
        // Reads the card at a specific index (0-8)
        public Card? ReadCardAt(int index)
        {
            if (index < 0 || index >= _cardSlots.Length)
            {
                return null;
            }
            return _cardSlots[index];
        }

        // Places a card (or null) at a specific index (0-8)
        public void PlaceCardAt(int index, Card? card)
        {
            if (index >= 0 && index < _cardSlots.Length)
            {
                _cardSlots[index] = card;
            }
        }
    }
}