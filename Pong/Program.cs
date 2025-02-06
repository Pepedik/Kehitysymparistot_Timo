using System.Numerics;
using Raylib_cs;

namespace Pong
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int width = 1000;
            int height = 700;
            int RectWidth = width / 20;
            int RectHeight = height / 3;

            int PosCap = 20;
            int PosCap1 = 225;
            int PosCap2 = 929;
            int PosCap3 = 225;

            int player1P = 0;
            int player2P = 0;

            float speed = 500.0f;
            int RectSpeed = 1;

            Vector2 Ball = new Vector2(500, 350);
            Vector2 Direction = new Vector2(1, 1);

            Raylib.InitWindow(width, height, "Pong");
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                Raylib.DrawCircleV(Ball, 16, Color.Black);

                Raylib.DrawRectangle(PosCap, PosCap1, RectWidth, RectHeight, Color.Blue);
                Raylib.DrawRectangle(PosCap2, PosCap3, RectWidth, RectHeight, Color.Orange);

                Rectangle Maila1 = new Rectangle(PosCap, PosCap1, RectWidth, RectHeight);
                Rectangle Maila2 = new Rectangle(PosCap2, PosCap3, RectWidth, RectHeight);

                if (Raylib.CheckCollisionPointRec(Ball, Maila1))
                {
                    Direction.X *= -1;
                }
                if (Raylib.CheckCollisionPointRec(Ball, Maila2))
                {
                    Direction.X *= -1;
                }

                Raylib.DrawText("" + player1P, 17, 1, 32, Color.Yellow);
                Raylib.DrawText("" + player2P, 970, 1, 32, Color.Yellow);

                Ball = Ball + Direction * speed * Raylib.GetFrameTime();

                if (Ball.X >= width || Ball.X <= 0)
                {
                    if (Ball.X < 0)
                    {
                        player2P += 1;
                    }
                    else
                    {
                        player1P += 1;
                    }
                    Ball = new Vector2(500, 350);
                }
                if (Ball.Y >= height || Ball.Y <= 0)
                {
                    Direction.Y = Direction.Y * -1;
                }

                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    PosCap1 -= RectSpeed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.S))
                {
                    PosCap1 += RectSpeed;
                }
                PosCap1 = Math.Clamp(PosCap1, 0, height - RectHeight);

                if (Raylib.IsKeyDown(KeyboardKey.A))
                {
                    PosCap3 -= RectSpeed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.D))
                {
                    PosCap3 += RectSpeed;
                }
                PosCap3 = Math.Clamp(PosCap3, 0, height - RectHeight);
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }

    }
}