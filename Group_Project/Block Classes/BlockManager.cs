using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
