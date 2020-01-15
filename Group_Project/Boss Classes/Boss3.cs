using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;

namespace Group_Project_2
{
    class Boss3 : GameObject
    {
        public enum State
        {
            Left,
            Right,
            Up,
            Down,
            ReadyAttack,
            Attack,
        }

        const int CellSize = 64;
        const float Speed = 2f;
        const int AttackTime = 30;
        const int AttackCooldown = 120;

        float[] rightX = new float[] { 93, 84, 92, 166, 174, 166, 127, 149, 138, 176, 164 };
        float[] rightY = new float[] { 65, 71, 72, 60, 58, 62, 77, 82, 81, 73, 86 };
        float[] leftX = new float[] { 169, 160, 168, 90, 98, 90, 131, 137, 140, 100, 88 };
        float[] leftY = new float[] { 63, 69, 70, 60, 65, 60, 76, 76, 77, 75, 88 };

        public State state = State.Down;
        public bool rightShoulderDead = false;
        public bool leftShoulderDead = false;
        Boss3RightShoulder rShoulder;
        Boss3LeftShoulder lShoulder;
        
        float angleToPlayer = 0;
        float vx = 0;
        float vy = 0;
        int timer = 0;
        int animationCounter = 0;
        int attackTimer;

        public Boss3(PlayScene playScene, float x, float y) : base(playScene)
        {
            imageWidth = 256;
            imageHeight = 256;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 128;
            hitboxOffsetBottom = 30;

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
                if (attackTimer > 0) attackTimer--;
                //else if (animationCounter > 2) animationCounter = 0;

                if (state == State.ReadyAttack)
                {
                    animationCounter = 12;
                    if (attackTimer <= 0)
                    {
                        state = State.Attack;
                        attackTimer = AttackTime;
                        Attack();                       
                    }
                }
                else if (state == State.Attack)
                {
                    animationCounter = 13;
                    if (attackTimer <= 0)
                    {
                        state = State.Down;
                        attackTimer = AttackCooldown;
                    }
                }
                else
                {
                    timer++;
                    if (timer % 10 == 0)
                    {
                        animationCounter++;                       
                    }
                    if (animationCounter >= 3) animationCounter = 0;

                    float centerX = x + imageWidth / 2;
                    float centerY = y + imageHeight / 2;
                    angleToPlayer = MyMath.PointToPointAngle(centerX, centerY, playScene.player.x, playScene.player.y);
                   
                    AnimationHandle();
                    
                    if (AttackRange())
                    {
                        if (attackTimer <= 0)
                        {
                            state = State.ReadyAttack;
                            attackTimer = AttackTime;
                        }
                        animationCounter = 0;
                    }
                    else
                    {
                        MoveX();
                        MoveY();
                    }
                }
                if (!rightShoulderDead) rShoulder.Move(x, y, animationCounter);
                if (!leftShoulderDead) lShoulder.Move(x, y, animationCounter);
            }
        }

        void Attack()
        {
            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        GetLeft() - 1 * CellSize, GetTop() - 1 * CellSize, GetRight() + 1 * CellSize, GetBottom() + 1 * CellSize,
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                player.TakeDamage(3);
            }
            playScene.pm.Smoke(x + imageWidth / 2, y + imageHeight, 300, 100, 50);

            for (int i = 0; i < 10; i++)
            {
                float blockLocX = x + imageWidth / 2 + MyRandom.PlusMinus(10*CellSize);
                float blockLocY = y + imageHeight + MyRandom.PlusMinus(10*CellSize);
                int blockID = MyRandom.Range(0, 4);
                playScene.map.CreateBlock(blockLocX, blockLocY, blockID);
            }
        }

        bool AttackRange()
        {
            Player player = playScene.player;
            if (MyMath.RectRectIntersection(
                        GetLeft() - 1 * CellSize, GetTop() - 1 * CellSize, GetRight() + 1 * CellSize, GetBottom() + 1 * CellSize,
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                return true;
            }
            else return false;
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
            if (angleToPlayer >= -(MyMath.PI / 4) && angleToPlayer < MyMath.PI / 4)
            {
                state = State.Right;
            }
            else if (angleToPlayer >= -(3 * MyMath.PI / 4) && angleToPlayer < -(MyMath.PI / 4))
            {
                state = State.Up;
            }
            else if (angleToPlayer >= MyMath.PI / 4 && angleToPlayer < 3 * MyMath.PI / 4)
            {
                state = State.Down;
            }
            else
            {
                state = State.Left;
            }
        }

        public override void Draw()
        {
            if (state == State.ReadyAttack)
            {
                Camera.DrawGraph(x, y, Image.boss3[animationCounter]);
            }
            else if (state == State.Attack)
            {
                Camera.DrawGraph(x, y, Image.boss3[animationCounter]);
            }
            else if (state == State.Left)
            {
                Camera.DrawGraph(x, y, Image.boss3[6 + animationCounter]);
            }
            else if (state == State.Right)
            {
                Camera.DrawGraph(x, y, Image.boss3[9 + animationCounter]);
            }
            else if (state == State.Up)
            {
                Camera.DrawGraph(x, y, Image.boss3[3 + animationCounter]);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.boss3[0 + animationCounter]);
            }
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void Kill()
        {
            base.Kill();
            lShoulder.Kill();
            rShoulder.Kill();
        }
    }
}
