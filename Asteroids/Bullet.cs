using Raylib_cs;
using System.Numerics;

namespace Asteroids
{
    public class Bullet
    {
        Ship ship;
        public float BW = 10;
        public float BH = 10;

        public float BSpeed = 100;
        public float Angle = 0;

        public TransformComponent transform;
        public Sprite image;

        public Bullet(Vector2 pos, Vector2 Velocity, Texture2D Lasers, float angle)
        {
            transform = new TransformComponent(pos, Velocity);
            image = new Sprite(Lasers);
            this.Angle = angle;
        }
        public void draw()
        {
            image.Draw(transform.Pos, Angle);
        }  
        public void Update()
        {
            transform.Update();
            transform.Loop();
        }
        
    }
}

