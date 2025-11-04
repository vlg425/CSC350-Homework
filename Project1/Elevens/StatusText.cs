//********************************************************************************
// Victor Garcia
// CSC350H
// Project 1: Elevens
//
//********************************************************************************

namespace Elevens
{
    public enum StatusText
    {
        // --- Game Actions ---
        ValidPair,
        InvalidPairSum,
        ValidTrio,
        InvalidTrioMove,
        InvalidSelection,
        NoCardSelected,

        // --- Game State ---
        Welcome,
        Win,
        Loss,
        PlayAgain, // For when the "Play again?" message is active
        None // For clearing the notification
    }
}
