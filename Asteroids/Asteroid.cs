using Raylib_cs;
using System.Drawing;
using System.Numerics;

namespace Asteroids
{
    internal class Asteroids
    {
        public TransformComponent transform;
        public Sprite image;
        public int size;
        public static Random rng = new Random();
        public float rad;

        /// <summary>
        /// This creates new asteroid.
        /// </summary>
        /// <param name="RandomPos">Random place</param>
        /// <param name="size">Size should be atleast 1</param>
        /// <param name="Texture">Texture has to match the size</param>
        public Asteroids(Vector2 RandomPos, int size, Texture2D Texture)
        {
            int screenW = Raylib.GetScreenWidth();
            int screenH = Raylib.GetScreenHeight();

            this.size = size;
            transform = new TransformComponent(RandomPos, Program.RandomDir()*80);

            image = new Sprite(Texture);
            rad = image.origin.X;
        }
        public void Draw()
        {
            image.Draw(transform.Pos, 0);
        }
        public void Update()
        {
            transform.Update();
            transform.Loop();
        }
    }
}
