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
        private Player player;
        private Image playerImage;
        private bool gameOver;
        private PointF playerHome;
        
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
                new SizeF(12, 12), Brushes.Yellow, 13, 10, 30);
            playerImage = Image.FromFile("../../topview_man.png");
            playerImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            playerHome = new PointF(canvas.Width / 2, 400);
            player = new Player(playerHome, new SizeF(40, 40), new SizeF(5, 5),
                playerImage, new Rectangle(Point.Empty, canvas.Size));
            gameOver = false;
            stopwatch = new Stopwatch();
            frame = 0;
            nextFrame = 0;

            Clear();
            Draw();
            stopwatch.Start();
            timer1.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (playerImage != null)
            {
                playerImage.Dispose();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    player.StartMoving(Player.Direction.Left);
                    break;
                case Keys.Right:
                    player.StartMoving(Player.Direction.Right);
                    break;
                case Keys.Up:
                    player.StartMoving(Player.Direction.Up);
                    break;
                case Keys.Down:
                    player.StartMoving(Player.Direction.Down);
                    break;
                case Keys.Return:
                    if (gameOver)
                    {
                        gameOver = false;
                        frame = 0;
                        bullets.Data.Clear();
                        player.StopMoving();
                        player.Position = playerHome;
                    }
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    player.StopMoving(Player.Direction.Left);
                    break;
                case Keys.Right:
                    player.StopMoving(Player.Direction.Right);
                    break;
                case Keys.Up:
                    player.StopMoving(Player.Direction.Up);
                    break;
                case Keys.Down:
                    player.StopMoving(Player.Direction.Down);
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bullets.IsContained(player))
            {
                gameOver = true;
            }

            Clear();
            if (frame >= 90 && frame % 6 == 0 && frame / 40 % 2 == 0)
            {
                generator.Generate();
            }
            bullets.Move();
            bullets.RemoveOutOfRange(new Rectangle(Point.Empty, canvas.Size));
            player.Move();
            using (var g = GetGraphics())
            {
                bullets.Draw(g);
                if (gameOver)
                {
                    var format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    g.DrawString("GAME OVER", new Font("MSゴシック", 50), Brushes.Red, canvas.Width / 2, canvas.Height / 2, format);
                }
                else
                {
                    player.Draw(g);
                }
            }
            Draw();
            frame++;

            nextFrame += 1000.0 / 60.0;
            int delay = (int)(nextFrame - stopwatch.Elapsed.TotalMilliseconds);
            if (delay > 0) Thread.Sleep(delay);
        }
    }
}
