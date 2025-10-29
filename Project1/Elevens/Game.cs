using System.Collections.Generic;
using System.Text;

namespace Elevens
{
    public class Game
    {
        // ... (properties remain the same) ...
        public Deck deck { get; private set; }
        public Board board { get; private set; }
        public bool gameOver { get; private set; }
        public bool playerWon { get; private set; }
        private MoveValidator moveValidator;

        public Game()
        {
            deck = new Deck();
            board = new Board();
            moveValidator = new MoveValidator();
            gameOver = false;
            playerWon = false;
        }
        
        // ... (StartNewGame remains the same) ...
        public void StartNewGame()
        {
            deck = new Deck();
            deck.Shuffle();
            board.ClearBoard();
            board.DealInitial(deck);
            gameOver = false;
            playerWon = false;
            if (!moveValidator.HasAnyValidMove(board.GetCards()))
            {
                gameOver = true;
                playerWon = false;
            }
        }

        // Tries to play the user's selected cards
        public bool TryPlaySelected(List<Card> selected)
        {
            bool isValidMove = false;
            
            // FIXED: Call the corrected IsValidPairSum11 method
            if (selected.Count == 2 && moveValidator.IsValidPairSum11(selected[0], selected[1]))
            {
                isValidMove = true;
            }
            else if (selected.Count == 3 && moveValidator.IsValidJQK(selected))
            {
                isValidMove = true;
            }

            if (isValidMove)
            {
                board.Discard(selected);
                board.Refill(deck);
                CheckForWin();
                return true;
            }
            return false;
        }

        // ... (CheckForWin and DisplayBoard remain the same) ...
        private void CheckForWin()
        {
            if (board.Count == 0 && deck.Empty)
            {
                gameOver = true;
                playerWon = true;
            }
            else if (deck.Empty && !moveValidator.HasAnyValidMove(board.GetCards()))
            {
                gameOver = true;
                playerWon = false;
            }
        }
        
        
        // ... (GetCardFromBoard and GetUndealtCount remain the same) ...
        public Card? GetCardFromBoard(int index)
        {
            var cards = board.GetCards();
            if (index >= 0 && index < cards.Count)
            {
                return cards[index];
            }
            return null;
        }

        public int GetUndealtCount()
        {
            return deck.Count;
        }
    }
}