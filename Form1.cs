using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
//Name: Jari Gerritsen & Victor Chavez
//Date: April 3
//Assignment: CPT
//Purpose: It's a platformer game that is played for enjoyment.


namespace Platform_Game
{
    public partial class Form1 : Form
    {
        //Global Variables 
        bool goleft = false;
        bool goright = false;
        bool jumping = false;
        bool instructions = false;
        bool pause = false;
        bool GameStart = false;

        int level = 1;
 
        int jumpSpeed = 10;
        int force = 8;
        int score = 0;

        int horizontalspeed = 3;
        int verticalspeed = 3;

        int enemyOneSpeed = 3;
        int enemyTwoSpeed = 3;

        

        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //character goes left
              if (e.KeyCode == Keys.Left)
            {
                goleft = true;              
                kon.Image = Resource2.Run7L;             
            }
              //character goes right
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                kon.Image = Resource2.Run7;
            }
            //character jumps
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
                kon.Image = Resource2.Run6;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //character stands still
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
                kon.Image = Resource2.Idle1;
            }
            //character stands still
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
                kon.Image = Resource2.Idle1;
            }
            //character stands still
            if (jumping)
            {
                jumping = false;
                kon.Image = Resource2.Idle1;
            }
            //Hi sir, we left this here incase the game was really hard. If you press "G" it skips the level and goes to the next one.
           if(e.KeyCode == Keys.G)
           {   
               score = 5;
               kon.Location = new Point(9, 25);
           }
            //press enter for start screen
           if (e.KeyCode == Keys.Enter)
           {
               if (GameStart = false)
               {
                   score = 0;

                   level = 1;
                   //setting all locations
                   kon.Location = new Point(25, 672);

                   enemyOne.Location = new Point(511, 622);
                   enemyTwo.Location = new Point(434, 313);

                   horizontalPlatform.Left = 319;
                   verticalPlatform.Top = 463;

                   verticalPlatform.Location = new Point(103, 463);
                   horizontalPlatform.Location = new Point(319, 173);

                   pictureBox2.Location = new Point(408, 653);
                   picturebox3.Location = new Point(58, 586);
                   pictureBox5.Location = new Point(319, 344);
                   pictureBox6.Location = new Point(566, 219);
                   pictureBox8.Location = new Point(0, 91);

                   gikon.Location = new Point(348, 139);
                   pictureBox4.Location = new Point(651, 185);
                   pictureBox7.Location = new Point(120, 251);
                   pictureBox9.Location = new Point(148, 526);
                   pictureBox10.Location = new Point(607, 622);

                   foreach (Control k in this.Controls)
                   {
                       if (k is PictureBox && k.Visible == false)
                       {
                           k.Visible = true;
                       }
                   }
               }
               //starting the game
               GameStart = true;
               //moving the start screen out of view
               pictureBox11.Location = new Point(40584, 64553);              
           }
            //reloads application 
            if(e.KeyCode == Keys.R)
            {
                Application.Restart();
            }
            //toggles instructions
            if(e.KeyCode == Keys.I)
            {
                //opens instructions
                if(instructions == false && GameStart == true)
                {
                    timer1.Stop();
                    picInstructions.Location = new Point(127, 91);
                    picInstructions.BringToFront();
                    instructions = true;
                }
                //turns off instructions
                else if (instructions == true && GameStart == true)
                {
                    timer1.Start();
                    picPause.Location = new Point(1330, 1410);
                    picInstructions.Location = new Point(5000, 5000);
                    instructions = false;
                }
            }
            //quits the game
            if(e.KeyCode == Keys.Back)
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Kon Rush", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            //pause and plays game
            if(e.KeyCode == Keys.P)
            {
                //pauses game
                if (instructions == false && pause == false && GameStart == true)
                {
                    picPause.Location = new Point(325, 350);
                    picPause.BringToFront();
                    timer1.Stop();
                    //a variable that can tell if the game is paused
                    pause = true;
                }
                    //plays game if game is paused
                else if (instructions == false && pause == true && GameStart == true)
                {
                    picPause.Location = new Point(1330, 1410);
                    timer1.Start();
                    pause = false;
                }
            }

        }
        //timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            //moves everything out of view for the start screen.
            if (GameStart == false)
            {
                kon.Location = new Point(2565, 6752);

                //enemy
                enemyOne.Location = new Point(4850, 2940);
                enemyTwo.Location = new Point(5800, 2940);

                //moving
                verticalPlatform.Location = new Point(3109, 6530);
                horizontalPlatform.Location = new Point(1003, 4603);

                //platforms
                pictureBox2.Location = new Point(4805, 3025);
                picturebox3.Location = new Point(60670, 70330);
                pictureBox5.Location = new Point(508, 3025);
                pictureBox6.Location = new Point(50110, 10160);
                pictureBox8.Location = new Point(00, 95651);

                //gikons
                gikon.Location = new Point(4017, 7401);
                pictureBox4.Location = new Point(580, 4208);
                pictureBox7.Location = new Point(6042, 4206);
                pictureBox9.Location = new Point(403, 2092);
                pictureBox10.Location = new Point(0673, 2090);

                pictureBox11.Location = new Point(0, 0);

            }
                //game has started
            else
            {



                txtScore.Text = "Level: " + level + "\nGikons: " + score;
                kon.Top += jumpSpeed;

                //setting up character movement
                if (jumping == true && force < 0)
                {
                    jumping = false;
                    kon.Image = Resource2.Idle1;
                }

                if (goleft == true)
                {
                    kon.Left -= 10;
                }

                if (goright == true)
                {
                    kon.Left += 10;
                }

                if (jumping == true)
                {
                    jumpSpeed = -8;
                    force -= 1;
                }
                else
                {
                    jumpSpeed = 12;
                }
                //makes the platforms solid
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "platform")
                    {
                        if (kon.Bounds.IntersectsWith(x.Bounds) && !jumping)
                        {
                            force = 8;
                            kon.Top = x.Top - kon.Height;
                        }
                        x.BringToFront();
                    }
                }
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "platform")
                    {
                        if (kon.Bounds.IntersectsWith(x.Bounds) && !jumping)
                        {
                            force = 8;
                            kon.Top = x.Top - kon.Height;
                        }
                    }
                    //adds Gikons which are basically like points
                    if (x is PictureBox && x.Tag == "gikon")
                    {
                        if (kon.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    //no falling off at spawn
                    if (kon.Bounds.IntersectsWith(picSide.Bounds))
                    {
                        kon.Location = new Point(25, 672);
                    }
                    
                    if ((string)x.Tag == "enemy")
                    {
                        //makes enemys reset you to level one
                        if (kon.Bounds.IntersectsWith(x.Bounds))
                        {
                            score = 0;

                            level = 1;

                            kon.Location = new Point(25, 672);

                            enemyOne.Location = new Point(511, 622);
                            enemyTwo.Location = new Point(434, 313);

                            horizontalPlatform.Left = 319;
                            verticalPlatform.Top = 463;

                            verticalPlatform.Location = new Point(103, 463);
                            horizontalPlatform.Location = new Point(319, 173);

                            pictureBox2.Location = new Point(408, 653);
                            picturebox3.Location = new Point(58, 586);
                            pictureBox5.Location = new Point(319, 344);
                            pictureBox6.Location = new Point(566, 219);
                            pictureBox8.Location = new Point(0, 91);

                            gikon.Location = new Point(348, 139);
                            pictureBox4.Location = new Point(651, 185);
                            pictureBox7.Location = new Point(120, 251);
                            pictureBox9.Location = new Point(148, 526);
                            pictureBox10.Location = new Point(607, 622);

                            foreach (Control k in this.Controls)
                            {
                                if (k is PictureBox && k.Visible == false)
                                {
                                    k.Visible = true;
                                }
                            }

                        }
                    }

                }
                //ending level (not actually a level)
                if (level == 6)
                {
                    txtScore.Visible = false;
                    label1.Visible = false;
                    rukia.Visible = false;
                    pictureBox8.Visible = false;
                    rukia2.Location = new Point(348, 665);
                }
                //lets the user know the game is over when Kon touches Rukia
                if (kon.Bounds.IntersectsWith(rukia2.Bounds))
                {
                    picGameOver.Location = new Point(123, 185);
                    label2.Location = new Point(246, 376);
                    label3.Location = new Point(246, 418);
                    rukia2.Image = Resource2.rukia_punch;
                    kon.Left -= 25;
                }
                //if the user falls off the edge reset them
                if (kon.Top + kon.Height > this.ClientSize.Height + 50)
                {
                    score = 0;

                    level = 1;

                    kon.Location = new Point(25, 672);

                    enemyOne.Location = new Point(511, 622);
                    enemyTwo.Location = new Point(434, 313);

                    verticalPlatform.Location = new Point(103, 463);
                    horizontalPlatform.Location = new Point(319, 173);

                    pictureBox2.Location = new Point(408, 653);
                    picturebox3.Location = new Point(58, 586);
                    pictureBox5.Location = new Point(319, 344);
                    pictureBox6.Location = new Point(566, 219);
                    pictureBox8.Location = new Point(0, 91);

                    gikon.Location = new Point(348, 139);
                    pictureBox4.Location = new Point(651, 185);
                    pictureBox7.Location = new Point(120, 251);
                    pictureBox9.Location = new Point(148, 526);
                    pictureBox10.Location = new Point(607, 622);

                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Visible == false)
                        {
                            x.Visible = true;
                        }
                    }

                }

                //if kon touches rukia with the required amount of gikons
                if (kon.Bounds.IntersectsWith(rukia.Bounds) && score == 5 && level == 1)
                {
                    //locations of all the objects for the next level
                    kon.Location = new Point(25, 672);

                    enemyOne.Location = new Point(94, 344);
                    enemyTwo.Location = new Point(308, 165);

                    verticalPlatform.Location = new Point(434, 649);
                    horizontalPlatform.Location = new Point(128, 263);

                    pictureBox2.Location = new Point(94, 375);
                    picturebox3.Location = new Point(58, 586);
                    pictureBox5.Location = new Point(308, 196);
                    pictureBox6.Location = new Point(5660, 219);
                    pictureBox8.Location = new Point(0, 91);

                    gikon.Location = new Point(16, 427);
                    pictureBox4.Location = new Point(515, 153);
                    pictureBox7.Location = new Point(110, 313);
                    pictureBox9.Location = new Point(72, 556);
                    pictureBox10.Location = new Point(463, 560);

                    score = 0;
                    txtScore.Text = "Level: " + level + "\nGikons: " + score;
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Visible == false)
                        {
                            x.Visible = true;
                        }
                    }
                    //goes to the next level
                    level = 2;
                }
                //the same thing is repeated 4 more times
                if (kon.Bounds.IntersectsWith(rukia.Bounds) && score == 5 && level == 2)
                {
                    //locations
                    kon.Location = new Point(25, 672);

                    //enemy
                    enemyOne.Location = new Point(9, 443);
                    enemyTwo.Location = new Point(423, 313);

                    //moving
                    verticalPlatform.Location = new Point(572, 721);
                    horizontalPlatform.Location = new Point(463, 91);

                    //platforms
                    pictureBox2.Location = new Point(9, 474);
                    picturebox3.Location = new Point(318, 547);
                    pictureBox5.Location = new Point(319, 344);
                    pictureBox6.Location = new Point(690, 91);
                    pictureBox8.Location = new Point(0, 91);

                    //gikons
                    gikon.Location = new Point(651, 409);
                    pictureBox4.Location = new Point(462, 307);
                    pictureBox7.Location = new Point(51, 435);
                    pictureBox9.Location = new Point(226, 594);
                    pictureBox10.Location = new Point(496, 513);

                    score = 0;
                    txtScore.Text = "Gikons: " + score;
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Visible == false)
                        {
                            x.Visible = true;
                        }
                    }
                    level = 3;
                }
                if (kon.Bounds.IntersectsWith(rukia.Bounds) && score == 5 && level == 3)
                {
                    //locations
                    kon.Location = new Point(25, 672);

                    //enemy
                    enemyOne.Location = new Point(485, 508);
                    enemyTwo.Location = new Point(58, 508);

                    //moving
                    verticalPlatform.Location = new Point(319, 425);
                    horizontalPlatform.Location = new Point(408, 653);

                    //platforms
                    pictureBox2.Location = new Point(485, 539);
                    picturebox3.Location = new Point(667, 733);
                    pictureBox5.Location = new Point(58, 539);
                    pictureBox6.Location = new Point(511, 116);
                    pictureBox8.Location = new Point(0, 91);

                    //gikons
                    gikon.Location = new Point(607, 313);
                    pictureBox4.Location = new Point(621, 46);
                    pictureBox7.Location = new Point(319, 288);
                    pictureBox9.Location = new Point(680, 685);
                    pictureBox10.Location = new Point(628, 513);

                    score = 0;
                    txtScore.Text = "Gikons: " + score;
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Visible == false)
                        {
                            x.Visible = true;
                        }
                    }
                    level = 4;
                }
                if (kon.Bounds.IntersectsWith(rukia.Bounds) && score == 5 && level == 4)
                {
                    //locations
                    kon.Location = new Point(25, 672);

                    //enemy
                    enemyOne.Location = new Point(485, 294);
                    enemyTwo.Location = new Point(58, 294);

                    //moving
                    verticalPlatform.Location = new Point(319, 653);
                    horizontalPlatform.Location = new Point(103, 463);

                    //platforms
                    pictureBox2.Location = new Point(485, 325);
                    picturebox3.Location = new Point(6670, 7330);
                    pictureBox5.Location = new Point(58, 325);
                    pictureBox6.Location = new Point(5110, 1160);
                    pictureBox8.Location = new Point(00, 91);

                    //gikons
                    gikon.Location = new Point(417, 741);
                    pictureBox4.Location = new Point(58, 428);
                    pictureBox7.Location = new Point(642, 426);
                    pictureBox9.Location = new Point(43, 292);
                    pictureBox10.Location = new Point(673, 290);

                    score = 0;
                    txtScore.Text = "Gikons: " + score;
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Visible == false)
                        {
                            x.Visible = true;
                        }
                    }
                    level = 5;
                }
                if (kon.Bounds.IntersectsWith(rukia.Bounds) && score == 5 && level == 5)
                {
                    //locations
                    kon.Location = new Point(25, 672);

                    //enemy
                    enemyOne.Location = new Point(4850, 2940);
                    enemyTwo.Location = new Point(5800, 2940);

                    //moving
                    verticalPlatform.Location = new Point(3109, 6530);
                    horizontalPlatform.Location = new Point(1003, 4603);

                    //platforms
                    pictureBox2.Location = new Point(4805, 3025);
                    picturebox3.Location = new Point(60670, 70330);
                    pictureBox5.Location = new Point(508, 3025);
                    pictureBox6.Location = new Point(50110, 10160);
                    pictureBox8.Location = new Point(00, 91);

                    //gikons
                    gikon.Location = new Point(4017, 7401);
                    pictureBox4.Location = new Point(580, 4208);
                    pictureBox7.Location = new Point(6042, 4206);
                    pictureBox9.Location = new Point(403, 2092);
                    pictureBox10.Location = new Point(0673, 2090);

                    score = 0;
                    txtScore.Text = "Gikons: " + score;
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Visible == false)
                        {
                            x.Visible = true;
                        }
                    }
                    level = 6;
                }
                    //lets the user know they do not have the required amount of gikons
                else if (kon.Bounds.IntersectsWith(rukia.Bounds) && score < 5)
                {
                    txtScore.Text = "Level: " + level + "\nGikons: " + score + Environment.NewLine + "Collect ALL gikons then talk to me!";
                }
                //sets the moving entitys for each level
                if (level == 1)
                {

                    horizontalPlatform.Left -= horizontalspeed;

                    if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
                    {
                        horizontalspeed = -horizontalspeed;
                    }

                    verticalPlatform.Top += verticalspeed;

                    if (verticalPlatform.Top < 237 || verticalPlatform.Top > 463)
                    {
                        verticalspeed = -verticalspeed;
                    }

                    enemyOne.Left -= enemyOneSpeed;

                    if (enemyOne.Left < pictureBox2.Left || enemyOne.Left + enemyOne.Width > pictureBox2.Left + pictureBox2.Width)
                    {
                        enemyOneSpeed = -enemyOneSpeed;

                    }
                    enemyTwo.Left += enemyTwoSpeed;

                    if (enemyTwo.Left < pictureBox5.Left || enemyTwo.Left + enemyTwo.Width > pictureBox5.Left + pictureBox5.Width)
                    {
                        enemyTwoSpeed = -enemyTwoSpeed;
                    }
                }
                if (level == 2)
                {

                    horizontalPlatform.Left -= horizontalspeed;

                    if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
                    {
                        horizontalspeed = -horizontalspeed;
                    }

                    verticalPlatform.Top += verticalspeed;

                    if (verticalPlatform.Top < 423 || verticalPlatform.Top > 649)
                    {
                        verticalspeed = -verticalspeed;
                    }
                    enemyOne.Left -= enemyOneSpeed;

                    if (enemyOne.Left < pictureBox2.Left || enemyOne.Left + enemyOne.Width > pictureBox2.Left + pictureBox2.Width)
                    {
                        enemyOneSpeed = -enemyOneSpeed;

                    }
                    enemyTwo.Left += enemyTwoSpeed;

                    if (enemyTwo.Left < pictureBox5.Left || enemyTwo.Left + enemyTwo.Width > pictureBox5.Left + pictureBox5.Width)
                    {
                        enemyTwoSpeed = -enemyTwoSpeed;
                    }
                }
                if (level == 3)
                {
                    horizontalPlatform.Left -= horizontalspeed;

                    if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
                    {
                        horizontalspeed = -horizontalspeed;
                    }

                    verticalPlatform.Top += verticalspeed;

                    if (verticalPlatform.Top < 137 || verticalPlatform.Top > 721)
                    {
                        verticalspeed = -verticalspeed;
                    }
                    enemyOne.Left -= enemyOneSpeed;

                    if (enemyOne.Left < pictureBox2.Left || enemyOne.Left + enemyOne.Width > pictureBox2.Left + pictureBox2.Width)
                    {
                        enemyOneSpeed = -enemyOneSpeed;

                    }
                    enemyTwo.Left += enemyTwoSpeed;

                    if (enemyTwo.Left < pictureBox5.Left || enemyTwo.Left + enemyTwo.Width > pictureBox5.Left + pictureBox5.Width)
                    {
                        enemyTwoSpeed = -enemyTwoSpeed;
                    }

                }
                if (level == 4)
                {
                    horizontalPlatform.Left -= horizontalspeed;

                    if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
                    {
                        horizontalspeed = -horizontalspeed;
                    }

                    verticalPlatform.Top += verticalspeed;

                    if (verticalPlatform.Top < 173 || verticalPlatform.Top > 425)
                    {
                        verticalspeed = -verticalspeed;
                    }
                    enemyOne.Left -= enemyOneSpeed;

                    if (enemyOne.Left < pictureBox2.Left || enemyOne.Left + enemyOne.Width > pictureBox2.Left + pictureBox2.Width)
                    {
                        enemyOneSpeed = -enemyOneSpeed;

                    }
                    enemyTwo.Left += enemyTwoSpeed;

                    if (enemyTwo.Left < pictureBox5.Left || enemyTwo.Left + enemyTwo.Width > pictureBox5.Left + pictureBox5.Width)
                    {
                        enemyTwoSpeed = -enemyTwoSpeed;
                    }

                }
                if (level == 5)
                {
                    horizontalPlatform.Left -= horizontalspeed;

                    if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
                    {
                        horizontalspeed = -horizontalspeed;
                    }

                    verticalPlatform.Top += verticalspeed;

                    if (verticalPlatform.Top < 200 || verticalPlatform.Top > 653)
                    {
                        verticalspeed = -verticalspeed;
                    }
                    enemyOne.Left -= enemyOneSpeed;

                    if (enemyOne.Left < pictureBox2.Left || enemyOne.Left + enemyOne.Width > pictureBox2.Left + pictureBox2.Width)
                    {
                        enemyOneSpeed = -enemyOneSpeed;

                    }
                    enemyTwo.Left += enemyTwoSpeed;

                    if (enemyTwo.Left < pictureBox5.Left || enemyTwo.Left + enemyTwo.Width > pictureBox5.Left + pictureBox5.Width)
                    {
                        enemyTwoSpeed = -enemyTwoSpeed;
                    }
                }
                //there is no level 6 because nothing is moving in level 6
            }
        }
            
        private void txtGikon_Click(object sender, EventArgs e)
        {

        }
        private void RestartGame()
        {
           

        }

        private void kon_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Kon Rush", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        

        
        
        

       

        
      
    }
}
