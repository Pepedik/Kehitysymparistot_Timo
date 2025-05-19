using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Artillery1
{
    internal class GameBullet
    {
        private Bullet Type;
        public Vector2 Position;
        public Vector2 Direction;
        public GameBullet(Vector2 position, Vector2 direction, Bullet Type)
        {
            this.Position = position;
            this.Direction = direction;
            this.Type = Type;
        }
    }
}
