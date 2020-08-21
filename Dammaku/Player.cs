using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dammaku
{
    class Player
    {
        public enum Direction
        {
            Left, Right, Up, Down, 
        }


        private Image image;
        private RectangleF region;
        private SizeF verocity;
        private SizeF speed;
        private Rectangle rangeOfMovement;


        public RectangleF Region => region;
        public PointF Position
        {
            get => new PointF(region.X + region.Width / 2, region.Y + region.Height / 2);
            set
            {
                region.X = value.X - region.Width / 2;
                region.Y = value.Y - region.Height / 2;
            }
        }


        public Player(PointF position, SizeF size, SizeF speed, Image image, Rectangle rangeOfMovement)
        {
            this.image = image ?? throw new ArgumentNullException(nameof(image));
            region.X = position.X - size.Width / 2;
            region.Y = position.Y - size.Height / 2;
            region.Size = size;
            verocity = SizeF.Empty;
            this.speed = speed;
            this.rangeOfMovement = rangeOfMovement;
        }

        public void Draw(Graphics g)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }
            g.DrawImage(image, Region);
        }

        public void Move()
        {
            region.Location += verocity;
            if (region.Left < rangeOfMovement.Left) region.X = rangeOfMovement.Left;
            if (region.Right > rangeOfMovement.Right) region.X = rangeOfMovement.Right - region.Width;
            if (region.Top < rangeOfMovement.Top) region.Y = rangeOfMovement.Top;
            if (region.Bottom > rangeOfMovement.Bottom) region.Y = rangeOfMovement.Bottom - region.Height;
        }

        public void StartMoving(Direction d)
        {
            switch (d)
            {
                case Direction.Left:
                    verocity.Width = -speed.Width;
                    break;
                case Direction.Right:
                    verocity.Width = speed.Width;
                    break;
                case Direction.Up:
                    verocity.Height = -speed.Height;
                    break;
                case Direction.Down:
                    verocity.Height = speed.Height;
                    break;
            }
        }

        public void StopMoving(Direction d)
        {
            switch (d)
            {
                case Direction.Left:
                case Direction.Right:
                    verocity.Width = 0;
                    break;
                case Direction.Up:
                case Direction.Down:
                    verocity.Height = 0;
                    break;
            }
        }

        public void StopMoving()
        {
            verocity = SizeF.Empty;
        }

        public bool Contain(PointF pt)
        {
            float a = region.Width / 2;
            float b = region.Height / 2;
            float x = pt.X - Position.X;
            float y = pt.Y - Position.Y;
            a *= a;
            b *= b;
            x *= x;
            y *= y;
            return x / a + y / b < 1;
        }
    }
}
