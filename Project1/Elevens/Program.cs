//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

using System.Text;

namespace Elevens
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Clear();

            ConsoleDisplay display = new ConsoleDisplay();
            StatusManager status = new StatusManager();

            int wins = 0;
            int totalGames = 0;

            // --- Outer "Play Again" Loop ---
            while (true)
            {
                totalGames++;
                Game game = new Game();
                bool[] selected = new bool[9];
                string statusMessage = status.GetStatus(StatusText.Welcome,game.CurrentState);

                // --- Initial Full-Screen Draw ---
                display.DrawHeader(game.deck.Count, wins, totalGames);
                display.DrawBoard(game.Board, selected);
                display.DrawFooter(game.CurrentState, statusMessage);

                // --- Inner "UI" Loop ---
                while (game.CurrentState == GameState.PlayerTurn)
                {
                    // --- Handle Input ---
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        List<Card> cardsToPlay = new List<Card>();
                        var boardCards = game.Board.GetCards();
                        for (int i = 0; i < boardCards.Count; i++)
                        {
                            if (selected[i])
                            {
                                cardsToPlay.Add(boardCards[i]);
                            }
                        }

                        StatusText moveResult = game.PlayTurn(cardsToPlay);
                        statusMessage = status.GetStatus(moveResult, game.CurrentState, cardsToPlay);
                        
                        Array.Clear(selected, 0, selected.Length);

                        display.DrawHeader(game.deck.Count, wins, totalGames);
                        display.DrawBoard(game.Board, selected);
                        display.DrawFooter(game.CurrentState, statusMessage);
                    }
                    else if (keyInfo.Key == ConsoleKey.Q)
                    {
                        display.DrawGoodbyeScreen(wins, totalGames);
                        return; // Exit application
                    }
                    else if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D9)
                    {
                        int index = keyInfo.Key - ConsoleKey.D1;
                        var boardCards = game.Board.GetCards();
                        if (index < boardCards.Count)
                        {
                            selected[index] = !selected[index];

                            // Redraw just the board to show the selection
                            display.DrawBoard(game.Board, selected);
                        }
                    }
                }
                // --- End of Inner "UI" Loop ---

                if (game.CurrentState == GameState.Win)
                {
                    wins++;
                    display.DrawHeader(game.deck.Count, wins, totalGames);
                }

                ConsoleKeyInfo playAgainKey;
                do
                {
                    playAgainKey = Console.ReadKey(true);
                } while (playAgainKey.Key != ConsoleKey.Y && playAgainKey.Key != ConsoleKey.N);

                if (playAgainKey.Key == ConsoleKey.N)
                {
                    break; 
                }
            }
            
            display.DrawGoodbyeScreen(wins, totalGames);
        }
    }
}

