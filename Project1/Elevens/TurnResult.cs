namespace Elevens
{
    //================================================================================
    // **TurnResult**
    //
    // Represents the outcome of a player's attempted turn.
    // Used by the View to generate the correct notification message.
    //================================================================================
    public enum TurnResult
    {
        // No message
        None,
        // "Rules: ..."
        Welcome,
        // "Valid Pair! ..."
        ValidPair,
        // "Invalid Pair! ..."
        InvalidPairSum,
        // "Valid Trio! ..."
        ValidTrio,
        // "Invalid Trio! ..."
        InvalidTrioMove,
        // "Invalid Move! ..."
        InvalidSelection,
        // "You must select..."
        NoCardSelected,
        // "That slot is empty..."
        SlotIsEmpty
    }
}