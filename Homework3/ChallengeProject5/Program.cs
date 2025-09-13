//*********************
//Victor Garcia
//CSC 350 - Homework 3
//Challenge Project 5
//*********************

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = {"('-')", "(^-^)", "(X_X)"};
string[] foods = {"@@@@@", "$$$$$", "#####"};

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;

InitializeGame();
// Set this to true to enable termination on nondirectional key input
bool terminateOnNondirectional = true;

// Set this to true to enable the speed boost feature
bool enableSpeedBoost = true;

while (!shouldExit)
{
    // Check for terminal resize before each move
    if (TerminalResized())
    {
        Console.Clear();
        Console.WriteLine("Console was resized. Program exiting.");
        shouldExit = true;
        break;
    }

    // If the player should be frozen, freeze them and skip the rest of the loop
    if (player == "(X_X)")
    {
        // freeze the player for a second and reset to normal
        System.Threading.Thread.Sleep(1000);
        player = "('-')";
        continue;
    }

    // Set movement speed to 1 by default
    int moveSpeed = 1;
    // If speed boost is enabled and player looks like (^-^), set speed to 3
    if (enableSpeedBoost)
    {
        if (player == "(^-^)")
        {
            moveSpeed = 3;
        }
    }

    Move(terminateOnNondirectional, moveSpeed);

    // Check if player consumed the food
    if (PlayerConsumedFood())
    {
        ChangePlayer();
        ShowFood();
    }
}
// Returns true if the player has consumed the food
bool PlayerConsumedFood()
{
    // Check if the player's position overlaps with the food's position
    // The player and food are both strings, so check if any part of the player overlaps the food horizontally
    // (Assume vertical overlap is just Y coordinate match)
    for (int i = 0; i < player.Length; i++)
    {
        if ((playerX + i) >= foodX && (playerX + i) < (foodX + foods[food].Length) && playerY == foodY)
        {
            return true;
        }
    }
    return false;
}

// Returns true if the Terminal was resized 
bool TerminalResized() 
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

// Displays random food at a random location
void ShowFood() 
{
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer() 
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Reads directional input from the Console and moves the player
void Move(bool terminateOnNondirectional, int moveSpeed)
{
    int lastX = playerX;
    int lastY = playerY;
    
    bool isDirectional = false;
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            playerY--;
            isDirectional = true;
            break;
        case ConsoleKey.DownArrow:
            playerY++;
            isDirectional = true;
            break;
        case ConsoleKey.LeftArrow:
            playerX--;
            isDirectional = true;
            break;
        case ConsoleKey.RightArrow:
            playerX++;
            isDirectional = true;
            break;
        case ConsoleKey.Escape:
            shouldExit = true;
            break;
    }

    // If enabled, terminate on nondirectional key input
    if (terminateOnNondirectional && !isDirectional && Console.ReadKey(true).Key != ConsoleKey.Escape)
    {
        shouldExit = true;
        return;
    }

    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++)
    {
        Console.Write(" ");
    }

    // Keep player position within the bounds of the Terminal window
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Clears the console, displays the food and player
void InitializeGame() 
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}
