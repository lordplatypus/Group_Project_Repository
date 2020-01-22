using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class MissileItem : GameObject
    {
        public MissileItem(PlayScene playscene, float x, float y) : base(playscene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 64;
            imageHeight = 64;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            Camera.DrawGraph(x, y, Image.item);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                Kill();
            }
        }
    }
}
