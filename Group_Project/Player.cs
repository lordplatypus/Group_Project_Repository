using DxLibDLL;
using MyLib;
using System;

namespace Group_Project_2
{
    public class Player : GameObject
    {
        public int life  = 10;
        const int MutekiJikan = 120;
        int mutekiTimer = 0;

        float WalkSpeed = 3f; //プレイヤーSPEED
        int   resultAnimation = 0;　//アニメーション

        int MouseX;　//マウス座標
        int MouseY;　//マウス座標
        int MouseInput;　//マウスクリック取得
        float MouseAngle; //マウス角度
        bool IsMouseClick = false; //マウスがclickしたか
        bool IsMouseRightClick = false; 
        float MouseCount = 0.0f; //マウスを連続で押さないようにする処理
        int xcount = 0 , ycount = 0; //横方向と縦方向のcount数
        int xycount = 0, yxcount = 0;
        int ix = 0, iy = 0;
        int ixy = 0, iyx = 0;

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
            MouseInput = DX.GetMouseInput();
            DX.GetMousePoint(out MouseX, out MouseY);
            angle = MyMath.PointToPointAngle(Screen.Width / 2, Screen.Height / 2, MouseX, MouseY);

            HandleInput();
            MoveX();
            MoveY();
            MouseShot();
            KeyboardAnimesion();
            MouseAnimesion();

            if (IsMouseClick)
            {
                MouseCount++;
                if (MouseCount > 20)
                {
                    IsMouseClick = false;
                    MouseCount = 0;
                }
            }

            if(IsMouseRightClick)
            {
                MouseCount++;
                if (MouseCount > 20)
                {
                    IsMouseRightClick = false;
                    MouseCount = 0;
                }
            }
            mutekiTimer--;
        }

        void HandleInput()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_7))
            {
                playScene.bm.CurrentSelectedBlock();
            }


            if (Input.GetButton(DX.PAD_INPUT_6))
            {
                vx = WalkSpeed;
                if (xcount < 0)
                    xcount = 0;
                    ++xcount;
                state = State.RIGHT;
            }
            else if (Input.GetButton(DX.PAD_INPUT_4))
            {
                vx = -WalkSpeed;
                if (xcount > 0)
                    xcount = 0;
                --xcount;
                state = State.LEFT;
            }
            else
            {
                vx = 0;
            }

            if (Input.GetButton(DX.PAD_INPUT_5))
            {
                vy = WalkSpeed;
                if (ycount > 0)
                    ycount = 0;
                --ycount;
                state = State.DOWN;
            }
            else if (Input.GetButton(DX.PAD_INPUT_8))
            {
                vy = -WalkSpeed;
                if (ycount < 0)
                    ycount = 0;
                ++ycount;
                state = State.UP;
            }
            else
            {
                vy = 0;
            }


            if (Input.GetButtonDown(DX.PAD_INPUT_10))
            {
                float lookX = 0;
                float lookY = 0;

                if (state == State.RIGHT)
                {
                    lookX = x + 80;
                    lookY = y + 24;
                }
                if (state == State.LEFT)
                {
                    lookX = x - 32;
                    lookY = y + 24;
                }
                if (state == State.UP)
                {
                    lookX = x + 24;
                    lookY = y - 32;
                }
                if (state == State.DOWN)
                {
                    lookX = x + 24;
                    lookY = y + 80;
                }

                playScene.bm.StoreBlock(lookX, lookY);
                playScene.gameObjects.Add(new Empty(playScene, lookX, lookY));
            }

            if ((MouseInput & DX.MOUSE_INPUT_RIGHT) != 0 && !IsMouseRightClick)
            {
                IsMouseRightClick = true;
                float lookX = 0;
                float lookY = 0;

                if (state == State.RIGHT)
                {
                    lookX = x + 112;
                    lookY = y + 32;
                }
                if (state == State.LEFT)
                {
                    lookX = x - 64;
                    lookY = y + 32;
                }
                if (state == State.UP)
                {
                    lookX = x + 32;
                    lookY = y - 64;
                }
                if (state == State.DOWN)
                {
                    lookX = x + 32;
                    lookY = y + 112;
                }

                playScene.bm.PlaceBlock(lookX, lookY);                   
            }           

            if (Input.GetButton(DX.PAD_INPUT_6) && (Input.GetButton(DX.PAD_INPUT_5)))
            {
                if (xycount < 0)
                    xycount = 0;
                ++xycount;
                state = State.DOWNRIGHT;
            }
            if (Input.GetButton(DX.PAD_INPUT_6) && (Input.GetButton(DX.PAD_INPUT_8)))
            {
                if (xycount > 0)
                    xycount = 0;
                --xycount;
                state = State.UPRIGTH;
            }
            if (Input.GetButton(DX.PAD_INPUT_4) && (Input.GetButton(DX.PAD_INPUT_5)))
            {
                if (yxcount < 0)
                    yxcount = 0;
                ++yxcount;
                state = State.DOWNLEFT;
            }
            if (Input.GetButton(DX.PAD_INPUT_4) && (Input.GetButton(DX.PAD_INPUT_8)))
            {
                if (yxcount > 0)
                    yxcount = 0;
                --yxcount;
                state = State.UPLEFT;
            }

            //count数から添字を求める
            ix = Math.Abs(xcount) % 30 / 10;
            iy = Math.Abs(ycount) % 30 / 10;
            ixy = Math.Abs(xycount) % 30 / 10;
            iyx = Math.Abs(yxcount) % 30 / 10;
        }

        void MouseShot()
        {
            if ((MouseInput & DX.MOUSE_INPUT_LEFT) != 0 && !IsMouseClick)
            {
                playScene.bm.ThrowBlock(x, y, angle);
                IsMouseClick = true;
            }
        }

        void KeyboardAnimesion()
        {
            if(state == State.RIGHT)
            {
                if (xcount >= 0)
                {
                    ix += 18;
                    resultAnimation = ix;
                }
            }
            if (state == State.LEFT)
            {
                if (xcount <= 1)
                {
                    ix += 6;
                    resultAnimation = ix;
                }
            }
            if (state == State.UP)
            {
                if (ycount >= 0)
                {
                    iy += 9;
                    resultAnimation = iy;
                }
            }
            if (state == State.DOWN)
            {
                if (ycount <= 1)
                {
                    resultAnimation = iy;
                }
            }
            if(state == State.DOWNRIGHT)
            {
                if (xycount >= 0)
                {
                    ixy += 21;
                    resultAnimation = ixy;
                }
            }
            if(state == State.UPRIGTH)
            {
                if(xycount <= 1)
                {
                    ixy += 15;
                    resultAnimation = ixy;
                }
            }
            if (state == State.DOWNLEFT)
            {
                if (yxcount >= 0)
                {
                    iy += 3;
                    resultAnimation = iy;
                }
            }
            if (state == State.UPLEFT)
            {
                if (yxcount <= 1)
                {
                    iy += 9;
                    resultAnimation = iy;
                }
            }
        }

        void MouseAnimesion()
        {
            MouseAngle = angle * (float)(180 / Math.PI);

            if (MouseAngle <= 0)
            {
                MouseAngle = MouseAngle + 360;
            }

            //角度でキャラのアニメーションを決める
            if (IsMouseClick)
            {
                if (MouseAngle >= 340 || MouseAngle <= 22.5)
                {
                    resultAnimation = 18;
                }
                else if (MouseAngle <= 67.5)
                {
                    resultAnimation = 21;
                }
                else if (MouseAngle <= 112.5)
                {
                    resultAnimation = 0;
                }
                else if (MouseAngle <= 157.5)
                {
                    resultAnimation = 3;
                }
                else if (MouseAngle <= 202.5)
                {
                    resultAnimation = 6;
                }
                else if (MouseAngle <= 247.5)
                {
                    resultAnimation = 9;
                }
                else if (MouseAngle <= 292.5)
                {
                    resultAnimation = 12;
                }
                else
                {
                    resultAnimation = 15;
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
            Camera.DrawGraph(x, y, Image.player[resultAnimation]);       
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

        public override void TakeDamage(int damage)
        {
            if (mutekiTimer <= 0)
            {
                life -= damage;
            }
            if(life <= 0)
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
