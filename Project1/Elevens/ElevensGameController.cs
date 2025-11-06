using System;
using System.Runtime.InteropServices;

namespace Elevens
{
    //================================================================================
    // **ElevensGameController**
    //
    // Connects ElevensGame and ElevensDisplay.
    // This class runs the main game loop, gets user input,
    // and tells them what to do.
    //================================================================================
    public class ElevensGameController
    {
        // --- Private Fields ---
        private ElevensGame _game;      
        private ElevensDisplay _display; 
        
        // Constructor:
        public ElevensGameController()
        {
            _game = new ElevensGame();
            _display = new ElevensDisplay();
        }

        // This is the main public method called by Program.cs.
        // It sets up the console and runs the main game loop.
        public void Run()
        {
            Console.Title = "Elevens";
            
            // Hide the console cursor for a cleaner look
            bool previousCursorVisible = false;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    previousCursorVisible = Console.CursorVisible;
                    Console.CursorVisible = false;
                }
                catch (Exception) { /* Ignored */ }
            }
            
            // --- Main Game Loop ---
            while (_game.CurrentState != GameState.Exiting)
            {
                // 1. Draw the screen
                DrawFrame();
                
                // 2. Handle the logic for the current state
                HandleGameState();
            }

            // --- Game Exit ---
            _display.ShowGameSummary(_game.GamesWon, _game.GamesPlayed);
            
            // Restore the cursor
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    Console.CursorVisible = previousCursorVisible;
                }
                catch (Exception) { /* Ignored */ }
            }
        }

        // "Routes" the logic to the correct helper method
        // based on the game's current state.
        private void HandleGameState()
        {
            switch (_game.CurrentState)
            {
                case GameState.Initializing:
                    _game.StartNewGame();
                    break;

                case GameState.PlayerTurn:
                    HandlePlayerTurn();
                    break;

                // When the game ends, wait for (Y/N) input
                case GameState.Win:
                case GameState.Loss:
                    HandlePlayAgainInput();
                    break;

                case GameState.GameOver:
                    HandlePlayAgainInput(); // Safety net
                    break;

                case GameState.Exiting:
                    // Do nothing; the loop will exit
                    break;
            }
        }

        // Handles all user input during a normal player turn.
        private void HandlePlayerTurn()
        {
            // 1. Get Input
            var key = Console.ReadKey(true).Key;

            // 2. Process Input
            switch (key)
            {
                case ConsoleKey.Q: 
                    _game.SetState(GameState.Exiting);
                    break;

                case ConsoleKey.Enter:
                    _game.AttemptTurn();
                    break;

                // Top row number keys
                case ConsoleKey.D1: case ConsoleKey.D2: case ConsoleKey.D3:
                case ConsoleKey.D4: case ConsoleKey.D5: case ConsoleKey.D6:
                case ConsoleKey.D7: case ConsoleKey.D8: case ConsoleKey.D9:
                    // '1' (49) - '0' (48) = 1
                    _game.ToggleSelection(key - ConsoleKey.D0);
                    break;

                // Numpad keys
                case ConsoleKey.NumPad1: case ConsoleKey.NumPad2: case ConsoleKey.NumPad3:
                case ConsoleKey.NumPad4: case ConsoleKey.NumPad5: case ConsoleKey.NumPad6:
                case ConsoleKey.NumPad7: case ConsoleKey.NumPad8: case ConsoleKey.NumPad9:
                    // 'NumPad1' (97) - 'NumPad0' (96) = 1
                    _game.ToggleSelection(key - ConsoleKey.NumPad0);
                    break;
            }

            // 3. AFTER the move, check for a win or loss
            if (_game.CurrentState == GameState.PlayerTurn)
            {
                _game.CheckForWinOrLoss();
            }
        }

        // Handles user input on the "Game Over" screen.
        private void HandlePlayAgainInput()
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Y)
            {
                _game.SetState(GameState.Initializing);
            }
            else if (key == ConsoleKey.N)
            {
                _game.SetState(GameState.Exiting);
            }
            // If any other key is pressed, do nothing.
        }

        // Draws the entire game screen from top to bottom.
        // Renamed from RenderFrame.
        private void DrawFrame()
        {
            Console.Clear(); // Clear screen to prevent artifacts
            
            // 1. Draw Header
            _display.DrawHeader(_game.Deck.Count, _game.GamesWon, _game.GamesPlayed);
            
            // 2. Draw Board
            _display.DrawBoard(_game.Board.CardsOnBoard, _game.SelectedSlots);
            
            // 3. Draw Footer
            _display.DrawFooter(_game.CurrentState, _game.CurrentTurnResult, _game.CurrentTurnCards);
        }
    }
}