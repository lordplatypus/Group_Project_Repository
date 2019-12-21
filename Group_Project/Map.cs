using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;

//Note
//Map
//-1 = empty
//0 = level 1
//1 = level 2
//2 = level 3
//3 = level 4
//4 = level 5
//5 = level 6
//6 = level 7
//7 = level 8
//8 = level 9
//9 = level 10

//Object Map
//-2 = no spawn zone
//-1 = empty
//0 = anchor left
//1 = anchor right
//2 = anchor up
//3 = anchor down
//4 = player
//5 = enemy 1
//6 = enemy 2
//7 = enemy 3
//8 = enemy 4
//9 = boss

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
        int width = 50;
        int height = 50;

        public Map(PlayScene playScene)
        {
            this.playScene = playScene;

            map = new int[width, height];
            objectMap = new int[width, height];

            CreateMapArray();
            CreateBossRoom();
            CreatePlayerRoom();
            CreateBasicRoom();
            CreateSecretRoom();
            CreatePathX();
            CreatePathY();
            CreateEnemies();
            SpawnObjects();
        }

        void CreateMapArray()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int blockToSpawn = MyRandom.Range(0, 1000);
                    if (blockToSpawn <= 600) map[x, y] = 0;
                    else if (blockToSpawn <= 800) map[x, y] = 1;
                    else if (blockToSpawn <= 900) map[x, y] = 2;
                    else if (blockToSpawn <= 950) map[x, y] = 3;
                    else if (blockToSpawn <= 975) map[x, y] = 4;
                    else if (blockToSpawn <= 988) map[x, y] = 5;
                    else if (blockToSpawn <= 994) map[x, y] = 6;
                    else if (blockToSpawn <= 996) map[x, y] = 7;
                    else if (blockToSpawn <= 998) map[x, y] = 8;
                    else if (blockToSpawn == 999) map[x, y] = 9;

                    objectMap[x, y] = -1;
                }
            }
        }

        void CreateBossRoom()
        {
            int bossRoomWidth = 20;
            int bossRoomHeight = 20;
            int bossRoomX = MyRandom.Range(1, width - bossRoomWidth);
            int bossRoomY = MyRandom.Range(1, height - bossRoomHeight);

            for (int x = bossRoomX; x < bossRoomX + bossRoomWidth; x++)
            {
                for (int y = bossRoomY; y < bossRoomY + bossRoomHeight; y++)
                {
                    if (y == bossRoomY + bossRoomHeight / 2 && x == bossRoomX) objectMap[x, y] = 0;
                    else if (y == bossRoomY + bossRoomHeight / 2 && x == bossRoomX + bossRoomWidth - 1) objectMap[x, y] = 1;
                    else if (x == bossRoomX + bossRoomWidth / 2 && y == bossRoomY) objectMap[x, y] = 2;
                    else if (x == bossRoomX + bossRoomWidth / 2 && y == bossRoomY + bossRoomHeight - 1) objectMap[x, y] = 3;
                    map[x, y] = -1;

                    if (x == bossRoomX + bossRoomWidth / 2 && y == bossRoomY + bossRoomHeight / 2)
                    {
                        objectMap[x, y] = 9;
                    }

                }
            }
        }

        void CreatePlayerRoom()
        {
            int playerRoomWidth = 10;
            int playerRoomHeight = 10;
            bool canPlace = false;
            int playerRoomX = 0;
            int playerRoomY = 0;
            while (!canPlace)
            {
                playerRoomX = MyRandom.Range(1, width - playerRoomWidth);
                playerRoomY = MyRandom.Range(1, height - playerRoomHeight);
                if (map[playerRoomX, playerRoomY] != -1 &&
                    map[playerRoomX + 9, playerRoomY + 9] != -1 &&
                    map[playerRoomX + 9, playerRoomY] != -1 &&
                    map[playerRoomX, playerRoomY + 9] != -1) canPlace = true;
            }

            for (int x = playerRoomX; x < playerRoomX + playerRoomWidth; x++)
            {
                for (int y = playerRoomY; y < playerRoomY + playerRoomHeight; y++)
                {
                    if (y == playerRoomY + playerRoomHeight / 2 && x == playerRoomX) objectMap[x, y] = 0;
                    else if (y == playerRoomY + playerRoomHeight / 2 && x == playerRoomX + playerRoomWidth - 1) objectMap[x, y] = 1;
                    else if (x == playerRoomX + playerRoomWidth / 2 && y == playerRoomY) objectMap[x, y] = 2;
                    else if (x == playerRoomX + playerRoomWidth / 2 && y == playerRoomY + playerRoomHeight - 1) objectMap[x, y] = 3;

                    else objectMap[x, y] = -2;

                    if (x == playerRoomX + playerRoomWidth / 2 && y == playerRoomY + playerRoomHeight / 2)
                    {
                        CreatePlayer(x, y);
                    }

                    map[x, y] = -1; 
                }
            }
        }

        void CreateBasicRoom()
        {
            int basicRoomWidth = 10;
            int basicRoomHeight = 10;
            bool canPlace = false;
            int numOfRooms = MyRandom.Range(1, 4);
            int basicRoomX = 0;
            int basicRoomY = 0;
            int failsafe = 0;
            for (int i = 0; i < numOfRooms; i++)
            {
                while (!canPlace)
                {
                    basicRoomX = MyRandom.Range(1, width - basicRoomWidth);
                    basicRoomY = MyRandom.Range(1, height - basicRoomHeight);
                    if (map[basicRoomX, basicRoomY] != -1 &&
                        map[basicRoomX + 9, basicRoomY + 9] != -1 &&
                        map[basicRoomX + 9, basicRoomY] != -1 &&
                        map[basicRoomX, basicRoomY + 9] != -1) canPlace = true;
                    failsafe++;
                    if (failsafe >= 100) return;
                }

                for (int x = basicRoomX; x < basicRoomX + basicRoomWidth; x++)
                {
                    for (int y = basicRoomY; y < basicRoomY + basicRoomHeight; y++)
                    {
                        if (y == basicRoomY + basicRoomHeight / 2 && x == basicRoomX) objectMap[x, y] = 0;
                        else if (y == basicRoomY + basicRoomHeight / 2 && x == basicRoomX + basicRoomWidth - 1) objectMap[x, y] = 1;
                        else if (x == basicRoomX + basicRoomWidth / 2 && y == basicRoomY) objectMap[x, y] = 2;
                        else if (x == basicRoomX + basicRoomWidth / 2 && y == basicRoomY + basicRoomHeight - 1) objectMap[x, y] = 3;
                        map[x, y] = -1;
                    }
                }
                canPlace = false;
                failsafe = 0;
            }
        }

        void CreateSecretRoom()
        {
            int secretRoomWidth = 5;
            int secretRoomHeight = 5;
            bool canPlace = false;
            int NumOfRooms = MyRandom.Range(0, 4);
            int secretRoomX = 0;
            int secretRoomY = 0;
            int failsafe = 0;
            for (int i = 0; i < NumOfRooms; i++)
            {
                while (!canPlace)
                {
                    secretRoomX = MyRandom.Range(1, width - secretRoomWidth);
                    secretRoomY = MyRandom.Range(1, height - secretRoomHeight);
                    if (map[secretRoomX, secretRoomY] != -1 &&
                        map[secretRoomX + 4, secretRoomY + 4] != -1 &&
                        map[secretRoomX + 4, secretRoomY] != -1 &&
                        map[secretRoomX, secretRoomY + 4] != -1) canPlace = true;
                    failsafe++;
                    if (failsafe >= 100) return;
                }
                for (int x = secretRoomX; x < secretRoomX + secretRoomWidth; x++)
                {
                    for (int y = secretRoomY; y < secretRoomY + secretRoomHeight; y++)
                    {
                        map[x, y] = -1;
                    }
                }
                canPlace = false;
                failsafe = 0;
            }
        }

        void CreatePathX()
        {
            bool done = false;
            while (!done)
            {
                done = true;
                //look for the first point
                bool found0 = false;
                bool found1 = false;
                int firstPointX = -2;
                int firstPointY = -2;
                for (int y = 1; y < height - 1; y++)
                {//after looking through the current row (x) move down one row
                    for (int x = 1; x < width - 1; x++)
                    {//looks in the x direction first
                        if (objectMap[x, y] == 1)
                        {//from left to right if '1' is found save that location and break out of the loops
                            found1 = true;
                            firstPointX = x;
                            firstPointY = y;
                        }
                        int x2 = width - 1 - x;
                        if (objectMap[x2, y] == 0)
                        {//from right to left if '0' is found save that location and break out of the loops
                            found0 = true;
                            firstPointX = x2;
                            firstPointY = y;
                        }

                        if (found0 || found1) break;
                    }
                    if (found0 || found1) break;
                }
                //Start looking for the next point
                bool foundSecondPoint = false;
                int secondPointX = -2;
                int secondPointY = -2;

                if (found0)
                {//if a '0' was found before '1', continue moving from right to left to find a '1'
                    for (int y = firstPointY; y < height - 1; y++)
                    {
                        for (int x = firstPointX; x > 1; x--)
                        {
                            if (objectMap[x, y] == 1)
                            {//if '1' is found, save the location and break out of the loops
                                secondPointX = x;
                                secondPointY = y;
                                foundSecondPoint = true;
                            }
                            if (foundSecondPoint) break;
                        }
                        if (foundSecondPoint) break;
                    }
                }
                else if (found1)
                {//if a '1' was found before '0', continue moving from left to right to find a '1'
                    for (int y = firstPointY; y < height - 1; y++)
                    {
                        for (int x = firstPointX; x < width - 1; x++)
                        {
                            if (objectMap[x, y] == 0)
                            {//if '1' is found, save the location and break out of the loops
                                secondPointX = x;
                                secondPointY = y;
                                foundSecondPoint = true;
                            }
                            if (foundSecondPoint) break;
                        }
                        if (foundSecondPoint) break;
                    }
                }
                //if a second point is found and depending on the first point found, start making the hallway to connect the 2 rooms
                if (foundSecondPoint && found0)
                {//if '0' was found first
                    for (int x = firstPointX; x >= secondPointX; x--)
                    {//move left until the y coordinate is the same as second point
                        map[x, firstPointY] = -1;
                    }
                    for (int y = firstPointY; y <= secondPointY; y++)
                    {//move down until it reaches the second point
                        map[secondPointX, y] = -1;
                    }
                }
                else if (foundSecondPoint && found1)
                {//if '1' was found first
                    for (int x = firstPointX; x <= secondPointX; x++)
                    {//move right until the y coordinate is the same as second point
                        map[x, firstPointY] = -1;
                    }
                    for (int y = firstPointY; y <= secondPointY; y++)
                    {//move down until it reaches the second point
                        map[secondPointX, y] = -1;
                    }
                }
                //remove places that the program already tried
                if (firstPointX != -2 && firstPointY != -2)
                {
                    objectMap[firstPointX, firstPointY] = -1;
                    done = false;
                }
                if (secondPointX != -2 && secondPointY != -2)
                {
                    objectMap[secondPointX, secondPointY] = -1;
                    done = false;
                }
            }
        }

        void CreatePathY()
        {
            bool done = false;
            while (!done)
            {
                done = true;
                //look for the first point
                bool found2 = false;
                bool found3 = false;
                int firstPointX = -2;
                int firstPointY = -2;
                for (int x = 1; x < width - 1; x++)
                {//after looking through the current column (y) move right one column
                    for (int y = 1; y < height - 1; y++)
                    {//looks in the y direction first
                        if (objectMap[x, y] == 3)
                        {//from top to bottom if '3' is found save that location and break out of the loops
                            found3 = true;
                            firstPointX = x;
                            firstPointY = y;
                        }
                        int y2 = height - 1 - y;
                        if (objectMap[x, y2] == 2)
                        {//from bottom to top if '2' is found save that location and break out of the loops
                            found2 = true;
                            firstPointX = x;
                            firstPointY = y2;
                        }

                        if (found2 || found3) break;
                    }
                    if (found2 || found3) break;
                }
                //Start looking for the next point
                bool foundSecondPoint = false;
                int secondPointX = -2;
                int secondPointY = -2;

                if (found2)
                {//if a '2' was found before '3', continue moving from bottom to top to find a '3'
                    for (int x = firstPointX; x < width - 1; x++)
                    {
                        for (int y = firstPointY; y > 1; y--)
                        {
                            if (objectMap[x, y] == 3)
                            {//if '3' is found, save the location and break out of the loops
                                secondPointX = x;
                                secondPointY = y;
                                foundSecondPoint = true;
                            }
                            if (foundSecondPoint) break;
                        }
                        if (foundSecondPoint) break;
                    }
                }
                else if (found3)
                {//if a '3' was found before '2', continue moving from top to bottom to find a '2'
                    for (int x = firstPointX; x < width - 1; x++)
                    {
                        for (int y = firstPointY; y < height - 1; y++)
                        {
                            if (objectMap[x, y] == 2)
                            {//if '2' is found, save the location and break out of the loops
                                secondPointX = x;
                                secondPointY = y;
                                foundSecondPoint = true;
                            }
                            if (foundSecondPoint) break;
                        }
                        if (foundSecondPoint) break;
                    }
                }
                //if a second point is found and depending on the first point found, start making the hallway to connect the 2 rooms
                if (foundSecondPoint && found2)
                {//if '2' was found first
                    for (int y = firstPointY; y >= secondPointY; y--)
                    {//move up until the x coordinate is the same as second point
                        map[firstPointX, y] = -1;
                    }
                    for (int x = firstPointX; x <= secondPointX; x++)
                    {//move right until it reaches the second point
                        map[x, secondPointY] = -1;
                    }
                    done = false;
                }
                else if (foundSecondPoint && found3)
                {//if '3' was found first
                    for (int y = firstPointY; y <= secondPointY; y++)
                    {//move down until the xw coordinate is the same as second point
                        map[firstPointX, y] = -1;
                    }
                    for (int x = firstPointX; x <= secondPointX; x++)
                    {//move right until it reaches the second point
                        map[x, secondPointY] = -1;
                    }
                    done = false;
                }
                //remove places that the program already tried
                if (firstPointX != -2 && firstPointY != -2)
                {
                    objectMap[firstPointX, firstPointY] = -1;
                    done = false;
                }
                if (secondPointX != -2 && secondPointY != -2)
                {
                    objectMap[secondPointX, secondPointY] = -1;
                    done = false;
                }
            }
        }

        void CreatePlayer(int x, int y)
        {
            objectMap[x, y] = 4;
        }

        int[] CreateEnemyList()
        {
            //the number of enemies of a certain type
            int enemy0Count = 1;
            int enemy1Count = 1;
            int enemy2Count = 1;
            int enemy3Count = 1;
            //an array that holds the above numbers, makes the following code smaller
            int[] numOfEachEnemyType = new int[] {enemy0Count, enemy1Count, enemy2Count, enemy3Count};
            //just a variable to hold the number of different enemy types
            int numOfDiffEnemies = numOfEachEnemyType.Length;
            //the actual enemy array that will be passed on, it holds the id and the amount needed to spawn the correct enemy type and amount
            int[] enemyList = new int[enemy0Count + enemy1Count + enemy2Count + enemy3Count];
            //'k' is just a typical counter
            int k = 0;
            for (int i = 0; i < numOfDiffEnemies; i++)
            {//this loop goes through the types of enemies
                for (int j = 0; j < numOfEachEnemyType[i]; j++)
                {//this loop goes through the amount of 1 enemy type
                    enemyList[k] = i + 5; //the '+ 5' is to offset the id, as id 5 = enemy0, id 6 = enemy1, and so on
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
            while(!done)
            {
                int x = MyRandom.Range(1, width);
                int y = MyRandom.Range(1, height);
                if (map[x, y] == -1 && objectMap[x,y] == -1)
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
                    if (objectMap[x, y] < 4) continue;
                    //convert from block coordinates to world coordinates
                    int worldX = x * CellSize;
                    int worldY = y * CellSize;
                    //spawn object based on ID stored at that location
                    if (objectMap[x, y] == 4)
                    {//Spawn player
                        Player player = new Player(playScene, worldX, worldY);
                        playScene.gameObjects.Add(player);
                        playScene.player = player;
                    }
                    if (objectMap[x, y] == 5)
                    {
                        playScene.gameObjects.Add(new Enemy1(playScene, worldX, worldY));
                    }
                    if (objectMap[x, y] == 6)
                    {
                        playScene.gameObjects.Add(new Enemy2(playScene, worldX, worldY));
                    }
                    if (objectMap[x, y] == 7)
                    {
                        playScene.gameObjects.Add(new Enemy3(playScene, worldX, worldY));
                    }
                    if (objectMap[x, y] == 8)
                    {
                        playScene.gameObjects.Add(new Enemy4(playScene, worldX, worldY));
                    }
                    if (objectMap[x, y] == 9)
                    {
                        playScene.gameObjects.Add(new Boss(playScene, worldX, worldY));
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
                return None;

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

        public void DeleteWallPlayer(float worldX, float worldY)
        {
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            if (mapX < 1 || mapX > width - 2 || mapY < 1 || mapY > height - 2) return;

            if (GetTerrain(worldX, worldY) >= 0)
            {
                map[mapX, mapY] = -1;
                playScene.blockcount++;
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

        public void CreateBlock(float worldX, float worldY, int blockLevel)
        {
            //int mapX = (int)(worldX / CellSize);
            //int mapY = (int)(worldY / CellSize);

            if (GetTerrain(worldX, worldY) == -1)
            {
                int mapX = (int)(worldX / CellSize);
                int mapY = (int)(worldY / CellSize);
                map[mapX, mapY] = blockLevel;
            }
        }

        public void CreateBlockPlayer(float worldX, float worldY, int blockLevel)
        {
            //int mapX = (int)(worldX / CellSize);
            //int mapY = (int)(worldY / CellSize);

            if (GetTerrain(worldX, worldY) == -1)
            {
                int mapX = (int)(worldX / CellSize);
                int mapY = (int)(worldY / CellSize);
                map[mapX, mapY] = blockLevel;
                playScene.blockcount--;
            }
        }
    }
}
