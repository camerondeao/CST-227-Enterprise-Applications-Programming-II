using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cameron_Deao_Milestone_2
{
    //Methods within this file now take a 2-D array as an argument. Each
    //method returns the array to be used within the applications GUI.
    public abstract class GameBoard
    {
        public bool revealBoard = false;
        public int totalSafeCells = 0;

        //Creating the board.
        public GameCell[,] EstablishBoard(GameCell[,] board, int grid)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    board[i, x].Row = x;
                    board[i, x].Col = i;
                }
            }
            return board;
        }

        public GameCell[,] ActivateCell(GameCell[,] board, int percentage)
        {
            //Creating two randoms to be used.
            Random active = new Random();
            Random cellChoice = new Random();
            //Variables used throughout the function.
            int rowChoice = 0;
            int colChoice = 0;
            //Performing the math to determine how many cells will go live.
            int totalCells = percentage * percentage;
            int range = active.Next(15, 21);
            double result = (double)range / (double)100;
            double totalPercent = result * (double)totalCells;
            int interval = (int)Math.Floor(totalPercent);
            totalSafeCells = totalCells - interval;
            //Randomly selecting indexes within the two-dimensional array to set live.
            for (int i = 0; i < interval; i++)
            {
                rowChoice = cellChoice.Next(0, board.GetLength(0));
                colChoice = cellChoice.Next(0, board.GetLength(1));
                board[rowChoice, colChoice].Live = true;
            }
            return board;
        }

        public GameCell[,] SetCount(GameCell[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    //Counter for live neighbors.
                    int neighborCounter = 0;
                    //Each if statement checks if a cell is live in a specific direction.
                    if (i > 0 && board[i - 1, x].Live == true)
                    {
                        //Each statement sets the found index neighbors live value to 9 
                        //if found to be true.
                        //board[i - 1, x].NeighborsLive = 9;
                        //Icrementing the counter up.
                        neighborCounter++;
                    }
                    if (x > 0 && board[i, x - 1].Live == true)
                    {
                        //board[i, x - 1].NeighborsLive = 9;
                        neighborCounter++;
                    }
                    if (i > 0 && x > 0 && board[i - 1, x - 1].Live == true)
                    {
                        //board[i - 1, x - 1].NeighborsLive = 9;
                        neighborCounter++;
                    }
                    if (i < board.GetLength(0) - 1 && board[i + 1, x].Live == true)
                    {
                        //board[i + 1, x].NeighborsLive = 9;
                        neighborCounter++;
                    }
                    if (x < board.GetLength(0) - 1 && board[i, x + 1].Live == true)
                    {
                        //board[i, x + 1].NeighborsLive = 9;
                        neighborCounter++;
                    }
                    if (i < board.GetLength(0) - 1 && x < board.GetLength(0) - 1 && board[i + 1, x + 1].Live == true)
                    {
                        //board[i + 1, x + 1].NeighborsLive = 9;
                        neighborCounter++;
                    }
                    if (i < board.GetLength(0) - 1 && x > 0 && board[i + 1, x - 1].Live == true)
                    {
                        //board[i + 1, x - 1].NeighborsLive = 9;
                        neighborCounter++;
                    }
                    if (i > 0 && x < board.GetLength(0) - 1 && board[i - 1, x + 1].Live == true)
                    {
                        //board[i - 1, x + 1].NeighborsLive = 9;
                        neighborCounter++;
                    }
                    //Setting the current index neighbors live value to the counter.
                    board[i, x].NeighborsLive = neighborCounter;
                }
            }
            return board;
        }

        //Commented out the console porition of displaying the board.
        public void DisplayBoard(GameCell[,] board)
        {
            //Setting int values to size of the array.
            int rowLength = board.GetLength(0);
            int colLenth = board.GetLength(1);
            //Variables used to showcase a header and side
            //counter for the board for easier input selection.
            int headerCounter = 0;
            int sideCounter = 0;
            //Using the console to achieve the right formatting.
            Console.Write("  ");
            //Writing the header for the console.
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write(headerCounter);
                headerCounter++;
            }
            //Using the console to achieve the right formatting.
            Console.Write(Environment.NewLine);
            Console.Write("  ");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("-");
            }
            Console.Write(Environment.NewLine);
            //Displaying the board if the game is still running.
            if (revealBoard == false)
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    //Using the console to achieve the right formatting.
                    Console.Write(sideCounter + "|");
                    for (int x = 0; x < board.GetLength(1); x++)
                    {
                        if (board[i, x].VisitedCell == true && board[i, x].NeighborsLive == 0)
                        {
                            //Displaying if a cell has been visited.
                            Console.Write(string.Format("`"));
                        }
                        if (board[i, x].VisitedCell == true && board[i, x].NeighborsLive > 0)
                        {
                            //Displaying the number of live neighbors for the cell.
                            Console.Write(board[i, x].NeighborsLive);
                        }
                        else if (board[i, x].VisitedCell == false)
                        {
                            //Displaying if the cell has not been visited.
                            Console.Write(string.Format("?"));
                        }
                    }
                    Console.Write(Environment.NewLine);
                    sideCounter++;
                }
                Console.Write(Environment.NewLine);
            }
            //Fires if the game has been lost and the whole 
            //board needs to be displayed.
            if (revealBoard == true)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    Console.Write(sideCounter + "|");
                    for (int x = 0; x < colLenth; x++)
                    {
                        //Displaying an X if a cell is live.
                        if (board[i, x].Live == true)
                        {
                            Console.Write(string.Format("X"));
                        }
                        //If the current cell is not live the neighbors live variable is displayed
                        //with it's current value.
                        else
                        {
                            Console.Write(string.Format(board[i, x].NeighborsLive.ToString()));
                        }
                    }
                    //Formatting to display the board properly.
                    Console.Write(Environment.NewLine);
                    sideCounter++;
                }
            }
        }
    }
}