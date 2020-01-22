using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_2
{
    public abstract class Enemy : GameObject
    {
        public float hp;

        public Enemy(PlayScene playScene) : base(playScene)
        {
            hp = 1;
        }

        public virtual void TakeDamage(int damage)
        {
            hp -= damage;

            if (hp <= 0) Kill();
        }
    }
}
