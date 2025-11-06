using System;
using System.Text;
using System.Collections.Generic;

namespace Elevens
{
    //================================================================================
    // **ElevensDisplay**
    //
    // This class is responsible for ALL drawing to the console.
    // reads data from the Model but cannot change it.
    // It also translates game enums into displayable strings.
    //================================================================================
    public class ElevensDisplay
    {
        // --- Constants ---
        private const int _columnWidth = 6;
        private const string _separator =    "=====================================================";
        private const string _blankLine =    "                                                     ";
        private const string _instructions = "[1]-[9] to toggle. [ENTER] to play. [Q] to quit.";

        // --- Private Helpers ---

        // Clears a specific rectangular region of the console.
        // Used to prevent flickering by only redrawing what's needed.
        private void ClearRegion(int top, int height)
        {
            Console.SetCursorPosition(0, top);
            for (int i = 0; i < height; i++)
            {
                Console.WriteLine(_blankLine);
            }
        }
        
        // Translates the game state and turn result into a
        // human-readable message for the footer.
        // - state: The current GameState.
        // - turnResult: The result of the last turn.
        // - cards: The cards involved in the last turn.
        // returns A string message for the player.
        private string GenerateNotification(GameState state, TurnResult turnResult, IReadOnlyList<Card> cards)
        {
            // GameState messages (Win/Loss) have top priority
            if (state == GameState.Win)
            {
                return "CONGRATULATIONS! YOU WIN! Play again? (Y/N)";
            }
            if (state == GameState.Loss)
            {
                return "No more moves! GAME OVER! Play again? (Y/N)";
            }

            // If not game over, show the turn result
            switch (turnResult)
            {
                case TurnResult.Welcome:
                    return "Rules: Valid Pair = 11, Valid Trio = J, Q, K.";
                case TurnResult.ValidPair:
                    return $"Valid Pair! {cards[0].Symbols} + {cards[1].Symbols} = 11.";
                case TurnResult.InvalidPairSum:
                    int sum = (int)cards[0].Rank + (int)cards[1].Rank;
                    return $"Invalid Pair! {cards[0].Symbols} + {cards[1].Symbols} = {sum}.";
                case TurnResult.ValidTrio:
                    // Find the J, Q, K to display them in order
                    string j = "";
                    string q = "";
                    string k = "";
                    foreach (Card card in cards)
                    {
                        if (card.Rank == Rank.Jack) j = card.Symbols;
                        else if (card.Rank == Rank.Queen) q = card.Symbols;
                        else if (card.Rank == Rank.King) k = card.Symbols;
                    }
                    return $"Valid Trio! {j}, {q}, {k}.";
                case TurnResult.InvalidTrioMove:
                    return "Invalid Trio! A J, Q, and K must be selected.";
                case TurnResult.InvalidSelection:
                    return "Invalid Move! Pair must = 11, Trio must = J, Q, K.";
                case TurnResult.NoCardSelected:
                    return "You must select cards to play.";
                case TurnResult.SlotIsEmpty:
                    return "That slot is empty. You cannot select it.";
                case TurnResult.None:
                default:
                    return ""; // Blank
            }
        }

        // --- Public Draw Methods ---

        // Draws the header (Lines 0-3)
        public void DrawHeader(int deckCount, int wins, int totalGames)
        {
            int headerStart = 0;
            string deckString = $"Deck: {deckCount} cards remaining";
            string winsString = $"Wins: {wins} of {totalGames} games";

            Console.SetCursorPosition(0, headerStart);
            Console.WriteLine("ELEVENS");
            Console.WriteLine(_separator);
            Console.WriteLine(deckString.PadRight(_separator.Length - winsString.Length) + winsString);
            Console.WriteLine(_separator);
        }

        // Draws the 9-slot board (Lines 4-8)
        public void DrawBoard(IReadOnlyList<Card?> cards, IReadOnlyList<int> selectedIndices)
        {
            int boardStart = 4;
            ClearRegion(boardStart, 5); // Clear the 5 lines of the board region

            Console.SetCursorPosition(0, boardStart); 
            Console.WriteLine(_instructions.PadRight(_separator.Length));

            // Use StringBuilders for performance
            var cardTopRow = new StringBuilder();
            var cardMidRow = new StringBuilder();
            var cardBotRow = new StringBuilder();
            var selectdRow = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                Card? card = cards[i];
                bool isSelected = selectedIndices.Contains(i);

                if (card != null)
                {
                    // Slot has a card
                    string rank = card.RankSymbol;
                    string suit = card.SuitSymbol;
                    string r = (rank == "10") ? $"{rank}{suit}" : $"{rank} {suit}";
                    string s = isSelected ? "[■]" : $"[{i + 1}]"; // Show selection or number

                    cardTopRow.Append("┌───┐".PadRight(_columnWidth));                  
                    cardMidRow.Append($"│{r}│".PadRight(_columnWidth));
                    cardBotRow.Append("└───┘".PadRight(_columnWidth));
                    selectdRow.Append($" {s}".PadRight(_columnWidth));
                }
                else
                {
                    // Slot is empty
                    string empty = "".PadRight(_columnWidth);
                    cardTopRow.Append(empty);
                    cardMidRow.Append(empty);
                    cardBotRow.Append(empty);
                    selectdRow.Append(" [x]".PadRight(_columnWidth)); // Show 'x' for empty
                }
            }
            
            // Draw the 4 built rows
            Console.WriteLine(cardTopRow.ToString());
            Console.WriteLine(cardMidRow.ToString());
            Console.WriteLine(cardBotRow.ToString());
            Console.WriteLine(selectdRow.ToString());
        }

        // Draws the footer (Lines 9-11)
        public void DrawFooter(GameState gameState, TurnResult turnResult, IReadOnlyList<Card> turnCards)
        {
            int footerStart = 9;
            ClearRegion(footerStart, 3); // Clear the 3 lines of the footer region
            Console.SetCursorPosition(0, footerStart);

            Console.WriteLine(_separator);

            // Get the message string from our helper
            string message = GenerateNotification(gameState, turnResult, turnCards);
            
            Console.WriteLine(message.PadRight(_separator.Length));

            Console.WriteLine(_separator);
        }

        // Draws the final "Thanks for playing" screen
        public void ShowGameSummary(int wins, int totalGames)
        {
            Console.CursorVisible = true;
            Console.Clear(); 
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine($"You won {wins} out of {totalGames} games.");
        }
    }
}