using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dammaku
{
    public partial class MainForm : Form
    {
        private Bitmap canvas;

        private BulletList bullets;
        private BulletGenerator generator;
        uint frame = 0;


        public MainForm()
        {
            InitializeComponent();
        }

        private Graphics GetGraphics()
        {
            return Graphics.FromImage(canvas);
        }

        private void Draw()
        {
            pictureBox1.Image = canvas;
        }

        private void Clear()
        {
            using (var g = GetGraphics())
            {
                g.Clear(Color.Transparent);
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bullets = new BulletList();
            generator = new CircleBarrage(bullets,
                new PointF(canvas.Width / 2, 100), 5,
                new SizeF(15, 15), Brushes.Yellow, 19);

            Clear();
            Draw();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Clear();
            if (frame % 10 == 0)
            {
                generator.Generate();
            }
            bullets.Move();
            bullets.RemoveOutOfRange(canvas.Size);
            bullets.Draw(GetGraphics());
            Draw();
            frame++;
        }
    }
}
