// Represents a standard 52-card deck.
public class Deck
{
    // The list of cards currently in the deck
    private List<Card> cards;

    // --- Properties ---

    // Gets a copy of the cards currently in the deck.
    public List<Card> Cards { get { return new List<Card>(cards); } }

    // The number of cards remaining in the deck.
    public int Count { get { return cards.Count; } }

    // Returns true if the deck has no cards left.
    public bool Empty { get { return cards.Count == 0; } }

    // --- Constructor ---

    // Creates a new, unshuffled 52-card deck.
    public Deck()
    {
        cards = new List<Card>();
        // Loop through all suits (Clubs, Diamonds, Hearts, Spades)
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            // Loop through all ranks (Ace to King)
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                // Add a new card of this suit and rank
                cards.Add(new Card(rank, suit));
            }
        }
    }

    // --- Public Methods ---

    // Removes and returns the top card from the deck.
    public Card? TakeTopCard()
    {
        if (Empty) return null; // Return null if the deck is empty
        
        Card topCard = cards[0];   // Get the card at the top (index 0)
        cards.RemoveAt(0);         // Remove it from the list
        return topCard;            // Return the card
    }
    
    // Shuffles the deck using the Fisher-Yates algorithm.
    public void Shuffle()
    {
        Random random = new Random();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            // Swap the cards at index k and n
            Card temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }
}