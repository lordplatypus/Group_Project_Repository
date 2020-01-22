using DxLibDLL;
using MyLib;

namespace Group_Project_2
{
    public class TitleScene : Scene
    {
        float Speed = 3f;
        float y = -70;
        int Fade = 0;
        int counter = 0;

        public TitleScene()
        {
            Sound.PlayBGM(Sound.titleBGM);
        }
        public override void Update()
        {
            counter++;

            if (Fade < 256)
            {
                Fade += 2;
            }

            y += Speed;

            if (y >= 140)
            {
                y = 140;
                if (counter >= 120)
                {
                    if (Input.GetButtonDown(DX.PAD_INPUT_1))
                    {
                        Game.ChangeScene(new PlayScene());
                    }
                }
            }
        }

        public override void Draw()
        {

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 256);
            DX.DrawGraphF(0, 0, Image.titlebg);
            DX.DrawGraphF(130, y, Image.title);

            if ((counter / 40) % 2 == 0)
            {
                DX.DrawGraphF(300, 600, Image.pushanybutton);
            }

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, Fade);
            DX.DrawGraphF(800, 800, Image.team);
        }

    }
}
