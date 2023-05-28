using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario_Kostina_kn_2_2
{
    public partial class Form2 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int score = 0;
        int marioSpeed = 7;

        int horizontalSpeed = 5;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;

        public Form2()
        {
            InitializeComponent();
        }

        private bool IsOnPlatform(PictureBox mario)
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "platform")
                {
                    if(mario.Bounds.IntersectsWith(control.Bounds) && mario.Bottom <= control.Top + 5)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            ScoreTXT.Text = "Рахунок: " + score;

            mario.Top += jumpSpeed;

            if (goLeft == true)
            {
                mario.Left -= marioSpeed;
            }

            if (goRight == true)
            {
                mario.Left += marioSpeed;
            }
            
            if(jumping == true && force < 0)
            {
                jumping = false;
            }

            /*if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }*/

            if (jumping && force >= -8 && !IsOnPlatform(mario))
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else if (jumping == false && !IsOnPlatform(mario))
            {
                jumpSpeed = 10;
                jumping = false;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (mario.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            mario.Top = x.Top - mario.Height;
                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                mario.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }



                    if ((string)x.Tag == "coin")
                    {
                        if (mario.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (mario.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            ScoreTXT.Text = "Рахунок: " + score;
                            MessageBox.Show("Ви програли\n Рахунок: " + score);
                        }
                    }
                }
            }
            
            horizontalPlatform.Left -= horizontalSpeed;

            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            enemy1.Left -= enemyOneSpeed;
            if (enemy1.Left < pictureBox2.Left || enemy1.Left + enemy1.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemy2.Left += enemyTwoSpeed;

            if (enemy2.Left < pictureBox5.Left || enemy2.Left + enemy2.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (mario.Top + mario.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                ScoreTXT.Text = "Score: " + score;
                MessageBox.Show("Ви випали зі світу\n Рахунок: " + score);
            }

            if (mario.Bounds.IntersectsWith(door.Bounds))
            {
                gameTimer.Stop();
                isGameOver = true;
                ScoreTXT.Text = "Score: " + score;
                MessageBox.Show("Ви виграли!\n Рахунок: " + score);
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {

            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            ScoreTXT.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            mario.Left = 12;
            mario.Top = 460;

            enemy1.Left = 480;
            enemy2.Left = 275;

            horizontalPlatform.Left = 106;

            gameTimer.Start();
        }
    }
}


                        