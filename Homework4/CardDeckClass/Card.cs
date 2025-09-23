//************************************************************
// Victor Garcia
// CSC350 
// Homework 4
// Card class
//************************************************************

public class Card
{
    //Fields, example: Rank rank;
    //check the help documentation for the fields
    Rank rank;
    Suit suit;
    bool faceUp;

    //Card Constructor
    public Card(Rank rank, Suit suit)
    {
        //implementation
        this.rank = rank;
        this.suit = suit;
    }

    //Define properties for all above fields
    //code example: public Suit Suit { get { return suit; } }
    public Rank Rank{ get{ return rank; } }
    public Suit Suit{ get{ return suit; } }   
    public bool FaceUp{ get{ return faceUp; } }

    public void FlipOver()
    {
        //implementation 
        faceUp = !faceUp;
    }
        
}
