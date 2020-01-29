using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;
using DxLibDLL;

namespace Group_Project_2
{
    class Boss3 : Enemy
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
        const float Speed = 1.5f;
        const int AttackTime = 30;
        const int AttackCooldown = 120;
        const int MissileCooldown = 420;

        public State state = State.Down;
        Boss3RightShoulder rShoulder;
        Boss3LeftShoulder lShoulder;
        
        float angleToPlayer = 0;
        float vx = 0;
        float vy = 0;
        int timer = 0;
        int animationCounter = 0;
        int attackTimer;
        int missileTimer;

        const int MutekiJikan = 30;
        int mutekiTimer = 0;

        bool isDying = false;
        bool exploded = false;
        int dyingTimer = 400;

        public Boss3(PlayScene playScene, float x, float y) : base(playScene)
        {
            imageWidth = 256;
            imageHeight = 256;
            hitboxOffsetLeft = 56;
            hitboxOffsetRight = 56;
            hitboxOffsetTop = 128;
            hitboxOffsetBottom = 30;

            this.x = x;
            this.y = y;
            hp = 35;
            maxHP = hp;
            //hp = 1;

            rShoulder = new Boss3RightShoulder(playScene, this, x, y);
            lShoulder = new Boss3LeftShoulder(playScene, this, x, y);

            playScene.gameObjects.Add(rShoulder);
            playScene.gameObjects.Add(lShoulder);
        }

        public override void Update()
        {
            if (isDying)
            {//death animation
                dyingTimer--;

                float centerX = x + imageWidth / 2;
                float centerY = y + imageHeight / 2;

                if (dyingTimer > 200)
                {
                    playScene.pm.Charge(centerX, centerY);
                    if (dyingTimer%5 == 0) Sound.PlaySE(Sound.basicExplosion);
                    if (dyingTimer % 10 == 0) playScene.pm.Explosion(centerX + MyRandom.PlusMinus(100), centerY + MyRandom.PlusMinus(100), 0);
                    x += MyRandom.PlusMinus(5);
                    y += MyRandom.PlusMinus(5);
                    rShoulder.Move(x, y, animationCounter);
                    lShoulder.Move(x, y, animationCounter);
                }
                else if (dyingTimer == 200)
                {
                    playScene.pm.ReverseShockWave(centerX, centerY);
                    Sound.PlaySE(Sound.implosions);
                }
                else if (dyingTimer == 150)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        playScene.pm.Explosion(centerX + MyRandom.PlusMinus(100), centerY + MyRandom.PlusMinus(100), 10);
                    }
                    exploded = true; //won't draw anything
                    rShoulder.TakeDamage(3); //to remove the shoulder image, just in case it wasn't destroyed earlier
                    lShoulder.TakeDamage(3); //same as above
                    Sound.PlaySE(Sound.finalExplosion);
                }
                else if (dyingTimer < 150 && dyingTimer % 10 == 0)
                {
                    playScene.pm.FireWork(centerX + MyRandom.PlusMinus(100), centerY + MyRandom.PlusMinus(100));
                }
                else if (dyingTimer <= 0) Kill();
            }
            else if (IsVisible())
            {//won't do anything unless it is witin the player's screen
                if (attackTimer > 0) attackTimer--; //timer for boss attacks that don't involve missiles
                if (missileTimer > 0) missileTimer--; //timer for missile attack

                if (state == State.ReadyAttack)
                {//wind up for smash attack
                    animationCounter = 16; //play correct animation
                    if (attackTimer <= 0)
                    {//once the timer hits 0 move from "ready" position to "attack" position
                        state = State.Attack;
                        attackTimer = AttackTime; //reset timer
                        Attack();                       
                    }
                }
                else if (state == State.Attack)
                {//Smash attack (more like smash attack cooldown)
                    animationCounter = 17; //play correct animation
                    if (attackTimer <= 0)
                    {//once the timer hits 0, move from the "attack" position to the normal walking animation
                        state = State.Down;
                        attackTimer = AttackCooldown; //set attack cooldown so the boss dosen't immediately attack again
                    }
                }
                else
                {                    
                    //grab the center of the boss
                    float centerX = x + imageWidth / 2;
                    float centerY = y + imageHeight / 2;
                    //find the angle to the player from the center of the boss
                    angleToPlayer = MyMath.PointToPointAngle(centerX, centerY, playScene.player.x, playScene.player.y);
                   
                    AnimationHandle(); //animations are handled here
                    
                    if (AttackRange())
                    {//checks to see if the player is whithin smash attack range
                        if (attackTimer <= 0)
                        {//if the timer is 0 start the smash attack
                            state = State.ReadyAttack;
                            attackTimer = AttackTime;
                        }
                        //if the player is within smash attack range, but the attack is on cooldown, the boss will just stand in one place
                        //this makes it so the boss dosen't constantly run into / spaz on the player
                        animationCounter = 0; 
                    }
                    else
                    {//not in smash attack range
                        if (missileTimer <= 0)
                        {//if the missile timer is 0, shoot missiles (2 of them)
                            playScene.gameObjects.Add(new Boss3Missile(playScene, x + imageWidth / 2, y + imageHeight / 2 + 50));
                            playScene.gameObjects.Add(new Boss3Missile(playScene, x + imageWidth / 2, y + imageHeight / 2 - 50));
                            missileTimer = MissileCooldown; //missile attack cooldown
                            Sound.PlaySE(Sound.missileLaunch); //missile launch sound effect
                        }
                        //actual boss movement
                        MoveX();
                        MoveY();
                    }
                }
                //movement for both shoulders
                rShoulder.Move(x, y, animationCounter);
                lShoulder.Move(x, y, animationCounter);
            }

            if (mutekiTimer > 0) mutekiTimer--; //invinciblity frames
        }

        void Attack()
        {//Smash attack
            Player player = playScene.player;
            int range = 1;
            if (MyMath.RectRectIntersection(
                        GetLeft() - range * CellSize, GetTop() - range * CellSize, GetRight() + range * CellSize, GetBottom() + range * CellSize,
                        player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {//if player is within the range of the attack, the player takes damage
                player.TakeDamage(2);
            }
            playScene.pm.Smoke(x + imageWidth / 2, y + imageHeight, 300, 100, 50); //particle effect

            for (int i = 0; i < 10; i++)
            {//randomly spawn (at most) 10 blocks somewhere on the map
                float blockLocX = x + imageWidth / 2 + MyRandom.PlusMinus(10*CellSize);
                float blockLocY = y + imageHeight + MyRandom.PlusMinus(10*CellSize);
                if (playScene.map.GetTerrain(blockLocX, blockLocY) == -2 || playScene.map.GetTerrain(blockLocX, blockLocY) == 5) continue;
                //int blockID = MyRandom.Range(0, 4);
                //playScene.map.CreateBlock(blockLocX, blockLocY, blockID);
                playScene.gameObjects.Add(new Boss3SmashAttack(playScene, blockLocX, blockLocY));
            }

            Sound.PlaySE(Sound.smashAttack); //smash sound effect
        }

        bool AttackRange()
        {//checks to see if the player is within "attack" range - true/false
            Player player = playScene.player;
            int range = 1;
            if (MyMath.RectRectIntersection(
                        GetLeft() - range * CellSize, GetTop() - range * CellSize, GetRight() + range * CellSize, GetBottom() + range * CellSize,
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
        {//animations are handled here
            timer++;
            if (timer % 10 == 0)
            {
                animationCounter++;
            }
            if (animationCounter >= 4) animationCounter = 0;

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
            if (exploded) return; //the moment the boss should be gone but isn't "dead" yet - still need to play fireworks before removing this class/object

            if (mutekiTimer % 2 == 0)
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
                    Camera.DrawGraph(x, y, Image.boss3[8 + animationCounter]);
                }
                else if (state == State.Right)
                {
                    Camera.DrawGraph(x, y, Image.boss3[12 + animationCounter]);
                }
                else if (state == State.Up)
                {
                    Camera.DrawGraph(x, y, Image.boss3[4 + animationCounter]);
                }
                else
                {
                    Camera.DrawGraph(x, y, Image.boss3[0 + animationCounter]);
                }
            }
            //Camera.DrawBox(GetLeft(), GetTop(), GetRight(), GetBottom(), DX.GetColor(0, 250, 250), 0);
            DrawHPBar();
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                playScene.player.TakeDamage(1);
            }
        }

        public override void Kill()
        {//once the boss dies, so does the shoulders
            base.Kill();
            lShoulder.Kill();
            rShoulder.Kill();
            Game.ChangeScene(new GameClearScene());
        }

        public override void TakeDamage(int damage)
        {
            if (mutekiTimer <= 0)
            {
                hp -= damage;
                Sound.PlaySE(Sound.takeDamage);
                if (hp <= 0) isDying = true;
                mutekiTimer = MutekiJikan;
            }
        }
    }
}
