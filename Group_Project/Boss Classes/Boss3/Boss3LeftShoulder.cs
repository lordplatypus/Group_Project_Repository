using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Group_Project_2
{
    class Boss3LeftShoulder : Enemy
    {
        Boss3 b;
        float xOffset = 0;
        float yOffset = 0;
        bool visible = true;
        bool destroyed = false;
        int flip = 0;
        float centerX = -18;
        float centerY = 18;
        float[] leftX = new float[] { 169, 160, 169, 168, 90, 98, 90, 90, 131, 137, 131, 140, 100, 100, 100, 100, 176, 164 };
        float[] leftY = new float[] { 63, 69, 63, 70, 60, 56, 60, 60, 76, 76, 76, 77, 50, 50, 50, 50, 73, 86 };
        //don't use animation count 12, 13, 14, 15

        public Boss3LeftShoulder(PlayScene playScene, Boss3 b, float x, float y) : base(playScene)
        {
            imageWidth = 64;
            imageHeight = 64;
            hitboxOffsetLeft = -36;
            hitboxOffsetRight = 36;
            hitboxOffsetTop = -36;
            hitboxOffsetBottom = 36;

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
            ChangeHitBox();

            //find the center of the shoulder (y is always the same)
            if (flip == 1) centerX = -18;
            else centerX = 18;

            //using the 2 arrays with shoulder locations + the current boss image (frame) + the center of the shoulder
            //find the correct position the shoulder should move to look like it is part of the boss
            xOffset = leftX[convert] - centerX;
            yOffset = leftY[convert] - centerY;
            
            //actaul movement
            x = bossX + xOffset;
            y = bossY + yOffset;           
        }

        int ConvertAnimationCount(int animationCount)
        {//converting the animationCount so I know the correct image (frame) currently playing.
            int convert = animationCount;

            if (animationCount > 15)
            {
                flip = 1; //0=false, 1=true - just flips the image if needed
                visible = true; //can you see the shoulder in the current frame?
            }
            else if (b.state == Boss3.State.Down)
            {
                flip = 1;
                visible = true;
            }
            else if (b.state == Boss3.State.Up)
            {
                convert += 4; //the actual "conversion" part
                flip = 0;
                visible = true;
            }
            else if (b.state == Boss3.State.Left)
            {
                convert += 8;
                flip = 1;
                visible = true;
            }
            else if (b.state == Boss3.State.Right)
            {
                convert += 12;
                flip = 0;
                visible = false;
            }

            return convert;
        }

        void ChangeHitBox()
        {
            if (b.state == Boss3.State.Right)
            {
                hitboxOffsetLeft = 32;
                hitboxOffsetRight = 32;
                hitboxOffsetTop = 32;
                hitboxOffsetBottom = 32;
            }
            else
            {
                hitboxOffsetLeft = -36;
                hitboxOffsetRight = 36;
                hitboxOffsetTop = -36;
                hitboxOffsetBottom = 36;
            }
        }

        public override void Draw()
        {
            if (visible && !destroyed)
            {//don't draw if not visible or when it is "destroyed"
                Camera.DrawRotaGraph(x, y, Image.leftShoulder, flip);
            }
            Camera.DrawBox(GetLeft(), GetTop(), GetRight(), GetBottom(), DX.GetColor(0, 250, 0), 0);
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void TakeDamage(int damage)
        {
            if (destroyed)
            {//if "dead" boss takes damage instead
                b.TakeDamage(damage);
            }
            else hp -= damage; //if not "dead"

            if (hp <= 0)
            {//mark the shoulder as "dead" once hp hits 0
                destroyed = true;
            }
        }
    }
}
