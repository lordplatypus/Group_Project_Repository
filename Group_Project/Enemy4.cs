using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class Enemy4 : GameObject
    {
        enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        const int CellSize = 64;
        const float Speed = 2f;

        float angleToPlayer = 0;
        bool foundPlayer = false;
        bool shouldExplode = false;
        int explodeCountDown = 30;
        float vx = 0;
        float vy = 0;

        Direction direction = Direction.Left;
        int limiter = 0;
        int animCount = 0;

        Point moveTo;

        public Enemy4(PlayScene playScene, float x, float y) : base (playScene)
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
            if (!shouldExplode)
            {
                if (foundPlayer)
                {
                    //Player player = playScene.player;
                    //angleToPlayer = MyMath.PointToPointAngle(x, y, player.x, player.y);
                    moveTo = playScene.map.Move(new Point(x, y));
                    angleToPlayer = MyMath.PointToPointAngle(x, y, moveTo.x, moveTo.y);
                    PlayerEnterExplodeRadius();
                    MoveX();
                    MoveY();
                }
                else if (!foundPlayer && IsVisible()) LookForPlayer();
                else if (!IsVisible()) foundPlayer = false;

                AnimationHandle();
            }
            else
            {
                explodeCountDown--;
                if (explodeCountDown <= 0) Kill();
            }
        }

        void LookForPlayer()
        {
            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        GetLeft() - 5 * CellSize, GetTop() - 5 * CellSize, GetRight() + 5 * CellSize, GetBottom() + 5 * CellSize,
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                foundPlayer = true;
            }
        }

        void PlayerEnterExplodeRadius()
        {
            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        GetLeft() - CellSize, GetTop() - CellSize, GetRight() + CellSize, GetBottom() + CellSize,
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                shouldExplode = true;
            }
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
                float wallRight = left - left % Map.CellSize + Map.CellSize;
                SetLeft(wallRight);
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {//check left
                float wallLeft = right - right % Map.CellSize;
                SetRight(wallLeft);
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
                float wallUp = top - top % Map.CellSize + Map.CellSize;
                SetTop(wallUp);
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {//check down
                float wallDown = bottom - bottom % Map.CellSize;
                SetBottom(wallDown);
            }
        }

        void AnimationHandle()
        {
            //for animations
            limiter++;
            if (limiter % 10 == 0)
            {
                animCount++;
                if (animCount > 3) animCount = 0;
            }
            if (angleToPlayer >= -(MyMath.PI/4) && angleToPlayer < MyMath.PI / 4)
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

            //Camera.DrawString(x, y, angleToPlayer.ToString());
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Empty)
            {               
                Kill();
            }

            if (other is PlayerShot)
            {
                Kill();
            }
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
            //Player player = playScene.player;
            //if (MyMath.RectRectIntersection(
            //            GetLeft() - 2 * CellSize, GetTop() - 2 * CellSize, GetRight() + 2 * CellSize, GetBottom() + 2 * CellSize,
            //            player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            //{
            //    player.TakeDamage(1);
            //}
        }
    }
}
