using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class Enemy1 : GameObject
    {

        const float Speed = 1f;
        
        float vx = Speed;
        float vy = -Speed;

        enum State
        {
            Left,
            Right,
            Up,
            Down,
        }

        State state = State.Up;

        public Enemy1(PlayScene playScene, float x, float y) : base(playScene)
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
            MoveX();
            MoveY();
        }

        void MoveX()
        {
            if (vy != 0) return;
            x += vx;
            if (vx > 0)
            {
                state = State.Right;
            }
            else if (vx < 0)
            {
                state = State.Left;
            }

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
                {
                    vx = -vx;
                }
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {//check left
                float wallLeft = right - right % Map.CellSize;
                SetRight(wallLeft);
                {
                    vx = -vx;
                }
            }
        }

        void MoveY()
        {
            //if (vx != 0) return;
            y += vy;

            if (vy > 0)
            {
                state = State.Down;
            }
            else if (vy < 0)
            {
                state = State.Up;
            }

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
                {
                    vy = -vy;
                }
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {//check down
                float wallDown = bottom - bottom % Map.CellSize;
                SetBottom(wallDown);
                {
                    vy = -vy;
                }
            }
        }

        public override void Draw()
        {
            if (state == State.Down)
            {
                Camera.DrawGraph(x, y, Image.moveEnemy[1]);
            }
            else if (state == State.Right)
            {
                Camera.DrawGraph(x, y, Image.moveEnemy[4]);
            }
            else if (state == State.Left)
            {
                Camera.DrawGraph(x, y, Image.moveEnemy[7]);
            }
            else if (state == State.Up)
            {
                Camera.DrawGraph(x, y, Image.moveEnemy[10]);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player || other is Enemy1 || other is Enemy2 || other is Enemy3 || other is Enemy4)
            {
                if (other.x < x && other.x + 48 > x)
                {
                    vy = 0;
                    vx = Speed;
                }
                if (other.x > x && other.x < x + 48)
                {
                    vy = 0;
                    vx = -Speed;
                }
                if (other.y < y && other.y + 48 > y)
                {
                    vx = 0;
                    vy = Speed;
                }
                if (other.y > y && other.y < y + 48)
                {
                    vx = 0;
                    vy = -Speed;
                }
            }

            if(other is PlayerShot)
            {
                Kill();
            }

            if (other is Empty)
            {
                Kill();
            }
        }
    }
}
