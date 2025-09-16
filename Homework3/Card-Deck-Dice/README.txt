Goal
Write a C# program that uses your existing Dice, Deck, and Card classes to:
    1.Choose a starting dealer (among 4 players) by dice roll.
    2.Deal an entire 52-card deck one card at a time so that each of four players ends up with exactly 13 cards.
    3.Print the cards in each player’s hand.
--------------------------------------------------------------------------------------------------------------------
Detailed Requirements
1.Setup
    - There are always 4 players.
    - Use your pre-implemented Dice class.
    - Each player rolls the dice once.
        Announce each roll, for example:
        "Player 1 rolls 4"
        "Player 2 rolls 6"
        "..."
    - The highest roll becomes the dealer.
    - If there is a tie, only the tied players re-roll until one winner remains.
        Display the winner:
        "Player k is the dealer."

2.Deck Preparation
    - Create a new Deck, call Shuffle(), and optionally Cut().

3. Dealing
    - Start dealing with the winning dealer.
    - Move clockwise, giving one card at a time to each player.
    - After the dealer, continue with the next player number and wrap around.
        Example: if Player 3 wins, the order is
        3 → 4 → 1 → 2 → 3 → 4 → …
    - Continue until all 52 cards are dealt.
    - Each player must end up with exactly 13 cards.

4. Print
    - Print each player’s full hand in the format:
        "Player 1:"
        "Rank of Suit"
        "Rank of Suit"
        "…"
--------------------------------------------------------------------------------------------------------------------
Deliverables (Submit the Link of the github repository)
    - Program.cs that integrates the existing Dice class and the provided Deck and Card classes.
    - Dice.cs
    - A screenshot of the console output showing:
        - all dice rolls (and any tie re-rolls),
        - the chosen dealer,
        - each player’s complete 13-card hand.
