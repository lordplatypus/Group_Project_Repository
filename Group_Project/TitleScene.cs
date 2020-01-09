using DxLibDLL;
using MyLib;

namespace Group_Project_2
{
    public class TitleScene : Scene
    {
        public TitleScene()
        {
        }

        public override void Update()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_1))
            {
                Game.ChangeScene(new PlayScene());
            }
        }

        public override void Draw()
        {
            DX.DrawString(0, 0, "タイトル", DX.GetColor(255, 255, 255));
        }
    }
}
