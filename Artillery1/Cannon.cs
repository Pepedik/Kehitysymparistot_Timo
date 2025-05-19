using Raylib_cs;
using System.Numerics;

namespace Artillery1
{
    internal class Cannon
    {
        Vector2 bullet;
        float MaxbulletSpeed = 350;
        Vector2 bulletVelocity;

        float CannonDir = 0;
        Color CannonColor;
        float Power = 0;
        float MaxPower = 100;
        bool IsMouseDown = false;
        bool IsIncreasing = false;

        public Bullet BulletType;

        public Vector2 Grav()
        {
            if (BulletType != null)
            {
                Vector2 bulletGravity = new Vector2(0, BulletType.Gravity);
                return bulletGravity;
            }
            else
            {
                return Vector2.Zero;
            }
        }

        Color Colors(int[] array)
        {
            Color BulletColor;
            BulletColor.R = (byte)array[0];
            BulletColor.G = (byte)array[1];
            BulletColor.B = (byte)array[2];
            BulletColor.A = (byte)array[3];
            return BulletColor;
        }

        public Cannon(Color color)
        {
            CannonColor = color;
        }
        public void draw(Vector2 Pos)
        {
            int[] color = new int[4] { 0, 255, 0, 255 };
            Raylib.DrawRectanglePro(new Rectangle(Pos, new Vector2(100, 20)), new Vector2(0, 10), CannonDir, CannonColor);
            if (BulletType != null)
            {
                Raylib.DrawCircleLinesV(bullet, 3, Colors(BulletType.Color));
            }
        }
        public void BulletUpdate()
        {
            bulletVelocity += Grav() * Raylib.GetFrameTime();
            bullet += bulletVelocity * Raylib.GetFrameTime();
        }
        public Vector2 GetBullet()
        {

            return bullet;
        }
        public void ResetBullet()
        {
            bullet = new Vector2(0, Raylib.GetScreenHeight() + 10);
            bulletVelocity = Vector2.Zero;
        }
        public bool Update(Vector2 pos, KeyboardKey TurnLeft, KeyboardKey TurnRight, Sound shootSound)
        {


            if (Raylib.IsKeyDown(TurnLeft))
            {
                CannonDir -= 90 * Raylib.GetFrameTime();
            }
            if (Raylib.IsKeyDown(TurnRight))
            {
                CannonDir += 90 * Raylib.GetFrameTime();
            }

            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                IsMouseDown = true;
                float powerChange = 30 * Raylib.GetFrameTime();
                if (IsMouseDown == true)
                {
                    if (Power > MaxPower)
                    {
                        Power = MaxPower;
                        IsIncreasing = false;
                    }
                    else if (Power <= 0)
                    {
                        Power = 0;
                        IsIncreasing = true;
                    }
                    if (IsIncreasing)
                    {
                        Power += powerChange;
                    }
                    else
                    {
                        Power -= powerChange;
                    }
                    Power += IsIncreasing ? powerChange : -powerChange;
                    Raylib.DrawRectangle(500, 350 - (int)Power, 10, (int)Power, Color.Lime);
                }

            }
            if (IsMouseDown == true && Raylib.IsMouseButtonReleased(MouseButton.Left))
            {

                Matrix3x2 Rotation = Matrix3x2.CreateRotation(CannonDir * Raylib.DEG2RAD);
                Vector2 Dir = Vector2.Transform(Vector2.UnitX, Rotation);
                bulletVelocity = Dir * Power * (float)5;
                bullet = pos + Dir * 100;
                Raylib.PlaySound(shootSound);
                Power = 0;
                IsMouseDown = false;
                BulletType = Bullet.AmmoTypes[Bullet.currentAmmoIndex];
                return true;
            }
            return false;
        }
    }
}
