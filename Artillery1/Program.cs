using Raylib_cs;
using System.Numerics;

namespace Artillery1
{
    enum turns
    {
        red,
        blue
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            Bullet.LoadAmmoTypes();

            int currentPlayer = 1;
            turns Turn = turns.red;

            List<Rectangle> Maasto = new List<Rectangle>();
            int TerrainW = 32;

            Vehicle Red = new Vehicle(new Vector2(100, 650), Color.Red, KeyboardKey.A, KeyboardKey.D);
            Vehicle Blue = new Vehicle(new Vector2(900, 650), Color.Blue, KeyboardKey.Left, KeyboardKey.Right);

            Raylib.InitWindow(1000, 700, "Artillery");

            Raylib.InitAudioDevice();
            Raylib.SetMasterVolume(0.2f);
            Raylib.GetMasterVolume();

            Sound shootSound = Raylib.LoadSound("Explosion.wav");


            int paloja = Raylib.GetScreenWidth() / TerrainW;

            Random generator = new Random();
            int X = 0;
            int Y = 0;
            int height = 60;

            for (int i = 0; i < paloja; i++)
            {
                height = height + generator.Next(-20, 20);
                height = Math.Clamp(height, 0, 120);

                Y = Raylib.GetScreenHeight() - height;
                Maasto.Add(new Rectangle(X, Y, TerrainW, height));
                X += TerrainW;
            }

            Red.VehiclePos.Y = Maasto[(int)Red.VehiclePos.X / TerrainW].Y;
            Blue.VehiclePos.Y = Maasto[(int)Blue.VehiclePos.X / TerrainW].Y;
 

            while (Raylib.WindowShouldClose() == false)
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Red.draw();
                Blue.draw();
                Red.BulletUpdate();
                Blue.BulletUpdate();


                if (Raylib.IsKeyPressed(KeyboardKey.Down))
                {
                    Bullet.currentAmmoIndex = (Bullet.currentAmmoIndex + 1) % Bullet.AmmoTypes.Count; 
                }
                if (Raylib.IsKeyPressed(KeyboardKey.Up))
                {
                    Bullet.currentAmmoIndex = (Bullet.currentAmmoIndex - 1 + Bullet.AmmoTypes.Count) % Bullet.AmmoTypes.Count;
                }
                if (Turn == turns.red)
                {
                    bool shoot = Red.Update(true, shootSound);
                    if (shoot)
                    {
                        Turn = turns.blue;
                    }

                }
                else
                {
                    bool shoot = Blue.Update(true, shootSound);
                    if (shoot)
                    {
                        Turn = turns.red;
                    }
                }
                int Index = (int)Red.GetBullet().X / TerrainW;
                if (Index >= 0 && Index < Maasto.Count && Raylib.CheckCollisionPointRec(Red.GetBullet(), Maasto[Index]))
                {
                    Red.ResetBullet();
                    Raylib.PlaySound(shootSound);
                }
                Index = (int)Blue.GetBullet().X / TerrainW;
                if (Index >= 0 && Index < Maasto.Count && Raylib.CheckCollisionPointRec(Blue.GetBullet(), Maasto[Index]))
                {
                    Blue.ResetBullet();
                    Raylib.PlaySound(shootSound);
                }
                if (Raylib.CheckCollisionCircles(Red.GetBullet(), 3, Blue.VehiclePos, 50))
                {
                    Blue.TakeDMG(Red.GetDamage());
                    Red.ResetBullet();
                }
                if (Raylib.CheckCollisionCircles(Blue.GetBullet(), 3, Red.VehiclePos, 50))
                {
                    Red.TakeDMG(Blue.GetDamage());
                    Blue.ResetBullet();
                }
                if (Red.Hp() == 0)
                {
                    Raylib.DrawText($"Blue wins", 450, 350, 20, Color.Green);
                }
                if (Blue.Hp() == 0)
                {
                    Raylib.DrawText($"Red wins", 450, 350, 20, Color.Green);
                }

                Raylib.DrawText($"Player {Turn}'s turn", 20, 20, 20, Color.White);
                Raylib.DrawText($"Red HP: {Red.Hp()}", 20, 50, 20, Color.White);
                Raylib.DrawText($"Blue HP: {Blue.Hp()}", 875, 50, 20, Color.White);

                for (int i = 0; i < Bullet.AmmoTypes.Count; i++)
                {
                    Raylib.DrawText(Bullet.AmmoTypes[i].Name, 100, 100+20*i, 20, i == Bullet.currentAmmoIndex ? Color.Gold:Color.Green);
                }

                for (int i = 0; i < Maasto.Count; i++)
                {
                    Rectangle Piece = Maasto[i];

                    Raylib.DrawRectangleRec(Piece, Color.Green);

                }
                Raylib.EndDrawing();
            }
            Raylib.UnloadSound(shootSound);
            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();

        }

    }
}
