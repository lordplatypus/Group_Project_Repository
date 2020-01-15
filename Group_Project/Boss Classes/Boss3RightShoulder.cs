using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    class Boss3RightShoulder : GameObject
    {
        Boss3 b;
        float xOffset = 0;
        float yOffset = 0;
        bool visible = true;
        bool destroyed = false;
        int flip = 0;
        float centerX = 18;
        float centerY = 18;
        float[] rightX = new float[] { 93, 84, 92, 166, 174, 166, 0, 0, 0, 127, 149, 138, 100, 88 };
        float[] rightY = new float[] { 65, 71, 72, 60, 58, 62, 0, 0, 0, 77, 82, 81, 75, 88 };
        //don't use animation count 6, 7, 8

        public Boss3RightShoulder(PlayScene playScene, Boss3 b, float x, float y) : base(playScene)
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
                centerX = 18;
            }
            else
            {
                centerX = -18;
            }

            xOffset = rightX[convert] - centerX;
            yOffset = rightY[convert] - centerY;

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
                flip = 0;
                visible = false;
            }
            else if (b.state == Boss3.State.Right)
            {
                convert += 9;
                flip = 1;
                visible = true;
            }

            return convert;
        }

        public override void Draw()
        {
            if (visible)
            {
                Camera.DrawRotaGraph(x, y, Image.rightShoulder, flip);
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
            else hp = -damage;

            if (hp <= 0)
            {
                destroyed = true;
            }
        }
    }
}
