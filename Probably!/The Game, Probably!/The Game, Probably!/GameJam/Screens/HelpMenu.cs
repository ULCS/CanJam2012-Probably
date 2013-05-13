#region Using Statements
using System;
using System.Threading;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace XnaAsignGame
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class HelpMenu : MenuScreen
    {
        #region Fields
        /*This variable is needed to determine if the help menu screen
          was run by the main menu screen or the pause menu so it knows which to go back to*/
        int windowSection;
        //Background variables needed
        Texture2D background;
        Texture2D stats;
        SpriteFont font;
        ContentManager content;   

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public HelpMenu(int section)
            : base("Help Menu")
        {
            windowSection = section;
            //Sets up the position of the menu
            x1 = 100.0f;
            y1 = 700.0f;

            //Sets the position of the title
            x2 += 100;

            MenuEntry backMenuEntry = new MenuEntry("Back");
           
            //Checks if its the pause menu or main menu
            if (windowSection == 1)
                backMenuEntry.Selected += backToPause;
            else
                backMenuEntry.Selected += OnCancel;            
            
            // Add entries to the menu.
            MenuEntries.Add(backMenuEntry);
        }


        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            //Loads the background if its the pause menu since the backgroundscreen is not loaded.
            background = content.Load<Texture2D>("helpMenu");                

            //Loads up the font and image
            font = content.Load<SpriteFont>("helpInfo");

        }

        #endregion

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {            
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);                 

        }

        #region Handle Input
        //Goes back to the pause menu by removing the screen
        void backToPause(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.RemoveScreen(this);
        }       
        #endregion

  

        //Draws for the menu
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
           
            spriteBatch.Begin();
                        
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
                Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);   
                spriteBatch.Draw(background, fullscreen, Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
