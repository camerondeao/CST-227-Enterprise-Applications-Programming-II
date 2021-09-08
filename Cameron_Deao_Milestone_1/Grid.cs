using Cameron_Deao_Milestone_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cameron_Deao_Milestone_1
{
    public partial class Grid : Form
    {
        //Creating a 2D-array for the buttons from the GameCell.
        public GameCell[,] newButtons;
        //Variables used within the class.
        public int buttonCount = 0;
        private int height = 0;
        private int width = 0;
        public int totalSafeCells = 0;
        public static int display;
        public string difficulty;
        public string userName; 
        MinesweeperGame mg = new MinesweeperGame();
        //Created a new stopwatch to track the time of the game.
        System.Diagnostics.Stopwatch myStopWatch = new System.Diagnostics.Stopwatch();

        public Grid()
        {
            //Starting the stopwatch.
            myStopWatch.Start();
            InitializeComponent();
        }

        //Setting variables to the appropriate difficulty 
        //and username.
        public void SetDifficulty(string setDifficulty)
        {
            difficulty = setDifficulty;
        }
        public void SetUsername(string name)
        {
            userName = name;
        }

        //Method to setup and display the buttons based on the user selection.
        public void SetupButtons(int totalButtons)
        {  
            newButtons = new GameCell[totalButtons, totalButtons];
            //Autosizing the form.
            this.AutoSize = true;
            //Setting an int value to the total number of buttons needed.
            int overallTotal = totalButtons * totalButtons;
            //Increment counter and threshold established.
            int incrementCounter = 0;
            int incrementThreshold = 9;
            //Iterating through the 2D array to setup the buttons.
            for(int i = 0; i < newButtons.GetLength(0); i++)
            {
                for(int x = 0; x < newButtons.GetLength(1); x++)
                {
                    //If the increment counter meets the threshold
                    //the buttons spacing is changed to be on a new line.
                    if(incrementCounter == incrementThreshold)
                    {
                        incrementThreshold = incrementThreshold + 9;
                        height = 0;
                        width = width + 20;
                    }
                    GameCell newButton = new GameCell();
                    //Buttons row and column are now set based
                    //on their index within the array.
                    newButton.Row = i;
                    newButton.Col = x;
                    //Setting the button location.
                    newButton.Location = new Point(height + 20, width + 40);
                    height = height + 60;
                    //Adding the new button into the array.
                    this.newButtons[i, x] = newButton;
                    //Creating the event handler for the new button.
                    //This was updated to a MouseEventHandler to enable the ability
                    //to right click a button to display a flag on the image property
                    //of the button.
                    this.newButtons[i, x].MouseDown += new MouseEventHandler(ButtonClick);
                    //newButtons[i, x].PerformClick();
                    //Adding them into the controls.
                    this.Controls.Add(newButtons[i, x]);
                    //Increasing the increment counter.
                    incrementCounter++;
                }
            }
            //Calling the MinesweeperGame methods to establish
            //the board, activate cells, and set the count.
            mg.EstablishBoard(newButtons, totalButtons);
            mg.ActivateCell(newButtons, totalButtons);
            mg.SetCount(newButtons);
            mg.revealBoard = true;
            mg.DisplayBoard(newButtons);
        }

        //Event handler for each button.
        private void ButtonClick(Object sender, MouseEventArgs e)
        {
            GameCell btn = sender as GameCell;
            //Checking if the right mouse click was
            //used for the click.
            if (e.Button == MouseButtons.Right)
            {
                btn.Image = Properties.Resources.MinesweeperFlag;
            }
            else
            {
                //Getting the row and column from the button.
                int row = btn.Row;
                int col = btn.Col;
                //Passing the board and the int variables into the playGame method.
                mg.playGame(newButtons, row, col);
                //If the player is not dead the recursive method is called.
                if (mg.playerDead == false)
                {
                    Recursive(btn.Col, btn.Row);
                }
                //Updating the display of the board in the GUI.
                for (int i = 0; i < newButtons.GetLength(0); i++)
                {
                    for (int x = 0; x < newButtons.GetLength(1); x++)
                    {
                        if (newButtons[i, x].VisitedCell == true)
                        {
                            //Moved the selection counter into this check based on the 
                            //number of cells visited.
                            mg.selectionCounter++;
                            newButtons[i, x].Text = newButtons[i, x].NeighborsLive.ToString();
                        }
                    }
                }
            }
            //Checking if the win condition was met.
            mg.CheckWinCondition();
            //Fires if the win condition is met.
            if(mg.winner)
            {
                //Stopping the stopwatch.
                myStopWatch.Stop();
                //Displaying the entire board.
                for(int i = 0; i < newButtons.GetLength(0); i++)
                {
                    for(int x = 0; x < newButtons.GetLength(1); x++)
                    {
                        //Setting the image of the buttons with bombs to the bomb
                        //image within the project and disabling the buttons.
                        if(newButtons[i,x].Live)
                        {
                            newButtons[i, x].Image = Properties.Resources.MinesweeperBomb;
                            newButtons[i, x].Enabled = false;
                        }
                        //setting the display of each button to their NeighborsLive count.
                        else
                        {
                            newButtons[i, x].Text = newButtons[i, x].NeighborsLive.ToString();
                            newButtons[i, x].Enabled = false;
                        }
                    }
                }
                //Adding a player to the master list within the PlayerStats
                //file. The method is called and passes in the object of 
                //PlayerStats with the appropriate values.
                PlayerStats.AddPlayer(new PlayerStats()
                {
                    playerName = userName,
                    levelPlayed = difficulty,
                    timePlayed = myStopWatch.Elapsed.Seconds,
                    gameCompleted = true
                });
                //MessageBox will appear showcasing a message and their elapsed time.
                MessageBox.Show("You WIN! Time elapsed : " + myStopWatch.Elapsed.Seconds.ToString() + "." + myStopWatch.Elapsed.Milliseconds.ToString() + " seconds");
                //Closing the board to showcase the leaderboard.
                this.Close();
            }
            //Fires if the lose condition is met.
            if (mg.playerDead)
            {
                //Stopping the stopwatch.
                myStopWatch.Stop();
                //Displaying the entire board.
                for (int i = 0; i < newButtons.GetLength(0); i++)
                {
                    for (int x = 0; x < newButtons.GetLength(1); x++)
                    {
                        //Setting the image of the buttons with bombs to the bomb
                        //image within the project and disabling the buttons.
                        if (newButtons[i, x].Live)
                        {
                            newButtons[i, x].Image = Properties.Resources.MinesweeperBomb;
                            newButtons[i, x].Enabled = false;
                        }
                        else
                        {
                            newButtons[i, x].Text = newButtons[i, x].NeighborsLive.ToString();
                            newButtons[i, x].Enabled = false;
                        }
                    }
                }
                //Adding a player to the master list within the PlayerStats
                //file. The method is called and passes in the object of 
                //PlayerStats with the appropriate values.
                PlayerStats.AddPlayer(new PlayerStats()
                {
                    playerName = userName,
                    levelPlayed = difficulty,
                    timePlayed = myStopWatch.Elapsed.Seconds,
                    gameCompleted = false
                });
                MessageBox.Show("GAME OVER");
                //Closing the board to showcase the leaderboard.
                this.Close();
            }
            //Resetting the selectionCounter variable.
            mg.selectionCounter = 0;
        }

        //Moved the Recursive method from MinesweeperGame.cs into this file.
        //Only works properly within this method and I'm not sure why.
        public void Recursive(int rowChoice, int colChoice)
        {
            //Checking the current cell to see if it has live neighbors and it isn't visited yet.
            if (newButtons[rowChoice, colChoice].NeighborsLive == 0 && newButtons[rowChoice, colChoice].VisitedCell == false)
            {
                //Setting the cell as visited.
                newButtons[rowChoice, colChoice].VisitedCell = true;
                newButtons[rowChoice, colChoice].Enabled = false;
                newButtons[rowChoice, colChoice].BackColor = Color.Gray;
                //These four if-statement check in all four directions their neighbor cells.
                if (rowChoice - 1 >= 0 && newButtons[rowChoice - 1, colChoice].Live == false)
                {
                    //Recursively passing in the new cell.
                    Recursive(rowChoice - 1, colChoice);
                }
                if (rowChoice + 1 < newButtons.GetLength(0) && newButtons[rowChoice + 1, colChoice].Live == false)
                {
                    Recursive(rowChoice + 1, colChoice);
                }
                if (colChoice + 1 < newButtons.GetLength(1) && newButtons[rowChoice, colChoice + 1].Live == false)
                {
                    Recursive(rowChoice, colChoice + 1);
                }
                if (colChoice - 1 >= 0 && newButtons[rowChoice, colChoice - 1].Live == false)
                {
                    Recursive(rowChoice, colChoice - 1);
                }
            }
            //Setting the current cell to visited.
            else
            {
                newButtons[rowChoice, colChoice].VisitedCell = true;
                newButtons[rowChoice, colChoice].Enabled = false;
                newButtons[rowChoice, colChoice].BackColor = Color.Gray;
            }
        }
        private void Grid_Load(object sender, EventArgs e)
        {
        }
    }
}
