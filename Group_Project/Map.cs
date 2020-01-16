using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;

//Note
//Map
//-2 = out of bounds
//-1 = empty
//0 = slime (スライム)
//1 = soil (土)
//2 = stone (石)
//3 = iron (鉄)
//4 = diamond (ダイアモンド)
//5 = indestructible (何も壊れないブロック)

//Object Map
//-2 = no spawn zone
//-1 = empty
//0 = player
//1 = enemy 1
//2 = enemy 2
//3 = enemy 3
//4 = enemy 4
//5 = enemy 5
//6 = boss 1
//7 = boss 2
//8 = boss 3
//9 = warp

namespace Group_Project_2
{
    public class Map
    {
        public const int CellSize = 64;
        public const int None = -1;
        public const int Wall = 0;

        PlayScene playScene;
        int[,] map;
        int[,] objectMap;
        int width = 0;
        int height = 0;

        int numBasic1 = 0;
        int numBasic2 = 0;
        int numBasic3 = 0;
        bool spawnSecret = false;
        int numEnemy1 = 0;
        int numEnemy2 = 0;
        int numEnemy3 = 0;
        int numEnemy4 = 0;
        int numEnemy5 = 0;
        int bossID = 0;
        int mainBlock = 0;
        int secondBlock = 0;
        bool spawnDiamonds = false;
        bool finalBoss = false;

        public Map(PlayScene playScene)
        {
            this.playScene = playScene;

            FloorSetUp();
        }

        public void FloorSetUp()
        {
            playScene.gameObjects.Clear();

            SetFloorInfo();
            map = new int[width, height];
            objectMap = new int[width, height];

            if (!finalBoss)
            {
                CreateMapArray();
                CreateBossRoom();
                CreatePlayerRoom();
                if (spawnSecret) CreateSecretRoom();
                CreateBasicRoom1();
                CreateBasicRoom2();
                CreateBasicRoom3();
                CreateVeins(secondBlock, 20);
                if (spawnDiamonds) CreateVeins(4, 1);
                CreateEnemies();
            }
            else CreateFinalBossRoom();

            SpawnObjects();
        }

        void SetFloorInfo()
        {
            int[] floorInfo = playScene.fm.ReturnFloorInfo();
            width = floorInfo[0];
            height = floorInfo[1];
            numBasic1 = floorInfo[2];
            numBasic2 = floorInfo[3];
            numBasic3 = floorInfo[4];
            if (floorInfo[5] == 1) spawnSecret = true;
            numEnemy1 = floorInfo[6];
            numEnemy2 = floorInfo[7];
            numEnemy3 = floorInfo[8];
            numEnemy4 = floorInfo[9];
            numEnemy5 = floorInfo[10];
            bossID = floorInfo[11];
            mainBlock = floorInfo[12];
            secondBlock = floorInfo[13];
            if (floorInfo[14] == 1) spawnDiamonds = true;
            if (floorInfo[15] == 1) finalBoss = true;
        }

        void CreateMapArray()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {//unbreakable wall
                        map[x, y] = 5;
                    }
                    else map[x, y] = mainBlock;

                    objectMap[x, y] = -1;
                }
            }
        }

        void CreateBossRoom()
        {
            int bossRoomWidth = 20;
            int bossRoomHeight = 20;
            int bossRoomX = width / 2 - bossRoomWidth / 2;
            int bossRoomY = 1;

            for (int x = bossRoomX; x < bossRoomX + bossRoomWidth; x++)
            {
                for (int y = bossRoomY; y < bossRoomY + bossRoomHeight; y++)
                {
                    map[x, y] = -1; //Empty space

                    if (x == bossRoomX + bossRoomWidth / 2 && y == bossRoomY + bossRoomHeight / 2 + 1)
                    {//marker to spawn the warp
                        objectMap[x, y] = 9;
                    }
                    else if (x == bossRoomX + bossRoomWidth / 2 && y == bossRoomY + bossRoomHeight / 2)
                    {//marker to spawn the boss
                        objectMap[x, y] = bossID;
                    }
                    else objectMap[x, y] = -2;
                }
            }
        }

        void CreatePlayerRoom()
        {
            int playerRoomWidth = 5;
            int playerRoomHeight = 5;
            int playerRoomX = width / 2 - playerRoomWidth / 2;
            int playerRoomY = height - (playerRoomHeight + 1);

            for (int x = playerRoomX; x < playerRoomX + playerRoomWidth; x++)
            {
                for (int y = playerRoomY; y < playerRoomY + playerRoomHeight; y++)
                {
                    map[x, y] = -1; //Empty space

                    if (x == playerRoomX + playerRoomWidth / 2 && y == playerRoomY + playerRoomHeight / 2)
                    {
                        objectMap[x, y] = 0;
                    }
                    else objectMap[x, y] = -2;
                }
            }
        }

        void CreateSecretRoom()
        {
            int secretRoomWidth = 5;
            int secretRoomHeight = 5;
            int secretRoomX = 5;
            int secretRoomY = 5;

            for (int x = secretRoomX; x < secretRoomX + secretRoomWidth; x++)
            {
                for (int y = secretRoomY; y < secretRoomY + secretRoomHeight; y++)
                {//make room left of the boss room
                    map[x, y] = -1;
                    objectMap[x, y] = -2;
                }
            }

            for (int x = 5; x < 30; x++)
            {//make hallway
                map[x, secretRoomHeight / 2 + secretRoomY] = -1;
                objectMap[x, secretRoomHeight / 2 + secretRoomY] = -2;
            }
        }

        void CreateBasicRoom1()
        {
            int basicRoomWidth = 5;
            int basicRoomHeight = 5;
            bool canPlace = false;
            int basicRoomX = 0;
            int basicRoomY = 0;
            int failsafe = 0;
            for (int i = 0; i < numBasic1; i++)
            {
                while (!canPlace)
                {
                    bool skip = false;
                    basicRoomX = MyRandom.Range(5, width - basicRoomWidth - 5);
                    basicRoomY = MyRandom.Range(5, height - basicRoomHeight - 5);
                    for (int x = basicRoomX - 5; x < basicRoomX + 10; x++)
                    {
                        for (int y = basicRoomY - 5; y < basicRoomY + 10; y++)
                        {
                            if (map[x, y] == -1) skip = true;
                            if (skip) break;
                        }
                        if (skip) break;
                    }
                    if (!skip) canPlace = true;

                    failsafe++;
                    if (failsafe >= 100) return;
                }

                for (int x = basicRoomX; x < basicRoomX + basicRoomWidth; x++)
                {
                    for (int y = basicRoomY; y < basicRoomY + basicRoomHeight; y++)
                    {
                        map[x, y] = -1; //Empty space
                    }
                }
                canPlace = false;
                failsafe = 0;
            }
        }

        void CreateBasicRoom2()
        {
            int basicRoomWidth = 10;
            int basicRoomHeight = 10;
            bool canPlace = false;
            int basicRoomX = 0;
            int basicRoomY = 0;
            int failsafe = 0;
            for (int i = 0; i < numBasic2; i++)
            {
                while (!canPlace)
                {
                    bool skip = false;
                    basicRoomX = MyRandom.Range(5, width - basicRoomWidth - 5);
                    basicRoomY = MyRandom.Range(5, height - basicRoomHeight - 5);
                    for (int x = basicRoomX - 5; x < basicRoomX + 15; x++)
                    {
                        for (int y = basicRoomY - 5; y < basicRoomY + 15; y++)
                        {
                            if (map[x, y] == -1) skip = true;
                            if (skip) break;
                        }
                        if (skip) break;
                    }
                    if (!skip) canPlace = true;

                    failsafe++;
                    if (failsafe >= 100) return;
                }

                for (int x = basicRoomX; x < basicRoomX + basicRoomWidth; x++)
                {
                    for (int y = basicRoomY; y < basicRoomY + basicRoomHeight; y++)
                    {
                        map[x, y] = -1; //Empty space
                    }
                }
                canPlace = false;
                failsafe = 0;
            }
        }

        void CreateBasicRoom3()
        {
            int basicRoomWidth = 15;
            int basicRoomHeight = 15;
            bool canPlace = false;
            int basicRoomX = 0;
            int basicRoomY = 0;
            int failsafe = 0;
            for (int i = 0; i < numBasic3; i++)
            {
                while (!canPlace)
                {
                    bool skip = false;
                    basicRoomX = MyRandom.Range(5, width - basicRoomWidth - 5);
                    basicRoomY = MyRandom.Range(5, height - basicRoomHeight - 5);
                    for (int x = basicRoomX - 5; x < basicRoomX + 20; x++)
                    {
                        for (int y = basicRoomY - 5; y < basicRoomY + 20; y++)
                        {
                            if (map[x, y] == -1) skip = true;
                            if (skip) break;
                        }
                        if (skip) break;
                    }
                    if (!skip) canPlace = true;

                    failsafe++;
                    if (failsafe >= 100) return;
                }

                for (int x = basicRoomX; x < basicRoomX + basicRoomWidth; x++)
                {
                    for (int y = basicRoomY; y < basicRoomY + basicRoomHeight; y++)
                    {
                        map[x, y] = -1; //Empty space
                    }
                }
                canPlace = false;
                failsafe = 0;
            }
        }

        void CreateFinalBossRoom()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {//unbreakable wall
                        map[x, y] = 5;
                    }
                    else map[x, y] = -1; //empty space

                    if (x == width / 2 && y == 2)
                    {//boss location
                        objectMap[x, y] = bossID;
                    }
                    else if (x == width / 2 && y == height - 2)
                    {//player location
                        objectMap[x, y] = 0;
                    }
                    else objectMap[x, y] = -1; //empty
                }
            }
        }

        void CreateVeins(int blockID, int veinNum)
        {
            for (int j = 0; j < veinNum; j++)
            {
                int veinSize = MyRandom.Range(3, 11);
                int veinX = MyRandom.Range(2, width - 1);
                int veinY = MyRandom.Range(2, height - 1);
                for (int i = 0; i < veinSize; i++)
                {
                    if (veinX > 1 && veinX < width - 1 && veinY > 1 && veinY < height - 1 && map[veinX, veinY] != -1) map[veinX, veinY] = blockID;

                    int veinPath = MyRandom.Range(0, 4);
                    if (veinPath == 0) veinX++;
                    else if (veinPath == 1) veinX--;
                    else if (veinPath == 2) veinY++;
                    else veinY--;
                }
            }
        }

        int[] CreateEnemyList()
        {
            //an array that holds the above numbers, makes the following code smaller
            int[] numOfEachEnemyType = new int[] { numEnemy1, numEnemy2, numEnemy3, numEnemy4, numEnemy5 };
            //just a variable to hold the number of different enemy types
            int numOfDiffEnemies = numOfEachEnemyType.Length;
            //the actual enemy array that will be passed on, it holds the id and the amount needed to spawn the correct enemy type and amount
            int[] enemyList = new int[numEnemy1 + numEnemy2 + numEnemy3 + numEnemy4 + numEnemy5];
            //'k' is just a typical counter
            int k = 0;
            for (int i = 0; i < numOfDiffEnemies; i++)
            {//this loop goes through the types of enemies
                for (int j = 0; j < numOfEachEnemyType[i]; j++)
                {//this loop goes through the amount of 1 enemy type
                    enemyList[k] = i + 1; //the '+ 1' is to offset the id, as id 1 = enemy1, id 2 = enemy2, and so on
                    k++;
                }
            }
            //return the created list
            return enemyList;
        }

        void CreateEnemies()
        {
            bool done = false;
            int[] enemyList = CreateEnemyList();
            int enemyCount = 0;
            while (!done)
            {
                int x = MyRandom.Range(1, width);
                int y = MyRandom.Range(1, height);
                if (map[x, y] == -1 && objectMap[x, y] == -1)
                {
                    objectMap[x, y] = enemyList[enemyCount];
                    enemyCount++;
                }
                if (enemyCount < enemyList.Length) done = false;
                else done = true;
            }
        }

        void SpawnObjects()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //if the spot is empty, skip
                    if (objectMap[x, y] < 0) continue;
                    //convert from block coordinates to world coordinates
                    int worldX = x * CellSize;
                    int worldY = y * CellSize;
                    //spawn object based on ID stored at that location
                    if (objectMap[x, y] == 0)
                    {//Spawn player
                        Player player = new Player(playScene, worldX, worldY);
                        playScene.gameObjects.Add(player);
                        playScene.player = player;
                    }
                    if (objectMap[x, y] == 1)
                    {
                        playScene.gameObjects.Add(new Enemy1(playScene, worldX, worldY));
                    }
                    if (objectMap[x, y] == 2)
                    {
                        playScene.gameObjects.Add(new Enemy2(playScene, worldX, worldY));
                    }
                    if (objectMap[x, y] == 3)
                    {
                        playScene.gameObjects.Add(new Enemy3(playScene, worldX, worldY));
                    }
                    if (objectMap[x, y] == 4)
                    {
                        playScene.gameObjects.Add(new Enemy4(playScene, worldX, worldY));
                    }
                    //if (objectMap[x, y] == 5)
                    //{
                    //    playScene.gameObjects.Add(new Enemy5(playScene, worldX, worldY));
                    //}
                    if (objectMap[x, y] == 6)
                    {
                        playScene.gameObjects.Add(new Boss(playScene, worldX, worldY));
                    }
                    //if (objectMap[x, y] == 7)
                    //{
                    //    playScene.gameObjects.Add(new Boss2(playScene, worldX, worldY));
                    //}
                    if (objectMap[x, y] == 8)
                    {
                        playScene.gameObjects.Insert(0, new Boss3(playScene, worldX, worldY));
                    }
                }
            }
        }

        public void DrawMap()
        {
            int left = (int)(Camera.x / CellSize);
            int top = (int)(Camera.y / CellSize);
            int right = (int)(Camera.x + Screen.Width - 1 / CellSize);
            int bottom = (int)(Camera.y + Screen.Height - 1 / CellSize);

            if (left < 0) left = 0;
            if (top < 0) top = 0;
            if (right >= width) right = width - 1;
            if (bottom >= height) bottom = height - 1;

            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= right; x++)
                {
                    int id = map[x, y];

                    if (id == None) continue;

                    Camera.DrawGraph(x * CellSize, y * CellSize, Image.block[id]);
                }
            }
        }

        public int GetTerrain(float worldX, float worldY)
        {
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            if (mapX < 0 || mapX >= width || mapY < 0 || mapY >= height)
                return -2;

            return map[mapX, mapY];
        }

        public bool IsWall(float worldX, float worldY)
        {
            int terrainID = GetTerrain(worldX, worldY);

            if (terrainID > -1)
            {
                return true;
            }
            else return false;
        }

        public void DeleteWall(float worldX, float worldY)
        {
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            if (mapX < 1 || mapX > width - 2 || mapY < 1 || mapY > height - 2) return;

            if (GetTerrain(worldX, worldY) >= 0)
            {
                map[mapX, mapY] = -1;
            }
        }

        public void BlowUpWall(float worldX, float worldY)
        {
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            for (int x = mapX - 2; x < mapX + 2; x++)
            {
                for (int y = mapY - 2; y < mapY + 2; y++)
                {
                    if (GetTerrain(x, y) >= 0)
                    {
                        if (x < 1 || x > width - 2 || y < 1 || y > height - 2) continue;
                        map[x, y] = -1;
                    }
                }
            }
        }

        public void CreateBlock(float worldX, float worldY, int blockType)
        {
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            if (mapX < 1 || mapX > width - 2 || mapY < 1 || mapY > height - 2) return;

            if (GetTerrain(worldX, worldY) == -1)
            {
                map[mapX, mapY] = blockType;
            }
        }

        public void CreateWarp()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //if the spot is empty, skip
                    if (objectMap[x, y] != 9) continue;
                    //convert from block coordinates to world coordinates
                    int worldX = x * CellSize;
                    int worldY = y * CellSize;
                    //spawn object based on ID stored at that location
                    if (objectMap[x, y] == 9)
                    {//Spawn player
                        playScene.gameObjects.Add(new Warp(playScene, worldX, worldY));
                    }
                }
            }
        }
    }
}
