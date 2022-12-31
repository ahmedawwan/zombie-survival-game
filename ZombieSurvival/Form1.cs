using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace ZombieSurvival
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        bool goup, godown, goleft, goright;
        string facing = "Up";
        double Health = 100;
        int speed = 10;
        int zombiespeed = 1;
        int ammo = 12;
        int score = 0;
        bool gameOver = false;
        Random rand = new Random();
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
                return;

            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
                facing = "Left";
                Player.Image = Properties.Resources.left;
            }

            if(e.KeyCode == Keys.Right)
            {
                goright = true;
                facing = "Right";
                Player.Image = Properties.Resources.right;
            }

            if(e.KeyCode == Keys.Up)
            {
                goup = true;
                facing = "Up";
                Player.Image = Properties.Resources.up;
            }

            if(e.KeyCode == Keys.Down)
            {
                godown = true;
                facing = "Down";
                Player.Image = Properties.Resources.down;
            }
            if (e.KeyCode == Keys.Space && ammo == 0)
            {
                SoundPlayer gunShot = new SoundPlayer();
                gunShot.SoundLocation = @"gunClick.wav";
                gunShot.Play();
            }
           


        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (gameOver)           
                return;
            if (e.KeyCode == Keys.Up)
                goup = false;
            if (e.KeyCode == Keys.Down)
                godown = false;
            if (e.KeyCode == Keys.Right)
                goright = false;
            if (e.KeyCode == Keys.Left)
                goleft = false;
            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                Shoot(facing);
                SoundPlayer gunShot = new SoundPlayer();
                gunShot.SoundLocation = @"gunShot.wav";
                gunShot.Play();

                if (ammo < 1)
                {
                    DropAmmo();
                    
                }
             
            }



        }
        public void nextlevel()
        {
            if (score == 10)
            {
                zombiespeed = 2;
                lblLevels.Text = "Level: 2";
            }
            if (score == 20)
            {
                zombiespeed = 3;
                lblLevels.Text = "Level: 3";
            }
            if (score == 30)
            {
                zombiespeed = 4;
                lblLevels.Text = "Level: 4";
            }
            if (score == 40)
            {
                zombiespeed = 5;
                lblLevels.Text = "Level: Final";
            }
        }

        private void GameEngine(object sender, EventArgs e)
        {
            nextlevel();
            if (cheatmode)
            {
                ammo = 12;
                Health = 100;
            }
                if (Health > 1)
            {
                progressBar1.Value = Convert.ToInt32(Health);
                
            }
            else
            {
                Player.Image = Properties.Resources.dead;
                game_Engine.Stop();
                gameOver = true;
                Restart.Visible = true;
            }
            lblAmmo.Text = "Ammo: " + ammo;
            lblKills.Text = "Kills: " + score;

            if (Health <= 20)
            {
                progressBar1.ForeColor = Color.Red;
                progressBar1.BackColor = Color.Red;
              
            }

            if(goleft && Player.Left > 0)
            {
                Player.Left -= speed;
            }
            if(goright && Player.Right + Player.Width < 960)
            {
                Player.Left += speed;
            }
            if(goup && Player.Top > 60)
            {
                Player.Top -= speed;
            }
            if(godown && Player.Top < 590)
            {
                Player.Top += speed;
            }

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && x.Tag == "Ammo")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Player.Bounds))
                    {
                        this.Controls.Remove((PictureBox)x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                if(x is PictureBox && x.Tag == "Bullet")
                {
                    if(((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 960 || ((PictureBox)x).Top > 560 || ((PictureBox)x).Top < 10)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                }

                if(x is PictureBox && x.Tag == "Zombie")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Player.Bounds))
                    {
                        Health -= 1;
                    }
                    if (((PictureBox)x).Left > Player.Left)
                    {
                        ((PictureBox)x).Left -= zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }
                    if (((PictureBox)x).Left < Player.Left)
                    {
                        ((PictureBox)x).Left += zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }
                    if (((PictureBox)x).Top > Player.Top)
                    {
                        ((PictureBox)x).Top -= zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (((PictureBox)x).Top < Player.Top)
                    {
                        ((PictureBox)x).Top += zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }

                foreach (Control j in this.Controls)
                {
                    if ((j is PictureBox && j.Tag == "Bullet") && (x is PictureBox && x.Tag == "Zombie")){
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                            makeZombie();
                        }
                    }
                }

            }

        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
             axWindowsMediaPlayer1.URL = @"SoundTrack.wav";
             axWindowsMediaPlayer1.Ctlcontrols.play();
            
        }

        private void Sound_Track(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = @"SoundTrack.wav";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }


        bool cheatmode = false;
        private void Cheats(object sender, EventArgs e)
        {
            if (cheatmode == false)
                cheatmode = true;
            else
                cheatmode = false;
            
        }

        public void DropAmmo()
        {
            PictureBox Ammo = new PictureBox();
            Ammo.Image = Properties.Resources.ammo_Image;
            Ammo.Tag = "Ammo";
            Ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            Ammo.Left = rand.Next(10,890);
            Ammo.Top = rand.Next(50,600);
            this.Controls.Add(Ammo);
            Ammo.BringToFront();
            Player.BringToFront();
            
        }

        public void Shoot(string direct)
        {
            
            Bullet shoot = new Bullet();
            shoot.direction = direct;
            shoot.bulletLeft = Player.Left + (Player.Width / 2);
            shoot.BulletTop = Player.Top + (Player.Width / 2);
            shoot.mkBullet(this);
            

        }

        public void makeZombie()
        {
            PictureBox Zombie = new PictureBox();
            Zombie.Image = Properties.Resources.zdown;
            Zombie.Left = rand.Next(0, 900);
            Zombie.Top = rand.Next(0,800);
            Zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            Zombie.Tag = "Zombie";
            this.Controls.Add(Zombie);
            axWindowsMediaPlayer1.URL = @"zombieSound.wav";
            axWindowsMediaPlayer1.Ctlcontrols.play();
            Player.BringToFront();
        }
    }
}
