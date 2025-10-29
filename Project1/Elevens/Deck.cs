public class Deck
{
    private List<Card> cards;

    public List<Card> Cards { get { return new List<Card>(cards); } }
    public int Count { get { return cards.Count; } }
    public bool Empty { get { return cards.Count == 0; } }

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

    // FIXED: Signature updated to indicate it can return null
    public Card? TakeTopCard()
    {
        if (Empty) return null;
        
        Card topCard = cards[0];
        cards.RemoveAt(0);
        return topCard;
    }
    
    // ... (Shuffle remains the same) ...
    public void Shuffle()
    {
        Random random = new Random();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }
}