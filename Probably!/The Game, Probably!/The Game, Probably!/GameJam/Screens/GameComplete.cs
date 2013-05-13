#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
#endregion

namespace XnaAsignGame
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameComplete : GameScreen
    {
        //Variables
        #region fields
        ContentManager content;
        Texture2D background;
        SpriteFont font;
        MapScreen map;
        #endregion

        #region Initialization

        public GameComplete(MapScreen mainMap)
        {
            //Sets the reference to the map screen
            map = mainMap;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #region load game content
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");          

            background = content.Load<Texture2D>("GameComplete");
            
            font = ScreenManager.Font;
                     
            ScreenManager.Game.ResetElapsedTime();
        }
        #endregion

        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region game update
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
        #endregion

        #region Input
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = 0;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];

            //Sets the input so if enter is pressed it will load the main menu and background screen.
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
            }

        }
        #endregion

        #region Main Draw
        public override void Draw(GameTime gameTime)
        {
            //Sets the level background colour
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            byte fade = TransitionAlpha;

            spriteBatch.Begin();

            //Draws the game over background
            spriteBatch.Draw(background, fullscreen,
                             new Color(fade, fade, fade));
            //Sets the position for the text
            Vector2 pos = new Vector2((viewport.Width / 2)-105, (viewport.Height / 2)+250);
            Vector2 pos2 = new Vector2((viewport.Width / 2) - 215, viewport.Height / 2 -100); 
            //Draws the text on the screen
            
            spriteBatch.DrawString(font,"Back to Menu",pos,Color.Yellow);

            string cutOff = map.gameTimer.ToString();
            //Checks if the miliseconds are showing and cuts them off if so
            if (cutOff.Length > 8)
                cutOff = cutOff.Remove(8);
            //removes the hours
            cutOff = cutOff.Remove(0, 3);         

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }
        #endregion


    }
}
