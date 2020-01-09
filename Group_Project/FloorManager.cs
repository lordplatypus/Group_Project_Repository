using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public class FloorManager
    {
        int floor;
        PlayScene playScene;

        public FloorManager(PlayScene playScene)
        {
            this.playScene = playScene;

            floor = 1;
        }

        public void MoveFloor()
        {
            floor++;
            playScene.map.FloorSetUp();
        }

        public int[] ReturnFloorInfo()
        {
            int[] floorInfo = new int[20];

            if (floor == 1) Floor1(floorInfo);
            if (floor == 2) Floor2(floorInfo);
            if (floor == 3) Floor3(floorInfo);

            return floorInfo;
        }

        int[] Floor1(int[] floorInfo)
        {
            //map size x
            floorInfo[0] = 60;
            //map size y
            floorInfo[1] = 60;
            //num of regular rooms 1
            floorInfo[2] = 0;
            //num of regular rooms 2
            floorInfo[3] = 4;
            //num of regular rooms 3
            floorInfo[4] = 0;
            //Spawn Secret Room? 0=no 1=yes
            floorInfo[5] = 0;
            //num of enemy 1
            floorInfo[6] = 4;
            //num of enemy 2
            floorInfo[7] = 0;
            //num of enemy 3
            floorInfo[8] = 3;
            //num of enemy 4
            floorInfo[9] = 0;
            //num of enemy 5
            floorInfo[10] = 0;
            //boss id / 6 = boss1 / 7 = boss2 / 8 = boss3
            floorInfo[11] = 6;
            //main block ID
            floorInfo[12] = 1;
            //second block(vein) block ID
            floorInfo[13] = 0;
            //diamonds appear? 0=no 1=yes
            floorInfo[14] = 0;
            //Final Boss? 0=no 1=yes
            floorInfo[15] = 0;

            return floorInfo;
        }

        int[] Floor2(int[] floorInfo)
        {
            //map size x
            floorInfo[0] = 80;
            //map size y
            floorInfo[1] = 80;
            //num of regular rooms 1
            floorInfo[2] = 1;
            //num of regular rooms 2
            floorInfo[3] = 5;
            //num of regular rooms 3
            floorInfo[4] = 1;
            //Spawn Secret Room? 0=no 1=yes
            floorInfo[5] = 1;
            //num of enemy 1
            floorInfo[6] = 0;
            //num of enemy 2
            floorInfo[7] = 3;
            //num of enemy 3
            floorInfo[8] = 0;
            //num of enemy 4
            floorInfo[9] = 3;
            //num of enemy 5
            floorInfo[10] = 5;
            //boss id / 6 = boss1 / 7 = boss2 / 8 = boss3
            floorInfo[11] = 7;
            //main block ID
            floorInfo[12] = 2;
            //second block(vein) block ID
            floorInfo[13] = 3;
            //diamonds appear? 0=no 1=yes
            floorInfo[14] = 1;
            //Final Boss? 0=no 1=yes
            floorInfo[15] = 0;

            return floorInfo;
        }

        int[] Floor3(int[] floorInfo)
        {
            //map size x
            floorInfo[0] = 20;
            //map size y
            floorInfo[1] = 20;
            //num of regular rooms 1
            floorInfo[2] = 0;
            //num of regular rooms 2
            floorInfo[3] = 0;
            //num of regular rooms 3
            floorInfo[4] = 0;
            //Spawn Secret Room? 0=no 1=yes
            floorInfo[5] = 0;
            //num of enemy 1
            floorInfo[6] = 0;
            //num of enemy 2
            floorInfo[7] = 0;
            //num of enemy 3
            floorInfo[8] = 0;
            //num of enemy 4
            floorInfo[9] = 0;
            //num of enemy 5
            floorInfo[10] = 0;
            //boss id / 6 = boss1 / 7 = boss2 / 8 = boss3
            floorInfo[11] = 8;
            //main block ID
            floorInfo[12] = 0;
            //second block(vein) block ID
            floorInfo[13] = 0;
            //diamonds appear? 0=no 1=yes
            floorInfo[14] = 0;
            //Final Boss? 0=no 1=yes
            floorInfo[15] = 1;

            return floorInfo;
        }
    }
}
