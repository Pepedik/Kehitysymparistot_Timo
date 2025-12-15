using RayGuiCreator;
using Raylib_cs;
namespace ValikkoPeli
{
    internal class PauseMenu
    {
        public event EventHandler Resume;
        public event EventHandler Options;
        public void Draw()
        {

            MenuCreator creater = new MenuCreator(10, 10, 16, 300);
            if (creater.Button("Resume"))
            {
                Resume.Invoke(this, new EventArgs());
            }
            if (creater.Button("Options"))
            {
                Options.Invoke(this, new EventArgs());
            }

        }
    }
}
