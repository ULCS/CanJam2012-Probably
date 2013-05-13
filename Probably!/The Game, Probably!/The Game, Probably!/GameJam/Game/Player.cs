using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace XnaAsignGame
{
    class Player
    {

        public Texture2D tex { get; set; } //is used for the texture
        public Vector2 size { get; set; } //is used for the texture size
        public Vector2 pos { get; set; } //is used for the texture position        
        public Boolean flipped = false;
        public float scale { get; set; }

        Rectangle frameArea;
        Vector2 frameLocation;
        float frameTimer = 0.0f;         //A Timer variable
        float intervalTimer = 100f;
        public bool aniOn = false;
        int currentFrame = 1; 

        public Player(Texture2D texture, Vector2 texSize, Vector2 texPos, float scaleSize)//constructuor intitiases everything here
        {
            tex = texture;
            size = texSize;
            pos = texPos;
            scale = scaleSize;
        }

        public void spriteDraw(SpriteBatch spBatch)
        {
            Rectangle temp = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            spBatch.Draw(tex, pos, frameArea, Color.White, 0f, frameLocation, scale, SpriteEffects.None, 0);//Scales the image and adds effects          
        }


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

                if (currentFrame == 2)
                {
                    currentFrame = 0;
                    aniOn = false;
                }
            }
            else
            {
                currentFrame = 0;
                aniOn = false;
            }
            int x = (int)size.X;
            int y = (int)size.Y;
            frameArea = new Rectangle((currentFrame * x), 0, x, y);
            frameLocation = new Vector2(frameArea.Width / 2, frameArea.Height / 2);
        }
 
    }
}
