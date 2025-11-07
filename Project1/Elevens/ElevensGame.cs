using System.Collections.Generic;

namespace Elevens
{
    //================================================================================
    // **ElevensGame**
    //
    // This class is the core of the Model. It holds the game's state,
    // (Deck, Board, selected cards) and all the "action" methods
    // that the Controller can call (e.g., AttemptTurn).
    //================================================================================
    public class ElevensGame
    {
        // --- Private Fields ---
        private readonly ElevensRules _rules;      // The "rulebook"
        private readonly List<int> _selectedSlots; // The list of *indices* (0-8)
        private List<Card> _currentTurnCards;      // The cards from the last play

        // --- Public Properties ---
        // These are read-only for the Controller and View
        public GameState CurrentState { get; private set; }
        public Deck Deck { get; }
        public Board Board { get; }
        public int GamesPlayed { get; private set; }
        public int GamesWon { get; private set; }
        public int CurrentRulesPage { get; private set; }
        public IReadOnlyList<int> SelectedSlots
        {
            get { return _selectedSlots; }
        }

        public TurnResult CurrentTurnResult { get; private set; }
        public IReadOnlyList<Card> CurrentTurnCards
        {
            get { return _currentTurnCards; }
        }

        // --- Constructor ---
        // Creates all the objects the game needs to run.
        public ElevensGame()
        {
            _rules = new ElevensRules();
            Deck = new Deck();
            Board = new Board(size: 9);
            _selectedSlots = new List<int>();
            _currentTurnCards = new List<Card>();

            GamesPlayed = 0;
            GamesWon = 0;
            CurrentState = GameState.Initializing;
            CurrentTurnResult = TurnResult.None;
            CurrentRulesPage = 1;
        }

        // --- Public Methods ---

        // Sets up the game for a new round.
        public void StartNewGame()
        {
            GamesPlayed++;
            DealNewBoard();

            // Guarantees the first board is always playable
            while (_rules.NoMovesAvailable(Board.CardsOnBoard))
            {
                DealNewBoard(); // Re-deal until we get a valid board
            }

            _selectedSlots.Clear();
            _currentTurnCards.Clear();
            CurrentTurnResult = TurnResult.Welcome;
            CurrentState = GameState.PlayerTurn;
        }

        // Checks for a Win (empty deck/board) or Loss (no moves) condition.
        public void CheckForWinOrLoss()
        {
            if (CurrentState != GameState.PlayerTurn)
            {
                return;
            }

            // Win Condition
            if (Deck.Count == 0 && Board.ActiveCardCount == 0)
            {
                GamesWon++;
                CurrentState = GameState.Win;
            }
            // Loss Condition
            else if (_rules.NoMovesAvailable(Board.CardsOnBoard))
            {
                CurrentState = GameState.Loss;
            }
        }

        // Selects or deselects a card slot.
        // number: The 1-based number (1-9) the user pressed.
        public void ToggleSelection(int number)
        {
            CurrentTurnResult = TurnResult.None; // Reset the message
            int index = number - 1; // Convert 1-based to 0-based

            if (Board.ReadCardAt(index) == null)
            {
                CurrentTurnResult = TurnResult.SlotIsEmpty;
                return;
            }

            if (_selectedSlots.Contains(index))
            {
                _selectedSlots.Remove(index); // Deselect
            }
            else if (_selectedSlots.Count < 3)
            {
                _selectedSlots.Add(index); // Select
            }
            else
            {
                // Tried to select a 4th card
                CurrentTurnResult = TurnResult.InvalidSelection;
            }
        }

        // Attempts to play the currently selected cards.
        public void AttemptTurn()
        {
            _currentTurnCards.Clear();

            // 1. Check for valid number of selected cards
            if (_selectedSlots.Count < 2 || _selectedSlots.Count > 3)
            {
                if (_selectedSlots.Count == 0)
                {
                    CurrentTurnResult = TurnResult.NoCardSelected;
                }
                else
                {
                    CurrentTurnResult = TurnResult.InvalidSelection;
                }
                _selectedSlots.Clear();
                return;
            }

            // 2. Get the actual Card objects from the selected indices
            List<Card> currentTurnCards = new List<Card>();
            foreach (int index in _selectedSlots)
            {
                Card? card = Board.ReadCardAt(index);
                if (card != null)
                {
                    currentTurnCards.Add(card);
                }
            }
            _currentTurnCards = currentTurnCards; // Store for the View
            
            // 3. Ask the "rulebook" if the play is valid
            if (_rules.CheckIfValid(_currentTurnCards))
            {
                // --- Valid Move ---
                if (_selectedSlots.Count == 2)
                {
                    CurrentTurnResult = TurnResult.ValidPair;
                }
                else
                {
                    CurrentTurnResult = TurnResult.ValidTrio;
                }
                
                // Replace cards on the board
                foreach (var index in _selectedSlots)
                {
                    Board.PlaceCardAt(index, Deck.Deal());
                }
            }
            else
            {
                // --- Invalid Move ---
                if (_selectedSlots.Count == 2)
                {
                    CurrentTurnResult = TurnResult.InvalidPairSum;
                }
                else
                {
                    CurrentTurnResult = TurnResult.InvalidTrioMove;
                }
            }

            // 4. Always clear selection after an attempt
            _selectedSlots.Clear();
        }

        // Allows the Controller to manually change the game state.
        public void SetState(GameState newState)
        {
            CurrentState = newState;
        }

        // --- Private Helpers ---

        // Resets the deck and deals 9 new cards to the board.
        private void DealNewBoard()
        {
            Deck.Reset();
            Deck.Shuffle();
            for (int i = 0; i < Board.BoardSize; i++)
            {
                Board.PlaceCardAt(i, Deck.Deal());
            }
        }

        // Changes the current rules page, clamping it between 1 and 3
        public void ChangeRulesPage(int direction)
        {
            int newPage = CurrentRulesPage + direction;

            if (newPage < 1)
            {
                newPage = 4; 
            }
            if (newPage > 4)
            {
                newPage = 1; 
            }
            
            CurrentRulesPage = newPage;
        }
    }
}