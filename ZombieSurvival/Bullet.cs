using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ZombieSurvival
{
    class Bullet
    {
        public string direction;
        public int speed = 20;
        PictureBox bullet = new PictureBox();
        Timer tm = new Timer();
        public int bulletLeft;
        public int BulletTop;

        public void mkBullet(Form form)
        {
            bullet.BackColor = System.Drawing.Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "Bullet";
            bullet.Left = bulletLeft;
            bullet.Top = BulletTop;
            bullet.BringToFront();
            form.Controls.Add(bullet);
            tm.Interval = speed;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
        }

        public void tm_Tick(object sender, EventArgs e)
        {
            if (direction == "Left")
                bullet.Left -= speed;
            if (direction == "Right")
                bullet.Left += speed;
            if (direction == "Up")
                bullet.Top -= speed;
            if (direction == "Down")
                bullet.Top += speed;

            if(bullet.Left < 16 || bullet.Left>860 || bullet.Top <10 || bullet.Top > 616)
            {
                tm.Stop();
                tm.Dispose();
                bullet.Dispose();
                tm = null;
                bullet = null;
            }
        }
    }
}
