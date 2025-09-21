using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            }
        }
    }

    //Implement a property to get Cards
    

    //Takes top card from deck (if deck is not empty, otherwise return null)
    public Card TakeTopCard()
    {
        //implementation
    }

    //Shuffle Method
    public void Shuffle()
    {
        //implementation
    }

    //Cut method
    public void Cut(int index)
    {
        //implementation
    }
}

