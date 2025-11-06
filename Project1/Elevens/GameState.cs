namespace Elevens
{
    //================================================================================
    // **GameState (Enum)**
    //
    // Represents the different "modes" the game can be in.
    // Used by the Game to decide what logic to run.
    //================================================================================
    public enum GameState
    {
        // Setting up a new game
        Initializing,
        // Waiting for 1-9 or Enter
        PlayerTurn,
        // Game just ended in a win
        Win,
        // Game just ended in a loss
        Loss,
        // Waiting for (Y/N) input (replaces Win/Loss state)
        GameOver,
        // Player pressed 'Q'
        Exiting
    }
}