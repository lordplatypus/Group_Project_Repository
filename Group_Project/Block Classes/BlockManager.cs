using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Group_Project_2
{
    //BlockID
    //0 = slime (スライム)
    //1 = soil (土)
    //2 = stone (石)
    //3 = iron (鉄)
    //4 = diamond (ダイアモンド)

    public class BlockManager
    {
        PlayScene playScene;

        int[] storedBlocks;

        static int selectedBlock = 0;

        public BlockManager(PlayScene playScene)
        {
            this.playScene = playScene;

            storedBlocks = new int[5];
            for (int i = 0; i < storedBlocks.Length; i++)
            {
                storedBlocks[i] = 0;
            }

            storedBlocks[2] = 100;//cheating
        }

        public void StoreBlock(float worldX, float worldY)
        {
            int blockID = playScene.map.GetTerrain(worldX, worldY);
            if (blockID == -1 || blockID == 5) return;

            playScene.map.DeleteWall(worldX, worldY);

            int red, green, blue;
            if (blockID == 0)
            {//slime
                red = 0;
                green = 255;
                blue = 0;
            }
            else if (blockID == 1)
            {//soil
                red = 176;
                green = 112;
                blue = 0;
            }
            else if (blockID == 2 || blockID == 3)
            {//stone and iron
                red = 100;
                green = 100;
                blue = 100;
            }
            else
            {//diamond
                red = 0;
                green = 215;
                blue = 208;
            }
            playScene.pm.Smoke(worldX, worldY, 30, 10, 10);
            playScene.pm.Spark(worldX, worldY, red, green, blue);
            playScene.pm.BreakWall(worldX, worldY, red, green, blue);

            storedBlocks[blockID]++;
        }

        public void CurrentSelectedBlock()
        {
            selectedBlock++;
            if (selectedBlock == 5) selectedBlock = 0;
        }

        public void ThrowBlock(float x, float y, float angle)
        {
            if (storedBlocks[selectedBlock] == 0) return;

            if (selectedBlock == 0) playScene.gameObjects.Add(new SlimeBlock(playScene, x, y, angle));
            else if (selectedBlock == 1) playScene.gameObjects.Add(new SoilBlock(playScene, x, y, angle));
            else if (selectedBlock == 2) playScene.gameObjects.Add(new StoneBlock(playScene, x, y, angle));
            else if (selectedBlock == 3) playScene.gameObjects.Add(new IronBlock(playScene, x, y, angle));
            else playScene.gameObjects.Add(new DiamondBlock(playScene, x, y, angle));

            storedBlocks[selectedBlock]--;
        }

        public void PlaceBlock(float x, float y)
        {
            if (storedBlocks[selectedBlock] == 0 ||
                playScene.map.GetTerrain(x, y) != -1) return;

            playScene.map.CreateBlock(x, y, selectedBlock);

            storedBlocks[selectedBlock]--;
        }

        public void Draw()
        {
            //DX.DrawString(Screen.Width - 120, 5, "ブロック数 " + storedBlocks[selectedBlock], DX.GetColor(255, 255, 255));

            if (selectedBlock == 0)
            {
                DX.DrawGraphF(Screen.Width - 432, 112, Image.block[0]);
                DX.DrawStringToHandle(Screen.Width - 432, 112, storedBlocks[selectedBlock].ToString(), DX.GetColor(0, 0, 0), Image.bigFont);
                DX.DrawStringToHandle(Screen.Width - 352, 112, "強: C", DX.GetColor(255, 255, 255), Image.smallFont);
                DX.DrawStringToHandle(Screen.Width - 352, 146, "飛: C", DX.GetColor(255, 255, 255), Image.smallFont);
            }
            else if (selectedBlock == 1)
            {
                DX.DrawGraphF(Screen.Width - 432, 112, Image.block[1]);
                DX.DrawStringToHandle(Screen.Width - 432, 112, storedBlocks[selectedBlock].ToString(), DX.GetColor(255, 255, 255), Image.bigFont);
                DX.DrawStringToHandle(Screen.Width - 352, 112, "強: C", DX.GetColor(255, 255, 255), Image.smallFont);
                DX.DrawStringToHandle(Screen.Width - 352, 146, "飛: B", DX.GetColor(255, 255, 255), Image.smallFont);
            }
            else if (selectedBlock == 2)
            {
                DX.DrawGraphF(Screen.Width - 432, 112, Image.block[2]);
                DX.DrawStringToHandle(Screen.Width - 432, 112, storedBlocks[selectedBlock].ToString(), DX.GetColor(0, 0, 0), Image.bigFont);
                DX.DrawStringToHandle(Screen.Width - 352, 112, "強: B", DX.GetColor(255, 255, 255), Image.smallFont);
                DX.DrawStringToHandle(Screen.Width - 352, 146, "飛: A", DX.GetColor(255, 255, 255), Image.smallFont);
            }
            else if (selectedBlock == 3)
            {
                DX.DrawGraphF(Screen.Width - 432, 112, Image.block[3]);
                DX.DrawStringToHandle(Screen.Width - 432, 112, storedBlocks[selectedBlock].ToString(), DX.GetColor(0, 0, 0), Image.bigFont);
                DX.DrawStringToHandle(Screen.Width - 352, 112, "強: A", DX.GetColor(255, 255, 255), Image.smallFont);
                DX.DrawStringToHandle(Screen.Width - 352, 146, "飛: S", DX.GetColor(255, 255, 255), Image.smallFont);
            }
            else
            {
                DX.DrawGraphF(Screen.Width - 432, 112, Image.block[4]);
                DX.DrawStringToHandle(Screen.Width - 432, 112, storedBlocks[selectedBlock].ToString(), DX.GetColor(0, 0, 0), Image.bigFont);
                DX.DrawStringToHandle(Screen.Width - 352, 112, "強: S", DX.GetColor(255, 255, 255), Image.smallFont);
                DX.DrawStringToHandle(Screen.Width - 352, 146, "飛: S", DX.GetColor(255, 255, 255), Image.smallFont);
            }
        }
    }
}
