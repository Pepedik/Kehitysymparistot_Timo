using Raylib_cs;
using System.Numerics;

namespace TANKS

{
    internal class Program
    {
        public class Tank
        {
            public int currentHP = 0;
            public int TankHP = 5;
            public int MaxHP = 5;
            public int Score = 0;
            public Bullet bullet;
            public float CurrentSpeed;
            public float X, Y;
            public Vector2 Direction = new Vector2();
            public Rectangle GetCollision()
            {
                return new Rectangle(X, Y, 50, 50);
            }
            public void shoot()
            {
                bullet = new Bullet(X, Y, Direction);
            }
            public Tank(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public void Update()
            {
                Y += Direction.Y * CurrentSpeed * Raylib.GetFrameTime();
                X += Direction.X * CurrentSpeed * Raylib.GetFrameTime();
                if (bullet != null)
                {
                    bullet.Update();
                }
                if (X < 0) X = 0;
                if (Y < 0) Y = 0;
                if (X > 1000 - 50) X = 1000 - 50;
                if (Y > 700 - 50) Y = 700 - 50;
            }

            public void drawtank(Color TColor, Color DColor)
            {

                int WheelW = 10;
                int WheelH = 85;
                int CannonW = 15;
                int CannonH = 45;
                int TankWidth = 50;
                int TankHeight = 50;
                int x = (int)(X);
                int y = (int)(Y);
                if (Direction.Y != 0)
                {
                    Raylib.DrawRectangle((int)x, (int)y, TankWidth, TankHeight, TColor);
                    Raylib.DrawRectangle(x + TankWidth / 2 - CannonW / 2, y + (int)Direction.Y * CannonH / 2 + CannonW / 4, CannonW, CannonH, DColor);
                    Raylib.DrawRectangle(x - WheelW, y - (WheelH - TankHeight) / 2, WheelW, WheelH, DColor);
                    Raylib.DrawRectangle(x + TankWidth, y - (WheelH - TankHeight) / 2, WheelW, WheelH, DColor);

                }
                else if (Direction.X != 0)
                {
                    Raylib.DrawRectangle((int)x, (int)Y, TankWidth, TankHeight, TColor);
                    Raylib.DrawRectangle(x + (int)Direction.X * CannonH / 2 + TankWidth / 2 - CannonH / 2,
                        y - CannonW / 2 + TankHeight / 2,
                        CannonH, CannonW, DColor);
                    Raylib.DrawRectangle(x - (WheelH - TankWidth) / 2, y - WheelW,
                        WheelH, WheelW, DColor);
                    Raylib.DrawRectangle(x - (WheelH - TankWidth) / 2, y + TankHeight,
                        WheelH, WheelW, DColor);
                }
                if (bullet != null)
                {
                    bullet.draw();
                }
            }
        }
        static void Main(string[] args)
        {
            Vector2 OldPos;
            Vector2 OldPos2;

            Vector2 StartPos;
            Vector2 StartPos2;

            int Width = 1000;
            int Height = 700;

            float Player1X = 100;
            float Player1Y = 350;
            float Player2X = 825;
            float Player2Y = 350;

            Tank Player1 = new Tank((int)Player1X, (int)Player1Y);
            Tank Player2 = new Tank((int)Player2X, (int)Player2Y);

            float Speed = 200;
            float CurrentSpeed = 0;
            float CurrentSpeed1 = 0;

            StartPos = new Vector2(Player1X,Player1Y);
            StartPos2 = new Vector2(Player2X, Player2Y);


            Raylib.InitAudioDevice();
            Raylib.SetMasterVolume(0.1f);
            Raylib.GetMasterVolume();
            Sound sound = Raylib.LoadSound("Explosion.wav");
            Music BgMusic = Raylib.LoadMusicStream("astral-creepy-dark-logo-254198.mp3");

            Raylib.PlayMusicStream(BgMusic);

            Raylib.InitWindow(Width, Height, "tanks");
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.DrawRectangle(450, 190, 50, 300, Color.Orange);
                
                Raylib.DrawText("" + Player1.TankHP, 10, 1, 32, Color.Yellow);
                Raylib.DrawText("" + Player2.TankHP, 970, 1, 32, Color.Yellow);
                Raylib.DrawText("" + Player1.Score, 10, 30, 32, Color.Red);
                Raylib.DrawText("" + Player2.Score, 970, 30, 32, Color.Red);

                if (Player1.bullet != null)
                {
                    if (Raylib.CheckCollisionRecs(Player1.bullet.GetCollision(), Player2.GetCollision()))
                    {

                        Player2.TankHP -= 1;
                        
                        if (Player2.TankHP < 0)
                        {
                            Raylib.PlaySound(sound);
                            Player2.TankHP = Player2.currentHP;
                        }
                        Player1.bullet = null;
                        if (Player2.TankHP == 0)
                        {
                            Player2.TankHP = Player2.MaxHP;
                            Player1.TankHP = Player1.MaxHP;

                            Player1.X = StartPos.X;
                            Player1.Y = StartPos.Y;
                            Player2.X = StartPos2.X;
                            Player2.Y = StartPos2.Y;
                            Player1.Score += 10;
                        }
                    }
                }
                if (Player2.bullet != null)
                {
                    if (Raylib.CheckCollisionRecs(Player2.bullet.GetCollision(), Player1.GetCollision()))
                    {
                        Player1.TankHP -= 1;
                       
                        if (Player1.TankHP < 0)
                        {
                            Raylib.PlaySound(sound);
                            Player1.TankHP = Player1.currentHP;
                        }
                        Player2.bullet = null;
                        if (Player1.TankHP == 0)
                        {
                            Player1.TankHP = Player1.MaxHP;
                            Player2.TankHP = Player2.MaxHP;

                            Player1.X = StartPos.X;
                            Player1.Y = StartPos.Y;
                            Player2.X = StartPos2.X;
                            Player2.Y = StartPos2.Y;
                            Player2.Score += 10;
                        }
                    }
                }


                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);



                Player1.drawtank(Color.Blue, Color.DarkBlue);
                Player2.drawtank(Color.Purple, Color.DarkPurple);
                CurrentSpeed = 0;
                CurrentSpeed1 = 0;
                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    Player1.Direction.Y = -1;
                    CurrentSpeed = Speed;
                    Player1.Direction.X = 0;

                }
                if (Raylib.IsKeyDown(KeyboardKey.S))
                {
                    Player1.Direction.Y = 1;
                    CurrentSpeed = Speed;
                    Player1.Direction.X = 0;

                }
                if (Raylib.IsKeyDown(KeyboardKey.D))
                {
                    Player1.Direction.X = 1;
                    CurrentSpeed = Speed;
                    Player1.Direction.Y = 0;

                }
                if (Raylib.IsKeyDown(KeyboardKey.A))
                {
                    Player1.Direction.X = -1;
                    CurrentSpeed = Speed;
                    Player1.Direction.Y = 0;

                }
                if (Raylib.IsKeyDown(KeyboardKey.Up))
                {
                    Player2.Direction.Y = -1;
                    CurrentSpeed1 = Speed;
                    Player2.Direction.X = 0;

                }
                if (Raylib.IsKeyDown(KeyboardKey.Down))
                {
                    Player2.Direction.Y = 1;
                    CurrentSpeed1 = Speed;
                    Player2.Direction.X = 0;

                }
                if (Raylib.IsKeyDown(KeyboardKey.Right))
                {
                    Player2.Direction.X = 1;
                    CurrentSpeed1 = Speed;
                    Player2.Direction.Y = 0;

                }
                if (Raylib.IsKeyDown(KeyboardKey.Left))
                {
                    Player2.Direction.X = -1;
                    CurrentSpeed1 = Speed;
                    Player2.Direction.Y = 0;

                }
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Player1.shoot();
                }
                if (Raylib.IsMouseButtonPressed(MouseButton.Right))
                {
                    Player2.shoot();
                }

                Player1.CurrentSpeed = CurrentSpeed;
                Player2.CurrentSpeed = CurrentSpeed1;

                OldPos = new Vector2 (Player1.X, Player1.Y);
                OldPos2 = new Vector2 (Player2.X, Player2.Y);

                Player1.Update();
                Player2.Update();

                
                if (Raylib.CheckCollisionRecs(new Rectangle(Player1.X, Player1.Y, 50, 50), new Rectangle(Player2.X, Player2.Y, 50, 50)))
                {
                    Player1.X = OldPos.X;
                    Player1.Y = OldPos.Y;
                    Player2.X = OldPos2.X;
                    Player2.Y = OldPos2.Y;
                }
                if (Raylib.CheckCollisionRecs(new Rectangle(450, 190, 50, 300), new Rectangle(Player2.X, Player2.Y, 50, 50)))
                {
                    Player2.X = OldPos2.X;
                    Player2.Y = OldPos2.Y;
                }
                if (Raylib.CheckCollisionRecs(new Rectangle(Player1.X, Player1.Y, 50, 50), new Rectangle(450, 190, 50, 300)))
                {   
                    Player1.X = OldPos.X;
                    Player1.Y = OldPos.Y;
                }
                if (Player1.bullet != null) 
                {
                    if (Raylib.CheckCollisionRecs(Player1.bullet.GetCollision(), new Rectangle(450, 190, 50, 300)))
                    {
                        Player1.bullet = null;
                    }
                }
                if (Player2.bullet != null)
                {
                    if (Raylib.CheckCollisionRecs(Player2.bullet.GetCollision(), new Rectangle(450, 190, 50, 300)))
                    {
                        Player2.bullet = null;
                    }
                }


                
                Raylib.EndDrawing();

                Raylib.UpdateMusicStream(BgMusic);
            }
            Raylib.CloseWindow();
        }
    }
}
