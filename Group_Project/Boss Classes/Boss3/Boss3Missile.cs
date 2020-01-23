using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    class Boss3Missile : Enemy
    {
        enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        const float Speed = 2f;
        const int CellSize = 64;
        Direction direction = Direction.Down;
        float angleToPlayer;
        float vx;
        float vy;
        int animCount = 0;
        int limiter = 0;
        int lifespan = 300;

        public Boss3Missile(PlayScene playScene, float x, float y) : base(playScene)
        {
            imageWidth = 48;
            imageHeight = 48;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

            this.x = x;
            this.y = y;
            hp = 1;
        }

        public override void Update()
        {
            angleToPlayer = MyMath.PointToPointAngle(x, y, playScene.player.x, playScene.player.y);
            MoveX();
            MoveY();
            AnimationHandle();
            lifespan--;
            if (lifespan <= 0) Kill();
        }

        void MoveX()
        {
            vx = (float)Math.Cos(angleToPlayer) * Speed;
            x += vx;

            float left = GetLeft();
            float right = GetRight() - .01f;
            float top = GetTop();
            float middle = top + 24;
            float bottom = GetBottom() - .01f;

            if (playScene.map.IsWall(left, top) ||
                playScene.map.IsWall(left, middle) ||
                playScene.map.IsWall(left, bottom))
            {//check right
                Kill();
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {//check left
                Kill();
            }
        }

        void MoveY()
        {
            vy = (float)Math.Sin(angleToPlayer) * Speed;
            y += vy;

            float left = GetLeft();
            float right = GetRight() - .01f;
            float top = GetTop();
            float middle = left + 24;
            float bottom = GetBottom() - .01f;

            if (playScene.map.IsWall(left, top) ||
                playScene.map.IsWall(middle, top) ||
                playScene.map.IsWall(right, top))
            {//check up
                Kill();
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {//check down
                Kill();
            }
        }

        void AnimationHandle()
        {
            //for animations
            limiter++;
            if (limiter % 10 == 0)
            {
                animCount++;
                if (animCount > 3)
                {
                    animCount = 0;
                }
            }
            if (angleToPlayer >= -(MyMath.PI / 4) && angleToPlayer < MyMath.PI / 4)
            {
                direction = Direction.Right;
            }
            else if (angleToPlayer >= -(3 * MyMath.PI / 4) && angleToPlayer < -(MyMath.PI / 4))
            {
                direction = Direction.Up;
            }
            else if (angleToPlayer >= MyMath.PI / 4 && angleToPlayer < 3 * MyMath.PI / 4)
            {
                direction = Direction.Down;
            }
            else
            {
                direction = Direction.Left;
            }
        }

        public override void Draw()
        {
            if (direction == Direction.Left)
            {
                Camera.DrawGraph(x, y, Image.teki4[animCount + 4]);
            }
            else if (direction == Direction.Right)
            {
                Camera.DrawGraph(x, y, Image.teki4[animCount]);
            }
            else if (direction == Direction.Up)
            {
                Camera.DrawGraph(x, y, Image.teki4[animCount + 8]);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.teki4[animCount + 12]);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player) Kill();
        }

        public override void Kill()
        {
            base.Kill();
            ////grab the center of the enemy. Makes other calculations easier
            float enemyCenterX = x + imageWidth / 2;
            float enemyCenterY = y + imageHeight / 2;
            ////play effect
            playScene.pm.Explosion(enemyCenterX, enemyCenterY, 100);
            ////check if there is blocks within the explosion radius
            playScene.map.BlowUpWall(enemyCenterX, enemyCenterY);
            ////checks to see if the player is within the explosion radius
            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        GetLeft() - 1 * CellSize, GetTop() - 1 * CellSize, GetRight() + 1 * CellSize, GetBottom() + 1 * CellSize,
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                player.TakeDamage(1);
            }
            Sound.PlaySE(Sound.basicExplosion);
        }
    }
}
