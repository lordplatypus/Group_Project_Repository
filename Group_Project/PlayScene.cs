using System;
using System.Collections.Generic;
using DxLibDLL;

namespace Group_Project_2
{
    public class PlayScene : Scene
    {
        public Map map;
        public Player player;
        public List<GameObject> gameObjects = new List<GameObject>();
        public ParticleManager pm;
        public FloorManager fm;
        public BlockManager bm;
        //ピッケルレベル
        public int pickaxelevel = 1;
        //ピッケルカウント
        public int pickaxeCount = 0;

        public PlayScene()
        {
            fm = new FloorManager(this);
            map = new Map(this);
            Camera.LookAt(player.x, player.y);
            pm = new ParticleManager();
            bm = new BlockManager(this);
            Sound.PlayBGM(Sound.playBGM);
        }

        public override void Update()
        {
            int gameObjectsCount = gameObjects.Count; // ループ前の個数を取得しておく 
            for (int i = 0; i < gameObjectsCount; i++)
            {
                gameObjects[i].Update();
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject a = gameObjects[i];

                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    if (a.isDead) break;

                    GameObject b = gameObjects[j];

                    if (b.isDead) continue;

                    if (MyMath.RectRectIntersection(
                        a.GetLeft(), a.GetTop(), a.GetRight(), a.GetBottom(),
                        b.GetLeft(), b.GetTop(), b.GetRight(), b.GetBottom()))
                    {
                        a.OnCollision(b);
                        b.OnCollision(a);
                    }
                }
            }

            gameObjects.RemoveAll(go => go.isDead);

            Camera.LookAt(player.x, player.y);

            pm.Update();
        }

        public override void Draw()
        {
            if (fm.floor == 1)
            {
                Camera.DrawGraph(player.x - Screen.Width / 2, player.y - Screen.Height / 2, Image.playbackground1);
            }
            else if (fm.floor == 2)
            {
                Camera.DrawGraph(player.x - Screen.Width / 2, player.y - Screen.Height / 2, Image.playbackground2);
            }
            else
            {
            }

            map.DrawMap();
            pm.Draw();
            foreach (GameObject go in gameObjects)
            {
                go.Draw();
            }

            //UI
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 100);            
            DX.DrawBox(Screen.Width - 448, 0, Screen.Width, 192, DX.GetColor(0, 0, 0), DX.TRUE);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 256);

            if (player.isitem)
            {
                //ライフ画像
                DX.DrawGraph(Screen.Width - 224, 112, Image.playitem);
                DX.DrawStringToHandle(Screen.Width - 224, 144, player.itemcount.ToString(), DX.GetColor(255, 255, 255), Image.smallFont);
            }
            //Draw Blocks
            bm.Draw();
            

            //枠
            //DX.DrawGraph(948, 8, Image.playerexperienc);
            //ライフ
            DX.DrawBox(Screen.Width - 432, 16, Screen.Width - 32, 48, DX.GetColor(255, 0, 0), DX.TRUE);
            DX.DrawBox(Screen.Width - 432, 16, Screen.Width - 432 + ((400 / 10) * player.life), 48, DX.GetColor(0, 255, 0), DX.TRUE);
            DX.DrawStringToHandle(Screen.Width - 432, 16, "HP: " + player.life, DX.GetColor(0, 0, 0), Image.bigFont);
            //ライフ画像
            //DX.DrawGraph(915, 10, Image.hp);

            //枠
            //DX.DrawGraph(948, 48, Image.playerexperienc);
            //level画像
            //DX.DrawGraph(895, 50, Image.lv);
            //ピッケル経験値
            if (pickaxelevel == 1)
            {
                DX.DrawBox(Screen.Width - 432, 64, Screen.Width - 32, 96, DX.GetColor(0, 0, 0), DX.FALSE);
                DX.DrawBox(Screen.Width - 432, 64, Screen.Width - 432 + ((1200 / 300) * pickaxeCount / 3), 96, DX.GetColor(255, 255, 0), DX.TRUE);
                DX.DrawStringToHandle(Screen.Width - 432, 64, "LV: 1", DX.GetColor(0, 0, 0), Image.bigFont);
            }
            else if (pickaxelevel == 2)
            {
                DX.DrawBox(Screen.Width - 432, 64, Screen.Width - 32, 96, DX.GetColor(0, 0, 0), DX.FALSE);
                DX.DrawBox(Screen.Width - 432, 64, Screen.Width - 432 + ((1200 / 600) * pickaxeCount / 3), 96, DX.GetColor(255, 255, 0), DX.TRUE);
                DX.DrawStringToHandle(Screen.Width - 432, 64, "LV: 2", DX.GetColor(0, 0, 0), Image.bigFont);
            }
            else
            {
                DX.DrawBox(Screen.Width - 432, 64, Screen.Width - 32, 96, DX.GetColor(0, 0, 0), DX.FALSE);
                DX.DrawBox(Screen.Width - 432, 64, Screen.Width - 32, 96, DX.GetColor(255, 255, 65), DX.TRUE);
                DX.DrawStringToHandle(Screen.Width - 432, 64, "LV: 3", DX.GetColor(0, 0, 0), Image.bigFont);
            }
        }
    }
}
