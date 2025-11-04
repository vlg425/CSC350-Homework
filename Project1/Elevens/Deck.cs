//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

namespace Elevens
{
    public class Deck
    {
        private List<Card> cards;
        public int Count => cards.Count;
        public bool Empty => cards.Count == 0;

        public Deck()
        {
            cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            Random random = new Random();
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        public Card? TakeTopCard()
        {
            if (Empty) return null;
            Card topCard = cards[0];
            cards.RemoveAt(0);
            return topCard;
        }
    }
}