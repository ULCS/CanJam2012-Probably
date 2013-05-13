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
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields
        /*This variable is needed to determine if the options screen
          was run by the menu screen or the pause menu so it knows which to go back to*/
        int windowSection;

        //Background variables needed
        Texture2D background;
        ContentManager content;

        //Menu Entries
        MenuEntry soundMenuEntry;
        MenuEntry shootEntry;
        MenuEntry leftEntry;
        MenuEntry rightEntry;
        MenuEntry upEntry;
        MenuEntry downEntry;

        //Sound variable
        static bool sound = true;

        //Key variables
        int keyNumber = 0;
        static Keys shoot = Keys.Space;
        static Keys left = Keys.Left;
        static Keys right = Keys.Right;
        static Keys down = Keys.Down;
        static Keys up = Keys.Up;
        bool wrongKey = false;      

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen(int section)
            : base("Options")
        {
            windowSection = section;
            
            //Changes the position of the menu
            x1 = 150.0f;
            y1 = 200.0f;

            //Changes the position of the title
            x2 += 100f;

            // Create our menu entries.            
            soundMenuEntry = new MenuEntry(string.Empty);
            shootEntry = new MenuEntry(string.Empty);
            upEntry = new MenuEntry(string.Empty);
            downEntry = new MenuEntry(string.Empty);
            leftEntry = new MenuEntry(string.Empty);
            rightEntry = new MenuEntry(string.Empty);
            

            SetMenuEntryText();

            MenuEntry defaultEntry = new MenuEntry("Reset to Default");
            MenuEntry backMenuEntry = new MenuEntry("Back");


            // Hook up menu event handlers.            
            soundMenuEntry.Selected += FrobnicateMenuEntrySelected;
            shootEntry.Selected += shootWait;
            upEntry.Selected += upWait;
            downEntry.Selected += downWait;
            leftEntry.Selected += leftWait;
            rightEntry.Selected += rightWait;
            defaultEntry.Selected += defaultSelected;

            //Checks if its the pause menu or main menu
            if (windowSection == 1)
                backMenuEntry.Selected += backToPause;
            else
                backMenuEntry.Selected += OnCancel;            
            
            // Add entries to the menu.
            MenuEntries.Add(soundMenuEntry);
            MenuEntries.Add(defaultEntry);
            MenuEntries.Add(shootEntry);
            MenuEntries.Add(upEntry);
            MenuEntries.Add(downEntry);
            MenuEntries.Add(leftEntry);
            MenuEntries.Add(rightEntry);
            MenuEntries.Add(backMenuEntry);
        }


        public override void LoadContent()
        {
            //Loads the background if its the pause menu since the backgroundscreen is not loaded.
            if (windowSection == 1)
            {
                //Sets up the directory
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                
                //Loads the background if needed
                background = content.Load<Texture2D>("mainBackground");
            }

        }

        //Sets the output of the menu
        void SetMenuEntryText()
        {            
            soundMenuEntry.Text = "Sound: " + (sound ? "on" : "off");
            if(keyNumber==1 && wrongKey)
                shootEntry.Text = "Player Shoot Key: " + (shoot) + " 'the key is in use already'";
            else
                shootEntry.Text = "Player Shoot Key: " + (shoot);

            if(keyNumber==2 && wrongKey)
                upEntry.Text = "Player Up Key: " + (up) + " 'the key is in use already'";
            else
                upEntry.Text = "Player Up Key: " + (up);

            if(keyNumber==3 && wrongKey)
                downEntry.Text = "Player Down Key: " + (down) + " 'the key is in use already'";
            else
                downEntry.Text = "Player Down Key: " + (down);

            if(keyNumber==4 && wrongKey)
                leftEntry.Text = "Player Left Key: " + (left) + " 'the key is in use already'";
            else
                leftEntry.Text = "Player Left Key: " + (left);

            if (keyNumber == 5 && wrongKey)
                rightEntry.Text = "Player Right Key: " + (right) + " 'the key is in use already'";
            else
                rightEntry.Text = "Player Right Key: " + (right);

        }

        #endregion

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.locked = locked;
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);


            if (locked)
            {

                Keys[] temp = Keyboard.GetState().GetPressedKeys();
                if (temp.Length > 0)
                {
                    if (temp[0] != Keys.Enter)
                    {
                        if (temp[0] != up && temp[0] != down
                        && temp[0] != shoot && temp[0] != left && temp[0] != right)
                        {
                            if (keyNumber == 1)
                            {
                                shoot = temp[0];
                                locked = false;
                                wrongKey = false;
                            }
                            else if (keyNumber == 2)
                            {
                                up = temp[0];
                                locked = false;
                                wrongKey = false;
                            }
                            else if (keyNumber == 3)
                            {
                                down = temp[0];
                                locked = false;
                                wrongKey = false;
                            }
                            else if (keyNumber == 4)
                            {
                                left = temp[0];
                                locked = false;
                                wrongKey = false;
                            }
                            else if (keyNumber == 5)
                            {
                                right = temp[0];
                                locked = false;
                                wrongKey = false;
                            }

                            keyNumber = 0;
                        }
                        else
                            wrongKey = true;
                    }
                    

                    SetMenuEntryText();
                    
                }

            }

        }

        //Return methods for the keys since they are static not public
        public Keys returnShoot()
        {
            return shoot;
        }

        public Keys returnUp()
        {
            return up;
        }

        public Keys returnDown()
        {
            return down;
        }

        public Keys returnLeft()
        {
            return left;
        }

        public Keys returnRight()
        {
            return right;
        }

        public bool returnSound()
        {
            return sound;
        }



        #region Handle Input


        void defaultSelected(object sender, PlayerIndexEventArgs e)
        {                    
            shoot = Keys.Space;        
            left = Keys.Left;        
            right = Keys.Right;        
            down = Keys.Down;        
            up = Keys.Up;
            SetMenuEntryText();
        }

        void shootWait(object sender, PlayerIndexEventArgs e)
        {
            locked = true;
            keyNumber = 1;            
        }

        void upWait(object sender, PlayerIndexEventArgs e)
        {
            locked = true;
            keyNumber = 2;
        }

        void downWait(object sender, PlayerIndexEventArgs e)
        {
            locked = true;
            keyNumber = 3;
        }

        void leftWait(object sender, PlayerIndexEventArgs e)
        {
            locked = true;
            keyNumber = 4;
        }

        void rightWait(object sender, PlayerIndexEventArgs e)
        {
            locked = true;
            keyNumber = 5;
        }

        void backToPause(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.RemoveScreen(this);
        }

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        
        {   /*This section uses a thread because it could slow down the game window
              by trying to mute the media player so its split into a thread so it does not
              affect the performance of the game.*/
            Thread thr = new Thread(mute);
            sound = !sound;
            thr.Start();
            SetMenuEntryText();
        }

        //Mutes/Unmutes the sound
        void mute()
        {
            MediaPlayer.IsMuted = !sound;
        }

        #endregion

        public override void Draw(GameTime gameTime)
        {
            if (windowSection == 1)
            {
                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
                Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
                byte fade = TransitionAlpha;

                spriteBatch.Begin(SpriteBlendMode.None);

                spriteBatch.Draw(background, fullscreen,
                                 new Color(fade, fade, fade));

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
