using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Asteroids
{


    internal class Ship
    {
        private bool isEngineOn;
        public float angle = 0f;
        public TransformComponent transform;
        public Sprite image;
        private float scale = 5.0f;
        public float MaxSpeed = 150;
        public float Speed;
        public Vector2 Dir;
        public int hp = 5;

        public Ship()
        {
            transform = new TransformComponent(new Vector2(450, 300), Vector2.Zero);
 
        }
        public bool IsShooting = false;
        
        /// <summary>
        /// Handles moving and shooting of the ship.
        /// </summary>
        public void Update()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsShooting = true;

            }
            else
            {
                IsShooting = false;
            }

            float deltaTime = Raylib.GetFrameTime();
            float turnSpeed = 2.0f * deltaTime;
            float acceleration = 100.0f * deltaTime;

            if (Raylib.IsKeyDown(KeyboardKey.A))
                angle += turnSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.D))
                angle -= turnSpeed;

            Vector2 direction = new Vector2(0, -1);

            Matrix4x4 rotation = Matrix4x4.CreateRotationZ(angle);

            Vector2 rotatedDirection = Vector2.Transform(direction, rotation);

            Dir = rotatedDirection = Vector2.Normalize(rotatedDirection);

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                isEngineOn = true;
                transform.velocity += rotatedDirection * acceleration;
                if (transform.velocity.Length() >= MaxSpeed)
                {
                    transform.velocity = Vector2.Normalize(transform.velocity) * MaxSpeed;
                }

                Vector2 flamePos = transform.Pos - rotatedDirection * 10f * scale;

                Vector2 perp = new Vector2(-rotatedDirection.Y, rotatedDirection.X);
                float flameWidth = 2.5f * scale;
                float flameLength = 5f * scale;

                Vector2 A = flamePos;
                Vector2 B = flamePos + perp * flameWidth + rotatedDirection * flameLength;
                Vector2 C = flamePos - perp * flameWidth + rotatedDirection * flameLength;

                Raylib.DrawTriangleLines(A, B, C, Color.Orange);
            }
            else
            {
                isEngineOn = false;
                transform.velocity *= 1f;
                if (transform.velocity.Length() >= MaxSpeed)
                {
                    transform.velocity = Vector2.Normalize(transform.velocity) * MaxSpeed;
                }
            }

            transform.Pos += transform.velocity * deltaTime;

            transform.Loop();


        }


        public void Draw()
        {
            Vector2[] shipShape = new Vector2[]
            {
            new Vector2(0, -10),
            new Vector2(-5, 5),
            new Vector2(5, 5)
            };

            Matrix4x4 rotation = Matrix4x4.CreateRotationZ(angle);

            Vector2[] transformedShape = new Vector2[shipShape.Length];

            for (int i = 0; i < shipShape.Length; i++)
            {
                Vector2 scaledPoint = shipShape[i] * scale;
                Vector2 rotatedPoint = Vector2.Transform(scaledPoint, rotation);
                transformedShape[i] = rotatedPoint + transform.Pos;
            }
            image.Draw(transform.Pos, angle);
        }
    }
}