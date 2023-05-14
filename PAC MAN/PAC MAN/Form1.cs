using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAC_MAN
{
    public partial class Form1 : Form
    {
        bool goUP, goDown, goLeft, goRight, isGameOver;
        int redSpeedGhost, pinkSpeedGhost, yallowGhostY, yallowGhostX, score, playerspeed;
        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

      
        
        private void MainGameTimer(object sender, EventArgs e)
        {
            label1.Text = "score= " + score;
            if (goUP == true){
                pacMan.Image = Properties.Resources.Up;
                pacMan.Top -= playerspeed;
            }
            else if (goDown == true)
            {
                pacMan.Image = Properties.Resources.down;
                pacMan.Top += playerspeed;
            }
            else if (goLeft == true)
            {
                pacMan.Image = Properties.Resources.left;
                pacMan.Left -= playerspeed;
            }
            else if (goRight == true)
            {
                pacMan.Image = Properties.Resources.right;
                pacMan.Left += playerspeed;
            }


            // to eat pacMan conins become invisible
            foreach(Control x in this.Controls)
            {
               if(x is PictureBox)
               {
                   // to eat pacMan conins become invisible
                   if((string)x.Tag == "coins" &&   x.Visible == true)
                   {
                         if(pacMan.Bounds.IntersectsWith(x.Bounds))
                         {
                             x.Visible = false;
                             score++;
                         }
                  }
                   //Walls
                   if ((string)x.Tag == "Wall")
                   {
                       if (pacMan.Bounds.IntersectsWith(x.Bounds))
                       {
                           gameOver("You Lose!!");
                       }
                       if (yallowGuy.Bounds.IntersectsWith(x.Bounds))
                       {
                           yallowGhostX = -yallowGhostX;
                       }

                   }


                   //Ghost
                   if ((string)x.Tag == "Ghost")
                   {
                       if (pacMan.Bounds.IntersectsWith(x.Bounds))
                       {
                           gameOver("You Lose!!");
                       }
                   }


               }
            }

            

            //red Ghost move
            redGuy.Left += redSpeedGhost;
            if (redGuy.Bounds.IntersectsWith(pictureBox1.Bounds) || redGuy.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                redSpeedGhost = -redSpeedGhost;
            }

            //pink Ghost move
            pinkGuy.Left -= pinkSpeedGhost;
            if (pinkGuy.Bounds.IntersectsWith(pictureBox4.Bounds) || pinkGuy.Bounds.IntersectsWith(pictureBox5.Bounds))
            {
                pinkSpeedGhost =-pinkSpeedGhost;
            }


            //Yallow Ghost move
            yallowGuy.Left -= yallowGhostX;
            yallowGuy.Top -= yallowGhostY;
            //Top
            if (yallowGuy.Top < 0 || yallowGuy.Top > 500) {
                yallowGhostY = -yallowGhostY;
            }
            //left
            if (yallowGuy.Left < 0 || yallowGuy.Left > 630)
            {
                yallowGhostX = -yallowGhostX;
            }



            //boundries
            if (pacMan.Top < 0)
            {
                pacMan.Top = 500;
            }
            else if (pacMan.Top > 500)
            {
                pacMan.Top = 0;
            }


            if (pacMan.Left < -7) {
                pacMan.Left = 600;
            }
            else if (pacMan.Left > 600)
            {
                pacMan.Left = 0;
            }

          
            if (score == 71) {
                gameOver("You Win!!");
            }
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goUP = true;
            else if (e.KeyCode == Keys.Down) goDown = true;
            else if (e.KeyCode == Keys.Left) goLeft = true;
            else if (e.KeyCode == Keys.Right) goRight = true;

        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goUP = false;
            else if (e.KeyCode == Keys.Down) goDown = false;
            else if (e.KeyCode == Keys.Left) goLeft = false;
            else if (e.KeyCode == Keys.Right) goRight = false;
            else if (e.KeyCode == Keys.Enter && isGameOver == true) resetGame();
        }
        private void resetGame(){
            score = 0;
            label1.Text = "score: 0";
            playerspeed = 8;
            redSpeedGhost = 5;
            pinkSpeedGhost = 5;
            yallowGhostX = 5;
            yallowGhostY = 5;
            isGameOver = false;

            pacMan.Left = 10;
            pacMan.Top = 49;

            
            redGuy.Left = 154;
            redGuy.Top = 60;

            pinkGuy.Left = 383;
            pinkGuy.Top = 413;

            yallowGuy.Left =417;//417, 298
            yallowGuy.Top = 298;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox) x.Visible = true;
            }
            gameTimer.Start();
        }
        private void gameOver(string s) { 
            isGameOver=true;
            gameTimer.Stop();
            label1.Text += Environment.NewLine + s;
        }

    }
}
