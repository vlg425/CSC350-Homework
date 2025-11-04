//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

namespace Elevens
{
    public enum GameState
    {
        PlayerTurn,
        Win,
        Loss
    }

    public class Game
    {
        public Board Board { get; private set; }
        public Deck deck { get; private set; }
        public GameState CurrentState { get; private set; }

        public Game()
        {
            deck = new Deck();
            deck.Shuffle();
            Board = new Board();
            Board.DealInitial(deck);
            CurrentState = GameState.PlayerTurn;
        }

        public StatusText PlayTurn(List<Card> selectedCards)
        {
            if (CurrentState != GameState.PlayerTurn)
                return StatusText.None;

            if (selectedCards.Count == 0)
                return StatusText.NoCardSelected;

            var moveResult = IsValidMove(selectedCards);

            if (moveResult == StatusText.ValidPair || moveResult == StatusText.ValidTrio)
            {
                Board.ReplaceAndRefill(selectedCards, deck);
                Board.Refill(deck);
                UpdateGameState();
                return moveResult; 
            }
            else
            {
                UpdateGameState(); 
                return moveResult; 
            }
        }

        private void UpdateGameState()
        {
            if (deck.Empty && Board.Count == 0)
            {
                CurrentState = GameState.Win;
            }
            else if (!AnotherPlayIsPossible())
            {
                CurrentState = GameState.Loss;
            }
        }

        private StatusText IsValidMove(List<Card> selection)
        {
            if (selection.Count == 2)
            {
                if ((int)selection[0].Rank + (int)selection[1].Rank == 11)
                {
                    return StatusText.ValidPair;
                }
                else
                {
                    return StatusText.InvalidPairSum;
                }
            }

            if (selection.Count == 3)
            {
                bool hasJ = selection.Any(c => c.Rank == Rank.Jack);
                bool hasQ = selection.Any(c => c.Rank == Rank.Queen);
                bool hasK = selection.Any(c => c.Rank == Rank.King);

                if (hasJ && hasQ && hasK)
                {
                    return StatusText.ValidTrio;
                }
                else
                {
                    return StatusText.InvalidTrioMove;
                }
            }

            return StatusText.InvalidSelection;
        }

        private bool AnotherPlayIsPossible()
        {
            var boardCards = Board.GetCards();

            if (boardCards.Count < 2) return false;

            if (boardCards.Any(c => c.Rank == Rank.Jack) &&
                boardCards.Any(c => c.Rank == Rank.Queen) &&
                boardCards.Any(c => c.Rank == Rank.King))
            {
                return true;
            }

            for (int i = 0; i < boardCards.Count; i++)
            {
                for (int j = i + 1; j < boardCards.Count; j++)
                {
                    if ((int)boardCards[i].Rank + (int)boardCards[j].Rank == 11)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

