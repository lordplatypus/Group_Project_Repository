using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class Enemy3 : GameObject
    {
        const int CellSize = 64;
        const float Speed = 0f;

        float angleToPlayer = 0;
        bool foundPlayer = false;
        float vx = 0;
        float vy = 0;

        bool isLook = false;
        bool isMaked = false;

        const int MutekiJikan = 30;
        int mutekiTimer = 0;

        public Enemy3(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;
            hp = 3;

            imageWidth = 48;
            imageHeight = 48;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
        }

        public override void Update()
        {
            if (foundPlayer)
            {
                Player player = playScene.player;
                angleToPlayer = MyMath.PointToPointAngle(x, y, player.x, player.y);
                MoveX();
                MoveY();
                isLook = true;
            }
            else if (!foundPlayer && IsVisible()) LookForPlayer();
            else if (!IsVisible()) foundPlayer = false;

            if (isLook == true && isMaked == false)
            {
                for (int i = 0; i < 360; i += 40)
                {
                    playScene.map.CreateBlock(x + 88 * (float)Math.Cos(i), y + 88 * (float)Math.Sin(i), 1);
                }
                isMaked = true;
            }

            mutekiTimer--;
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

        public override void Draw()
        {
            Camera.DrawGraph(x, y, Image.blockenemy);
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void TakeDamage(int damage)
        {
            if (mutekiTimer <= 0)
            {
                base.TakeDamage(damage);
                mutekiTimer = MutekiJikan;
            }
        }
    }
}
