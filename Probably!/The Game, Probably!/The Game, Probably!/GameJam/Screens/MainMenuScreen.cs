#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
#endregion

namespace XnaAsignGame
{
    class MainMenuScreen : MenuScreen
    {
        ContentManager content;
        SpriteFont UI;

        #region Initialization


        //Constuctor
        public MainMenuScreen()
            : base("Landion The Destroyer")
        {         
            //Changes the position of the menu
            x1 = 150.0f;
            y1 = 400.0f;

            //Changes the position of the title
            x2 += 100f;

            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Start Game");
            MenuEntry helpMenuEntry = new MenuEntry("Instructions");
            MenuEntry exitMenuEntry = new MenuEntry("    Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            helpMenuEntry.Selected += helpSelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(helpMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            musicLoad();
            UI = content.Load<SpriteFont>("UI");
            
        }

        public void musicLoad()
        {
            //Loads the game over music and settings
            MediaPlayer.Stop();            
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            //Starts the game over music
            MediaPlayer.Play(content.Load<Song>("Music/MainGame"));
        }

        #endregion



        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new OpeningScreen1());
        }

        void helpSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HelpMenu(0), e.PlayerIndex);
        }


         /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit the game?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion

        #region draw
        //Draws the future soldier
        public override void Draw(GameTime gameTime)
        {            
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.DrawString(UI, "Created by: Nayira S. - David W. - Fracesco A.", new Vector2(40, 694), Color.WhiteSmoke);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
