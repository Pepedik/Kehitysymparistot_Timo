using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace LunarLander
{
    internal class Ship
    {
        public float PlayerPos = 0;
        bool IsEngineOn = false;
        float motorpower = 0;
        float playerSpeed = 0;
        float MaxFuel = 70;
        float FuelConsumption = 2;
        float Grav = 10;
        public float velocity = 0;
        public Rectangle Collision = new Rectangle();
        public Vector2 PlayerCenter = new Vector2(450, 0);
        public void Update()
        {
            motorpower = 0;
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                if (MaxFuel > 0)
                {
                    IsEngineOn = true;

                    if (IsEngineOn == true)
                    {
                        Vector2 TuliA = new Vector2(40, 75 + PlayerPos) + PlayerCenter;
                        Vector2 TuliB = new Vector2(50, 100 + PlayerPos) + PlayerCenter;
                        Vector2 TuliC = new Vector2(60, 75 + PlayerPos) + PlayerCenter;

                        motorpower = -25;

                        MaxFuel -= FuelConsumption * Raylib.GetFrameTime();

                        Raylib.DrawTriangleLines(TuliA, TuliB, TuliC, Color.White);
                    }
                }
            }
            velocity += Grav * Raylib.GetFrameTime();
            velocity += motorpower * Raylib.GetFrameTime();
            PlayerPos += velocity * Raylib.GetFrameTime();
        }
        public void Draw()
        {
            Vector2 A = new Vector2(50 , 25 + PlayerPos) + PlayerCenter;
            Vector2 B = new Vector2(25, 75 + PlayerPos) + PlayerCenter;
            Vector2 C = new Vector2(75, 75 + PlayerPos) + PlayerCenter;

            Raylib.DrawTriangleLines(A, B, C, Color.White);

            Collision = new Rectangle(25 + (int)PlayerCenter.X, 25 + (int)PlayerPos, 75 - 25, 75 - 25);
            Raylib.DrawRectangleLines((int)Collision.X, (int)Collision.Y,(int)Collision.Width, (int)Collision.Height,  Color.Black);

            Raylib.DrawRectangle(10, 10 , (int) MaxFuel, 20, Color.White);
        }

    }
}
