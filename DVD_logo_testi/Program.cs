using System.Numerics;
using Raylib_cs;

namespace DVD_logo_testi
{
    internal class Program
    {
        
        
        
        static void Main(string[] args)
        {
            string logo = "DVD";
            Vector2 Dlogo = new Vector2(1, 0);
            Vector2 Adir = new Vector2(1, 1);

            int width = 800;
            int height = 800;

            float speed = 100.0f;

            Raylib.InitWindow(width, height, "DVD_Logo");
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Vector2 TextSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), logo, 32, 2);
                Raylib.DrawText(logo, (int)Dlogo.X, (int)Dlogo.Y, 32, Color.Yellow);

                Raylib.EndDrawing();

                Dlogo = Dlogo + Adir * speed * Raylib.GetFrameTime();

                if (Dlogo.X < 0 || Dlogo.X + TextSize.X > width)
                {
                    Adir.X *= -1;
                }
                if (Dlogo.Y < 0 || Dlogo.Y + TextSize.Y > height)
                {
                    Adir.Y *= -1;
                }

            }
            Raylib.CloseWindow();
        }

    }
}
