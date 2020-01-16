using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;

namespace Group_Project_2
{
    class Boss3SmashAttack : GameObject
    {
        const int CellSize = 64;
        const int timer = 30;

        float left;
        float right;
        float top;
        float bottom;
        uint color = DX.GetColor(255, 0, 0);
        int count = 0;

        public Boss3SmashAttack(PlayScene playScene, float x, float y) : base(playScene)
        {
            imageWidth = 64;
            imageHeight = 64;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

            this.x = x - (x % CellSize);
            this.y = y - (y % CellSize);
            left = this.x + CellSize/2 - 1;
            right = this.x + CellSize/2 + 1;
            top = this.y + CellSize/2 - 1;
            bottom = this.y + CellSize/2 + 1; 
        }

        public override void Update()
        {
            count++;

            float difference = 31 / timer;
            left -= difference;
            right += difference;
            top -= difference;
            bottom += difference;

            if (count == timer)
            {
                DealDamage();
                SpawnBlock();
                Kill();
            }
        }

        void SpawnBlock()
        {
            int blockID = MyRandom.Range(0, 4);
            playScene.map.CreateBlock(x, y, blockID);

            int red, green, blue;
            if (blockID == 0)
            {//slime
                red = 0;
                green = 255;
                blue = 0;
            }
            else if (blockID == 1)
            {//soil
                red = 176;
                green = 112;
                blue = 0;
            }
            else if (blockID == 2)
            {//stone
                red = 100;
                green = 100;
                blue = 100;
            }
            else
            {//iron
                red = 200;
                green = 200;
                blue = 200;
            }
            x += CellSize / 2;
            y += CellSize / 2;
            playScene.pm.Smoke(x, y, 30, 10, 10);
            playScene.pm.Spark(x, y, red, green, blue);
            playScene.pm.BreakWall(x, y, red, green, blue);
        }

        void DealDamage()
        {
            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        GetLeft(), GetTop(), GetRight(), GetBottom(),
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                player.TakeDamage(1);
            }
        }

        public override void Draw()
        {
            Camera.DrawBox(left, top, right, bottom, color, 1);
        }

        public override void OnCollision(GameObject other)
        {           
        }        
    }
}
