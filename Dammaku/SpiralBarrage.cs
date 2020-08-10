using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dammaku
{
    class SpiralBarrage : CircleBarrage
    {
        private double startAngle = 0.0;
        private double rotateAngle;


        public SpiralBarrage(BulletList list, PointF center, float speed, SizeF bulletSize, Brush brush, uint genNum, double rotateAngle)
            : base(list, center, speed, bulletSize, brush, genNum)
        {
            this.rotateAngle = rotateAngle * Math.PI / 180;
        }

        public override void Generate()
        {
            double a = startAngle;
            double da = 2 * Math.PI / GeneratingNum;
            for (int i = 0; i < GeneratingNum; i++)
            {
                AddBullet(a);
                a += da;
            }
            startAngle += rotateAngle;
        }
    }
}
