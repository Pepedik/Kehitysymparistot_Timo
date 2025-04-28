using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TANKS
{
    internal class Bullet : Program.Tank
    {
        int BulletH = 10;
        int BulletW = 10;
        float bulletSpeed = 600f;
        public Bullet(float X, float Y, Vector2 Direction) : base((int)X, (int)Y)
        {
            CurrentSpeed = bulletSpeed;
            this.Direction = Direction;
        }
        public void draw()
        {
            {
                Raylib.DrawRectangle((int)X, (int)Y, BulletH, BulletW, Color.White);
            }
        }
    }
}