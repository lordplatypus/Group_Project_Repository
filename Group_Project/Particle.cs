using DxLibDLL;
using MyLib;

namespace Group_Project_2
{
    public class Particle
    {
        public bool isDead = false;
        public float x;
        public float y;
        public int lifeSpan;
        public int age = 0;
        public int imageHandle;
        private float scale = 1f;
        public float startScale = 1f;
        public float endScale = 1f;
        public float vx;
        public float vy;
        public float angularVelocity;
        public float angularDamp = 1f;
        public float angle;
        public float forceX;
        public float forceY;
        public float damp = 1f;
        public int red = 255;
        public int green = 255;
        public int blue = 255;
        private int alpha = 255;
        public int startAlpha = 255;
        public int endAlpha = 255;
        public int blendMode = DX.DX_BLENDMODE_ALPHA;
        
        public void Update()
        {
            age++;

            if (age > lifeSpan)
            {
                isDead = true;
                return;
            }

            float progressRate = (float)age / lifeSpan;

            scale = MyMath.Lerp(startScale, endScale, progressRate);

            vx += forceX;
            vy += forceY;

            vx *= damp;
            vy *= damp;

            x += vx;
            y += vy;

            angularVelocity *= angularDamp;
            angle += angularVelocity;

            alpha = (int)MyMath.Lerp(startAlpha, endAlpha, progressRate);
        }

        public void Draw()
        {
            //if (isDead)
            //    return;

            //DX.SetDrawBright(red, green, blue);
            //DX.SetDrawBlendMode(blendMode, alpha);

            //DX.DrawRotaGraphFastF(x, y, scale, angle, imageHandle);

            //DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
            //DX.SetDrawBright(255, 255, 255);
            Camera.DrawParticle(x, y, isDead, red, green, blue, blendMode, alpha, scale, angle, imageHandle);
        }
    }
}
