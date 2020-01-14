using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    class Boss3 : GameObject
    {
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        public Direction direction = Direction.Down;
        public bool rightShoulderDead = false;
        public bool leftShoulderDead = false;
        Boss3RightShoulder rShoulder;
        Boss3LeftShoulder lShoulder;
        const float Speed = 2f;
        float angleToPlayer = 0;
        float vx = 0;
        float vy = 0;

        public Boss3(PlayScene playScene, float x, float y) : base(playScene)
        {
            imageWidth = 128;
            imageHeight = 128;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

            this.x = x;
            this.y = y;
            hp = 35;

            rShoulder = new Boss3RightShoulder(playScene, this, x, y);
            lShoulder = new Boss3LeftShoulder(playScene, this, x, y);

            playScene.gameObjects.Add(rShoulder);
            playScene.gameObjects.Add(lShoulder);
        }

        public override void Update()
        {
            if (IsVisible())
            {
                angleToPlayer = MyMath.PointToPointAngle(x, y, playScene.player.x, playScene.player.y);

                MoveX();
                MoveY();
                AnimationHandle();
                if (!rightShoulderDead) rShoulder.Move(x, y);
                if (!leftShoulderDead) lShoulder.Move(x, y);
            }
        }

        void MoveX()
        {
            vx = (float)Math.Cos(angleToPlayer) * Speed;
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
            }
            else if (direction == Direction.Right)
            {
            }
            else if (direction == Direction.Up)
            {
            }
            else
            {
            }
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void TakeDamage(int damage)
        {
            if (rightShoulderDead && leftShoulderDead) base.TakeDamage(damage);
        }
    }
}
