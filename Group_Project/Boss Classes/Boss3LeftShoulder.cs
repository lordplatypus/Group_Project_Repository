using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    class Boss3LeftShoulder : GameObject
    {
        Boss3 b;
        float xOffset = 0;
        float yOffset = 0;
        bool visible = true;
        bool destroyed = false;
        int flip = 0;
        float centerX = -18;
        float centerY = 18;
        float[] leftX = new float[] { 169, 160, 168, 90, 98, 90, 131, 137, 140, 0, 0, 0, 176, 164 };
        float[] leftY = new float[] { 63, 69, 70, 60, 56, 60, 76, 76, 77, 0, 0, 0, 73, 86 };
        //don't use animation count 9, 10, 11

        public Boss3LeftShoulder(PlayScene playScene, Boss3 b, float x, float y) : base(playScene)
        {
            imageWidth = 64;
            imageHeight = 64;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

            this.x = x;
            this.y = y;
            hp = 3;
            this.b = b;
        }

        public override void Update()
        {
        }

        public void Move(float bossX, float bossY, int animationCount)
        {
            int convert = ConvertAnimationCount(animationCount);

            if (flip == 1)
            {
                centerX = -18;
            }
            else
            {
                centerX = 18;
            }

            xOffset = leftX[convert] - centerX;
            yOffset = leftY[convert] - centerY;
            
            x = bossX + xOffset;
            y = bossY + yOffset;           
        }

        int ConvertAnimationCount(int animationCount)
        {
            int convert = animationCount;

            if (animationCount > 11)
            {
                flip = 1;
                visible = true;
            }
            else if (b.state == Boss3.State.Down)
            {
                flip = 1;
                visible = true;
            }
            else if (b.state == Boss3.State.Up)
            {
                convert += 3;
                flip = 0;
                visible = true;
            }
            else if (b.state == Boss3.State.Left)
            {
                convert += 6;
                flip = 1;
                visible = true;
            }
            else if (b.state == Boss3.State.Right)
            {
                convert += 9;
                flip = 0;
                visible = false;
            }

            return convert;
        }

        public override void Draw()
        {
            if (visible && !destroyed)
            {
                Camera.DrawRotaGraph(x, y, Image.leftShoulder, flip);
            }
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void TakeDamage(int damage)
        {
            if (destroyed)
            {
                b.TakeDamage(damage);
            }
            else hp =- damage;

            if (hp <= 0)
            {
                destroyed = true;
            }
        }
    }
}
