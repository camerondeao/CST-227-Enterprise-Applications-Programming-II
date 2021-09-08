using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cameron_Deao_Milestone_2
{
    //MinesweeperGame class extends GamesBoard and implements IPlayable.
    //Removed the Recursive method from this method and repositioned it
    //within the Grid.cs file. It only works properly within that file.
    class MinesweeperGame : GameBoard, IPlayable
    {
        //Variables used throughout the class.
        public int selectionCounter = 0;
        public bool playerDead = false;
        public bool visitedCell = false;
        public bool winner = false;

        //Win condition method. Checks the current cells visited
        //against the total number of safe cells.
        public void CheckWinCondition()
        {
            if(selectionCounter == totalSafeCells)
            {
                winner = true;
            }
        }
        //Implementing the playGame method.
        public GameCell[,] playGame(GameCell[,] board, int colChoice, int rowChoice)
        {
            if (board[rowChoice, colChoice].Live == true)
            {
                playerDead = true;
            }
            return board;
        }
    }
}