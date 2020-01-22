using DxLibDLL;
using MyLib;

namespace Group_Project_2
{
    public class Player : GameObject
    {
        //アイテムを取得しているか
        public bool isitem = false;
        //アイテム使用回数
        public int itemcount = 10;
        //ライフ
        public int life = 10;
        //無敵時間
        const int MutekiJikan = 120;
        //無敵タイマー
        int mutekiTimer = 0;
        //スピード
        float WalkSpeed = 3f;
        //歩くアニメーションをするかどうか
        bool Animesion = false;
        //つるはしのアニメーションをするかどうか
        bool AnimesionTsuruhashi = false;
        //アニメーション数を数えるカウント
        int counter = 0;
        //歩く番地アニメーションをするかどうか
        int Animeisoncounter = 0;
        //掘る番地アニメーションをするかどうか
        int AnimeisoncounterTsuruhashi = 0;
        //発射レート
        int shotlate = 30;
        //発射カウント
        int shotcount = 0;
        //ブロック壊すカウント
        int blockcount = 0;
        //掘れるかどうかの処理
        bool blockdig = false;

        //ステート
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

        //現在のステート
        State state = State.DOWN;
        //前のステート
        State frontstate = State.DOWN;
        //現在のスピード
        float vx = 0;
        //現在のスピード
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
            frontstate = state;
            if (playScene.pickaxelevel == 1 && playScene.pickaxeCount >= 300)
            {
                playScene.pm.Stars(x, y);
                playScene.pickaxelevel = 2;
                playScene.pickaxeCount = 0;
            }
            else if (playScene.pickaxelevel == 2 && playScene.pickaxelevel >= 600)
            {
                playScene.pm.Stars(x, y);
                playScene.pickaxelevel = 3;
                playScene.pickaxeCount = 0;
            }
            //掘るアニメーションをしていたら
            if (!AnimesionTsuruhashi)
            {//ステート管理(移動処理)
                HandleInput();
            }
            //X方向に歩く
            MoveX();
            //Y方向に歩く
            MoveY();
            //投げて、撃って、置く
            ThrowPutDig();
            //無敵タイマー
            mutekiTimer--;
            //ショットタイマー
            shotcount--;

            //歩くアニメーション
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
            //掘るアニメーション
            if (AnimesionTsuruhashi)
            {
                counter++;

                if (counter % 10 == 0)
                {
                    AnimeisoncounterTsuruhashi++;
                }
                if (AnimeisoncounterTsuruhashi == 3)
                {
                    AnimeisoncounterTsuruhashi = 0;
                    AnimesionTsuruhashi = false;
                    counter = 0;
                }
            }

            if (vx != 0 || vy != 0 || state != frontstate)
            {
                blockcount = 0;
                blockdig = false;
            }

            if (itemcount <= 0)
            {
                isitem = false;
            }
        }

        //ステート管理(移動処理)
        void HandleInput()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_7))
            {//ブライスの変更　－　ブロックを選択
                playScene.bm.CurrentSelectedBlock();
            }

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

        //投げて、撃って、置く
        void ThrowPutDig()
        {
            //投げる
            if (!AnimesionTsuruhashi && shotcount <= 0 && Input.GetButtonDown(DX.PAD_INPUT_2))
            {
                if (state == State.UP) angle = 270;
                if (state == State.DOWN) angle = 90;
                if (state == State.LEFT) angle = 180;
                if (state == State.RIGHT) angle = 0;
                if (state == State.UPLEFT) angle = 225;
                if (state == State.UPRIGTH) angle = 315;
                if (state == State.DOWNLEFT) angle = 135;
                if (state == State.DOWNRIGHT) angle = 45;
                angle *= MyMath.Deg2Rad;

                if (!isitem)
                {
                    //ブライスの変更　－　どうやってブロックを投げることをちょっと変更しました
                    shotcount = shotlate;
                    playScene.bm.ThrowBlock(x, y, angle);
                }
                else
                {
                    playScene.gameObjects.Add(new MissileBlock(playScene, x, y, angle));
                    itemcount--;
                }
            }
            //置く
            if (!AnimesionTsuruhashi && Input.GetButtonDown(DX.PAD_INPUT_5) && !isitem)
            {//ブライスの変更　－　どうやってブロックを出すことをちょっと変更しました
                float lookX = 0;
                float lookY = 0;
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
                if (state == State.LEFT)
                {
                    lookX = x - 64;
                    lookY = y + 32;
                }
                if (state == State.RIGHT)
                {
                    lookX = x + 112;
                    lookY = y + 32;
                }

                playScene.bm.PlaceBlock(lookX, lookY);
            }

            //掘る
            if (state != State.UPLEFT && state != State.UPRIGTH && state != State.DOWNLEFT && state != State.DOWNRIGHT)
            {
                if (!AnimesionTsuruhashi && AnimeisoncounterTsuruhashi == 0 && Input.GetButtonDown(DX.PAD_INPUT_6) && !isitem)
                {//ブライスの変更　－　どうやってブロックを掘ることをちょっと変更しました

                    //掘るアニメーションをオンにする
                    AnimesionTsuruhashi = true;
                    //歩くアニメーションをオフにする
                    Animesion = false;
                    //カウントを0にする
                    counter = 0;
                    //プレイヤーのスピードを0にする
                    vy = 0;
                    vx = 0;
                    float lookX = 0;
                    float lookY = 0;

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
                    if (state == State.LEFT)
                    {
                        lookX = x - 32;
                        lookY = y + 24;
                    }
                    if (state == State.RIGHT)
                    {
                        lookX = x + 80;
                        lookY = y + 24;
                    }

                    //敵に当たる攻撃
                    playScene.gameObjects.Add(new Empty(playScene, lookX, lookY));

                    //掘る場所
                    LookDig(lookX, lookY);
                }
            }
        }

        //X方向に歩く
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
                vx = 0;
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {
                float wallLeft = right - right % Map.CellSize;
                SetRight(wallLeft);
                vx = 0;
            }
        }

        //Y方向に歩く
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
                vy = 0;
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {
                float wallDown = bottom - bottom % Map.CellSize;
                SetBottom(wallDown);
                vy = 0;
            }
        }

        //描画
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
                if (state == State.UP) Camera.DrawGraph(x, y, Image.playertsuruhasi[9 + AnimeisoncounterTsuruhashi]);
                if (state == State.DOWN) Camera.DrawGraph(x, y, Image.playertsuruhasi[6 + AnimeisoncounterTsuruhashi]);
                if (state == State.LEFT) Camera.DrawGraph(x, y, Image.playertsuruhasi[0 + AnimeisoncounterTsuruhashi]);
                if (state == State.RIGHT) Camera.DrawGraph(x, y, Image.playertsuruhasi[3 + AnimeisoncounterTsuruhashi]);
            }
        }

        //当たり判定
        public override void OnCollision(GameObject other)
        {
            if (other is Enemy1 || other is Enemy2 || other is Enemy3 || other is Enemy4)
            {
                if (mutekiTimer <= 0)
                {
                    TakeDamage();
                }
            }
            if (other is MissileItem)
            {
                isitem = true;
            }
        }

        //1以上の時ダメージ
        public override void TakeDamage(int damage)
        {
            if (mutekiTimer <= 0)
            {
                life -= damage;
                mutekiTimer = MutekiJikan;
            }
            if (life <= 0)
            {
                Kill();
            }
        }

        //ダメージ
        void TakeDamage()
        {
            life -= 1; // ライフ減少

            if (life <= 0)
            {
                // ライフが無くなったら死亡
                Kill();
                Game.ChangeScene(new GameOverScene());
            }
            else
            {
                // 無敵時間発動
                mutekiTimer = MutekiJikan;
            }
        }

        //何を掘るか調べる
        void LookDig(float x, float y)
        {
            blockcount++;
            //現在のブロック番号
            int blocknumber = 0;
            blocknumber = playScene.map.GetTerrain(x, y);

            if (playScene.pickaxelevel == 1)
            {
                if (blocknumber == 0 && blockcount == 2)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 2;
                    blockdig = true;
                }
                else if (blocknumber == 1 && blockcount == 3)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 3;
                    blockdig = true;
                }
                else if (blocknumber == 2 && blockcount == 5)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 5;
                    blockdig = true;
                }
                else if (blocknumber == 3 && blockcount == 8)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 7;
                    blockdig = true;
                }
            }
            else if (playScene.pickaxelevel == 2)
            {
                if (blocknumber == 0)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 2;
                    blockdig = true;
                }
                else if (blocknumber == 1)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 3;
                    blockdig = true;
                }
                else if (blocknumber == 2 && blockcount == 3)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 5;
                    blockdig = true;
                }
                else if (blocknumber == 3 && blockcount == 5)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 7;
                    blockdig = true;
                }
                else if (blocknumber == 4 && blockcount == 20)
                {
                    playScene.pickaxeCount = playScene.pickaxeCount + 30;
                    blockdig = true;
                }
            }
            else
            {
                blockdig = true;
            }

            if (blockdig)
            {
                playScene.bm.StoreBlock(x, y);
            }

        }
    }
}