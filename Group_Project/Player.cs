using DxLibDLL;
using MyLib;
using System;

namespace Group_Project_2
{
    public class Player : GameObject
    {
        public int life = 10;
        const int MutekiJikan = 120;
        int mutekiTimer = 0;
        float WalkSpeed = 3f;
        bool Animesion = false;
        bool AnimesionTsuruhashi = false;
        int counter = 0;
        int Animeisoncounter = 0;

        enum State
        {
            DOWN,
            UP,
            LEFT,
            RIGHT,
            DOWNLEFT,
            DOWNRIGHT,
            UPLEFT,
            UPRIGTH,
        }

        State state = State.DOWN;

        float vx = 0;
        float vy = 0;

        public Player(PlayScene playScene, float x, float y) : base(playScene)
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
            HandleInput();
            MoveX();
            MoveY();
            ThrowPutDig();

            mutekiTimer--;

            if (Animesion)
            {
                counter++;

                if (counter % 10 == 0)
                {
                    Animeisoncounter++;
                }
                if (Animeisoncounter == 2)
                {
                    Animeisoncounter = 0;
                    counter = 0;
                }
            }
            if (AnimesionTsuruhashi)
            {
                counter++;

                if (counter % 10 == 0)
                {
                    Animeisoncounter++;
                }
                if (Animeisoncounter == 3)
                {
                    Animeisoncounter = 0;
                    AnimesionTsuruhashi = false;
                    counter = 0;
                }
            }
        }

        void HandleInput()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_7))
            {//ブライスの変更　－　ブロックを選択
                playScene.bm.CurrentSelectedBlock();
            }

            if (!AnimesionTsuruhashi)
            {
                if (Input.GetButton(DX.PAD_INPUT_UP))
                {
                    vy = -WalkSpeed;
                    state = State.UP;
                    if (Input.GetButton(DX.PAD_INPUT_LEFT))
                    {
                        vy = -WalkSpeed;
                        vx = -WalkSpeed;
                        state = State.UPLEFT;
                    }
                    else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
                    {
                        vy = -WalkSpeed;
                        vx = WalkSpeed;
                        state = State.UPRIGTH;
                    }
                    else
                    {
                        vx = 0;
                    }
                    Animesion = true;
                }
                else if (Input.GetButton(DX.PAD_INPUT_DOWN))
                {
                    vy = WalkSpeed;
                    state = State.DOWN;
                    if (Input.GetButton(DX.PAD_INPUT_LEFT))
                    {
                        vy = WalkSpeed;
                        vx = -WalkSpeed;
                        state = State.DOWNLEFT;
                    }
                    else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
                    {
                        vy = WalkSpeed;
                        vx = WalkSpeed;
                        state = State.DOWNRIGHT;
                    }
                    else
                    {
                        vx = 0;
                    }
                    Animesion = true;
                }
                else if (Input.GetButton(DX.PAD_INPUT_LEFT))
                {
                    vx = -WalkSpeed;
                    state = State.LEFT;
                    Animesion = true;
                    if (!Input.GetButton(DX.PAD_INPUT_UP) && !Input.GetButton(DX.PAD_INPUT_DOWN))
                    {
                        vy = 0;
                    }
                }
                else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
                {
                    vx = WalkSpeed;
                    state = State.RIGHT;
                    Animesion = true;
                    if (!Input.GetButton(DX.PAD_INPUT_UP) && !Input.GetButton(DX.PAD_INPUT_DOWN))
                    {
                        vy = 0;
                    }
                }
                else
                {
                    Animesion = false;
                    vy = 0;
                    vx = 0;
                }
            }
        }

        void ThrowPutDig()
        {
            if (!AnimesionTsuruhashi)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_2))
                {//ブライスの変更　－　どうやってブロックを投げることをちょっと変更しました
                    angle = 0;

                    //if (state == State.UP) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 270 * MyMath.Deg2Rad));
                    //if (state == State.DOWN) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 90 * MyMath.Deg2Rad));
                    //if (state == State.LEFT) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 180 * MyMath.Deg2Rad));
                    //if (state == State.RIGHT) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 0 * MyMath.Deg2Rad));
                    //if (state == State.UPLEFT) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 225 * MyMath.Deg2Rad));
                    //if (state == State.UPRIGTH) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 315 * MyMath.Deg2Rad));
                    //if (state == State.DOWNLEFT) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 135 * MyMath.Deg2Rad));
                    //if (state == State.DOWNRIGHT) playScene.gameObjects.Add(new PlayerShot(playScene, x, y, 45 * MyMath.Deg2Rad));
                    if (state == State.UP) angle = 270;
                    if (state == State.DOWN) angle = 90;
                    if (state == State.LEFT) angle = 180;
                    if (state == State.RIGHT) angle = 0;
                    if (state == State.UPLEFT) angle = 225;
                    if (state == State.UPRIGTH) angle = 315;
                    if (state == State.DOWNLEFT) angle = 135;
                    if (state == State.DOWNRIGHT) angle = 45;

                    angle *= MyMath.Deg2Rad;
                    playScene.bm.ThrowBlock(x, y, angle);
                }
                
                if (Input.GetButtonDown(DX.PAD_INPUT_5))
                {//ブライスの変更　－　どうやってブロックを出すことをちょっと変更しました
                    float lookX = 0;
                    float lookY = 0;
                    if (state == State.UP)
                    {
                        //playScene.map.CreateBlockPlayer(x + 32, y - 64, 0);
                        lookX = x + 32;
                        lookY = y - 64;
                    }
                    if (state == State.DOWN)
                    {
                        //playScene.map.CreateBlockPlayer(x + 32, y + 112, 0);
                        lookX = x + 32;
                        lookY = y + 112;
                    }
                    if (state == State.LEFT) 
                    {
                        //playScene.map.CreateBlockPlayer(x - 64, y + 32, 0);
                        lookX = x - 64;
                        lookY = y + 32;
                    }
                    if (state == State.RIGHT) 
                    {
                        //playScene.map.CreateBlockPlayer(x + 112, y + 32, 0);
                        lookX = x + 112;
                        lookY = y + 32;
                    }

                    playScene.bm.PlaceBlock(lookX, lookY);
                }
            }

            if (state != State.UPLEFT && state != State.UPRIGTH && state != State.DOWNLEFT && state != State.DOWNRIGHT)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_6))
                {//ブライスの変更　－　どうやってブロックを掘ることをちょっと変更しました
                    AnimesionTsuruhashi = true;
                    Animesion = false;
                    Animeisoncounter = 0;
                    counter = 0;
                    vy = 0;
                    vx = 0;
                    float lookX = 0;
                    float lookY = 0;

                    if (state == State.UP)
                    {
                        //playScene.gameObjects.Add(new Empty(playScene, x + 24, y - 32));
                        //playScene.map.DeleteWallPlayer(x + 24, y - 32);
                        lookX = x + 24;
                        lookY = y - 32;
                    }
                    if (state == State.DOWN)
                    {
                        //playScene.gameObjects.Add(new Empty(playScene, x + 24, y + 80));
                        //playScene.map.DeleteWallPlayer(x + 24, y + 80);
                        lookX = x + 24;
                        lookY = y + 80;
                    }
                    if (state == State.LEFT)
                    {
                        //playScene.gameObjects.Add(new Empty(playScene, x - 32, y + 24));
                        //playScene.map.DeleteWallPlayer(x - 32, y + 24);
                        lookX = x - 32;
                        lookY = y + 24;
                    }
                    if (state == State.RIGHT)
                    {
                        //playScene.gameObjects.Add(new Empty(playScene, x + 80, y + 24));
                        //playScene.map.DeleteWallPlayer(x + 80, y + 24);
                        lookX = x + 80;
                        lookY = y + 24;
                    }

                    playScene.bm.StoreBlock(lookX, lookY);
                    playScene.gameObjects.Add(new Empty(playScene, lookX, lookY));
                }
            }
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
                float wallRight = left - left % Map.CellSize + Map.CellSize;
                SetLeft(wallRight);
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {
                float wallLeft = right - right % Map.CellSize;
                SetRight(wallLeft);
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
            {
                float wallUp = top - top % Map.CellSize + Map.CellSize;
                SetTop(wallUp);
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {
                float wallDown = bottom - bottom % Map.CellSize;
                SetBottom(wallDown);
            }
        }

        public override void Draw()
        {
            if (!AnimesionTsuruhashi)
            {
                if (state == State.UP) Camera.DrawGraph(x, y, Image.player[12 + Animeisoncounter]);
                if (state == State.DOWN) Camera.DrawGraph(x, y, Image.player[0 + Animeisoncounter]);
                if (state == State.LEFT) Camera.DrawGraph(x, y, Image.player[6 + Animeisoncounter]);
                if (state == State.RIGHT) Camera.DrawGraph(x, y, Image.player[18 + Animeisoncounter]);
                if (state == State.UPLEFT) Camera.DrawGraph(x, y, Image.player[9 + Animeisoncounter]);
                if (state == State.UPRIGTH) Camera.DrawGraph(x, y, Image.player[15 + Animeisoncounter]);
                if (state == State.DOWNLEFT) Camera.DrawGraph(x, y, Image.player[3 + Animeisoncounter]);
                if (state == State.DOWNRIGHT) Camera.DrawGraph(x, y, Image.player[21 + Animeisoncounter]);
            }
            else
            {
                if (state == State.UP) Camera.DrawGraph(x, y, Image.playertsuruhasi[9 + Animeisoncounter]);
                if (state == State.DOWN) Camera.DrawGraph(x, y, Image.playertsuruhasi[6 + Animeisoncounter]);
                if (state == State.LEFT) Camera.DrawGraph(x, y, Image.playertsuruhasi[0 + Animeisoncounter]);
                if (state == State.RIGHT) Camera.DrawGraph(x, y, Image.playertsuruhasi[3 + Animeisoncounter]);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy1 || other is Enemy2 || other is Enemy3 || other is Enemy4)
            {
                if (mutekiTimer <= 0)
                {
                    TakeDamage();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (mutekiTimer <= 0)
            {
                life -= damage;
            }
            if (life <= 0)
            {
                Kill();
            }
        }

        void TakeDamage()
        {
            life -= 1; // ライフ減少

            if (life <= 0)
            {
                // ライフが無くなったら死亡
                Kill();
            }
            else
            {
                // 無敵時間発動
                mutekiTimer = MutekiJikan;
            }
        }
    }
}
