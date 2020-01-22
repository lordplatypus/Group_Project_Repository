using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Group_Project_2
{
    public class Boss : GameObject
    {
        enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        const int CellSize = 64;

        float AngleSpeed = MyMath.PI / 256;
        Direction direction = Direction.Down;
        float theta = 0f;
        float radius = 450;
        float centerX;
        float centerY;
        int cooldownTimer = 0;
        int cooldown = 240;
        bool foundPlayer = false;

        public Boss(PlayScene playScene, float x, float y) : base(playScene)
        {
            imageWidth = 128;
            imageHeight = 128;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

            this.x = x;
            this.y = y;
            centerX = x - imageWidth / 2;
            centerY = y - imageHeight / 2;
            hp = 20;
        }

        public override void Update()
        {
            theta += AngleSpeed;
            if (theta >= 2 * MyMath.PI) theta = 0;

            if (foundPlayer)
            {
                if (cooldownTimer <= 0) Shoot();
                else cooldownTimer--;
            }
            else LookForPlayer();

            //if (!IsVisible()) foundPlayer = false;

            MoveX();
            MoveY();
            AnimationHandle();
        }

        void LookForPlayer()
        {
            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        centerX - 9 * CellSize, centerY - 9 * CellSize, centerX + 9 * CellSize, centerY + 9 * CellSize,
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                foundPlayer = true;
            }
        }

        void Shoot()
        {
            Player player = playScene.player;
            float bossCenterX = x + imageWidth / 2;
            float bossCenterY = y + imageHeight / 2;
            float angleToPlayer = MyMath.PointToPointAngle(bossCenterX, bossCenterY, player.x, player.y);

            playScene.gameObjects.Add(new BossBullet(playScene, bossCenterX, bossCenterY, angleToPlayer));
            cooldownTimer = cooldown;
        }

        void MoveX()
        {
            x = ((float)Math.Cos(theta) * radius) + centerX;

            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float top = GetTop();
            float middle = top + 64;
            float bottom = GetBottom() - 0.01f;

            if (playScene.map.IsWall(left, top) ||
                playScene.map.IsWall(left, middle) ||
                playScene.map.IsWall(left, bottom))
            {
                playScene.map.DeleteWall(left, top);
                playScene.map.DeleteWall(left, middle);
                playScene.map.DeleteWall(left, bottom);
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {
                playScene.map.DeleteWall(right, top);
                playScene.map.DeleteWall(right, middle);
                playScene.map.DeleteWall(right, bottom);
            }
        }

        void MoveY()
        {
            y = ((float)Math.Sin(theta) * radius) + centerY;

            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float top = GetTop();
            float middle = left + 64;
            float bottom = GetBottom() - 0.01f;

            if (playScene.map.IsWall(left, top) ||
                playScene.map.IsWall(middle, top) ||
                playScene.map.IsWall(right, top))
            {
                playScene.map.DeleteWall(left, top);
                playScene.map.DeleteWall(middle, top);
                playScene.map.DeleteWall(right, top);
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {
                playScene.map.DeleteWall(left, bottom);
                playScene.map.DeleteWall(middle, bottom);
                playScene.map.DeleteWall(right, bottom);
            }
        }

        void AnimationHandle()
        {
            //for animations
            if (theta >= MyMath.PI / 4 && theta < (3f / 4f) * MyMath.PI)
            {
                direction = Direction.Left;
            }
            else if (theta >= (3f / 4f) * MyMath.PI && theta < (5f / 4f) * MyMath.PI)
            {
                direction = Direction.Up;
            }
            else if (theta >= (5f / 4f) * MyMath.PI && theta < (7f / 4f) * MyMath.PI)
            {
                direction = Direction.Right;
            }
            else
            {
                direction = Direction.Down;
            }
        }

        public override void Draw()
        {
            if (direction == Direction.Left)
            {
                Camera.DrawGraph(x, y, Image.boss[2]);
            }
            else if (direction == Direction.Right)
            {
                Camera.DrawGraph(x, y, Image.boss[3]);
            }
            else if (direction == Direction.Up)
            {
                Camera.DrawGraph(x, y, Image.boss[1]);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.boss[0]);
            }
        }

        public override void OnCollision(GameObject other)
        {
            //if (other is PlayerShot)
            //{
            //    HP--;
            //    if (HP == 10)
            //    {
            //        AngleSpeed *= 2;
            //        cooldown /= 2;
            //    }
            //    if (HP <= 0) Kill();
            //}
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            if (hp == 10)
            {
                AngleSpeed *= 2;
                cooldown /= 2;
            }
        }

        public override void Kill()
        {
            base.Kill();

            playScene.map.CreateWarp();
        }
    }
}
