using Raylib_cs;
using System.Numerics;

namespace Asteroids
{
    public class Sprite
    {
        Texture2D image;
        Rectangle drawSource;
        public Vector2 origin;

        public Sprite(Texture2D image)
        {
            this.image = image;
            drawSource = new Rectangle(0, 0, image.Width, image.Height);
            origin = new Vector2(image.Width / 2, image.Height / 2);
        }
        public void Draw(Vector2 position, float rotationRadians)
        {
            float rotationDegrees = Raylib.RAD2DEG * rotationRadians;

            Rectangle drawDestination = new Rectangle(position, image.Width, image.Height);

            Raylib.DrawTexturePro(this.image,
              drawSource, drawDestination,
              origin, rotationDegrees, Color.White
              );
        }
    }
}
