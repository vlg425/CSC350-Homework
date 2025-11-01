// Represents a single playing card with a Rank and a Suit.
public class Card
{
    // --- Properties ---

    // The rank (value) of the card, e.g., Ace, Two, King.
    public Rank Rank { get; }

    // The suit of the card, e.g., Hearts, Spades.
    public Suit Suit { get; }

    // --- Constructor ---

    // Creates a new card with a specific rank and suit.
    public Card(Rank rank, Suit suit)
    {
        this.Rank = rank;
        this.Suit = suit;
    }
    
    // --- Methods ---

    // Returns a human-readable string for the card.
    // Example: "Ace of Spades"
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}