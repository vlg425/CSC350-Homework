using System.Collections.Generic;

namespace Elevens
{
    public class ElevensGame
    {
        public Deck deck { get; private set; }
        public Board board { get; private set; }
        public bool gameOver { get; private set; }
        public bool playerWon { get; private set; }
        public int gamesPlayed { get; private set; }
        public int gamesWon { get; private set; }

        private SelectedCards selectedCards;
        private MoveValidator moveValidator;

        public ElevensGame()
        {
            this.deck = new Deck();
            this.board = new Board();
            this.selectedCards = new SelectedCards();
            this.moveValidator = new MoveValidator();
            gameOver = false;
            playerWon = false;
            gamesPlayed = 0;
            gamesWon = 0;
        }
        // StartNewGame – Shuffles and deals 9 cards to start play
        public void StartNewGame()
        {
            deck.Shuffle();
            board.DealInitial(deck);
            gameOver = false;
            playerWon = false;
        }

        // SelectCard – Adds or removes a card from selection
        public void SelectCard(Card card)
        {
            selectedCards.Toggle(card);
        }

        // TryReplaceSelected – If selected cards form a valid move, remove and refill
        public void TryReplaceSelected()
        {
            var selected = selectedCards.GetSelected();
            bool validMove = false;

            if (selected.Count == 2 && moveValidator.IsValidPairSum11(selected[0], selected[1]))
            {
                validMove = true;
            }
            else if (selected.Count == 3 && moveValidator.IsValidJQK(selected))
            {
                validMove = true;
            }

            if (validMove)
            {
                board.Discard(selected);
                board.Refill(deck);
                selectedCards.Deselect();
                CheckForWin();
            }
        }

        // CheckForWin – Determines if the game is over
        public void CheckForWin()
        {
            if (board.Count == 0 && deck.Empty)
            {
                gameOver = true;
                playerWon = true;
                gamesPlayed++;
                gamesWon++;
            }
            else if (!moveValidator.HasAnyValidMove(board.GetCards()))
            {
                gameOver = true;
                playerWon = false;
                gamesPlayed++;
            }
        }
        // Restart – Resets everything for a new game
        public void Restart()
        {
            deck = new Deck();
            board.ClearBoard();
            selectedCards.Deselect();
            StartNewGame();
        }

        // GetUndealtCount – Returns the number of cards left in the deck
        public int GetUndealtCount()
        {
            return deck.Cards.Count;
        }

        public void DisplayBoard()
        {
            var cards = board.GetCards();
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine($"{cards[i].Rank} of {cards[i].Suit}");
            }
        }
    }
}