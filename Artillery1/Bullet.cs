using Newtonsoft.Json;

namespace Artillery1
{
    public class Bullet
    {
        public string Name;
        public float Gravity;
        public int DMG;
        public int[] Color;

        public static List<Bullet> AmmoTypes = new List<Bullet>();
        public static int currentAmmoIndex = 0;
        public Bullet()
        {

        }
        public Bullet(Bullet Copy)
        {
            this.Name = Copy.Name;
            this.Gravity = Copy.Gravity;
            this.DMG = Copy.DMG;
            this.Color = (int[])Copy.Color.Clone();
        }
        public static Bullet CreateCopy(Bullet Copy)
        {
            Bullet copy = new Bullet();
            if (Copy != null)
            {
                copy.Name = Copy.Name;
                copy.Gravity = Copy.Gravity;
                copy.DMG = Copy.DMG;
                copy.Color = Copy.Color;
            }
            return copy;
        }
        public Bullet CreateCopy()
        {
            return (Bullet)MemberwiseClone();
        }


       

        public static void LoadAmmoTypes()
        {
            AmmoTypes.Clear();
            string[] files = { "Smol.JSON", "Larg.JSON" }; 
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    string json = File.ReadAllText(file);
                    Bullet ammo = JsonConvert.DeserializeObject<Bullet>(json);
                    if (ammo != null)
                        AmmoTypes.Add(ammo);
                }
            }
        }
    }
}