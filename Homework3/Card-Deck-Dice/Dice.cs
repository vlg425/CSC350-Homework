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