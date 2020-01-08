using System;
using System.Collections.Generic;
using DxLibDLL;
using MyLib;

namespace Group_Project_2
{
    public class ParticleManager
    {
        List<Particle> particles = new List<Particle>();

        public void Update()
        {
            foreach (Particle particle in particles)
            {
                if (!particle.isDead)
                {
                    particle.Update();
                }
            }

            particles.RemoveAll(p => p.isDead);
        }

        public void Draw()
        {
            foreach (Particle particle in particles)
            {
                particle.Draw();
            }
        }

        public void Splash(float x, float y)
        {
            particles.Add(
                new Particle()
                {
                    x = x,
                    y = y,
                    lifeSpan = MyRandom.Range(40, 70),
                    imageHandle = Image.particleDot1,
                    vy = MyRandom.Range(-4f, -7f),
                    vx = MyRandom.PlusMinus(1.5f),
                    forceY = 0.15f,
                    startScale = 0.5f,
                    endScale = 0f,
                    red = 170,
                    green = 170,
                    blue = 255,
                });
        }

        public void Spark(float x, float y, float angle)
        {
            for (int i = 0; i < 30; i++)
            {
                angle += MyRandom.PlusMinus(0.04f);
                float speed = MyRandom.Range(4f, 17f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(22, 35),
                        imageHandle = Image.particleDot1,
                        vx = (float)Math.Cos(angle) * speed,
                        vy = (float)Math.Sin(angle) * speed,
                        forceY = 0.13f,
                        damp = 0.95f,
                        startScale = 0.1f,
                        endScale = 0.05f,
                        red = 255,
                        green = 255,
                        blue = 0,
                        endAlpha = 0,
                    });
            }
        }

        public void Fire(float x, float y)
        {
            particles.Add(new Particle()
            {
                x = x + MyRandom.PlusMinus(8),
                y = y + MyRandom.PlusMinus(8),
                lifeSpan = MyRandom.Range(30, 60),
                startScale = MyRandom.Range(0.3f, 0.8f),
                endScale = MyRandom.Range(0.2f, 0.4f),
                imageHandle = Image.particleFire,
                startAlpha = MyRandom.Range(170, 255),
                endAlpha = 0,
                blendMode = DX.DX_BLENDMODE_ADD,
                forceY = -0.07f,
                vx = MyRandom.PlusMinus(2f),
                vy = MyRandom.PlusMinus(2f),
                damp = 0.97f,
                angle = MyRandom.PlusMinus(MyMath.PI),
                angularVelocity = MyRandom.PlusMinus(0.12f),
                angularDamp = 0.97f,
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="distanceAway">How far away the explosion can happen</param>
        public void Explosion(float x, float y, int distanceAway)
        {
            for (int i = 0; i < 70; i++)
            {
                float angle = MyRandom.PlusMinus(MyMath.PI);
                float speed = MyRandom.Range(0f, 8f);
                float startScale = MyRandom.Range(0.5f, 1.3f);

                particles.Add(
                    new Particle()
                    {
                        x = x + MyRandom.PlusMinus(distanceAway),
                        y = y + MyRandom.PlusMinus(distanceAway),
                        lifeSpan = MyRandom.Range(15, 45),
                        imageHandle = Image.particleFire,
                        vx = (float)Math.Cos(angle) * speed,
                        vy = (float)Math.Sin(angle) * speed,
                        damp = 0.88f,
                        startScale = startScale,
                        endScale = startScale * 0.75f,
                        endAlpha = 0,
                        blendMode = DX.DX_BLENDMODE_ADD,
                        angle = MyRandom.PlusMinus(3.14f),
                        angularVelocity = MyRandom.PlusMinus(0.15f),
                        angularDamp = 0.94f,
                    });
            }
        }

        public void Steam(float x, float y)
        {
            particles.Add(new Particle()
            {
                x = x,
                y = y,
                lifeSpan = MyRandom.Range(40, 90),
                imageHandle = Image.particleSteam,
                vx = MyRandom.PlusMinus(0.5f),
                vy = MyRandom.Range(-6f, -9f),
                forceX = MyRandom.Range(0.03f, 0.07f),
                startScale = MyRandom.Range(0.1f, 0.15f),
                endScale = MyRandom.Range(0.3f, 0.8f),
                startAlpha = MyRandom.Range(120, 170),
                endAlpha = 0,
                angle = MyRandom.PlusMinus(MyMath.PI),
                angularVelocity = MyRandom.PlusMinus(0.13f),
                angularDamp = 0.98f,
                damp = 0.935f,
            });
        }

        public void Smoke(float x, float y)
        {
            for (int i = 0; i < 30; i++)
            {
                particles.Add(new Particle()
                {
                    x = x + MyRandom.PlusMinus(5),
                    y = y + MyRandom.PlusMinus(2),
                    lifeSpan = MyRandom.Range(15, 40),
                    imageHandle = Image.particleSmoke,
                    vx = MyRandom.PlusMinus(3f),
                    vy = MyRandom.PlusMinus(0.7f) + -0.5f,
                    damp = 0.93f,
                    forceY = 0.02f,
                    startScale = 0.15f,
                    endScale = 0.3f,
                    startAlpha = 170,
                    endAlpha = 0,
                    angle = MyRandom.PlusMinus(3.14f),
                    angularVelocity = MyRandom.PlusMinus(0.05f),
                    angularDamp = 0.98f,
                });
            }
        }

        public void PickupItem(float x, float y)
        {
            particles.Add(new Particle()
            {
                x = x,
                y = y,
                lifeSpan = 12,
                imageHandle = Image.particleRing2,
                startScale = 0.15f,
                endScale = 0.35f,
                startAlpha = 150,
                endAlpha = 0,
                blendMode = DX.DX_BLENDMODE_ADD,
                red = 170,
                green = 170,
            });

            particles.Add(new Particle()
            {
                x = x,
                y = y,
                lifeSpan = 24,
                imageHandle = Image.particleGlitter1,
                startScale = 1f,
                endScale = 0.4f,
                endAlpha = 0,
                blendMode = DX.DX_BLENDMODE_ADD,
                blue = 100,
            });

            for (int i = 0; i < 7; i++)
            {
                particles.Add(new Particle()
                {
                    x = x + MyRandom.PlusMinus(10f),
                    y = y + MyRandom.PlusMinus(10f),
                    lifeSpan = MyRandom.Range(20, 50),
                    imageHandle = Image.particleGlitter1,
                    startScale = 0.6f,
                    endScale = 0.1f,
                    startAlpha = 190,
                    endAlpha = 0,
                    blendMode = DX.DX_BLENDMODE_ADD,
                    blue = 100,
                    vx = MyRandom.PlusMinus(2f),
                    vy = MyRandom.Range(-3, -7f),
                    damp = 0.96f,
                    forceY = 0.15f,
                });
            }
        }

        public void ShockWave(float x, float y)
        {
            particles.Add(new Particle()
            {
                x = x,
                y = y,
                lifeSpan = 10,
                imageHandle = Image.particleRing4,
                startScale = 0.15f,
                endScale = 0.8f,
                endAlpha = 0,
                angle = 3.14f / 180f * 0f,
            });
        }

        public void Blood(float x, float y)
        {

            for (int i = 0; i < 40; i++)
            {
                float angle = 3.14f / 180f * (225f + MyRandom.PlusMinus(4f));
                float speed = MyRandom.Range(3f, 4.5f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(8, 20),
                        imageHandle = Image.particleDot1,
                        vy = (float)Math.Sin(angle) * speed,
                        vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.98f,
                        startScale = 0.2f,
                        endScale = 0.1f,
                        red = 170,
                        green = 0,
                        blue = 0,
                        //endAlpha = 0,
                    });
            }

            for (int i = 0; i < 40; i++)
            {
                float angle = 3.14f / 180f * (-65f + MyRandom.PlusMinus(4f));
                float speed = MyRandom.Range(3f, 5.5f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(10, 30),
                        imageHandle = Image.particleDot1,
                        vy = (float)Math.Sin(angle) * speed,
                        vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.98f,
                        startScale = 0.2f,
                        endScale = 0.1f,
                        red = 170,
                        green = 0,
                        blue = 0,
                        //endAlpha = 0,
                    });
            }

            for (int i = 0; i < 20; i++)
            {
                float angle = MyRandom.PlusMinus(3.14f);
                float speed = MyRandom.Range(0.2f, 3.5f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(10, 20),
                        imageHandle = Image.particleDot1,
                        vy = (float)Math.Sin(angle) * speed,
                        vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.98f,
                        startScale = 0.3f,
                        endScale = 0.2f,
                        red = 170,
                        green = 0,
                        blue = 0,
                        startAlpha = 150,
                        endAlpha = 0,
                    });
            }
        }

        public void Stars(float x, float y)
        {
            for (int i = 0; i < 100; i++)
            {
                float angle = MyRandom.PlusMinus(3.14f);
                float speed = MyRandom.Range(1.5f, 7f);
                float scale = MyRandom.Range(0.3f, 0.5f);

                particles.Add(
                    new Particle()
                    {
                        x = x + MyRandom.PlusMinus(3),
                        y = y + MyRandom.PlusMinus(3),
                        lifeSpan = MyRandom.Range(120, 180),
                        imageHandle = Image.particleStar2,
                        vy = (float)Math.Sin(angle) * speed - MyRandom.Range(0, 7f),
                        vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.99f,
                        startScale = scale,
                        endScale = scale,
                        red = MyRandom.Range(100, 255),
                        green = MyRandom.Range(100, 255),
                        blue = MyRandom.Range(100, 255),
                        startAlpha = 255,
                        endAlpha = 0,
                        angle = MyRandom.PlusMinus(3.14f),
                        angularVelocity = MyRandom.PlusMinus(0.08f),
                        angularDamp = 0.985f,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void FlowLine(float x, float y)
        {
            particles.Add(
                new Particle()
                {
                    x = x + MyRandom.PlusMinus(30),
                    y = y + MyRandom.PlusMinus(30),
                    lifeSpan = MyRandom.Range(20, 30),
                    imageHandle = Image.particleLine1,
                    vy = -MyRandom.Range(5f, 7f),
                    vx = 0,
                    damp = 1f,
                    startScale = 0.4f,
                    endScale = 0.2f,
                    startAlpha = 255,
                    endAlpha = 0,
                    angle = 3.14f / 180f * 90,
                    blendMode = DX.DX_BLENDMODE_ADD,
                });
        }

        public void RadialLine(float x, float y)
        {
            for (int i = 0; i < 7; i++)
            {
                float angle = MyRandom.PlusMinus(3.1415f);
                float speed = MyRandom.Range(2.5f, 7f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(8, 15),
                        imageHandle = Image.particleLine2,
                        vy = (float)Math.Sin(angle) * speed,
                        vx = (float)Math.Cos(angle) * speed,
                        damp = 1f,
                        startScale = 0.4f,
                        endScale = 0.6f,
                        startAlpha = 255,
                        endAlpha = 0,
                        angle = angle,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }

            particles.Add(
                new Particle()
                {
                    x = x,
                    y = y,
                    lifeSpan = 10,
                    imageHandle = Image.particleDot1,
                    startScale = 0.4f,
                    endScale = 2f,
                    startAlpha = 255,
                    endAlpha = 0,
                    blendMode = DX.DX_BLENDMODE_ADD,
                });

            for (int i = 0; i < 20; i++)
            {
                float angle3 = MyRandom.PlusMinus(3.1415f);
                float speed3 = MyRandom.Range(0.3f, 4f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(14, 48),
                        imageHandle = Image.particleDot1,
                        vy = (float)Math.Sin(angle3) * speed3,
                        vx = (float)Math.Cos(angle3) * speed3,
                        damp = 0.91f,
                        startScale = MyRandom.Range(0.04f, 0.13f),
                        endScale = 0.03f,
                        startAlpha = 255,
                        endAlpha = 0,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void Slash(float x, float y, float angle)
        {
            float speed = 6f;
            float vy = (float)Math.Sin(angle) * speed;
            float vx = (float)Math.Cos(angle) * speed;

            particles.Add(
                new Particle()
                {
                    x = x - vx * 3f,
                    y = y - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    vy = vy,
                    vx = vx,
                    damp = 0.98f,
                    startScale = 0.7f,
                    endScale = 0.6f,
                    startAlpha = 255,
                    endAlpha = 0,
                    angle = angle,
                    blendMode = DX.DX_BLENDMODE_ADD,
                });


            for (int i = 0; i < 7; i++)
            {
                float angle2 = MyRandom.PlusMinus(3.1415f);
                float speed2 = MyRandom.Range(2f, 5f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(7, 12),
                        imageHandle = Image.particleLine2,
                        vy = (float)Math.Sin(angle2) * speed2,
                        vx = (float)Math.Cos(angle2) * speed2,
                        damp = 1f,
                        startScale = 0.3f,
                        endScale = 0.4f,
                        startAlpha = 255,
                        endAlpha = 0,
                        angle = angle2,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }

            for (int i = 0; i < 30; i++)
            {
                float angle3 = MyRandom.PlusMinus(3.1415f);
                float speed3 = MyRandom.Range(0.3f, 4f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(10, 45),
                        imageHandle = Image.particleDot1,
                        vy = (float)Math.Sin(angle3) * speed3,
                        vx = (float)Math.Cos(angle3) * speed3,
                        damp = 0.91f,
                        startScale = MyRandom.Range(0.03f, 0.1f),
                        endScale = 0.03f,
                        startAlpha = 255,
                        endAlpha = 0,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void FireWork(float x, float y)
        {
            int[,] color = {
                {255, 170, 170},
                {170, 170, 255},
                {240, 240, 160},
                {255, 170, 170},
            };

            int colorPattern = MyRandom.Range(0, 2);

            for (int i = 0; i < 400; i++)
            {
                float angle = MyRandom.PlusMinus(3.1415f);
                float speed = MyRandom.Range(0f, 3f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(80, 140),
                        imageHandle = Image.particleDot3,
                        vy = (float)Math.Sin(angle) * speed,
                        vx = (float)Math.Cos(angle) * speed,
                        damp = 0.95f,
                        forceX = MyRandom.Range(0.002f, 0.006f),
                        forceY = MyRandom.Range(0.016f, 0.027f),
                        startScale = MyRandom.Range(0.2f, 0.26f),
                        endScale = 0f,
                        startAlpha = 255,
                        endAlpha = 0,
                        red   = color[colorPattern * 2, 0],
                        green = color[colorPattern * 2, 1],
                        blue  = color[colorPattern * 2, 2],
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
            for (int i = 0; i < 700; i++)
            {
                float angle = MyRandom.PlusMinus(3.1415f);
                float speed = MyRandom.Range(0f, 6f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(80, 120),
                        imageHandle = Image.particleDot3,
                        vy = (float)Math.Sin(angle) * speed,
                        vx = (float)Math.Cos(angle) * speed,
                        damp = 0.95f,
                        forceX = MyRandom.Range(0.002f, 0.006f),
                        forceY = MyRandom.Range(0.016f, 0.027f),
                        startScale = MyRandom.Range(0.2f, 0.26f),
                        endScale = 0f,
                        startAlpha = 255,
                        endAlpha = 0,
                        red   = color[colorPattern * 2 + 1, 0],
                        green = color[colorPattern * 2 + 1, 1],
                        blue  = color[colorPattern * 2 + 1, 2],
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void Stone(float x, float y)
        {
            for (int i = 0; i < 7; i++)
            {
                float scale = MyRandom.Range(0.02f, 0.08f);

                particles.Add(new Particle()
                {
                    x = x + MyRandom.PlusMinus(4),
                    y = y + MyRandom.PlusMinus(3),
                    lifeSpan = MyRandom.Range(30, 40),
                    vy = -MyRandom.Range(2f, 4.5f),
                    vx = MyRandom.PlusMinus(2.3f),
                    imageHandle = Image.particleStone1,
                    forceY = 0.13f,
                    angle = MyRandom.PlusMinus(MyMath.PI),
                    angularVelocity = MyRandom.PlusMinus(0.3f),
                    angularDamp = 0.985f,
                    startScale = scale,
                    endScale = scale,
                    endAlpha = 0,
                    red = 255,
                    green = 200,
                    blue = 180,
                });
            }

            for (int i = 0; i < 20; i++)
            {
                particles.Add(new Particle()
                {
                    x = x + MyRandom.PlusMinus(5),
                    y = y + MyRandom.PlusMinus(2),
                    lifeSpan = MyRandom.Range(15, 40),
                    imageHandle = Image.particleSmoke,
                    vx = MyRandom.PlusMinus(3f),
                    vy = MyRandom.PlusMinus(0.7f) + -0.5f,
                    damp = 0.93f,
                    forceY = 0.02f,
                    startScale = 0.15f,
                    endScale = 0.3f,
                    startAlpha = 170,
                    endAlpha = 0,
                    angle = MyRandom.PlusMinus(3.14f),
                    angularVelocity = MyRandom.PlusMinus(0.05f),
                    angularDamp = 0.98f,
                    red = 255,
                    green = 200,
                    blue = 180,
                });
            }
        }

        public void Heal(float x, float y)
        {
            particles.Add(new Particle()
            {
                x = x,
                y = y,
                lifeSpan = 25,
                imageHandle = Image.particleRing2,
                startScale = 0.1f,
                endScale = 0.4f,
                startAlpha = 180,
                endAlpha = 0,
                blendMode = DX.DX_BLENDMODE_ADD,
                red = 170,
                green = 255,
                blue = 170
            });
            particles.Add(new Particle()
            {
                x = x,
                y = y,
                lifeSpan = 12,
                imageHandle = Image.particleRing2,
                startScale = 0.2f,
                endScale = 0.6f,
                startAlpha = 150,
                endAlpha = 0,
                blendMode = DX.DX_BLENDMODE_ADD,
                red = 170,
                green = 255,
                blue = 170
            });

            for (int i = 0; i < 20; i++)
            {
                particles.Add(new Particle()
                {
                    x = x + MyRandom.PlusMinus(20),
                    y = y + MyRandom.PlusMinus(20),
                    vx = MyRandom.PlusMinus(0.8f),
                    vy = MyRandom.Range(-0.3f, 0.9f),
                    forceY = -0.06f,
                    damp = 0.98f,
                    lifeSpan = MyRandom.Range(10, 45),
                    imageHandle = Image.particleGlitter1,
                    startScale = 0.05f,
                    endScale = MyRandom.Range(0.4f, 0.8f),
                    startAlpha = 255,
                    endAlpha = 0,
                    blendMode = DX.DX_BLENDMODE_ADD,
                    red = 170,
                    green = 255,
                    blue = 170
                });
            }

            for (int i = 0; i < 20; i++)
            {
                particles.Add(
                    new Particle()
                    {
                        x = x + MyRandom.PlusMinus(30),
                        y = y  + 30 + MyRandom.PlusMinus(30),
                        lifeSpan = MyRandom.Range(20, 40),
                        imageHandle = Image.particleLine1,
                        vy = -MyRandom.Range(1f, 5f),
                        vx = 0,
                        forceY = -0.07f,
                        damp = 1f,
                        startScale = 0.3f,
                        endScale = 0.15f,
                        startAlpha = 255,
                        endAlpha = 0,
                        angle = 90f * MyMath.Deg2Rad,
                        blendMode = DX.DX_BLENDMODE_ADD,
                        red = 170,
                        green = 255,
                        blue = 170
                    });
            }
        }

        public void Charge(float x, float y)
        {
            float angle = MyRandom.PlusMinus(MyMath.PI);
            float distance = MyRandom.Range(20f, 80f);
            float distanceX = (float)Math.Cos(angle) * distance;
            float distanceY = (float)Math.Sin(angle) * distance;
            int lifeSpan = MyRandom.Range(15, 40);

            particles.Add(new Particle() {
                x = x + distanceX,
                y = y + distanceY,
                lifeSpan = lifeSpan,
                imageHandle = Image.particleDot2,
                vx = -distanceX / lifeSpan,
                vy = -distanceY / lifeSpan,
                startScale = MyRandom.Range(0.13f, 0.25f),
                endScale = 0.0f,
                startAlpha = 0,
                endAlpha = 255,
                angle = angle,
            });
        }

        public void Claw(float x, float y)
        {
            float angle = 70f * MyMath.Deg2Rad;
            float speed = 6f;
            float vy = (float)Math.Sin(angle) * speed;
            float vx = (float)Math.Cos(angle) * speed;

            particles.Add(
                new Particle()
                {
                    x = x - vx * 3f,
                    y = y - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    vy = vy,
                    vx = vx,
                    damp = 0.98f,
                    startScale = 0.7f,
                    endScale = 0.6f,
                    startAlpha = 255,
                    endAlpha = 0,
                    angle = angle,
                    red = 255,
                    green = 0,
                    blue = 0,
                });

            particles.Add(
                new Particle()
                {
                    x = x + (float)Math.Cos(angle + 90f * MyMath.Deg2Rad) * 15f - vx * 3f,
                    y = y + (float)Math.Sin(angle + 90f * MyMath.Deg2Rad) * 15f - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    vy = vy,
                    vx = vx,
                    damp = 0.98f,
                    startScale = 0.55f,
                    endScale = 0.45f,
                    startAlpha = 255,
                    endAlpha = 0,
                    angle = angle,
                    red = 255,
                    green = 0,
                    blue = 0,
                });

            particles.Add(
                new Particle()
                {
                    x = x + (float)Math.Cos(angle - 90f * MyMath.Deg2Rad) * 15f - vx * 3f,
                    y = y + (float)Math.Sin(angle - 90f * MyMath.Deg2Rad) * 15f - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    vy = vy,
                    vx = vx,
                    damp = 0.98f,
                    startScale = 0.55f,
                    endScale = 0.45f,
                    startAlpha = 255,
                    endAlpha = 0,
                    angle = angle,
                    red = 255,
                    green = 0,
                    blue = 0,
                });

            for (int i = 0; i < 7; i++)
            {
                float angle2 = MyRandom.PlusMinus(3.1415f);
                float speed2 = MyRandom.Range(2f, 6f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(7, 12),
                        imageHandle = Image.particleLine2,
                        vy = (float)Math.Sin(angle2) * speed2,
                        vx = (float)Math.Cos(angle2) * speed2,
                        damp = 1f,
                        startScale = 0.3f,
                        endScale = 0.45f,
                        startAlpha = 255,
                        endAlpha = 0,
                        angle = angle2,
                        blendMode = DX.DX_BLENDMODE_ADD,
                        red = 255,
                        green = 150,
                        blue = 40,
                    });
            }

            for (int i = 0; i < 45; i++)
            {
                float angle3 = MyRandom.PlusMinus(3.1415f);
                float speed3 = MyRandom.Range(0.3f, 4f);

                particles.Add(
                    new Particle()
                    {
                        x = x,
                        y = y,
                        lifeSpan = MyRandom.Range(10, 45),
                        imageHandle = Image.particleDot1,
                        vy = (float)Math.Sin(angle3) * speed3,
                        vx = (float)Math.Cos(angle3) * speed3,
                        forceY = 0.03f,
                        damp = 0.91f,
                        startScale = MyRandom.Range(0.04f, 0.2f),
                        endScale = 0.05f,
                        startAlpha = 255,
                        endAlpha = 0,
                        red = 255,
                        green = 0,
                        blue = 0,
                    });
            }
        }

        //public void BreakWall(float x, float y)
        //{
        //    particles.Add(
        //        new Particle()
        //        {
        //            x = x,
        //            y = y,
        //            lifeSpan = MyRandom.Range(40, 70),
        //            imageHandle = Image.square,
        //            vy = MyRandom.Range(-4f, -7f),
        //            vx = MyRandom.PlusMinus(1.5f),
        //            forceY = 0.15f,
        //            startScale = 1f,
        //            endScale = 1f,
        //            red = 200,
        //            green = 200,
        //            blue = 200,
        //        });
        //}
    }
}
