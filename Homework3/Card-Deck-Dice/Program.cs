//************************************************************************
// Victor Garcia
// CSC 350H
// Homework 3 - Card, Deck, Dice
//************************************************************************

using Cards2;

public class Player
{
    public int PlayerNumber { get; set; }
    public int Roll { get; set; }

    public Player(int n)
    {
        PlayerNumber = n;
        Roll = 0;
    }
}

public class CardDeckDice
{
    public static int RollForDealer(int numberOfPlayers)
    {
        Dice dice = new Dice(2);
        List<Player> players = new List<Player>();
        for (int i = 1; i <= numberOfPlayers; i++) players.Add(new Player(i));

        // Start with all players as dealer
        List<Player> remaining = new List<Player>(players);

        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Rolling for dealer...");
        Console.WriteLine("-----------------------------------");

        while (true)
        {
            int highest = 0;

            // Roll and announce for each player in dealer
            for (int i = 0; i < remaining.Count; i++)
            {
                dice.Roll();
                remaining[i].Roll = dice.TopSide;
                Console.WriteLine($"Player {remaining[i].PlayerNumber} rolls {remaining[i].Roll}");
                if (remaining[i].Roll > highest)
                    highest = remaining[i].Roll;
            }

            // Keep only those who matched the highest
            List<Player> tied = new List<Player>();
            for (int i = 0; i < remaining.Count; i++)
            {
                if (remaining[i].Roll == highest)
                    tied.Add(remaining[i]);
            }

            if (tied.Count == 1)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Player {tied[0].PlayerNumber} is the dealer.");
                Console.WriteLine("-----------------------------------");
                return tied[0].PlayerNumber; // return dealer number
            }

            // Tie: announce and prepare the next round with only tied players
            Console.WriteLine("-----------------------------------");
            Console.Write($"Player ");
            for (int i = 0; i < tied.Count; i++)
            {
                Console.Write(tied[i].PlayerNumber);
                if (i < tied.Count - 1) Console.Write(" and ");
                tied[i].Roll = 0; // reset for next roll
            }
            Console.WriteLine($" tied with {highest}! ReRoll:");
            Console.WriteLine("-----------------------------------");

            remaining = tied; // next round: only tied players
        }
    }

    public static Card[][] DealAllCards(Deck deck, int dealerId, int numberOfPlayers)
{
    // 4 players, each with 13 cards
    Card[][] hands = new Card[numberOfPlayers][];
    for (int i = 0; i < numberOfPlayers; i++)
    {
        hands[i] = new Card[13];
    }

    // Keep track of how many cards each player has received
    int[] counts = new int[numberOfPlayers];

    // Dealing order: start at dealer, wrap around
    for (int i = 0; i < 52; i++)
    {
        int playerIndex = (dealerId - 1 + i) % numberOfPlayers; // 0..3
        Card card = deck.TakeTopCard();

        hands[playerIndex][counts[playerIndex]] = card;
        counts[playerIndex]++;
    }

    return hands;
}



    public static void Main(string[] args)
    {
        int numberOfPlayers = 4;

    // Roll for dealer
    int dealer = RollForDealer(numberOfPlayers);

    // Prepare deck
    Deck deck = new Deck();
    deck.Shuffle();
    deck.Cut(21); // optional

    // Deal all cards
    Card[][] hands = DealAllCards(deck, dealer, numberOfPlayers);

    // Confirm each player got 13 cards
    for (int i = 0; i < numberOfPlayers; i++)
    {
        Console.WriteLine($"Player {i + 1} has {hands[i].Length} cards.");
    }



    }
}












