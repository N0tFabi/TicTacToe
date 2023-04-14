using System;

class TicTacToeAI
{
    static int moveCounter = 0;
    static char[,] board = new char[3, 3];

    static void Main(string[] args)
    {
        const int AI_DEPTH = 10;

        Console.Clear();
        initializeBoard();
        while (true)
        {
            Console.Clear();
            printBoard();
            playerMove();
            if (gameWon())
            {
                Console.Clear();
                printBoard();
                Console.WriteLine("Player wins!");
                break;
            }
            if (boardFull())
            {
                Console.WriteLine("Tie game!");
                break;
            }

            Console.Clear();
            printBoard();
            aiMove(AI_DEPTH);
            if (gameWon())
            {
                Console.Clear();
                printBoard();
                Console.WriteLine("Computer wins!");
                break;
            }
            if (boardFull())
            {
                Console.WriteLine("Tie game!");
                break;
            }
        }
    }

    static void initializeBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
    }

    static void playerMove()
    {
        Console.WriteLine("Enter row and column (e.g. A 2): ");
        int row = 0, col = 0, countInputTimes = 0;
        char rawRow = ' ';
        do
        {
            if (countInputTimes >= 1) { Console.WriteLine("Invalid move. Try again!"); }
            rawRow = Char.ToLower(Console.ReadKey().KeyChar);
            col = Convert.ToInt32(rawRow) - 97;
            row = int.Parse(Console.ReadLine()) - 1;
            countInputTimes++;
        } while (board[row, col] != ' ');

        board[row, col] = 'X';
        moveCounter++;
    }

    static void aiMove(int depth)
    {
        Console.WriteLine("\nNow it's the AI's turn.\n");

        int bestScore = int.MinValue;
        int bestRow = -1;
        int bestCol = -1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = 'O';
                    int score = minimax(depth - 1, false, int.MinValue, int.MaxValue);
                    board[i, j] = ' ';

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestRow = i;
                        bestCol = j;
                    }
                }
            }
        }

        board[bestRow, bestCol] = 'O';
        moveCounter++;
    }

    static int minimax(int depth, bool maximizingPlayer, int alpha, int beta)
    {
        if (gameWon())
        {
            if (maximizingPlayer)
            {
                return -10;
            }
            else
            {
                return 10;
            }
        }

        if (boardFull() || depth == 0)
        {
            return 0;
        }

        if (maximizingPlayer)
        {
            int maxScore = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'O';
                        int score = minimax(depth - 1, false, alpha, beta);
                        board[i, j] = ' ';

                        maxScore = Math.Max(maxScore, score);
                        alpha = Math.Max(alpha, score);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return maxScore;
        }
        else
        {
            int minScore = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'X';
                        int score = minimax(depth - 1, true, alpha, beta);
                        board[i, j] = ' ';

                        minScore = Math.Min(minScore, score);
                        beta = Math.Min(beta, score);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return minScore;
        }
    }


    static bool gameWon()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ')
            {
                return true;
            }
            if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' ')
            {
                return true;
            }
        }
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ')
        {
            return true;
        }
        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' ')
        {
            return true;
        }
        return false;
    }

    static bool boardFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }

    static void printBoard()
    {
        Console.WriteLine("   " + "  A   B   C  \n   +---+---+---+");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(" " + (int)(i + 1) + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write("| " + board[i, j] + " ");
            }
            Console.WriteLine("|\n   +---+---+---+");
        }
    }
}
