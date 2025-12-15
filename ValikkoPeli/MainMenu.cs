using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RayGuiCreator;

namespace ValikkoPeli
{
    internal class MainMenu
    {
        public event EventHandler PlayPressed;
        public event EventHandler SettingsPressed;
        public event EventHandler QuitPressed;

        public void Draw()
        {
            MenuCreator creater = new MenuCreator(10, 10, 16, 300);

            
            creater.Label("Main Menu");
            if (creater.Button("Play"))
            {
                PlayPressed.Invoke(this, new EventArgs());
            }
            if (creater.Button("Settings"))
            {
                SettingsPressed.Invoke(this, new EventArgs());
            }
            if (creater.Button("Quit"))
            {
                QuitPressed.Invoke(this, new EventArgs());
            }

        }
    }
}
