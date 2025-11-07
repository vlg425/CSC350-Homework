using System;
using System.Text;
using System.Collections.Generic;

namespace Elevens
{
    //================================================================================
    // **ElevensDisplay (The "View")**
    //================================================================================
    public class ElevensDisplay
    {
        // --- Private Helpers ---
        private void SetPos(int x, int y)
        {
            try { Console.SetCursorPosition(x, y); }
            catch (ArgumentOutOfRangeException) { /* Console too small */ }
        }

        // Centers text within a given width
        private string CenterText(string text, int width)
        {
            if (text.Length >= width) return text.Substring(0, width);
            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }

        // Translates game state into a human-readable notification
        private string GenerateNotification(GameState state, TurnResult turnResult, IReadOnlyList<Card> cards)
        {
            if (state == GameState.ConfirmQuit)
            {
                return "Are you sure you want to quit? [Y]/[N]";
            }
            if (state == GameState.Win)
            {
                return "CONGRATULATIONS! YOU WIN! Play again? [Y]/[N]";
            }
            if (state == GameState.Loss)
            {
                return "No more moves! You Lose! Play again? [Y]/[N]";
            }
            if (state == GameState.ViewingRules)
            {
                return "Use [←] and [→] to view rules. [R] to return.";
            }

            switch (turnResult)
            {
                case TurnResult.Welcome: return "[Numkeys]Select [Enter]Confirm [R]Rules [Q]Quit";
                case TurnResult.ValidPair: return $"Valid Pair! {cards[0].Symbols} + {cards[1].Symbols} = 11.";
                case TurnResult.InvalidPairSum:
                    int sum = (int)cards[0].Rank + (int)cards[1].Rank;
                    return $"Invalid Pair! {cards[0].Symbols} + {cards[1].Symbols} = {sum}.";
                case TurnResult.ValidTrio:
                    string j = ""; string q = ""; string k = "";
                    foreach (Card card in cards) {
                        if (card.Rank == Rank.Jack) j = card.Symbols;
                        else if (card.Rank == Rank.Queen) q = card.Symbols;
                        else if (card.Rank == Rank.King) k = card.Symbols;
                    }
                    return $"Valid Trio! {j}, {q}, {k}.";
                case TurnResult.InvalidTrioMove: return "Invalid Trio! A J, Q, and K must be selected.";
                case TurnResult.InvalidSelection: return "Invalid Move! Pair must = 11, Trio must = J, Q, K.";
                case TurnResult.NoCardSelected: return "You must select cards to play.";
                case TurnResult.SlotIsEmpty: return "That slot is empty. You cannot select it.";
                case TurnResult.None: default: return "[Numkeys]Select [Enter]Confirm [R]Rules [Q]Quit";
            }
        }

        // This new helper method replaces your entire if/else block
        private void DrawDeckImage(int deckCount)
        {
            int stackChars = (int)Math.Ceiling((deckCount / 43.0) * 6);
            string top, mid, bot;

            if (deckCount > 0)
            {
                top = "┌───┐" + "".PadRight(stackChars, '┐');
                mid = "│░░░│" + "".PadRight(stackChars, '│');
                bot = "└───┘" + "".PadRight(stackChars, '┘');
            }
            else
            {
                top = ""; mid = ""; bot = "";
            }

            SetPos(52, 6); Console.Write(CenterText(top, 14));
            SetPos(52, 7); Console.Write(CenterText(mid, 14));
            SetPos(52, 8); Console.Write(CenterText(bot, 14));
        }

        // --- Component Draw Methods ---

        // 1. Draws the static header banner
        private void DrawHeaderBanner()
        {
            SetPos(0, 0); Console.Write("┌──────────────────░█▀▀░█░░░█▀▀░█░█░█▀▀░█▀█░█▀▀░──────────────────┐");
            SetPos(0, 1); Console.Write("│░░░░░░░░░░░░░░░░░░░█▀▀░█░░░█▀▀░▀▄▀░█▀▀░█░█░▀▀█░░░░░░░░░░░░░░░░░░░│");
            SetPos(0, 2); Console.Write("└──────────────────░▀▀▀░▀▀▀░▀▀▀░░▀░░▀▀▀░▀░▀░▀▀▀░──by Victor Garcia┘");
        }

        // 2. Draws the STATIC frame for the main content
        private void DrawStaticFrame()
        {
            SetPos(0, 3);  Console.Write("╔═════════════════════════════════════════════════╦═══════════════╗");
            SetPos(0, 4);  Console.Write("║                                                 ║               ║");
            SetPos(0, 5);  Console.Write("╠═════════════════════════════════════════════════╬═══════════════╣");
            SetPos(0, 6);  Console.Write("║                                                 ║               ║");
            SetPos(0, 7);  Console.Write("║                                                 ║               ║");
            SetPos(0, 8);  Console.Write("║                                                 ║               ║");
            SetPos(0, 9);  Console.Write("║                                                 ║               ║");
            SetPos(0, 10); Console.Write("╚═════════════════════════════════════════════════╩═══════════════╝");
        }
        
        // 3. Draws the DYNAMIC content *inside* the frame
        private void DrawDynamicContent(
            GameState state,
            int deckCount, 
            int wins, 
            int totalGames, 
            IReadOnlyList<Card?> cards, 
            IReadOnlyList<int> selectedIndices, 
            TurnResult turnResult, 
            IReadOnlyList<Card> turnCards,
            int rulesPage
            )
        {
            // --- A. Draw Left Panel (Game) ---
            
            // Notification 
            string message = GenerateNotification(state, turnResult, turnCards);
            SetPos(2, 4); Console.Write(CenterText(message, 47));

            // Board (Cards and Selectors)
            int startX = 2 + (47 - (9 * 5)) / 2; 
            var selectorRow = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                Card? card = cards[i];
                bool isSelected = selectedIndices.Contains(i);
                int xPos = startX + (i * 5);
                
                if (isSelected && state != GameState.Loss)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                string r, s;
                if (card != null)
                {
                    r = (card.Rank == Rank.Ten) ? $"{card.RankSymbol}{card.SuitSymbol}" : $"{card.RankSymbol} {card.SuitSymbol}";
                    if (state == GameState.Loss)
                    {
                        s = "[x]";
                    }
                    else
                    {
                        s = isSelected ? "[■]" : $"[{i + 1}]";
                    }

                    SetPos(xPos, 6); Console.Write( "┌───┐");
                    SetPos(xPos, 7); Console.Write($"│{r}│");
                    SetPos(xPos, 8); Console.Write( "└───┘");
                }
                else
                {
                    s = "[x]";
                    SetPos(xPos, 6); Console.Write("┌───┐");
                    SetPos(xPos, 7); Console.Write("│░░░│");
                    SetPos(xPos, 8); Console.Write("└───┘");
                }
                
                if (isSelected && state != GameState.Loss)
                {
                    Console.ResetColor();
                }
                
                selectorRow.Append($" {s} ");
            }
            
            SetPos(2, 9); Console.Write(CenterText(selectorRow.ToString(), 47));

            
            // --- B. Draw Right Panel (Info) ---
            string winsString = $"Wins: {wins} / {totalGames}";
            string deckString = $"Deck: {deckCount} left";

            SetPos(51, 4); Console.Write(CenterText(winsString, 15));
            
            if (state == GameState.Win)
            {
                SetPos(52, 6); Console.Write(CenterText("╔══════╗", 14));
                SetPos(52, 7); Console.Write(CenterText("║ WIN! ║", 14));
                SetPos(52, 8); Console.Write(CenterText("╚══════╝", 14));
            }
            else
            {
               DrawDeckImage(deckCount);  
            }

            SetPos(51, 9); Console.Write(CenterText(deckString, 15)); 

            // --- C. Draw Rules (if in rules state) ---
            if (state == GameState.ViewingRules)
            {
                // --- THIS IS THE FIX ---
                // Clear the board area
                string blank = "                                               "; // 47 chars
                SetPos(2, 6); Console.Write(blank);
                SetPos(2, 7); Console.Write(blank);
                SetPos(2, 8); Console.Write(blank);
                
                // Use CenterText instead of manual spacing
                switch (rulesPage)
                {
                    case 1:
                        SetPos(2, 6); Console.Write(CenterText("Select cards to create a Pair or Trio", 47));
                        SetPos(2, 7); Console.Write(CenterText("and remove them from the game board.", 47));
                        break;
                    case 2:
                        SetPos(2, 6); Console.Write(CenterText("A Pair can be made when you select two", 47));
                        SetPos(2, 7); Console.Write(CenterText("cards from A-10 that add up to Eleven.", 47));
                        break;
                    case 3:
                        SetPos(2, 6); Console.Write(CenterText("A Trio can only be made by selecting the", 47));
                        SetPos(2, 7); Console.Write(CenterText("Jack, Queen, and King cards together.", 47));
                        break;
                    case 4:
                        SetPos(2, 6); Console.Write(CenterText("Successfully remove every card from the", 47));
                        SetPos(2, 7); Console.Write(CenterText("game board and you Win!", 47));
                        break;
                }
                SetPos(2, 9); Console.Write(CenterText($"[←] {rulesPage}/4 [→]", 47));
                // --- END FIX ---
            }       
        }

        // --- Public Main Draw Method ---
        
        public void DrawGameScreen(
            GameState state,
            int deckCount, 
            int wins, 
            int totalGames, 
            IReadOnlyList<Card?> cards, 
            IReadOnlyList<int> selectedIndices, 
            TurnResult turnResult, 
            IReadOnlyList<Card> turnCards,
            int currentRulesPage 
            )
        {
            // 1. Draw Static Header
            DrawHeaderBanner();
            
            // 2. Draw Static Frame
            DrawStaticFrame();
            
            // 3. Draw all dynamic content into the frame
            DrawDynamicContent(
                state, deckCount, wins, totalGames,
                cards, selectedIndices, turnResult, turnCards,
                currentRulesPage
            );

            // Set the cursor out of the way at the bottom
            SetPos(0, 13);
        }

        // This is the final screen.
        public void ShowGameSummary(int wins, int totalGames)
        {
            Console.CursorVisible = true;
            Console.Clear(); 
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine($"You won {wins} out of {totalGames} games.");
        }
    }
}