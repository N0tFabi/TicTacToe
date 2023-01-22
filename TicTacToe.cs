using System;

class TicTacToeAI {
    static int moveCounter = 0;
    static char[,] board = new char[3, 3];

    static void Main(string[] args) {
        Console.Clear();
        initializeBoard();
        while (true) {
            Console.Clear();
            printBoard();
            playerMove();
            if (gameWon()) {
                Console.Clear();
                printBoard();
                Console.WriteLine("Player wins!");
                break;
            }
            if (boardFull()) {
                Console.WriteLine("Tie game!");
                break;
            }

            Console.Clear();
            printBoard();
            aiMove();
            if (gameWon()) {
                Console.Clear();
                printBoard();
                Console.WriteLine("Computer wins!");
                break;
            }
            if (boardFull()) {
                Console.WriteLine("Tie game!");
                break;
            }
        }
    }

    static void initializeBoard() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                board[i, j] = ' ';
            }
        }
    }

    static void playerMove() {
        Console.WriteLine("Enter row and column (e.g. A 2): ");
        int row = 0, col = 0, countInputTimes = 0;
        char rawRow = ' ';
        do {
            if (countInputTimes >= 1) { Console.WriteLine("Invalid move. Try again!"); }
            rawRow = Char.ToLower(Console.ReadKey().KeyChar);
            col = Convert.ToInt32(rawRow) - 97;
            row = int.Parse(Console.ReadLine()) - 1;
            countInputTimes++;
        } while (board[row, col] != ' ');
        
        board[row, col] = 'X';
        moveCounter++;
    }

    static void aiMove() {
        Console.WriteLine("\nNow it's the AI's turn.\n");
        // Check for winning move
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[i, j] == ' ') {
                    board[i, j] = 'O';
                    if (gameWon()) {
                        moveCounter++;
                        return;
                    } else {
                        board[i, j] = ' ';
                    }
                }
            }
        }
        // Check for blocking move
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[i, j] == ' ') {
                    board[i, j] = 'X';
                    if (gameWon()) {
                        board[i, j] = 'O';
                        moveCounter++;
                        return;
                    } else {
                        board[i, j] = ' ';
                    }
                }
            }
        }
        // Otherwise, make a random move
        Random rnd = new Random();
        int row = rnd.Next(0, 3);
        int col = rnd.Next(0, 3);
        while (board[row, col] != ' ') {
            row = rnd.Next(0, 3);
            col = rnd.Next(0, 3);
        }
    board[row, col] = 'O';
    moveCounter++;
    }

    static bool gameWon() {
        for (int i = 0; i < 3; i++) {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ') {
                return true;
            }
            if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' ') {
                return true;
            }
        }
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ') {
            return true;
        }
        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' ') {
            return true;
        }
        return false;
    }

    static bool boardFull() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[i, j] == ' ') {
                    return false;
                }
            }
        }
        return true;
    }

    static void printBoard() {
        Console.WriteLine("   " + "  A   B   C  \n   +---+---+---+");
        for (int i = 0; i < 3; i++) {
            Console.Write(" " + (int)(i+1) + " ");
            for (int j = 0; j < 3; j++) {
                Console.Write("| " + board[i, j] + " ");
            }
            Console.WriteLine("|\n   +---+---+---+");
        }
    }
}
