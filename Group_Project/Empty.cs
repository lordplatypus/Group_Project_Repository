using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class Empty : GameObject
    {
        const int damage = 1;
        int count = 0;

        public Empty(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 48;
            imageHeight = 48;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
        }

        public override void Update()
        {
            count++;
            if(count >= 10)
            {
                Kill();
            }
        }

        public override void Draw()
        {
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player) return;

            other.TakeDamage(damage);
        }
    }
}

