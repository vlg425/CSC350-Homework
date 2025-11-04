
Project 1: Elevens
Author: Victor Garcia (CSC350H)

======================================================================

== Description ==

"Elevens" is a simple, text-based console card game implemented in C# and .NET 9. The objective is to clear the entire 52-card deck by strategically removing cards from a 9-card board.

The game is built with a focus on separating concerns, with different classes managing the game logic (Game.cs), the console display (ConsoleDisplay.cs), and the game state messages (StatusManager.cs).


== How to Play ==

The goal is to remove all cards from the deck and the board.

1.  A 9-card board is dealt from a 52-card deck.
2.  You can remove cards from the board in two ways:
    * **Valid Pair:** Select two cards that add up to 11 (e.g., 8 + 3, 7 + 4, 10 + Ace).
    * **Valid Trio:** Select a Jack, a Queen, and a King.
3.  After you make a valid move, the selected cards are removed from the board and replaced with new cards from the deck.
4.  The game ends when:
    * **You WIN:** The deck is empty and the board is cleared.
    * **You LOSE:** No more valid moves (Pairs or Trios) are possible on the board.

== Controls ==

* [1] - [9]: Toggle the selection for the card in that position.
* [Enter]:   Submit your selected cards to make a play.
* [Q]:       Quit the game at any time.
* [Y] / [N]: Choose to play a new game after a win or loss.


== How to Build and Run ==

[cite_start]This project is a .NET 9.0 console application[cite: 1].

1.  Ensure you have the .NET 9.0 SDK (or newer) installed on your machine.
2.  Open a terminal or command prompt.
3.  Navigate to the project folder (e.g., ".../project 1/elevens").
4.  Run the following command:

    dotnet run

This command will automatically build and launch the game in your console.


== Project Structure ==

The project logic is separated into the following key classes:

* Program.cs:           The main entry point. Handles the primary game loop and user input (key presses).
* Game.cs:              The core game logic engine. Manages the game state (Win/Loss/Turn), validates moves, and coordinates the board and deck.
* Board.cs:             Manages the 9 cards currently in play on the board. Handles card replacement logic.
* Deck.cs:              Represents the 52-card deck, with methods for shuffling and drawing cards.
* Card.cs / Rank.cs:    Defines the Card object, its Rank, and its Suit. (Note: This project also requires a `Suit.cs` enum file defining Hearts, Diamonds, Clubs, Spades).

* ConsoleDisplay.cs:    A dedicated class for rendering all UI components to the console (header, board, card art, and footer).
* StatusManager.cs:     A helper class that translates game logic enums (like `StatusText.ValidPair`) into user-friendly notification strings.
* StatusText.cs:        An enum listing all possible game move results and states (e.g., `ValidPair`, `InvalidTrioMove`, `Welcome`).
* Elevens.csproj:       The C# project file, which specifies the project target (net9.0)[cite: 1].


