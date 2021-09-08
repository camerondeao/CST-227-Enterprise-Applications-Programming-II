using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cameron_Deao_Milestone_1
{
    public partial class ChoosingDifficulty : Form
    {
        //Creating the necessary form pieces.
        private Button playGame = new Button();
        private Label message = new Label();
        private Label userNameMessage = new Label();
        private RadioButton easy = new RadioButton();
        private RadioButton medium = new RadioButton();
        private RadioButton hard = new RadioButton();
        private TextBox userName = new TextBox();
        string username;
        string difficultySelected;
        int gridSize = 0;

        //Updated the method to include a label that will ask
        //the user for a name. The name and difficulty selected 
        //are now made available for return from the Program.cs
        //file so the leaderboard display can be updated appropriately
        //based on the difficulty and the username passed in.
        //Method used to establish the form and locations.
        public ChoosingDifficulty()
        {
            message.Text = "Select Level";   
            Size = new Size(300, 200);
            message.Size = new Size(message.PreferredWidth, message.PreferredHeight);
            message.Location = new Point(40, 30);
            easy.Location = new Point(40, 50);
            medium.Location = new Point(40, 70);
            hard.Location = new Point(40, 90);
            userNameMessage.Text = "Username:";
            userNameMessage.Size = new Size(message.PreferredWidth, message.PreferredHeight);
            userNameMessage.Location = new Point(30, 120);
            playGame.Location = new Point(100, 150);
            userName.Location = new Point(100, 120);
            easy.Text = "Easy";
            medium.Text = "Medium";
            hard.Text = "Hard";
            playGame.Text = "Play Game";
            Controls.Add(easy);
            Controls.Add(medium);
            Controls.Add(hard);
            Controls.Add(message);
            Controls.Add(playGame);
            Controls.Add(userName);
            Controls.Add(userNameMessage);
            playGame.Click += new EventHandler(playGame_Click);
            InitializeComponent();
            this.Text = "Select Level";
        }
        //Event handler for the button.
        protected void playGame_Click(Object sender, EventArgs e)
        {
            //If statements check which radio button was selected
            //and pass the correct value into the grid form class.
            if(easy.Checked)
            {
                gridSize = 9;
                difficultySelected = "Easy";
            }
            if(medium.Checked)
            {
                gridSize = 12;
                difficultySelected = "Medium";
            }
            if(hard.Checked)
            {
                gridSize = 15;
                difficultySelected = "Hard";
            }
            username = userName.Text;
            //Closing the window after the Play Game button is clicked.
            this.Close();
        }
        private void ChoosingDifficulty_Load(object sender, EventArgs e)
        {

        }

        public string DifficultySelected()
        {
            return difficultySelected;
        }

        public int GridSize()
        {
            return gridSize;
        }

        public string UserName()
        {
            return username;
        }
    }
}
