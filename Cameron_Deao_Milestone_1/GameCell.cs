/*Cameron Deao
 * CST-227
 * James Shinevar
 * 7/14/2019
 * Repo: https://bitbucket.org/cdeao/cst-227-milestone-6/src/master/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Cameron_Deao_Milestone_2
{
    public class GameCell : Button
    {
        //Each variable used within gamecell.
        private bool visited;
        private bool live;
        private int neighborsLive;
        private int row;
        private int col;

        public GameCell()
        {
            visited = false;
            live = false;
            neighborsLive = 0;
            row = -1;
            col = -1;
        }

        //Getters and setters used for each property.
        public bool VisitedCell
        {
            get { return visited; }
            set { visited = value; }
        }

        public bool Live
        {
            get { return live; }
            set { live = value; }
        }

        public int NeighborsLive
        {
            get { return neighborsLive; }
            set { neighborsLive = value; }
        }

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public int Col
        {
            get { return col; }
            set { col = value; }
        }
    }
}
