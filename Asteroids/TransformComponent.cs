using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class TransformComponent
    {
        public Vector2 Pos;
        public Vector2 velocity;
        public TransformComponent(Vector2 pos, Vector2 Velocity)
        {  
            Pos = pos;
            velocity = Velocity;
        }
        public void Loop()
        {
            int screenW = Raylib.GetScreenWidth();
            int screenH = Raylib.GetScreenHeight();

            if (Pos.X > screenW) Pos.X = 0;
            if (Pos.X < 0) Pos.X = screenW;
            if (Pos.Y > screenH) Pos.Y = 0;
            if (Pos.Y < 0) Pos.Y = screenH;
        }
        public void Update()
        {
            Pos += velocity * Raylib.GetFrameTime();
        }
    }
}
