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

        public FloorManager()
        {
            floor = 1;
        }

        public void MoveFloor()
        {
            floor++;
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
            //num of secret rooms
            floorInfo[5] = 0;
            //num of enemy 1
            floorInfo[6] = 1;
            //num of enemy 2
            floorInfo[7] = 1;
            //num of enemy 3
            floorInfo[8] = 1;
            //num of enemy 4
            floorInfo[9] = 1;
            //num of enemy 5
            floorInfo[10] = 1;
            //boss id
            floorInfo[11] = 1;
            //main block ID
            floorInfo[12] = 1;
            //second block(vein) block ID
            floorInfo[13] = 0;
            //diamonds appear? 0=no 1=yes
            floorInfo[14] = 0;

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
            //num of secret rooms
            floorInfo[5] = 0;
            //num of enemy 1
            floorInfo[6] = 1;
            //num of enemy 2
            floorInfo[7] = 1;
            //num of enemy 3
            floorInfo[8] = 1;
            //num of enemy 4
            floorInfo[9] = 1;
            //num of enemy 5
            floorInfo[10] = 1;
            //boss id
            floorInfo[11] = 0;
            //main block ID
            floorInfo[12] = 2;
            //second block(vein) block ID
            floorInfo[13] = 3;
            //diamonds appear? 0=no 1=yes
            floorInfo[14] = 0;

            return floorInfo;
        }

        int[] Floor3(int[] floorInfo)
        {
            return floorInfo;
        }
    }
}
