using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Asteroids
{
    internal class Program
    {
        Ship Player;
        List<Asteroids> asteroids;
        List<Bullet> bullets;
        Texture2D Lasers;
        List<Texture2D> AasteroidImage;
        public float timer = 0;
        public float Score;
        public static Vector2 RandomDir()
        {
            Random R = new Random();
            Vector2 RandomDirection = new Vector2(
                R.NextSingle() * 2.0f - 1.0f,
                R.NextSingle() * 2.0f - 1.0f);
            RandomDirection = Vector2.Normalize(RandomDirection);
            return RandomDirection;

        }

        static void Main(string[] args)
        {
            Program game = new Program();
            game.Initiate();
            game.run();
            
        }
        void StartLevel()
        {
            asteroids.Clear();
            bullets.Clear();

            for (int i = 0; i < 10; i += 1)
            {
                Vector2 startpos;

                startpos = new Vector2(Raylib.GetScreenWidth() / 2
                    , Raylib.GetScreenHeight() / 2);

                Vector2 dir = RandomDir();

                startpos += dir * 400;

                asteroids.Add(new Asteroids(startpos, 2, AasteroidImage[2]));
            }
        }
        void run() 
        { 
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Player.Draw();
                Player.Update();
                timer -= Raylib.GetFrameTime();

                for (int i = 0; i < asteroids.Count; i += 1)
                {
                    Asteroids a = asteroids[i];
                    a.Update();
                    a.Draw();
                }
               
                
                if(Player.IsShooting == true)
                {
                    bullets.Add(new Bullet(Player.transform.Pos, Player.Dir * 250, Lasers, Player.angle));
                }
                
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].draw();
                    bullets[i].Update();

                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    for (int j = 0; j < asteroids.Count; j++)
                    {
                        Bullet bullet = bullets[i];
                        Asteroids asteroid = asteroids[j];
                        if (Raylib.CheckCollisionPointCircle(bullet.transform.Pos, asteroid.transform.Pos, asteroid.rad))
                        {
                            if (asteroid.size > 0)
                            {
                                asteroids.Add(new Asteroids(asteroid.transform.Pos, asteroid.size -1, AasteroidImage[asteroid.size -1]));
                                asteroids.Add(new Asteroids(asteroid.transform.Pos, asteroid.size -1, AasteroidImage[asteroid.size -1]));
                            }
                            Score += 100;

                            asteroids.Remove(asteroid); 
                            bullets.Remove(bullet);
                            break;
                        }
                    }
                }
                for (int i = 0; i < asteroids.Count; i++)
                {
                    Asteroids asteroid = asteroids[i];
                    if (Raylib.CheckCollisionPointCircle(Player.transform.Pos, asteroid.transform.Pos, asteroid.rad))
                    {
                        if (timer <= 0)
                        {
                            Player.hp -= 1;
                            timer = 1.0f;
                        }
                        if (Player.hp <= 0)
                        {
                            StartLevel();
                        }
                        asteroids.Remove(asteroid);
                        break;
                    }
                }
                Raylib.DrawText($"{Player.hp}", 100, 100, 30, Color.White);
                Raylib.DrawText($"{Score}", 150, 100, 30, Color.White);


                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }

        void Initiate()
        {
            Raylib.InitWindow(1000, 700, "Asteroids");
            Player = new Ship();
            asteroids = new List<Asteroids>();
            Player.image = new Sprite(Raylib.LoadTexture("Data/Images/playerShip1_blue.png"));

            AasteroidImage = new List<Texture2D>()
            {
                Raylib.LoadTexture("Data/Images/meteorGrey_small1.png"),
                Raylib.LoadTexture("Data/Images/meteorBrown_med1.png"),
                Raylib.LoadTexture("Data/Images/meteorGrey_big3.png")
            };

            Lasers = Raylib.LoadTexture("Data/Images/laserBlue01.png");
            bullets = new List<Bullet>();

            StartLevel();

        }
    }
}
