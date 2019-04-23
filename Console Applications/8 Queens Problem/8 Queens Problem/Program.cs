/*
    Colorful Console - http://colorfulconsole.com/ 
*/

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace _8_Queens_Problem
{
    class Program
    {
        static int squaresInRowInChessBoard = 8;
        static int numberOfQueensPlaced;
        static bool[,] ChessBoard = new bool[8, 8];
        static void Main(string[] args)
        {
            Solve8QueensProblem();
            PrintGameOfLifeArray(); // Printing the Array
            Console.ReadKey();
        }

        public static void Solve8QueensProblem()
        {
            numberOfQueensPlaced = 0;
            PlaceQueenOnLocation(0, 0);
        }

        public static void PlaceQueenOnLocation(int rowIndex, int columnIndex)
        {
            int[,] nextValidLocationFromCurrent = new int[2, 1];

            // Place the queen on the location
            ChessBoard[rowIndex, columnIndex] = true;
            numberOfQueensPlaced++;

            // Identify location on the next row to place the next queen
            nextValidLocationFromCurrent = IdentifyNextValidLocationFromCurrent(rowIndex, columnIndex);

            // Exit criteria
            if (numberOfQueensPlaced == 8) return;

            // Recursively call Place the queen on the new location
            PlaceQueenOnLocation(nextValidLocationFromCurrent[0, 0], nextValidLocationFromCurrent[1, 0]);
        }

        public static int[,] IdentifyNextValidLocationFromCurrent(int rowIndex, int columnIndex)
        {
            int[,] nextValidLocation = { { -1 }, { -1 } };

            for (int intColumnCounter = columnIndex + 1; intColumnCounter < squaresInRowInChessBoard; intColumnCounter++)
            {
                nextValidLocation[0, 0] = rowIndex + 1;
                nextValidLocation[1, 0] = intColumnCounter;

                return nextValidLocation;
            }
            return nextValidLocation;
        }

        public static void PrintGameOfLifeArray()
        {
            for (int intRows = 0; intRows < ChessBoard.GetLength(0); intRows++)
            {
                for (int intColumns = 0; intColumns < ChessBoard.GetLength(1); intColumns++)
                {
                    FormatBooleanToText(ChessBoard[intRows, intColumns]);
                }
                Console.WriteLine();Console.WriteLine(); Console.WriteLine();
            }
        }

        public static void FormatBooleanToText(bool ArrayValue)
        {
            if (ArrayValue)
            {
                Console.Write("     " + "QUEEN", Color.Green);
            }
            else
            {
                Console.Write("     " + "EMPTY ", Color.Red);
            }
        }

    }
}
