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


        public MainForm()
        {
            InitializeComponent();
        }

        public Graphics GetGraphics()
        {
            return Graphics.FromImage(canvas);
        }

        public void Draw()
        {
            pictureBox1.Image = canvas;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var g = GetGraphics())
            {
                g.Clear(Color.Transparent);
            }
            Draw();
        }
    }
}
