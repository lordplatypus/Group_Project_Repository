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
        public int pickaxelevel = 3;
        //ピッケルカウント
        public int pickaxeCount = 0;

        public PlayScene()
        {
            fm = new FloorManager(this);
            map = new Map(this);
            Camera.LookAt(player.x, player.y);
            pm = new ParticleManager();
            bm = new BlockManager(this);
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
            if (player.isitem)
            {
                //ライフ画像
                DX.DrawGraph(1125, 15, Image.playitem);
                DX.DrawString(1180, 20, "" + player.itemcount, DX.GetColor(255, 255, 255));
            }
            else
            {
                bm.Draw();
            }

            //枠
            DX.DrawGraph(948, 8, Image.playerexperienc);
            //ライフ
            DX.DrawBox(950, 10, 950 + (15 * player.life), 25, DX.GetColor(0, 255, 0), DX.TRUE);
            //ライフ画像
            DX.DrawGraph(915, 10, Image.hp);

            //枠
            DX.DrawGraph(948, 48, Image.playerexperienc);
            //level画像
            DX.DrawGraph(895, 50, Image.lv);
            //ピッケル経験値
            if (pickaxelevel == 1)
            {
                DX.DrawBox(950, 50, 950 + (pickaxeCount / 2), 65, DX.GetColor(255, 255, 0), DX.TRUE);
                DX.DrawString(935, 50, "1", DX.GetColor(255, 255, 255));
            }
            else if (pickaxelevel == 2)
            {
                DX.DrawBox(950, 50, 950 + pickaxeCount / 4, 65, DX.GetColor(255, 255, 0), DX.TRUE);
                DX.DrawString(935, 50, "2", DX.GetColor(255, 255, 255));
            }
            else
            {
                DX.DrawBox(950, 50, 1100, 65, DX.GetColor(255, 255, 65), DX.TRUE);
                DX.DrawString(935, 50, "3", DX.GetColor(255, 255, 255));
            }
        }
    }
}
