using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dammaku
{
    class Bullet
    {
        private PointF position;
        private SizeF verocity;
        private SizeF size;
        private Brush brush;


        public Bullet(PointF position, SizeF verocity, SizeF size, Brush brush)
        {
            this.position = position;
            this.verocity = verocity;
            this.size = size;
            this.brush = brush ?? throw new ArgumentNullException(nameof(brush));
        }

        public void Draw(Graphics graphics)
        {
            var vx = position.X - size.Width / 2;
            var vy = position.Y - size.Height / 2;
            graphics.FillEllipse(brush, vx, vy, size.Width, size.Height);
        }

        public void Move()
        {
            position += verocity;
        }
    }
}
