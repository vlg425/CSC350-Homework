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

            // Main game loop: continues as long as the game is not over
            while (!game.gameOver)
            {
                Console.Clear(); // Clear the screen at the start of each turn

                // --- Display Game State ---
                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"Cards left in deck: {game.GetUndealtCount()}");
                Console.WriteLine("Current Board:");
                display.DrawBoard(game.board);

                // --- Get Player Input ---
                Console.ForegroundColor = ConsoleColor.Cyan; // Make the prompt color stand out
                Console.WriteLine("\nSelect cards to play by typing their numbers, separated by spaces (e.g., '0 4'):");
                Console.ResetColor(); // Reset color for user input

                var selectedCardsForMove = new List<Card>();
                string? input = Console.ReadLine();

                // Check for empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter numbers. Press Enter to try again.");
                    Console.ResetColor();
                    Console.ReadLine(); // <-- FIX 1: Pause for user to read
                    continue; // Skip the rest of the loop and ask again
                }

                // --- Parse and Validate Input ---
                string[] parts = input.Trim().Split(' ');
                bool validInput = true; 

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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: Index '{index}' is not valid. Press Enter to try again.");
                            Console.ResetColor();
                            Console.ReadLine(); // <-- FIX 1: Pause for user to read
                            validInput = false;
                            break; 
                        }
                    }
                    else
                    {
                        // The input wasn't even a number
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error: '{part}' is not a valid number. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine(); // <-- FIX 1: Pause for user to read
                        validInput = false;
                        break; 
                    }
                }

                // If any part of the input was bad, restart the loop
                if (!validInput)
                {
                    continue;
                }

                // --- Attempt the Move ---
                bool moveWasMade = game.TryPlaySelected(selectedCardsForMove);
                
                // If the move was not legal, inform the user
                if (!moveWasMade)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a valid move. Remember: pairs must sum to 11, or you need a J, Q, and K.");
                    Console.WriteLine("Press Enter to try again.");
                    Console.ResetColor();
                    Console.ReadLine(); // <-- FIX 1: Pause for user to read
                }
            }

            // --- Game Over ---
            // This code runs only after the 'while' loop finishes (when game.gameOver is true)
            
            Console.Clear(); // Clear the final board one last time
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Cards left in deck: {game.GetUndealtCount()}");
            Console.WriteLine("Final Board:");
            
            display.DrawBoard(game.board); // <-- FIX 2: Draw the final board state
            
            Console.WriteLine("\n--- Game Over ---");
            if (game.playerWon)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Green for win
                Console.WriteLine("Congratulations! You won! 🎉");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Yellow for loss
                Console.WriteLine("No more moves are possible. Better luck next time!");
                Console.ResetColor();
            }
        }
    }
}