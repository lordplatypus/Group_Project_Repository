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
        public static int blockenemy;

        //エネミー3
        public static int[] teki3 = new int[4];

        //エネミー4
        public static int[] teki4 = new int[24];

        //ボース
        public static int[] boss = new int[4];
        public static int[] boss3 = new int[20];
        public static int rightShoulder;
        public static int leftShoulder;

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


        public static void Load()
        {

            //ブロック
            DX.LoadDivGraph("Image/Map/Block.png", block.Length, 6, 1, 64, 64, block);
            slimeBullet = DX.LoadGraph("Image/Objects/SlimeBullet.png");
            soilBullet = DX.LoadGraph("Image/Objects/SoilBullet.png");
            stoneBullet = DX.LoadGraph("Image/Objects/StoneBullet.png");
            ironBullet = DX.LoadGraph("Image/Objects/IronBullet.png");
            diamondBullet = DX.LoadGraph("Image/Objects/DiamondBullet.png");

            //プレイヤー
            DX.LoadDivGraph("Image/player/player.png", player.Length, 3, 8, 48, 48, player);
            DX.LoadDivGraph("Image/player/tsuruhashi.png", playertsuruhasi.Length, 3, 4, 64, 64, playertsuruhasi);

            //プレイヤー弾
            playerbaretto = DX.LoadGraph("Image/Player/playerbaretto.png");

            //敵１
            DX.LoadDivGraph("Image/Enemys/move_enemy.png", moveEnemy.Length, 3, 8, 48, 48, moveEnemy);

            //敵２
            DX.LoadDivGraph("Image/Enemys/teki3.png", teki3.Length, 4, 1, 48, 48, teki3);

            //敵３
            blockenemy = DX.LoadGraph("Image/Enemys/block_enemy.png");

            //敵４
            DX.LoadDivGraph("Image/Enemys/teki4.png", teki4.Length, 4, 4, 48, 48, teki4);

            //ボス
            DX.LoadDivGraph("Image/Enemys/boss1.png", boss.Length, 2, 2, 128, 128, boss);
            //DX.LoadDivGraph("Image/Enemys/StoneBoss.png", boss3.Length, 3, 5, 256, 256, boss3);
            DX.LoadDivGraph("Image/Enemys/StoneBossv2.png", boss3.Length, 4, 5, 256, 256, boss3);
            rightShoulder = DX.LoadGraph("Image/Enemys/StoneBossRightShoulder.png");
            leftShoulder = DX.LoadGraph("Image/Enemys/StoneBossLeftShoulder.png");

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
        }
    }
}
