public class Deck
{
    private List<Card> cards;
    private static readonly Random Rng = new Random();

    // Checks if the deck has run out of cards
    public bool IsEmpty => cards.Count == 0;

    // Constructor creates a full, ordered 52-card deck
    public Deck()
    {
        cards = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(rank, suit));
            }
        }
    }

    // Shuffles the deck using the Fisher-Yates algorithm
    public void Shuffle()
    {
        for (int n = cards.Count - 1; n > 0; --n)
        {
            int k = Rng.Next(n + 1);
            // Swap the cards
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    // Removes and returns the top card from the deck
    public Card TakeTopCard()
    {
        if (IsEmpty)
        {
            return null; // Return null if the deck is empty
        }
        Card topCard = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return topCard;
    }
}