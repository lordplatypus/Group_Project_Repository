using DxLibDLL;

namespace Group_Project_2
{
    public static class Image
    {
        //プレイヤー
        public static int[] player = new int[24];
        public static int[] playertsuruhasi = new int[12];

        //プレイヤー弾
        public static int playerbaretto;

        //エネミー1
        public static int[] moveEnemy = new int[24];

        //エネミー2
        public static int[] teki2 = new int[12];

        //エネミー3
        public static int teki3;

        //エネミー4
        public static int[] teki4 = new int[24];

        // エネミー５
        public static int[] enemy5 = new int[4];

        //ボス1
        public static int[] Slime = new int[10];

        //ボス2
        public static int[] boss = new int[4];

        //ボス３
        public static int[] boss3 = new int[20];
        public static int rightShoulder;
        public static int leftShoulder;

        //アイテム
        public static int item;
        public static int playitem;

        //パーティクル
        public static int particleDot1;
        public static int particleDot2;
        public static int particleDot3;
        public static int particleRing1;
        public static int particleRing2;
        public static int particleRing3;
        public static int particleRing4;
        public static int particleFire;
        public static int particleSteam;
        public static int particleSmoke;
        public static int particleGlitter1;
        public static int particleStar1;
        public static int particleStar2;
        public static int particleLine1;
        public static int particleLine2;
        public static int particleSlash;
        public static int particleStone1;
        public static int particleSquare;


        //ブロック
        public static int[] block = new int[10];
        public static int slimeBullet;
        public static int soilBullet;
        public static int stoneBullet;
        public static int ironBullet;
        public static int diamondBullet;

        //プレイヤーシーン
        public static int playbackground1;
        public static int playbackground2;
        //ピッケル必要経験値
        public static int playerexperienc;
        //HP表示
        public static int hp;
        //経験値表示
        public static int lv;
        //タイトルシーン
        public static int titlebg;
        public static int title;
        public static int pushanybutton;
        public static int team;

        //ゲームオーバーシーン
        public static int gameovergroundback;
        public static int gameover;
        public static int gameoverreturntitle;
        //ゲームクリアシーン
        public static int gamecleargroundback;
        public static int gameclear;
        public static int gameclearreturntitle;
        //階段
        public static int Stairs;


        public static void Load()
        {

            //ブロック
            DX.LoadDivGraph("Image/Map/Block.png", block.Length, 6, 1, 64, 64, block);
            slimeBullet = DX.LoadGraph("Image/Objects/SlimeBullet.png");
            soilBullet = DX.LoadGraph("Image/Objects/SoilBullet.png");
            stoneBullet = DX.LoadGraph("Image/Objects/StoneBullet.png");
            ironBullet = DX.LoadGraph("Image/Objects/IronBullet.png");
            diamondBullet = DX.LoadGraph("Image/Objects/DiamondBullet.png");

            //アイテム
            playitem = DX.LoadGraph("Image/Item/MissileI.png");
            item = DX.LoadGraph("Image/Item/MissileIte.png");

            //プレイヤー
            DX.LoadDivGraph("Image/player/player.png", player.Length, 3, 8, 48, 48, player);
            DX.LoadDivGraph("Image/player/tsuruhashi.png", playertsuruhasi.Length, 3, 4, 64, 64, playertsuruhasi);

            //プレイヤー弾
            playerbaretto = DX.LoadGraph("Image/Player/playerbaretto.png");

            //敵１
            DX.LoadDivGraph("Image/Enemys/move_enemy.png", moveEnemy.Length, 3, 8, 48, 48, moveEnemy);

            //敵２
            DX.LoadDivGraph("Image/Enemys/teki2.png", teki2.Length, 3, 4, 48, 48, teki2);

            //敵３
            teki3 = DX.LoadGraph("Image/Enemys/block_enemy.png");

            //敵４
            DX.LoadDivGraph("Image/Enemys/teki4.png", teki4.Length, 4, 4, 48, 48, teki4);

            // 敵５
            DX.LoadDivGraph("Image/Enemys/enemy5.png", enemy5.Length, 2, 2, 48, 48, enemy5);

            //ボス1
            DX.LoadDivGraph("Image/Enemys/Slime.png", Slime.Length, 2, 5, 128, 128, Slime);

            //ボス2
            DX.LoadDivGraph("Image/Enemys/boss1.png", boss.Length, 2, 2, 128, 128, boss);

            //ボス3
            DX.LoadDivGraph("Image/Enemys/StoneBossv2.png", boss3.Length, 4, 5, 256, 256, boss3);
            rightShoulder = DX.LoadGraph("Image/Enemys/StoneBossRightShoulder.png");
            leftShoulder = DX.LoadGraph("Image/Enemys/StoneBossLeftShoulder.png");

            //プレイヤーシーン
            playbackground1 = DX.LoadGraph("Image/Scene/PlayScene/playbackground1.png");
            playbackground2 = DX.LoadGraph("Image/Scene/PlayScene/playbackground2.png");
            playerexperienc = DX.LoadGraph("Image/Scene/PlayScene/PickaxeExperiencPoint.png");
            hp = DX.LoadGraph("Image/Scene/PlayScene/playhp.png");
            lv = DX.LoadGraph("Image/Scene/PlayScene/playlv.png");

            //パーティクル
            particleDot1 = DX.LoadGraph("Image/Particle/particle_dot_1.png");
            particleDot2 = DX.LoadGraph("Image/Particle/particle_dot_2.png");
            particleDot3 = DX.LoadGraph("Image/Particle/particle_dot_3.png");
            particleRing1 = DX.LoadGraph("Image/Particle/particle_ring_1.png");
            particleRing2 = DX.LoadGraph("Image/Particle/particle_ring_2.png");
            particleRing3 = DX.LoadGraph("Image/Particle/particle_ring_3.png");
            particleRing4 = DX.LoadGraph("Image/Particle/particle_ring_4.png");
            particleFire = DX.LoadGraph("Image/Particle/particle_fire.png");
            particleSteam = DX.LoadGraph("Image/Particle/particle_steam.png");
            particleSmoke = DX.LoadGraph("Image/Particle/particle_smoke.png");
            particleGlitter1 = DX.LoadGraph("Image/Particle/particle_glitter_1.png");
            particleStar1 = DX.LoadGraph("Image/Particle/particle_star_1.png");
            particleStar2 = DX.LoadGraph("Image/Particle/particle_star_2.png");
            particleLine1 = DX.LoadGraph("Image/Particle/particle_line_1.png");
            particleLine2 = DX.LoadGraph("Image/Particle/particle_line_2.png");
            particleSlash = DX.LoadGraph("Image/Particle/particle_slash.png");
            particleStone1 = DX.LoadGraph("Image/Particle/particle_stone_1.png");
            particleSquare = DX.LoadGraph("Image/Particle/square.png");

            //タイトルシーン
            titlebg = DX.LoadGraph("Image/Scene/Title/title_bg.png");
            title = DX.LoadGraph("Image/Scene/Title/title.png");
            pushanybutton = DX.LoadGraph("Image/Scene/Title/push_any_button.png");
            team = DX.LoadGraph("Image/Scene/Title/team.png");

            //ゲームオーバーシーン
            gameovergroundback = DX.LoadGraph("Image/Scene/GameOver/gameover_groundback.png");
            gameover = DX.LoadGraph("Image/Scene/GameOver/gameover.png");
            gameoverreturntitle = DX.LoadGraph("Image/Scene/GameOver/gameoverreturntitle.png");

            //ゲームクリアシーン
            gamecleargroundback = DX.LoadGraph("Image/Scene/GameClear/gameclear_groundback.png");
            gameclear = DX.LoadGraph("Image/Scene/GameClear/gameclear.png");
            gameclearreturntitle = DX.LoadGraph("Image/Scene/GameClear/gameclearreturntitle.png");

            //階段
            Stairs = DX.LoadGraph("Image/Objects/Stairs.png");
        }
    }
}
