using System.Collections.Generic;

namespace Elevens
{
    // A helper class to manage a list of selected cards.
    // (Note: This class is not currently used by Program.cs, which makes its own list)
    public class SelectedCards
    {
        // Internal list to store the cards
        private List<Card> selectedCards;

        // Initializes a new, empty selection.
        public SelectedCards()
        {
            selectedCards = new List<Card>();
        }

        // The number of cards currently selected.
        public int Count { get { return selectedCards.Count; } }

        // Toggles a card's selection state (selects or deselects).
        // Will not allow more than 3 cards to be selected.
        public void Toggle(Card card)
        {
            if (selectedCards.Contains(card))
            {
                // If already selected, deselect it
                selectedCards.Remove(card);
            }
            else
            {
                // If not selected, add it (up to a max of 3)
                if (selectedCards.Count < 3)
                {
                    selectedCards.Add(card);
                }
            }
        }

        // Clears the current selection.
        public void Deselect()
        {
            selectedCards.Clear();
        }

        // Returns a copy of the list of selected cards.
        public List<Card> GetSelected()
        {
            return new List<Card>(selectedCards);
        }
    }
}