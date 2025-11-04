//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

using System.Text;

namespace Elevens
{
    // Handles all logic for drawing the game state to the console.
    public class ConsoleDisplay
    {
        private const int ColumnWidth = 6;
        private const string Separator =    "=====================================================";
        private const string BlankLine =    "                                                     ";
        private const string Instructions = "[1]-[9] to toggle. [ENTER] to play. [Q] to quit.";

        public ConsoleDisplay()
        {
            //BlankLine = "".PadRight(Separator.Length);
        }

        private void ClearRegion(int top, int height)
        {
            Console.SetCursorPosition(0, top);
            for (int i = 0; i < height; i++)
            {
                Console.WriteLine(BlankLine);
            }
        }

        // --- COMPONENT 1: HEADER (Lines 0-3) ---
        public void DrawHeader(int deckCount, int wins, int totalGames)
        {
            int headerStart = 0;
            string deckString = $"Deck: {deckCount} cards remaining";
            string winsString = $"Wins: {wins} of {totalGames} games";

            Console.SetCursorPosition(0, headerStart);
            Console.WriteLine("ELEVENS");
            Console.WriteLine(Separator);
            Console.WriteLine(deckString.PadRight(Separator.Length - winsString.Length) + winsString);
            Console.WriteLine(Separator);
        }

        // --- COMPONENT 2: BOARD (Lines 4-8) ---
        public void DrawBoard(Board board, bool[] selected)
        {
            int boardStart = 4;
            ClearRegion(boardStart, 5); 

            Console.SetCursorPosition(0, boardStart); 
            Console.WriteLine(Instructions.PadRight(Separator.Length));

            var cards = board.GetCards();
            if (cards.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                return;
            }

            var cardTopRow = new StringBuilder();
            var cardMidRow = new StringBuilder();
            var cardBotRow = new StringBuilder();
            var selectdRow = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                if (i < cards.Count)
                {
                    var card = cards[i];
                    
                    string rank = card.RankSymbol;
                    string suit = card.SuitSymbol;
                    string r = (rank == "10") ? $"{rank}{suit}" : $"{rank} {suit}";
                    string s = selected[i] ? "[■]" : $"[{i + 1}]";

                    cardTopRow.Append( "┌───┐".PadRight(ColumnWidth));                  
                    cardMidRow.Append($"│{r}│".PadRight(ColumnWidth));
                    cardBotRow.Append( "└───┘".PadRight(ColumnWidth));
                    selectdRow.Append($" {s}".PadRight(ColumnWidth));
                }
                else
                {
                    string empty = "".PadRight(ColumnWidth);
                    cardTopRow.Append(empty);
                    cardMidRow.Append(empty);
                    cardBotRow.Append(empty);
                    selectdRow.Append(empty);
                }
            }
            
            Console.WriteLine(cardTopRow.ToString());
            Console.WriteLine(cardMidRow.ToString());
            Console.WriteLine(cardBotRow.ToString());
            Console.WriteLine(selectdRow.ToString());
        }

        // --- COMPONENT 3: FOOTER (Lines 9-11) ---
        public void DrawFooter(GameState gameState, string? notification)
        {
            int footerStart = 9;
            ClearRegion(footerStart, 3); 
            Console.SetCursorPosition(0, footerStart);

            Console.WriteLine(Separator);

            string message;
            if (notification != null)
            {
                message = notification;
            }
            else
            {
                message = "";
            }
            
            Console.WriteLine(message.PadRight(Separator.Length));

            Console.WriteLine(Separator);
        }

        // --- FINAL SCREEN ---
        public void DrawGoodbyeScreen(int wins, int totalGames)
        {
            Console.CursorVisible = true;
            Console.Clear(); 
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine($"You won {wins} out of {totalGames} games.");
        }
    }
}

