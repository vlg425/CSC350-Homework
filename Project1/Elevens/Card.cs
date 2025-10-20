public class Card
{
    // Properties for the card's rank, suit, and visibility
    public Rank Rank { get; }
    public Suit Suit { get; }
    public bool FaceUp { get; private set; }

    // Calculates the point value for the game of Elevens
    // Number cards are their number, Ace is 1, and J, Q, K are 0.
    public int PointValue =>
        Rank >= Rank.Ace && Rank <= Rank.Ten ? (int)Rank + 1 : 0;

    // Constructor to initialize a new card
    public Card(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
        FaceUp = false; // Cards are created face down
    }

    // Flips the card's visibility
    public void FlipOver()
    {
        FaceUp = !FaceUp;
    }

    // Provides a readable string for the card, e.g., "Ace of Spades"
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}