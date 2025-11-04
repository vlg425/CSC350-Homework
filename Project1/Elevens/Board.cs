//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

namespace Elevens
{
    public class Board
    {
        private List<Card> cardsOnBoard;
        private const int BOARD_SIZE = 9;

        public int Count => cardsOnBoard.Count;
        public List<Card> GetCards() => new List<Card>(cardsOnBoard);

        public Board()
        {
            cardsOnBoard = new List<Card>();
        }

        public void DealInitial(Deck deck)
        {
            Refill(deck);
        }

        public void ReplaceAndRefill(List<Card> selected, Deck deck)
        {
            var indices = selected.Select(card => cardsOnBoard.IndexOf(card))
                                  .Where(index => index != -1)
                                  .OrderByDescending(index => index)
                                  .ToList();

            foreach (int index in indices)
            {
                Card? newCard = deck.TakeTopCard();
                if (newCard != null)
                {
                    cardsOnBoard[index] = newCard;
                }
                else
                {
                    cardsOnBoard.RemoveAt(index);
                }
            }
        }

        public void Refill(Deck deck)
        {
            while (cardsOnBoard.Count < BOARD_SIZE && !deck.Empty)
            {
                Card? newCard = deck.TakeTopCard();
                if (newCard != null)
                {
                    cardsOnBoard.Add(newCard);
                }
            }
        }
    }
}