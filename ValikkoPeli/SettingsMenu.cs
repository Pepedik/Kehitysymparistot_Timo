using RayGuiCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValikkoPeli
{
    internal class SettingsMenu
    {
        public event EventHandler Back;
        public event EventHandler Red;
        public event EventHandler Blue;
        public void Draw()
        {
            MenuCreator creater = new MenuCreator(10, 10, 16, 300);
            if (creater.Button("Back"))
            {
                Back.Invoke(this, new EventArgs());
            }
            if (creater.Button("Red"))
            {
                Red.Invoke(this, new EventArgs());
            }
            if (creater.Button("Blue"))
            {
                Blue.Invoke(this, new EventArgs());
            }

        }

    }
}
