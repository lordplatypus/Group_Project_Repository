using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    class Boss3LeftShoulder : GameObject
    {
        Boss3 b;
        float xOffset;
        float yOffset;

        public Boss3LeftShoulder(PlayScene playScene, Boss3 b, float x, float y) : base(playScene)
        {
            imageWidth = 128;
            imageHeight = 128;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

            this.x = x;
            this.y = y;
            hp = 3;
            this.b = b;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public void MoveX(float x)
        {

        }

        public void MoveY(float y)
        {

        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject other)
        {
            throw new NotImplementedException();
        }

        public override void Kill()
        {
            base.Kill();
            b.leftShoulderDead = true;
        }
    }
}
