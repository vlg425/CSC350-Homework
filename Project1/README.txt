# Elevens Card Game
**Author: Victor Garcia**
**For: CSC350**

This is a console application of the "Elevens" card game, built in C#.

## 🎮 What is Elevens?

The goal is to clear the entire 52-card deck by removing cards from a 9-slot board. You win if you clear the deck, and you lose if you get stuck with no more moves.

You can clear cards in two ways:
1.  **Pair:** Select any two cards that add up to **11**. (e.g., 8 + 3, 10 + Ace)
2.  **Trio:** Select a **Jack**, a **Queen**, and a **King** (from anywhere on the board).

*Note: Aces are worth 1. Jacks, Queens, and Kings cannot be used in pairs.*

---

## ⌨️ How to Play (Controls)

* **[1] - [9]**: Use the number keys (top row or numpad) to select or deselect a card.
* **[Enter]**: Press to play your selected cards.
* **[Q]**: Press at any time to quit the game.
* **[Y] / [N]**: After a game ends, press 'Y' to play again or 'N' to exit.

---

## 🛠️ How to Build and Run

You must have the .NET SDK installed on your computer.

1.  Open your terminal or command prompt.
2.  Navigate to the root folder of the project (`/Elevens`).
3.  To build the project, run:
    ```
    dotnet build
    ```
4.  To run the game, use:
    ```
    dotnet run
    ```

---