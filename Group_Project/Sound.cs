using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Group_Project_2
{
    public static class Sound
    {
        //BGM
        public static int lastPlayedBGM = 0;
        public static int gameClearBGM;
        public static int gameOverBGM;
        public static int playBGM;
        public static int titleBGM;

        //ボース3
        public static int basicExplosion;
        public static int finalExplosion;
        public static int implosions;
        public static int smashAttack;
        public static int missileLaunch;
        public static int takeDamage;

        public static void Load()
        {
            //BGM
            gameClearBGM = DX.LoadSoundMem("Sound/BGM/game_clear.wav");
            gameOverBGM = DX.LoadSoundMem("Sound/BGM/game_over.wav");
            playBGM = DX.LoadSoundMem("Sound/BGM/play_bgm.mp3");
            titleBGM = DX.LoadSoundMem("Sound/BGM/title_bgm.wav");

            //Boss3
            basicExplosion = DX.LoadSoundMem("Sound/SE/Boss3/Boss3BasicExplosion.wav");
            finalExplosion = DX.LoadSoundMem("Sound/SE/Boss3/Boss3FinalExplosion.wav");
            implosions = DX.LoadSoundMem("Sound/SE/Boss3/Boss3Implode.wav");
            smashAttack = DX.LoadSoundMem("Sound/SE/Boss3/Boss3SmashAttack.wav");
            missileLaunch = DX.LoadSoundMem("Sound/SE/Boss3/Boss3MissileLaunch.wav");
            takeDamage = DX.LoadSoundMem("Sound/SE/Boss3/Boss3TakeDamage.mp3");
        }

        public static void PlaySE(int handle)
        {
            DX.PlaySoundMem(handle, DX.DX_PLAYTYPE_BACK);
        }

        public static void PlayBGM(int handle)
        {
            DX.StopSoundMem(lastPlayedBGM);
            DX.PlaySoundMem(handle, DX.DX_PLAYTYPE_LOOP);
            lastPlayedBGM = handle;
        }
    }
}
