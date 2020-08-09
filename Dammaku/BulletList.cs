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
    }
}
