using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    class Boss3 : GameObject
    {
        enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        Direction direction = Direction.Down;
        public bool rightShoulderDead = false;
        public bool leftShoulderDead = false;
        Boss3RightShoulder rShoulder;
        Boss3LeftShoulder lShoulder;

        public Boss3(PlayScene playScene, float x, float y) : base(playScene)
        {
            imageWidth = 128;
            imageHeight = 128;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;

            this.x = x;
            this.y = y;
            hp = 35;

            rShoulder = new Boss3RightShoulder(playScene, this, x, y);
            lShoulder = new Boss3LeftShoulder(playScene, this, x, y);

            playScene.gameObjects.Add(rShoulder);
            playScene.gameObjects.Add(lShoulder);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject other)
        {
            throw new NotImplementedException();
        }

        public override void TakeDamage(int damage)
        {
            if (rightShoulderDead && leftShoulderDead) base.TakeDamage(damage);
        }
    }
}
