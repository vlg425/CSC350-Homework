using System;
using System.Collections.Generic;

namespace Elevens
{
    //================================================================================
    // **Deck**
    //
    // Manages the 52-card deck. Handles shuffling and dealing.
    //================================================================================
    public class Deck
    {
        // --- Fields ---
        // A "master" list of 52 cards, used to reset the deck
        private readonly List<Card> _masterCardList;
        // The "active" draw pile we deal from
        private readonly List<Card> _drawPile;
        // A single, shared Random object for shuffling
        private static Random _random = new Random();

        // --- Properties ---
        
        // How many cards are left in the draw pile
        public int Count
        {
            get { return _drawPile.Count; }
        }

        // --- Constructor ---
        // Creates a new deck, builds the 52-card master list,
        // and copies it to the active draw pile.
        public Deck()
        {
            _masterCardList = new List<Card>();

            // Loop through all suits and ranks to build 52 cards
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _masterCardList.Add(new Card(suit, rank));
                }
            }
            
            // The draw pile starts as a copy of the master list
            _drawPile = new List<Card>(_masterCardList);
        }

        // --- Public Methods ---

        // Resets the draw pile to a full 52 cards.
        public void Reset()
        {
            _drawPile.Clear();
            _drawPile.AddRange(_masterCardList);
        }

        // Shuffles the draw pile using the Fisher-Yates algorithm.
        public void Shuffle()
        {
            int n = _drawPile.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                
                // Swap card [k] with card [n]
                Card tempCard = _drawPile[k];
                _drawPile[k] = _drawPile[n];
                _drawPile[n] = tempCard;
            }
        }

        // Deals one card from the top of the deck.
        // returns: A Card, or null if the deck is empty.
        public Card? Deal()
        {
            if (_drawPile.Count == 0)
            {
                return null; // Deck is empty
            }
            
            // Get the top card
            Card cardToDeal = _drawPile[0];
            // Remove it from the draw pile
            _drawPile.RemoveAt(0);
            
            return cardToDeal;
        }
    }
}