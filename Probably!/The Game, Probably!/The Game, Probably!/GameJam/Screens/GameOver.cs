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
    class GameOver : GameScreen
    {
        //Variables
        #region fields
        Song gameOverTheme;
        ContentManager content;
        Texture2D background;
        SpriteFont font;
        MapScreen map;
        #endregion

        #region Initialization

        public GameOver(MapScreen mainMap)
        {
            //Sets the reference to the map screen
            map = mainMap;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public void musicLoad()
        {
            //Loads the game over music and settings
            MediaPlayer.Stop();
            gameOverTheme = content.Load<Song>("Music/GameOver");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            //Starts the game over music
            MediaPlayer.Play(gameOverTheme);
        }

        #region load game content
        public override void LoadContent()
        {
            //Sets the directory up
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");          
            
            //Loads the game over background
            background = content.Load<Texture2D>("gameOver");

            musicLoad();
            //Starts a thread off so it does not slow down the process of the game
            Thread thrMusic = new Thread(musicLoad);
            thrMusic.Start();

            //Loads the font
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
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];
            //Need to add controller input here
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
            Vector2 pos = new Vector2((viewport.Width / 2)-100, viewport.Height / 2+300);
            Vector2 pos2 = new Vector2((viewport.Width / 2) - 215, viewport.Height / 2+200); 
            //Draws the text on the screen

           // spriteBatch.DrawString(font, "Score: 'Level: " + map.currentLevel + " Time taken: " + map.gameTimer.Minutes.ToString()
           // + ":" + map.gameTimer.Seconds.ToString() + "'", pos2, Color.White);
            string cutOff = map.gameTimer.ToString();
            //Checks if the miliseconds are showing and cuts them off if so
            if (cutOff.Length > 8)
                cutOff = cutOff.Remove(8);
            //removes the hours
            cutOff = cutOff.Remove(0, 3);         

            spriteBatch.DrawString(font,"Back to Menu",pos,Color.Yellow);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }
        #endregion


    }
}
