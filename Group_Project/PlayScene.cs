using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Group_Project_2
{
    public class PlayScene : Scene
    {
        public Map map;
        public Player player;
        public List<GameObject> gameObjects = new List<GameObject>();
        public int blockcount = 0;

        public ParticleManager pm;

        public FloorManager fm;

        public PlayScene()
        {
            fm = new FloorManager(this);
            map = new Map(this);
            Camera.LookAt(player.x, player.y);
            pm = new ParticleManager();
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
            map.DrawMap();
            pm.Draw();
            foreach (GameObject go in gameObjects)
            {
                go.Draw();
            }

            DX.DrawString(Screen.Width - 120, 5, "ブロック数" + blockcount.ToString("0"), DX.GetColor(255, 255, 255));
            DX.DrawBox(950, 5, 1050 + (10 * player.life), 20, DX.GetColor(0, 255, 0), DX.TRUE);
        }
    }
}
