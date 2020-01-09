using DxLibDLL;

namespace Group_Project_2
{
    public static class Camera
    {
        public static float x;
        public static float y;

        public static void LookAt(float targetX, float TargetY)
        {
            x = targetX - Screen.Width / 2;
            y = TargetY - Screen.Height / 2;
        }

        public static void DrawGraph(float worldX, float worldY, int handle, bool flip = false)
        {
            DX.DrawGraphF(worldX - x, worldY - y, handle);
        }

        public static void DrawLineBox(float left, float top, float right, float bottom, uint color)
        {
            DX.DrawBox(
                (int)(left - x + .5f),
                (int)(top - y + .5f),
                (int)(right - x + .5f),
                (int)(bottom - y + .5f),
                color,
                DX.FALSE);
        }

        public static void DrawParticle(float worldX, float worldY, bool isDead, int red, int green, int blue, int blendMode, int alpha, float scale, float angle, int imageHandle)
        {
            if (isDead)
                return;

            DX.SetDrawBright(red, green, blue);
            DX.SetDrawBlendMode(blendMode, alpha);

            DX.DrawRotaGraphFastF(worldX - x, worldY - y, scale, angle, imageHandle);

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
            DX.SetDrawBright(255, 255, 255);
        }
    }
}
