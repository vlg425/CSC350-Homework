using System.Collections.Generic;
using System.Text;

namespace Elevens
{
    // Manages the core game logic, state, and rules for Elevens.
    public class Game
    {
        // --- Properties ---

        // The deck of cards used for the game.
        public Deck deck { get; private set; }

        // The 9-card board where plays are made.
        public Board board { get; private set; }

        // Flag to indicate if the game has ended.
        public bool gameOver { get; private set; }

        // Flag to indicate if the player won the game.
        public bool playerWon { get; private set; }

        // Helper class to check if moves are valid.
        private MoveValidator moveValidator;

        // --- Constructor ---

        // Initializes a new game instance.
        public Game()
        {
            deck = new Deck();
            board = new Board();
            moveValidator = new MoveValidator();
            gameOver = false;
            playerWon = false;
        }
        
        // --- Public Methods ---

        // Resets the game to a new, shuffled state.
        public void StartNewGame()
        {
            deck = new Deck();
            deck.Shuffle();
            board.ClearBoard();
            board.DealInitial(deck);
            gameOver = false;
            playerWon = false;
            
            // Keep reshuffling the whole deck until we get a playable hand.
            while (!moveValidator.HasAnyValidMove(board.GetCards()))
            {
                // This check is a safety net, but should be almost impossible
                // to hit with a full 52-card deck.
                if (deck.Empty) {
                    gameOver = true;
                    playerWon = false;
                    return; // Exit if we somehow have no cards and no moves
                }

                Console.WriteLine("Initial hand is unplayable. Reshuffling and dealing a new hand...");
                
                // Re-gather all 52 cards, shuffle, and deal again.
                deck = new Deck(); // Get a fresh 52-card deck
                deck.Shuffle();
                board.ClearBoard();
                board.DealInitial(deck); // Deal 9 new cards
            }
            // The loop will only finish when the board has at least one valid move.
        }

        // Attempts to play the cards selected by the user.
        public bool TryPlaySelected(List<Card> selected)
        {
            bool isValidMove = false;
            
            // Check if it's a valid pair summing to 11
            if (selected.Count == 2 && moveValidator.IsValidPairSum11(selected[0], selected[1]))
            {
                isValidMove = true;
            }
            // Check if it's a valid J-Q-K set
            else if (selected.Count == 3 && moveValidator.IsValidJQK(selected))
            {
                isValidMove = true;
            }

            // If the move is valid, update the board and check the game state
            if (isValidMove)
            {
                board.Discard(selected);
                board.Refill(deck);
                CheckForWin(); // Check for a win/loss after refilling
                return true;
            }

            // If the move was not valid, return false
            return false;
        }
        
        // Gets a specific card from the board by its index.
        public Card? GetCardFromBoard(int index)
        {
            var cards = board.GetCards();
            if (index >= 0 && index < cards.Count)
            {
                return cards[index];
            }
            return null; // Return null if the index is out of bounds
        }

        // Gets the number of cards remaining in the deck.
        public int GetUndealtCount()
        {
            return deck.Count;
        }

        // --- Private Methods ---

        // Checks if the game has been won or lost and updates the game state.
        private void CheckForWin()
        {
            // Win condition: The board and deck are both empty.
            if (board.Count == 0 && deck.Empty)
            {
                gameOver = true;
                playerWon = true;
            }
            // Loss condition: There are no possible moves left on the board.
            // This is the fix: We don't need to check if the deck is empty.
            // If the board is stuck, the game is over.
            else if (!moveValidator.HasAnyValidMove(board.GetCards()))
            {
                gameOver = true;
                playerWon = false;
            }
        }
    }
}