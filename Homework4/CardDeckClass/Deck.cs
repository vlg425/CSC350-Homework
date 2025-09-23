//************************************************************
// Victor Garcia
// CSC350 
// Homework 4
// Deck class
//************************************************************

public class Deck
{
    List<Card> cards = new List<Card>();

    //Deck Constructor
    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                //create a new card and add it to the deck
                cards.Add(new Card(rank, suit));
            }
        }
    }

    //Implement a property to get Cards
    public List<Card> Cards { get { return cards; } }

    //Implement a property to check if deck is empty
    public bool Empty { get { return cards.Count == 0; } }


    //Takes top card from deck (if deck is not empty, otherwise return null)
    public Card TakeTopCard()
    {
        //implementation
        if (Empty) return null;
        Card topCard = cards[0];
        cards.RemoveAt(0);
        return topCard;
    }

    //Shuffle Method
    public void Shuffle()
    {
        //implementation
        Random random = new Random();
        int idx = cards.Count;
        while (idx > 1)
        {
            int temp = random.Next(idx--);
            Card swap = cards[idx];
            cards[idx] = cards[temp];
            cards[temp] = swap;
        }
    }

    //Cut method(cut deck at given location)
    public void Cut(int index)
    {
        //implementation



    }
}

