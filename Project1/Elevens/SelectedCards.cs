
using System.Collections.Generic;

namespace Elevens
{
    public class SelectedCards
    {
        private List<Card> selectedCards;

        // Selection Constructor
        public SelectedCards()
        {
            selectedCards = new List<Card>();
        }

        // Number of selected cards
        public int Count { get { return selectedCards.Count; } }

        // Toggle – Selects or deselects a card (maximum 3)
        public void Toggle(Card card)
        {
            if (selectedCards.Contains(card))
            {
                selectedCards.Remove(card);
            }
            else
            {
                if (selectedCards.Count < 3)
                {
                    selectedCards.Add(card);
                }
            }
        }

        // Clear – Clears the current selection
        public void Deselect()
        {
            selectedCards.Clear();
        }

        // GetSelected – Returns all selected cards
        public List<Card> GetSelected()
        {
            return new List<Card>(selectedCards);
        }
    }
}