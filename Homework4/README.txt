=======================
CSC350 Project 1: Elevens Card Game
=======================

A console-based implementation of the "Elevens" card game, written in C# for .NET. The objective is to clear the entire 52-card deck by making valid plays on a 9-card board.


-------------------
How to Build and Run
-------------------

This project is built with .NET. You can run it from the command line or directly from an IDE like Visual Studio Code.

1.  Prerequisites: Make sure you have the .NET SDK installed on your system.
2.  Open Terminal: Open a terminal or command prompt in the root directory of the project (the folder containing `Elevens.csproj` [cite: 1]).
3.  Build the Project Run the following command to compile the application:
    dotnet build
4.  Run the Game: After the build is successful, run this command to start the game:
    dotnet run


-------------------
How to Play
-------------------

The goal of Elevens is to remove all cards from the deck and the board.

1.  The Board The game starts with 9 cards dealt from a shuffled 52-card deck. Each card on the board is assigned an index number from 0 to 8.
2.  Making a Move: To make a move, type the index numbers of the cards you wish to play, separated by spaces, and press Enter. For example: `0 4`
3.  Valid Moves: There are two types of valid moves:
    - Pair Sum 11: Select two cards whose ranks add up to 11 (e.g., a 3 and an 8). Aces count as 1. Face cards (Jack, Queen, King) cannot be used in these pairs.
    - J-Q-K Set: Select a set of three cards consisting of one Jack, one Queen, and one King.
4.  Gameplay Loop: After you make a valid move, the selected cards are removed from the board, and the empty spots are automatically refilled with new cards from the deck.
5.  Winning: You win the game if you successfully clear the entire board and the deck runs out of cards.
6.  Losing: The game is over if the deck is empty and there are no more possible valid moves on the board.


-------------------
Project File Structure
-------------------

* `Program.cs`: The main entry point for the application. It contains the primary game loop and handles all user input from the console.
* `Game.cs`: The core game engine. It manages the game state, including the deck, the board, and win/loss conditions.
* `Board.cs`: Represents the 9 cards on the game board. Manages dealing, refilling, and discarding cards.
* `Deck.cs`: Represents a standard 52-card deck. It is responsible for creation, shuffling, and dealing the top card.
* `Card.cs`: Defines a single playing card with a `Rank` and a `Suit`.
* `MoveValidator.cs`: Contains the logic to verify if a player's selected cards constitute a valid move (either a sum-to-11 pair or a J-Q-K set).
* `ConsoleDisplay.cs`: Responsible for rendering the ASCII art representation of the cards and the board in the console.
* `Rank.cs` / `Suit.cs`: Enum files that define the possible ranks (Ace through King) and suits (Hearts, Diamonds, etc.) for the cards.
* `Elevens.csproj`: The .NET project file that defines project settings and dependencies.