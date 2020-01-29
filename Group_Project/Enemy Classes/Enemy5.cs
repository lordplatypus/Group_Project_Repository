using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;

namespace Group_Project_2
{    
    public class Enemy5 : Enemy
    {
        enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        const int CellSize = 64;
        const float Speed = 3f;

        float angleToPlayer = 0;
        bool foundPlayer = false;
        bool avoidPlayershot = false;

        float vx = 0;
        float vy = 0;

        Direction direction = Direction.Left;
        int limiter = 0;
        int animCount = 0;
        
        int cooldownTimer = 0;
        int cooldown = 180;
        float playerposition = 0;


        public Enemy5(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;
            hp = 5;
            maxHP = hp;

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

                playerposition = MyMath.PointToPointAngle(x, y, player.x, player.y);

                if (cooldownTimer <= 0) Shot();
                else cooldownTimer--;
            }
            else if (!foundPlayer && IsVisible()) LookForPlayer();
            else if (!IsVisible()) foundPlayer = false;

            if (!avoidPlayershot) LookForPlayerAttack();

            AnimationHandle();
            DrawHitBox();
        }

        void LookForPlayer()
        {
            Player player = playScene.player;            

            if (MyMath.RectRectIntersection(
                            GetLeft() - 5 * CellSize, GetTop() - 5 * CellSize, GetRight() + 5 * CellSize, GetBottom() + 5 * CellSize,
                            player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                foundPlayer = true;
                //avoidPlayershot = true;
            }
        }

        void Shot()
        {
            Player player = playScene.player;
            float enemy5centerX = x + imageWidth / 2;
            float enemy5centerY = y + imageHeight / 2;
            float angletoplayer = MyMath.PointToPointAngle(enemy5centerX, enemy5centerY, player.x, player.y);

            playScene.gameObjects.Add(new Enemy5Shot(playScene, enemy5centerX, enemy5centerY, angletoplayer));
            cooldownTimer = cooldown;
        }

        void LookForPlayerAttack()
        {
            GameObject playerBullet = null;
            if (playScene.gameObjects.Last() is SoilBlock || playScene.gameObjects.Last() is SlimeBlock || playScene.gameObjects.Last() is IronBlock ||
                playScene.gameObjects.Last() is StoneBlock || playScene.gameObjects.Last() is DiamondBlock)
            {
                playerBullet = playScene.gameObjects.Last();
            }

            if (playerBullet == null) return;

            int range = 1;
            if (MyMath.RectRectIntersection(
                            GetLeft() - range * CellSize, GetTop() - range * CellSize, GetRight() + range * CellSize, GetBottom() + range * CellSize,
                            playerBullet.GetLeft(), playerBullet.GetTop(), playerBullet.GetRight(), playerBullet.GetBottom()))
            {
                //avoidPlayershot = true;
                avoidPlayerShot(playerBullet);
            }
        }

        void avoidPlayerShot(GameObject playerBullet)
        {            
            //if()
            //{
            //    vx = 32;
            //    x += vx;
            //}

            if (Math.Abs(playerBullet.x - x) > Math.Abs(playerBullet.y - y))
            {
                if (playScene.map.GetTerrain(x, y + CellSize * 2) == -1) y += CellSize * 2;
                else if (playScene.map.GetTerrain(x, y - CellSize * 2) == -1) y -= CellSize * 2;
            }
            else
            {
                if (playScene.map.GetTerrain(x + CellSize * 2, y) == -1) x += CellSize * 2;
                else if (playScene.map.GetTerrain(x - CellSize * 2, y) == -1) x -= CellSize * 2;
            }
            avoidPlayershot = true;
        }

        void MoveX()
        {
            //if (MyMath.RectRectIntersection(-5 * CellSize, -5 * CellSize, 5 * CellSize, 5 * CellSize, x - 64, y + 64, x + 64, y - 64)) 
            //{
            //    for (int i = 0; i < 5; i++)
            //    {                                        
            //        vx++;
            //        x = vx;
            //    }                
            //}

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
            limiter++;
            if (limiter % 10 == 0)
            {
                animCount++;
                if (animCount > 3) animCount = 0;
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
                Camera.DrawGraph(x, y, Image.enemy5[2]);
            }
            else if (direction == Direction.Right)
            {
                Camera.DrawGraph(x, y, Image.enemy5[3]);
            }
            else if (direction == Direction.Up)
            {
                Camera.DrawGraph(x, y, Image.enemy5[1]);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.enemy5[0]);
            }

            DrawHPBar();
        }        

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                playScene.player.TakeDamage(1);
            }
        }
    }
}
