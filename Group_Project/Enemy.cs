using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Group_Project_2
{
    public abstract class Enemy : GameObject
    {
        public float hp;
        public float maxHP;

        public Enemy(PlayScene playScene) : base(playScene)
        {
            hp = 1;
            maxHP = hp;
        }

        public override void TakeDamage(int damage)
        {
            hp -= damage;

            if (hp <= 0) Kill();
        }

        public virtual void DrawHPBar()
        {
            Camera.DrawBox(x, y - 5, x + (imageWidth), y, DX.GetColor(0, 0, 0), DX.TRUE);
            Camera.DrawBox(x, y - 5, x + (imageWidth / maxHP * hp), y, DX.GetColor(255, 0, 0), DX.TRUE);
        }
    }
}
