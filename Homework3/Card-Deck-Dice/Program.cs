//************************************************************************
// Victor Garcia
// CSC 350H
// Homework 3 - Card, Deck, Dice
//************************************************************************

using System.Security.Cryptography.X509Certificates;
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
    public static void RollForDealer(int numberOfPlayers)
    {
        Dice dice = new Dice(2);
        List<Player> players = new List<Player>();
        for (int i = 1; i <= numberOfPlayers; i++) players.Add(new Player(i));

        // Start with all players as dealer
        List<Player> dealer = new List<Player>(players);

        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Rolling for dealer...");
        Console.WriteLine("-----------------------------------");

        while (true)
        {
            int highest = 0;

            // Roll and announce for each player in dealer
            for (int i = 0; i < dealer.Count; i++)
            {
                dice.Roll();
                dealer[i].Roll = dice.TopSide;
                Console.WriteLine($"Player {dealer[i].PlayerNumber} rolls {dealer[i].Roll}");
                if (dealer[i].Roll > highest)
                    highest = dealer[i].Roll;
            }

            // Keep only those who matched the highest
            List<Player> tied = new List<Player>();
            for (int i = 0; i < dealer.Count; i++)
            {
                if (dealer[i].Roll == highest)
                    tied.Add(dealer[i]);
            }

            if (tied.Count == 1)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Player {tied[0].PlayerNumber} is the dealer.");
                Console.WriteLine("-----------------------------------");
                return; // done
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
            Console.WriteLine(" tied with 6! ReRoll:");
            Console.WriteLine("-----------------------------------");

            dealer = tied; // next round: only tied players
        }
    }


    public static void Main(string[] args)
    {
        int numberOfPlayers = 4;
        RollForDealer(numberOfPlayers);

    }
}












