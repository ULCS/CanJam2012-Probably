#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
#endregion

namespace XnaAsignGame
{
    class PauseMenuScreen : MenuScreen
    {
        #region variables
        ContentManager content;
        Texture2D square;
        Rectangle boarderSquare;

        bool end = false;
        #endregion

        #region Initialization

        public PauseMenuScreen()
            : base("Paused")
        {   //Sets the position for the menu
            x1 = 400f;
            y1 = 290f;
            //Sets the position for the title
            x2 = 500;
            y2 = 200;

            // Flag that there is no need for the game to transition
            // off when the pause menu is on top of it.
            IsPopup = true;
          
            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry helpMenuEntry = new MenuEntry("Instructions");
            MenuEntry menuGameMenuEntry = new MenuEntry("Back to Menu");
            MenuEntry exitGameMenuEntry = new MenuEntry("Exit the game");
            
            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            menuGameMenuEntry.Selected += QuitGameMenuEntrySelected;
            exitGameMenuEntry.Selected += ExitGameMenuEntrySelected;
            helpMenuEntry.Selected += helpSelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(helpMenuEntry);
            MenuEntries.Add(menuGameMenuEntry);
            MenuEntries.Add(exitGameMenuEntry);

        }
        #endregion

        public override void LoadContent()
        {

            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            square = content.Load<Texture2D>("special");
            //boarderSquare = new Rectangle(-175, -20, 1500, 750);
            boarderSquare = new Rectangle(290, 205, 400, 300);

        }

        #region Handle Input

        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;
            end = false;
            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void ExitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to close the game down?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;
            end = true;
            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            if (end)
            {
                ScreenManager.Game.Exit();                
            }
            else
            {
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),new MainMenuScreen());
            }
        }

        //Runs the help menu
        void helpSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HelpMenu(1), e.PlayerIndex);
        }
        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(square, boarderSquare, Color.White);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
