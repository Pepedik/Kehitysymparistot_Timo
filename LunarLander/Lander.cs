using Raylib_cs;
using System.Numerics;

namespace LunarLander
{
    internal class Lander
    {
        string Lose = "";
        Ship Player;
        Vector2 OldPos;
        static void Main(string[] args)
        {
            Lander game = new Lander();
            game.init();
            game.GameLoop();
        }
        void init()
        {
            Raylib.InitWindow(1000, 700, "LunarLander");
            Player = new Ship();
        }
        void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                Draw();
                Update();
            }
            Raylib.CloseWindow();
        }


        void Update()
        {
            OldPos = new Vector2(Player.PlayerCenter.X, Player.PlayerPos);

            Raylib.GetFrameTime();
            Player.Update();

            if (Raylib.CheckCollisionRecs(new Rectangle(0, 675, 1000, 30), Player.Collision))
            {
                Player.PlayerCenter.X = OldPos.X;
                Player.PlayerPos = OldPos.Y;
                Player.velocity = 0;
                if (Player.velocity > 15)
                {
                    Lose = "You lost";
                }
                if (Player.velocity < 15)
                {
                    Lose = "You won";
                }
            }
        }
        void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Player.Draw();

            Raylib.DrawRectangle(0, 675, 1000, 30, Color.White);

            Raylib.DrawText(Lose, 450, 350, 32, Color.White);
            Raylib.DrawText(Player.velocity.ToString(), 10, 30, 24, Color.White);

            Raylib.EndDrawing();
        }

    }
}

