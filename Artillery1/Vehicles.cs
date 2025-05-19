using Raylib_cs;
using System.Numerics;

namespace Artillery1
{
    internal class Vehicle
    {
        int HP = 5;
        float VehicleSpeed = 0;
        float fuel = 10;
        public Vector2 VehiclePos;
        Color Vehiclecolor;
        Cannon VehicleCannon;
        KeyboardKey TurnLeft;
        KeyboardKey TurnRight;
        
        public Vehicle(Vector2 Pos, Color C, KeyboardKey TurnLeft, KeyboardKey TurnRight)
        {
            VehiclePos = Pos;
            Vehiclecolor = C;
            VehicleCannon = new Cannon(C);
            this.TurnLeft = TurnLeft;
            this.TurnRight = TurnRight;
        }

        public void draw()
        {
            Raylib.DrawCircleV(VehiclePos, 50, Vehiclecolor);
            VehicleCannon.draw(VehiclePos);
        }

        public bool Update(bool isCurrentTurn, Sound shootSound)
        {
            if (isCurrentTurn)
            {
                return VehicleCannon.Update(VehiclePos, TurnLeft, TurnRight, shootSound);
            }

            return false;
        }
        public void BulletUpdate()
        {
            VehicleCannon.BulletUpdate();
        }
        public Vector2 GetBullet()
        {
            return VehicleCannon.GetBullet();
        }
        public int Hp()
        {
            return HP;
        }
        public void TakeDMG(int Amount)
        {
            HP -= Amount;
        }
        public void ResetBullet()
        {
            VehicleCannon.ResetBullet();
        }
        public int GetDamage()
        {
            return VehicleCannon.BulletType.DMG;        
        }
    }
}
