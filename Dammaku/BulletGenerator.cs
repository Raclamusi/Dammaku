using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dammaku
{
    abstract class BulletGenerator
    {
        private BulletList bullets;


        public BulletList Bullets
        {
            get => bullets;
            set => bullets = value ?? throw new ArgumentNullException(nameof(value));
        }


        public BulletGenerator(BulletList list)
        {
            bullets = list ?? throw new ArgumentNullException(nameof(list));
        }


        public abstract void Generate();
    }
}
