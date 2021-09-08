/*Cameron Deao
 * CST-227
 * James Shinevar
 * 7/14/2019
 * Repo: https://bitbucket.org/cdeao/cst-227-milestone-6/src/master/ */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cameron_Deao_Milestone_1
{
    public partial class LeaderBoard : Form
    {
        //Added a property to this list to allow the ability to see
        //and set the list outside of this file.
        public static List<PlayerStats> playerBoard { get; set; }
        public LeaderBoard()
        {

            InitializeComponent();
        }

        private void LeaderBoard_Load(object sender, EventArgs e)
        {
            
        }

        //Method used to create the leaderboard for display.
        public void SetLeaderboard()
        {
            //Created an array of labels and set the size to the 
            //list within the file. Setting the size of the array to 
            //the size of the list will avoid index out of range exceptions
            //in the event that there are fewer than five scores to be displayed.
            Label[] testingLabels = new Label[playerBoard.Count];
            int increment = 30;
            //Iterating through the array to create and place the labels.
            for (int i = 0; i < testingLabels.Length; i++)
            {
                //Creating a new label.
                testingLabels[i] = new Label();
                //Setting the text to the value at the index of the list.
                testingLabels[i].Text = "Name: " + playerBoard[i].playerName + " Difficulty: " + playerBoard[i].levelPlayed + 
                    " Score:" + playerBoard[i].timePlayed + " Game Completed:" + playerBoard[i].gameCompleted;
                //Setting the size.
                Size = new Size(300, 200);
                testingLabels[i].Size = new Size(testingLabels[i].PreferredWidth, testingLabels[i].PreferredHeight);
                //Setting the location.
                testingLabels[i].Location = new Point(40, increment);
                //Adding the label to the Controls.
                Controls.Add(testingLabels[i]);
                //Increasing the increment to avoid labels stacking on top of each other.
                increment = increment + 20;
            }
            //Initializing the components.
            InitializeComponent();
        }
    }
}