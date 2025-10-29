public class Card
{
    // Properties
    public Rank Rank { get; }
    public Suit Suit { get; }


    // Card Constructor
    public Card(Rank rank, Suit suit)
    {
        this.Rank = rank;
        this.Suit = suit;
    }
    // Returns string representation of the card
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}