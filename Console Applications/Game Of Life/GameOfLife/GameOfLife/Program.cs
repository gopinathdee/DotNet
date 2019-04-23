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

namespace GameOfLife
{
    class Program
    {
        static bool[,] GameOfLifeArray = new bool[10, 10];
        static bool[,] GameOfLifeArrayProcessStatus;

        static void Main(string[] args) {
            InitializeGameOfLifeArray();
            PrintGameOfLifeArray(); // Printing the Array

            while (true)
            {
                GameOfLifeArrayProcessStatus = new bool[10, 10]; // Re-Initializing the procesing flag array
                ProcessGameOfLifeArray();
                PrintGameOfLifeArray();

                Console.WriteLine("");
                Console.Write("Do you want to process another generation ('Y' or 'Any Key'): ");

                var keyPressed = Console.ReadKey();
                if (keyPressed.Key != ConsoleKey.Y)
                {
                    Environment.Exit(0);
                }
                Console.WriteLine("");
            }
        }

        public static void InitializeGameOfLifeArray()
        {
            GameOfLifeArray[0, 1] = true;
            GameOfLifeArray[1, 2] = true;
            GameOfLifeArray[2, 0] = true;
            GameOfLifeArray[2, 1] = true;
            GameOfLifeArray[2, 2] = true;
        }

        public static void PrintGameOfLifeArray()
        {
            Console.WriteLine("========================================= {0} ======================================================", DateTime.Now);
            for (int intRows = 0; intRows < GameOfLifeArray.GetLength(0); intRows++)
            {
                for (int intColumns = 0; intColumns < GameOfLifeArray.GetLength(1); intColumns++)
                {
                    FormatBooleanToText(GameOfLifeArray[intRows, intColumns]);
                }
                Console.WriteLine("");
            }
        }

        public static void ProcessGameOfLifeArray()
        {
            ProcessGameOfLifeCell(0, 0);
        }

        public static void ProcessGameOfLifeCell(int rowIndex, int columnIndex)
        {
            bool isCurrentlyAlive;
            int[,] nextCellToProcess = new int[2, 1]; /* Array that will store the indexes of the next cell that is to be processed */

            // Determine number of neighbours
            int numberOfLiveNeighbours;

            isCurrentlyAlive = GameOfLifeArray[rowIndex, columnIndex];
            numberOfLiveNeighbours = DetermineNumberOfNeighbours(rowIndex, columnIndex);

            nextCellToProcess = DetermineNextCellToProcess(rowIndex, columnIndex);

            //Console.WriteLine ("Currently Processing ({0}, {1}). Will process ({2}, {3}) next", rowIndex, columnIndex, nextCellToProcess[0, 0], nextCellToProcess[1, 0]);

            // Exit criteria for the Recursive function. If there are no cells to process, (-1, -1) will be returned
            if ((nextCellToProcess[0, 0] == -1) && nextCellToProcess[1, 0] == -1) return;

            // Recursively call to process the Next Cell
            ProcessGameOfLifeCell(nextCellToProcess[0,0], nextCellToProcess[1,0]);
            GameOfLifeArray[rowIndex, columnIndex] = ((isCurrentlyAlive) && ((numberOfLiveNeighbours == 2) || (numberOfLiveNeighbours == 3))) || (!isCurrentlyAlive && (numberOfLiveNeighbours == 3));
        }

        public static int DetermineNumberOfNeighbours(int rowIndex, int columnIndex)
        {
            int numberOfAliveNeighbours = 0;
            bool cellStatus;

            for (int intRowCounter = rowIndex - 1; intRowCounter <= rowIndex + 1; intRowCounter++)
            {
                for (int intColumnCounter = columnIndex - 1; intColumnCounter <= columnIndex + 1; intColumnCounter++)
                {
                    if ((intRowCounter == rowIndex) && (intColumnCounter == columnIndex))
                    { }
                    else
                    {
                        try
                        {
                            cellStatus = GameOfLifeArray[intRowCounter, intColumnCounter];
                            if (cellStatus)
                            {
                                numberOfAliveNeighbours++;
                            }
                        }
                        catch { } // Ignoring the "Index Out of Bounds" error
                    }
                }
            }

            GameOfLifeArrayProcessStatus[rowIndex, columnIndex] = true; // Mark the cell as processed, so that it is not processed again
            return numberOfAliveNeighbours;
        }

        public static int[,] DetermineNextCellToProcess(int rowIndex, int columnIndex)
        {
            int[,] nextCellToProcess = {{-1},{-1}};
            bool cellStatus;

            for (int intRowCounter = rowIndex - 1; intRowCounter <= rowIndex + 1; intRowCounter++)
            {
                for (int intColumnCounter = columnIndex - 1; intColumnCounter <= columnIndex + 1; intColumnCounter++)
                {
                    if ((intRowCounter == rowIndex) && (intColumnCounter == columnIndex))
                    { }
                    else
                    {
                        try
                        {
                            cellStatus = GameOfLifeArray[intRowCounter, intColumnCounter];
                            if (!GameOfLifeArrayProcessStatus[intRowCounter, intColumnCounter])
                            {
                                nextCellToProcess[0, 0] = intRowCounter;
                                nextCellToProcess[1, 0] = intColumnCounter;
                                return nextCellToProcess;
                            }
                        }
                        catch { } // Ignoring the "Index Out of Bounds" error
                    }
                }
            }
            return nextCellToProcess;
        }

        public static void FormatBooleanToText(bool ArrayValue)
        {
            if(ArrayValue) {
                Console.Write("    " + "LIFE ", Color.Green);
            }
            else {
                Console.Write("    " + "EMPTY", Color.Red);
            }
        }
    }
    //public enum 
}
