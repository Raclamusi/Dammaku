using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Dammaku
{
    public partial class MainForm : Form
    {
        private Bitmap canvas;

        private BulletList bullets;
        private BulletGenerator generator;
        
        private Stopwatch stopwatch;
        private uint frame;
        private double nextFrame;


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
            generator = new RipplingBarrage(bullets,
                new PointF(canvas.Width / 2, 100), 5,
                new SizeF(10, 10), Brushes.Yellow, 15, 15, 20);
            stopwatch = new Stopwatch();
            frame = 0;
            nextFrame = 0;

            Clear();
            Draw();
            stopwatch.Start();
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
            using (var g = GetGraphics())
            {
                bullets.Draw(g);
            }
            Draw();
            frame++;

            nextFrame += 1000.0 / 60.0;
            int delay = (int)(nextFrame - stopwatch.Elapsed.TotalMilliseconds);
            if (delay > 0) Thread.Sleep(delay);
        }
    }
}
