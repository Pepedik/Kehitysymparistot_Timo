using System.Numerics;
using Raylib_cs;

namespace Reylib_Testi

{
    internal class Program
    {
        static void Main(string[] args)
        {
            int height = 800;
            int width = 800;
            Vector2 A = new Vector2(width/ 2, 0);
            Vector2 B = new Vector2(0, height / 2);
            Vector2 C = new Vector2(width, height * 3 / 4);

            Vector2 Amove = new Vector2(1, 1);
            Vector2 Bmove = new Vector2(1, -1);
            Vector2 Cmove = new Vector2(-1, -1);
            

            Raylib.InitWindow(width, height, "Raylibtesti");
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                Raylib.DrawLineV(A, B, Color.Green);
                Raylib.DrawLineV(B, C, Color.Yellow);
                Raylib.DrawLineV(C, A, Color.SkyBlue);
                Raylib.EndDrawing();

                A = A + Amove * 100 * Raylib.GetFrameTime();
                B = B + Bmove * 100 * Raylib.GetFrameTime();
                C = C + Cmove * 100 * Raylib.GetFrameTime();

                if (A.X>=width || A.X <= 0)
                {
                    Amove.X = Amove.X * -1;
                }
                if (B.X >= width || B.X <= 0)
                {
                    Bmove.X = Bmove.X * -1;
                }
                if (C.X >= width || C.X <= 0)
                {
                    Cmove.X = Cmove.X * -1;
                }
                
                if (A.Y >= height || A.Y <= 0)
                {
                    Amove.Y = Amove.Y * -1;
                }
                if (B.Y >= height || B.Y <= 0)
                {
                    Bmove.Y = Bmove.Y * -1;
                }
                if (C.Y >= height || C.Y <= 0)
                {
                    Cmove.Y = Cmove.Y * -1;
                }

            }
            Raylib.CloseWindow();
        }
    }
}
