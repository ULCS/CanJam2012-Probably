#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Collision;


using System.Collections.Generic;

#endregion

namespace XnaAsignGame
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class MapScreen : GameScreen
    {
        #region Fields

        //Game variables
        public TimeSpan gameTimer = TimeSpan.Zero;
        public int gameLive = 3;



        
        //General Variables
        ContentManager content;
        SpriteFont gameFont;
        Texture2D background;

        //image variables
        Vector2 playerPosition = new Vector2(50, 720);


        Vector2 endPosition = new Vector2(980, 50);


        Rectangle frameArea;
        Vector2 frameLocation;
        float frameTimer = 0.0f;         //A Timer variable
        float intervalTimer = 50f;      
        bool aniOn = false;
        int spriteWidth = 64;           // Width of a single sprite frame
        int spriteHeight = 64;          // Height of a single sprite frame
        int currentFrame = 1;    

        //needed for key reference
        OptionsMenuScreen getKeys;


        SpriteFont UI;

        Player[] thePlayer1;

        int spawnCounter = 0;
        int spawnCountMax = 90;

        int sheepkilledCounter = 0;
        //int sheepKilledMax = 100;

        List<Sheep> theSheep1;
        int levelSpeed = 3;
        int levelSpeedMin = 2;


        rock placeHolder;
        Boolean startDelay;


        Boolean clicked = false;
        List<rock> theRocks = new List<rock>();
        List<Lives> theLives = new List<Lives>();
        List<explosion> theExplosions = new List<explosion>();

        #endregion

        #region Initialization

        //Constructor
        #region constructor
        public MapScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }
        #endregion


        public override void LoadContent()
        {
            //sets the directory up
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            
            //Is needed to get the static keys from the option menu
            getKeys = new OptionsMenuScreen(0);

            //Loads the font up
            gameFont = content.Load<SpriteFont>("gamefont");
  
            //Loads the background up
            background = content.Load<Texture2D>("level1");

            placeHolder = new rock();

            thePlayer1 = new Player[5];
            thePlayer1[0] = new Player(content.Load<Texture2D>("cat"), new Vector2(96, 96), new Vector2(50, 550), 0.84f);
            thePlayer1[1] = new Player(content.Load<Texture2D>("cat"), new Vector2(96, 96), new Vector2(50, 460), 0.78f);
            thePlayer1[2] = new Player(content.Load<Texture2D>("cat"), new Vector2(96, 96), new Vector2(50, 360), 0.72f);
            thePlayer1[3] = new Player(content.Load<Texture2D>("cat"), new Vector2(96, 96), new Vector2(50, 260), 0.66f);
            thePlayer1[4] = new Player(content.Load<Texture2D>("cat"), new Vector2(96, 96), new Vector2(50, 170), 0.60f);

            theSheep1 = new List<Sheep>();
            

            UI = content.Load<SpriteFont>("UI");

            startDelay = true;

            int xPos = 10;

            for(int i=0;i <5; i++)
            {
                Lives liveTemp = new Lives(content.Load<Texture2D>("hearts0"), content.Load<Texture2D>("hearts1"), content.Load<Texture2D>("hearts2"),
                content.Load<Texture2D>("hearts3"), content.Load<Texture2D>("hearts4"), new Vector2(32, 32), new Vector2(xPos, 690), 4);

                theLives.Add(liveTemp);
                xPos += 36;//Gap between hearts.
            }


            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        

        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion

        #region Update and Draw
        //Runs the sprite sheet animation
        public void animation(GameTime gm)
        {
            frameTimer += (float)gm.ElapsedGameTime.TotalMilliseconds;
            if (aniOn)
            {
                if (frameTimer > intervalTimer)
                {
                    //Show the next frame
                    currentFrame++;
                    //Reset the timer
                    frameTimer = 0.0f;
                }

                if (currentFrame == 4)
                {
                    currentFrame = 0;
                }
            }
            else
            {
                currentFrame = 0;
            }
            frameArea = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            frameLocation = new Vector2(frameArea.Width / 2, frameArea.Height / 2);
        }

        //Checks for collision
        public void collisionCheck()
        {
            for (int i = 0; i < theSheep1.Count; i++)
            {
                if (theSheep1[i].pos.X <= 300)
                {
                    theSheep1[i].isAlive = false;
                    //take a life off

                    for (int j = theLives.Count - 1; j >= 0; j--)
                    {
                        if (theLives[j].liveCount != 0)
                        {
                            theLives[j].liveCount -= 1;
                            break;
                        }
                    }                
                    
                }
            }

            
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                if (sheepkilledCounter >= 2012)
                {
                    ScreenManager.AddScreen(new GameComplete(this), ControllingPlayer);
                }

                //Runs the animation update
                animation(gameTime);

                mouseCheck();
                spawnSheep();

                rockCheck();

                //Runs the check for collision
                collisionCheck();


                if (theLives.Count != 0)
                {
                    if (theLives[0].liveCount == 0)
                    {
                        ScreenManager.AddScreen(new GameOver(this), ControllingPlayer);
                    }
                }

                //This checks if there is currently any delay on the rocks being thrown.
                if (placeHolder.useGet())
                {
                    placeHolder.useDelay();
                    //Label to say in use
                }
                else
                {
                    //label to say not in use ;).
                }

                for (int i = 0; i < thePlayer1.Length; i++)
                {
                    thePlayer1[i].animation(gameTime);
                } 
                 
                //This checks if any sheep have been hit and killed and if so removes them whilest incrementing the counter.
                for (int i = 0; i < theSheep1.Count; i++)
                {
                    if (!theSheep1[i].isAlive)
                    {
                        explosion tempExp = new explosion(content.Load<Texture2D>("Explosion2"), new Vector2(64, 64), new Vector2(theSheep1[i].pos.X, theSheep1[i].pos.Y));
                        theExplosions.Add(tempExp);
                        theSheep1.RemoveAt(i);
                        i -= 1;                                              
                    }
                    else
                    {
                        theSheep1[i].move();
                        theSheep1[i].animation(gameTime);
                    }
                }

                //Sets the explosion up.
                for (int i = 0; i < theExplosions.Count; i++)
                {
                    if (theExplosions[i].isAlive)
                    {
                        theExplosions[i].animation(gameTime);
                    }
                    else
                    {                        
                        theExplosions.RemoveAt(i);
                        i--;
                    }
                }


                //This checks if any rocks have hit a target or gone beyond the boundary and if so removes them.
                for (int i = 0; i < theRocks.Count; i++)
                {
                    if (!theRocks[i].isAlive)
                    {
                        theRocks.RemoveAt(i);
                    }
                    else if (theRocks[i].pos.X >= 1000)
                    {
                        theRocks.RemoveAt(i);
                    }
                    else
                    {
                        theRocks[i].move();
                    }
                }

                //Changes the difficulty
                if (sheepkilledCounter >= 20)
                {
                    levelSpeedMin = 2;
                    levelSpeed = 4;
                    spawnCountMax = 80;
                }
                else if (sheepkilledCounter >= 50)
                {
                    levelSpeedMin = 3;
                    levelSpeed = 4;
                    spawnCountMax = 75;
                }
                else if (sheepkilledCounter > 140)
                {
                    levelSpeedMin = 3;
                    levelSpeed = 5;
                    spawnCountMax = 70;
                }
                else if (sheepkilledCounter > 180)
                {
                    levelSpeedMin = 4;
                    levelSpeed = 4;
                    spawnCountMax = 65;
                }
                else if (sheepkilledCounter > 300)
                {
                    levelSpeedMin = 4;
                    levelSpeed = 5;
                    spawnCountMax = 60;
                }
                else if (sheepkilledCounter > 600)
                {
                    levelSpeedMin = 4;
                    levelSpeed = 4;
                    spawnCountMax = 55;
                }
                else if (sheepkilledCounter > 1500)
                {
                    levelSpeedMin = 4;
                    levelSpeed = 5;
                    spawnCountMax = 50;
                }
            }
        }


        private void rockCheck()
        {
            for (int i = 0; i < theRocks.Count; i++)
            {
                for(int j= 0; j<theSheep1.Count; j++)
                {
                    if (objectCollision.fullSearchCollision(theSheep1[j].pos.X, theSheep1[j].pos.Y, theSheep1[j].pos.X + theSheep1[j].size.X, theSheep1[j].pos.Y + theSheep1[j].size.Y,
                        theRocks[i].pos.X, theRocks[i].pos.Y, theRocks[i].pos.X + theRocks[i].size.X, theRocks[i].pos.Y + theRocks[i].size.Y, 0, 0))
                    {
                        theRocks[i].isAlive = false;                        
                        theSheep1[j].isAlive = false;                       
                        sheepkilledCounter += 1;
                    }

                }
            }
        }

        private void mouseCheck()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)//Released button
            {
                clicked = true;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released && clicked)//Released button
            {
                for (int i = 0; i < thePlayer1.Length; i++)
                {

                    if (mouseCollision.Collision(Mouse.GetState().X, Mouse.GetState().Y, thePlayer1[i].pos.X, thePlayer1[i].pos.Y,
                                thePlayer1[i].pos.X + (thePlayer1[i].size.X * thePlayer1[i].scale)
                                , thePlayer1[i].pos.Y + (thePlayer1[i].size.Y * thePlayer1[i].scale)))//Checks if mouse click over one of the towers
                    {                        
                        rock temp = new rock(content.Load<Texture2D>("Rock"), new Vector2(32, 32), 
                            new Vector2(thePlayer1[i].pos.X+(thePlayer1[i].size.X/2)-20, 
                                (thePlayer1[i].pos.Y+(thePlayer1[i].size.Y/2)-20)-16), 5);
                        if (!temp.useGet())
                        {
                            theRocks.Add(temp);
                            temp.useSet(true);
                            thePlayer1[i].aniOn = true;
                        }
                    }
                }

                clicked = false;
            }
        }

        private void spawnSheep()
        {
            spawnCounter += 1;
            if (spawnCounter >= spawnCountMax && startDelay)
            {
                spawnCounter = 0;
                startDelay = false;
            }
            else if (spawnCounter >= spawnCountMax)
            {
                spawnCounter = 0;

                Random random = new Random();
                int randomNumber = random.Next(0, 5);

                int yPos = 0;

                if (randomNumber == 4)
                {
                    yPos = 570;
                }
                else if (randomNumber == 3)
                {
                    yPos = 480;
                }
                else if (randomNumber == 2)
                {
                    yPos = 380;
                }
                else if (randomNumber == 1)
                {
                    yPos = 280;
                }
                else if (randomNumber == 0)
                {
                    yPos = 190;
                }


                Random random2 = new Random();
                int randomNumber2 = random2.Next(levelSpeedMin, levelSpeed);

                Sheep temp = new Sheep(content.Load<Texture2D>("sheep"), new Vector2(96, 64), new Vector2(900, yPos), randomNumber2);
                theSheep1.Add(temp);

            }
        }

        //Checks the player input based on the keys chosen in the options menu
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = 0;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];           

            

            if (input.IsPauseGame(ControllingPlayer))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(getKeys.returnRight()))
                {
                  //Fire bullet here "with delay on it."
                }
                    

                if (keyboardState.IsKeyDown(getKeys.returnUp()))
                    movement.Y--;

                if (keyboardState.IsKeyDown(getKeys.returnDown()))
                    movement.Y++;   

                if (movement.Length() > 1)
                    movement.Normalize();                              
                    
            }
        }

        //Loads the background up
        public void backgroundLoad(SpriteBatch spriteBatch)
        {
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            spriteBatch.Draw(background, fullscreen, Color.White);
        }

        public void gameInterface(SpriteBatch spriteBatch)
        {
            

            Texture2D temp = content.Load<Texture2D>("BottomBar");
            spriteBatch.Draw(temp, new Vector2(1, 661), Color.White);

            temp = content.Load<Texture2D>("Sheep Killed");
            spriteBatch.Draw(temp, new Vector2(620, 690), Color.White);

            spriteBatch.DrawString(UI, sheepkilledCounter.ToString(), new Vector2(820, 694), Color.WhiteSmoke);
            
        }

        public override void Draw(GameTime gameTime)
        {
            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            //Sets the level background colour
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);

            spriteBatch.Begin();

            //Draws the background
            backgroundLoad(spriteBatch);            

            //Draws the player
            for (int i = 0; i < thePlayer1.Length; i++)
            {
                thePlayer1[i].spriteDraw(spriteBatch);
            }

            for (int i = 0; i < theSheep1.Count; i++)
            {
                theSheep1[i].spriteDraw(spriteBatch);
            }

            for (int i = 0; i < theRocks.Count; i++)
            {
                theRocks[i].spriteDraw(spriteBatch);
            }


            gameInterface(spriteBatch);

            for (int i = 0; i < theLives.Count; i++)
            {
                theLives[i].spriteDraw(spriteBatch);
            }

            for (int i = 0; i < theExplosions.Count; i++)
            {
                if (theExplosions[i].isAlive)
                {
                    theExplosions[i].spriteDraw(spriteBatch);
                }
            }
            

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }


        #endregion
    }
}
