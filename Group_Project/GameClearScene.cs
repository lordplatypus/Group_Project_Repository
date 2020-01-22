using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;
using DxLibDLL;

namespace Group_Project_2
{
    class GameClearScene : Scene
    {
        float Speed = 10f;
        float x = -1280;

        int counter = 0;

        public GameClearScene()
        {

        }
        public override void Update()
        {
            counter++;

            x += Speed;

            if (x >= 0)
            {
                x = 0;
            }

            if (Input.GetButtonDown(DX.PAD_INPUT_1))
            {
                Game.ChangeScene(new TitleScene());
            }
        }
        public override void Draw()
        {
            DX.DrawGraphF(x, 0, Image.gamecleargroundback);
            DX.DrawGraphF(x + 40, 140, Image.gameclear);

            if (x >= 0)
            {
                if ((counter / 40) % 2 == 0)
                {
                    DX.DrawGraphF(x + 350, 635, Image.gameclearreturntitle);
                }
            }
        }
    }
}
