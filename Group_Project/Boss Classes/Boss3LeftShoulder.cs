﻿using System;
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

        void ChangeOffset()
        {
            if (b.state == Boss3.State.Left)
            {
                xOffset = 0;
                yOffset = 0;
            }
            else if (b.state == Boss3.State.Right)
            {
                xOffset = 0;
                yOffset = 0;
            }
            else if (b.state == Boss3.State.Up)
            {
                xOffset = 0;
                yOffset = 0;
            }
            else if (b.state == Boss3.State.Down)
            {
                xOffset = 0;
                yOffset = 0;
            }

            if (b.state == Boss3.State.Right) visible = false;
            else visible = true;
        }

        public void Move(float bossX, float bossY)
        {
            ChangeOffset();
            MoveX(bossX);
            MoveY(bossY);
        }

        void MoveX(float bossX)
        {
            x = bossX + xOffset;
        }

        void MoveY(float bossY)
        {
            y = bossY + yOffset;
        }

        public override void Draw()
        {
            if (visible)
            {
                Camera.DrawGraph(x, y, Image.boss3Shoulders[0]);
            }
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void Kill()
        {
            base.Kill();
            b.leftShoulderDead = true;
        }
    }
}
