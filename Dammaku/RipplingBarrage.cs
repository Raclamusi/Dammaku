using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dammaku
{
    class RipplingBarrage : CircleBarrage
    {
        private double startAngle = 0.0;
        private double ripplingSpeed;
        private double ripplingAngle = 0.0;
        private double maxAngle;


        public RipplingBarrage(BulletList list, PointF center, float speed, SizeF bulletSize, Brush brush, uint genNum, double ripplingSpeed, double maxAngle)
            : base(list, center, speed, bulletSize, brush, genNum)
        {
            this.ripplingSpeed = ripplingSpeed * Math.PI / 180;
            this.maxAngle = maxAngle * Math.PI / 180;
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
            ripplingAngle += ripplingSpeed;
            startAngle = maxAngle * Math.Sin(ripplingAngle);
        }
    }
}
