using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class PlayerMissileBullet : GameObject
    {
        const int CellSize = 64;
        const float Speed = 10f;
        float vx = 0;
        float vy = 0;
        int lifespan = 120;

        public PlayerMissileBullet(PlayScene playScene, float x, float y, float angle) : base(playScene)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            vx = (float)Math.Cos(angle) * Speed;
            vy = (float)Math.Sin(angle) * Speed;

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

            lifespan--;
            if (lifespan <= 0) Kill();
        }

        void MoveX()
        {
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

        public override void Draw()
        {
            Camera.DrawGraph(x, y, Image.particleDot1);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                Kill(other);
            }
        }

        public void Kill(GameObject other)
        {
            base.Kill();
            float enemyCenterX = x + imageWidth / 2;
            float enemyCenterY = y + imageHeight / 2;
            playScene.pm.Explosion(enemyCenterX, enemyCenterY, 100);
            playScene.map.BlowUpWall(enemyCenterX, enemyCenterY);

            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        GetLeft() - 2 * CellSize, GetTop() - 2 * CellSize, GetRight() + 2 * CellSize, GetBottom() + 2 * CellSize,
                        other.GetLeft(), other.GetTop(), other.GetRight(), other.GetBottom()))
            {
                other.TakeDamage(1);
            }
        }
    }
}
