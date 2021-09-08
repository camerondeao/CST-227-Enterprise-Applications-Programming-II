using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cameron_Deao_Milestone_2
{
    //Interface created with a single method.
    //Updated the interface playGame to require a 2-D array
    //and two int variables to be passed in, while returning
    //the array.
    interface IPlayable
    {
       GameCell[,] playGame(GameCell[,] board, int rowChoice, int colChoice);
    }
}
