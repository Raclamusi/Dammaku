using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dammaku
{
    class BulletList
    {
        public List<Bullet> Data { get; }


        public BulletList()
        {
            Data = new List<Bullet>();
        }

        public void Draw(Graphics graphics)
        {
            foreach (var b in Data)
            {
                b.Draw(graphics);
            }
        }

        public void Move()
        {
            foreach (var b in Data)
            {
                b.Move();
            }
        }

        public void RemoveOutOfRange(Rectangle range)
        {
            for (int i = Data.Count - 1; i >= 0; i--)
            {
                if (!Data[i].Contain(range))
                {
                    Data.RemoveAt(i);
                }
            }
        }

        public void RemoveOutOfRange(Size size) => RemoveOutOfRange(new Rectangle(Point.Empty, size));
    }
}
