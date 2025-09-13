//************************************************************************
// Victor Garcia
// CSC 350H
// Homework 3 - Card, Deck, Dice
//************************************************************************

using Cards2;
using System;

public class Dice
{
    private int numOfSides; //number of sides on the dice
    private int topSide; //the side that is currently on top

    //default constructor, 6 sides
    public Dice()
    {
        numOfSides = 6;
        topSide = 1;
    }

    public Dice(int numOfSides)
    {
        if (numOfSides < 2)  //coin flip smallest kind of dice?
        {
            numOfSides = 6;
        }
        this.numOfSides = numOfSides;
        topSide = 1;
    }

    public void Roll()
    {
        Random rand = new Random();
        topSide = rand.Next(1, numOfSides + 1);

        if (topSide < 1 || topSide > numOfSides)
        {
            topSide = 1;
        }

    }

    public int TopSide { get { return topSide; } }

    public int NumOfSides(int numOfSides)
    {
        return numOfSides;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create and roll a 6-sided dice
        Dice dice = new Dice(6);
        dice.Roll();
        Console.WriteLine($"Rolled a {dice.TopSide} on a {dice.NumOfSides(6)}-sided dice.");

        // Create and roll a 20-sided dice
        Dice d20 = new Dice(20);
        d20.Roll();
        Console.WriteLine($"Rolled a {d20.TopSide} on a {d20.NumOfSides(20)}-sided dice.");

        // Create and display a deck of cards
        Deck deck = new Deck();
        deck.Shuffle();
        Console.WriteLine("Dealt 5 cards from the deck:");
        for (int i = 0; i < 5; i++)
        {
            Card card = deck.TakeTopCard();
            if (card != null)
            {
                Console.WriteLine(card);
            }
            else
            {
                Console.WriteLine("No more cards in the deck.");
            }
        }
        
    }
}