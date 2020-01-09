using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    class Enemy2 : GameObject
    {
        const float EnemySpace = 2.0f;
        float vx = EnemySpace;
        int counter = 0;

        const int MutekiJikan = 30;
        int mutekiTimer = 0;

        public Enemy2(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;
            hp = 5;

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
            counter++;

            if (counter % 128 == 0)
            {
                if (vx >= 0)
                {
                    playScene.gameObjects.Add(new EnemyShot(playScene, x, y, 0));
                }
                else
                {
                    playScene.gameObjects.Add(new EnemyShot(playScene, x, y, 180));
                }
            }
            mutekiTimer--;
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
            {
                vx = -vx;
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {
                vx = -vx;
            }
        }

        public override void Draw()
        {
            if (vx <= 0)
            {
                Camera.DrawGraph(x, y, Image.teki3[2]);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.teki3[3]);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy1 || other is Enemy2 || other is Enemy3 || other is Enemy4)
            {
                vx = -vx;
            }
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
