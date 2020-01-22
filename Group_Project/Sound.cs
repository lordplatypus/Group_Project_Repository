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
        //ボース3
        public static int basicExplosion;
        public static int finalExplosion;
        public static int implosions;
        public static int smashAttack;
        public static int missileLaunch;
        public static int takeDamage;

        public static void Load()
        {
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

        public static void PlayBGM(string handle)
        {
            string music = "/Sound/BGM/" + handle;
            DX.StopMusic();
            DX.PlayMusic(music, DX.DX_PLAYTYPE_LOOP);
        }
    }
}
