namespace Elevens
{
    //================================================================================
    // **Program**
    //
    // This class is the entry point for the entire application.
    //================================================================================
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Create the controller
            ElevensGameController controller = new ElevensGameController();

            // 2. Turn the key and run the game
            controller.Run();
        }
    }
}