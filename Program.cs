using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXgame
{
    public class Program
    {
        static char[,] board; // Oyun tahtası
        static char currentPlayer = 'X'; // Başlangıçta X ile başlar
        static int moveCount = 0;
        static void Main(string[] args)
        {
            Console.Write("Oyun tahtasının satır sayısını girin: ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Oyun tahtasının sütun sayısını girin: ");
            int cols = int.Parse(Console.ReadLine());

            InitializeBoard(rows, cols);
            PrintBoard();

            

            while (true)
            {
                Console.WriteLine($"Sıra {currentPlayer} oyuncusunda.");
                Console.Write("Satır numarasını giriniz = ");
                int row;
                if (!int.TryParse(Console.ReadLine(), out row))
                {
                    endGame();
                    break;
                }
                Console.Write("Sütun numarasını giriniz = ");
                int col;
                if (!int.TryParse(Console.ReadLine(), out col))
                {
                    endGame();
                    break;
                }

                if (IsValidMove(row, col))
                {
                    MakeMove(row, col);
                    PrintBoard();

                    if (CheckForWin())
                    {
                        Console.WriteLine($"Oyun bitti. {currentPlayer} oyuncu kazandı!");
                        break;
                    }
                    else if (CheckForDraw())
                    {
                        Console.WriteLine("Oyun berabere bitti.");
                        break;
                    }
                    moveCount++;
                    SwitchPlayer();
                }
                else
                {
                    Console.WriteLine("Geçersiz hamle. Lütfen tekrar deneyin.");
                }
            }
        }
        static void InitializeBoard(int rows, int cols)
        {
            board = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        static void PrintBoard()
        {
            Console.WriteLine(new string('-', 4 * board.GetLength(1) + 1));

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write($" {board[i, j]} |");
                }
                Console.WriteLine("\n" + new string('-', 4 * board.GetLength(1) + 1));
            }
        }
        static bool IsValidMove(int row, int col)
        {
            return (row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1) && board[row, col] == ' ');
        }

        static void MakeMove(int row, int col)
        {
            board[row, col] = currentPlayer;
        }

        static void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        static bool CheckForWin()
        {
            // Dikey ve yatay kontrol
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    return true;

                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                    return true;
            }

            // Çapraz kontrol
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;

            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            return false;
        }
        static bool CheckForDraw()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == ' ')
                        return false;
                }
            }
            return true;
        }
        static void endGame()
        {
            Console.WriteLine("Oyun sona erdi hamle yapılmadı!");
        }
    }
}
