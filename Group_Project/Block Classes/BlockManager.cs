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
        }

        public void StoreBlock(float worldX, float worldY)
        {
            int blockID = playScene.map.GetTerrain(worldX, worldY);
            if (blockID == -1 || blockID == 5) return;

            playScene.map.DeleteWall(worldX, worldY);

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
            DX.DrawString(Screen.Width - 120, 5, "ブロック数 " + storedBlocks[selectedBlock], DX.GetColor(255, 255, 255));

            if (selectedBlock == 0) DX.DrawGraphF(Screen.Width - 64, 30, Image.slimeBullet);
            else if (selectedBlock == 1) DX.DrawGraphF(Screen.Width - 64, 30, Image.soilBullet);
            else if (selectedBlock == 2) DX.DrawGraphF(Screen.Width - 64, 30, Image.stoneBullet);
            else if (selectedBlock == 3) DX.DrawGraphF(Screen.Width - 64, 30, Image.ironBullet);
            else DX.DrawGraphF(Screen.Width - 64, 30, Image.diamondBullet);
        }
    }
}
