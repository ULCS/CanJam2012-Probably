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
    class OpeningScreen2 : MenuScreen
    {
        #region Fields

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
        public OpeningScreen2()
            : base("Opening Screen")
        {
           
            //Sets up the position of the menu
            x1 = 100.0f;
            y1 = 700.0f;

            //Sets the position of the title
            x2 += 100;

            MenuEntry nextMenuEntry = new MenuEntry("Next");

            nextMenuEntry.Selected += nextScene;
          
            
            // Add entries to the menu.
            MenuEntries.Add(nextMenuEntry);
        }


        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            //Loads the background if its the pause menu since the backgroundscreen is not loaded.

                background = content.Load<Texture2D>("StartGame2");                

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
        void nextScene(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new MapScreen(), ControllingPlayer);
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
