namespace Elevens
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new ElevensGame();
            game.StartNewGame();

            game.DisplayBoard();
        }
    }
}