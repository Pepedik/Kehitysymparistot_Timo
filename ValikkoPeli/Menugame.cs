using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using Asteroids;
using System.Security.Cryptography;

namespace ValikkoPeli
{
    enum State
    {
        MainMenu,
        Game,
        SettingsMenu,
        PauseMenu,

        Quit
    }

    internal class Menugame
    {
        Stack <State> current;

        MainMenu mainmenu;
        Asteroids.Program AsteroidsGame;
        SettingsMenu settingsMenu;
        PauseMenu pausemenu;

        void PlayPressed(object sender, EventArgs args)
        {
            current.Push(State.Game);
        }
        void SettingsPressed(object sender, EventArgs args)
        {
            current.Push(State.SettingsMenu);
        }
        void BackPressed(object sender, EventArgs args)
        {
            current.Pop();
        }
        void ResumePressed(object sender, EventArgs args)
        {
            current.Pop();
        }
        void QuitPressed(object sender, EventArgs args)
        {
            current.Push(State.Quit);
        }
        void BluePressed(object sender, EventArgs args)
        {
            AsteroidsGame.Skin(1);
        }
        void RedPressed(object sender, EventArgs args)
        {
            AsteroidsGame.Skin(2);
        }

        public void Run()
        {
            Raylib.InitWindow(1000, 700, "Valikko");

            current = new Stack <State>();
            current.Push(State.MainMenu);
            AsteroidsGame = new Asteroids.Program();
            AsteroidsGame.Initiate();

            Raylib.SetExitKey(0);

            mainmenu = new MainMenu();
            settingsMenu = new SettingsMenu();
            pausemenu = new PauseMenu();
            mainmenu.PlayPressed += this.PlayPressed;
            mainmenu.SettingsPressed += this.SettingsPressed;
            settingsMenu.Back += this.BackPressed;
            pausemenu.Resume += this.ResumePressed;
            pausemenu.Options += this.SettingsPressed;
            mainmenu.QuitPressed += this.QuitPressed;
            settingsMenu.Red += this.RedPressed;
            settingsMenu.Blue += this.BluePressed;

            while (Raylib.WindowShouldClose() == false && current.Peek() != State.Quit)
            {
                Update();
            }
            Raylib.CloseWindow();
        }

        public void Update()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);


            switch(current.Peek())
            {
                case State.MainMenu:
                    mainmenu.Draw();
                    break;
                case State.Game:
                    AsteroidsGame.frame();
                    if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                    {
                        current.Push(State.PauseMenu);
                    }
                    break;
                case State.SettingsMenu:
                    settingsMenu.Draw();
                    break;
                case State.Quit:
  
                    break;
                case State.PauseMenu:
                    pausemenu.Draw();
                    break;


                
            }
            Raylib.EndDrawing();
        }
    }
}
