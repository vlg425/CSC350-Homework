using System.Reflection.Metadata;

namespace Elevens
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var display = new ConsoleDisplay();
            game.StartNewGame();

            Console.WriteLine("--- Welcome to Elevens! ---");
            Console.WriteLine("Select pairs of cards that sum to 11, or a set of Jack, Queen, King.");

            // Main game loop
            while (!game.gameOver)
            {
                // Display board state and deck count
                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"Cards left in deck: {game.GetUndealtCount()}");
                Console.WriteLine("Current Board:");
                display.DrawBoard(game.board);

                // Get player input for card selection
                Console.WriteLine("\nSelect cards to play by typing their numbers, separated by spaces (e.g., '0 4'):");
                var selectedCardsForMove = new List<Card>();
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input. Please enter numbers.");
                    continue;
                }

                // Parse and validate user input
                string[] parts = input.Trim().Split(' ');
                bool validInput = true;
                foreach (var part in parts)
                {
                    if (int.TryParse(part, out int index))
                    {
                        Card? card = game.GetCardFromBoard(index);
                        if (card != null)
                        {
                            selectedCardsForMove.Add(card);
                        }
                        else
                        {
                            Console.WriteLine($"Error: Index '{index}' is not valid. Please try again.");
                            validInput = false;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: '{part}' is not a valid number. Please try again.");
                        validInput = false;
                        break;
                    }
                }

                if (!validInput)
                {
                    continue; // Restart the loop to get fresh input
                }

                // Try to play the selected cards
                bool moveWasMade = game.TryPlaySelected(selectedCardsForMove);
                if (!moveWasMade)
                {
                    Console.WriteLine("That is not a valid move. Remember: pairs must sum to 11, or you need a Jack, Queen, and King.");
                }
            }

            // Game over message
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