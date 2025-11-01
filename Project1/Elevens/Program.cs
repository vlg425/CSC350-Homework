namespace Elevens
{
    // This class holds the Main method, which is the entry point for the program.
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the game engine and the console display helper
            var game = new Game();
            var display = new ConsoleDisplay();
            game.StartNewGame();

            Console.WriteLine("--- Welcome to Elevens! ---");
            Console.WriteLine("Select pairs of cards that sum to 11, or a set of Jack, Queen, King.");

            // Main game loop: continues as long as the game is not over
            while (!game.gameOver)
            {
                // --- Display Game State ---
                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"Cards left in deck: {game.GetUndealtCount()}");
                Console.WriteLine("Current Board:");
                display.DrawBoard(game.board);

                // --- Get Player Input ---
                Console.WriteLine("\nSelect cards to play by typing their numbers, separated by spaces (e.g., '0 4'):");
                var selectedCardsForMove = new List<Card>(); // To hold the cards the user picks
                string? input = Console.ReadLine(); // Read the text the user types

                // Check for empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input. Please enter numbers.");
                    continue; // Skip the rest of the loop and ask again
                }

                // --- Parse and Validate Input ---
                string[] parts = input.Trim().Split(' '); // Split input by spaces, e.g., "0 4" -> ["0", "4"]
                bool validInput = true; // Flag to track if the input is good

                foreach (var part in parts)
                {
                    // Try to convert the text part to a number (index)
                    if (int.TryParse(part, out int index))
                    {
                        // If it's a number, get the card from the board at that index
                        Card? card = game.GetCardFromBoard(index);
                        if (card != null)
                        {
                            // Add the valid card to the list for this move
                            selectedCardsForMove.Add(card);
                        }
                        else
                        {
                            // The index was a number, but not a valid spot on the board
                            Console.WriteLine($"Error: Index '{index}' is not valid. Please try again.");
                            validInput = false;
                            break; // Stop parsing this input
                        }
                    }
                    else
                    {
                        // The input wasn't even a number (e.g., "a" or "hello")
                        Console.WriteLine($"Error: '{part}' is not a valid number. Please try again.");
                        validInput = false;
                        break; // Stop parsing this input
                    }
                }

                // If any part of the input was bad, restart the loop
                if (!validInput)
                {
                    continue;
                }

                // --- Attempt the Move ---
                // Try to play the selected cards
                bool moveWasMade = game.TryPlaySelected(selectedCardsForMove);
                
                // If the move was not legal, inform the user
                if (!moveWasMade)
                {
                    Console.WriteLine("That is not a valid move. Remember: pairs must sum to 11, or you need a Jack, Queen, and King.");
                }
            }

            // --- Game Over ---
            // This code runs only after the 'while' loop finishes (when game.gameOver is true)
            Console.WriteLine("\n--- Game Over ---");
            if (game.playerWon)
            {
                Console.WriteLine("Congratulations! You won! 🎉");
            }
            else
            {
                Console.WriteLine("No more moves are possible. Better luck next time!");
            }
        }
    }
}