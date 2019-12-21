using System;
using DxLibDLL;

namespace Group_Project_2
{
    public class PlayerShot : GameObject
    {
        const float MoveSpeed = 10f; // 横移動速度 
        float vx; // 横移動速度
        float vy; // 縦方向速度

        // プレイヤーの弾 
        public PlayerShot(PlayScene playScene, float x, float y,float angle) : base(playScene)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;

            imageWidth = 32;
            imageHeight = 32;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

        }

        public override void Update()
        {

            MoveX();
            MoveY();

            if (!IsVisible())
            {
                isDead = true;
            }
        }

        void MoveX()
        {
            //角度からx方向の移動速度を算出
            vx = (float)Math.Cos(angle) * MoveSpeed;

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
                isDead = true;
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {//check left
                isDead = true;
            }
        }

        void MoveY()
        {
            //角度からx方向の移動速度を算出
            vy = (float)Math.Sin(angle) * MoveSpeed;

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
                isDead = true;
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {//check down
                isDead = true; 
            }
        }

        public override void Draw()
        {
            Camera.DrawGraph(x,y,Image.playerbaretto);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy1)
            {
                Kill();
            }

            if (other is Enemy2)
            {
                Kill();
            }

            if (other is Enemy3)
            {
                Kill();
            }

            if (other is Enemy4)
            {
                Kill();
            }

            if(other is Boss)
            {
                Kill();
            }
        }
    }
}
