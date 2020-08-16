using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dammaku
{
    class CircleBarrage : BulletGenerator
    {
        private Brush brush;


        public PointF Center { get; set; }
        public float Speed { get; set; }
        public SizeF BulletSize { get; set; }
        public Brush Brush
        {
            get => brush;
            set => brush = value ?? throw new ArgumentNullException(nameof(value));
        }
        public uint GeneratingNum { get; set; }


        public CircleBarrage(BulletList list, PointF center, float speed, SizeF bulletSize, Brush brush, uint genNum)
            : base(list)
        {
            Center = center;
            Speed = speed;
            BulletSize = bulletSize;
            this.brush = brush ?? throw new ArgumentNullException(nameof(brush));
            GeneratingNum = genNum;
        }

        public override void Generate()
        {
            double a = 0;
            double da = 2 * Math.PI / GeneratingNum;
            for (int i = 0; i < GeneratingNum; i++)
            {
                AddBullet(a);
                a += da;
            }
        }

        protected void AddBullet(double angle)
        {
            var v = new SizeF((float)(Speed * Math.Sin(angle)), (float)(Speed * -Math.Cos(angle)));
            bullets.Data.Add(new Bullet(Center, v, BulletSize, brush));
        }
    }
}
