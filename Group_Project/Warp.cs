using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class Warp : GameObject
    {

        public Warp(PlayScene playScene, float x, float y) : base(playScene)
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
            Camera.DrawGraph(x, y, Image.Stairs);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                playScene.fm.MoveFloor();
            }
        }

        public override void TakeDamage(int damage)
        {
            
        }
    }
}
