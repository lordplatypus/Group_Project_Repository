using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Group_Project_2
{
    public class Slime : Enemy
    {
        enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        const int CellSize = 64;
        const float Speed = 2;
        
        float angleToPlayer = 0;
        Direction direction = Direction.Down;
        float centerX;
        float centerY;
        int moveCount = 0;
        int mutekiCount = 0;
        int cooldownTimer = 0;
        int cooldown = 240;
        int mutekiTimer = 60;
        bool foundPlayer = false;
        bool muteki = false;

        float vx = 0;
        float vy = 0;

        public Slime(PlayScene playScene, float x, float y) : base(playScene)
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
            hp = 10;
            maxHP = hp;
        }

        public override void Update()
        {
            moveCount++;
            if (moveCount > 60)
            {
                moveCount = 0;
            }
            mutekiCount++;
            if (mutekiCount > 10)
            {
                mutekiCount = 0;
            }


            if (foundPlayer)
            {
                Player player = playScene.player;
                angleToPlayer = MyMath.PointToPointAngle(x, y, player.x, player.y);
                MoveX();
                MoveY();
                if (cooldownTimer <= 0) Shoot();
                else cooldownTimer--;
            }
            else if (!foundPlayer && IsVisible()) LookForPlayer();
            else if (!IsVisible()) foundPlayer = false;

            if (muteki == true)
            {
                mutekiTimer--;
                if (mutekiTimer <= 0)
                {
                    muteki = false;
                }
            }

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

            playScene.gameObjects.Add(new SlimeBullet(playScene, bossCenterX, bossCenterY, angleToPlayer));
            cooldownTimer = cooldown;
        }

        void MoveX()
        {
            vx = (float)Math.Cos(angleToPlayer) * Speed;
            if (muteki) vx /= 2;
            x += vx;

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
            vy = (float)Math.Sin(angleToPlayer) * Speed;
            if (muteki) vy /= 2;
            y += vy;

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
            if (angleToPlayer == 0)
            {
                direction = Direction.Down;
            }
            else if (angleToPlayer >= -(MyMath.PI / 4) && angleToPlayer < MyMath.PI / 4)
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
            if (muteki == true && mutekiCount > 5) return;
            if (moveCount <= 30)
            {
                if (direction == Direction.Left)
                {
                    Camera.DrawGraph(x, y, Image.Slime[2]);
                }
                else if (direction == Direction.Right)
                {
                    Camera.DrawGraph(x, y, Image.Slime[4]);
                }
                else if (direction == Direction.Up)
                {
                    Camera.DrawGraph(x, y, Image.Slime[6]);
                }
                else
                {
                    Camera.DrawGraph(x, y, Image.Slime[0]);
                }
            }
            else
            {
                if (direction == Direction.Left)
                {
                    Camera.DrawGraph(x, y, Image.Slime[3]);
                }
                else if (direction == Direction.Right)
                {
                    Camera.DrawGraph(x, y, Image.Slime[5]);
                }
                else if (direction == Direction.Up)
                {
                    Camera.DrawGraph(x, y, Image.Slime[7]);
                }
                else
                {
                    Camera.DrawGraph(x, y, Image.Slime[1]);
                }
            }

            DrawHPBar();
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

            if (other is Player)
            {
                playScene.player.TakeDamage(1);
            }
        }

        public override void TakeDamage(int damage)
        {
            if (muteki == true) return;
            muteki = true;
            mutekiTimer = 60;
            base.TakeDamage(damage);
        }

        public override void Kill()
        {
            base.Kill();
            playScene.pickaxeCount += 100;

            playScene.map.CreateWarp();
        }
    }
}
