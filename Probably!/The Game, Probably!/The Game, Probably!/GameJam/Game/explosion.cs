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
    class explosion
    {
        public Texture2D tex { get; set; } //is used for the texture
        public Vector2 size { get; set; } //is used for the texture size
        public Vector2 pos { get; set; } //is used for the texture position 

        public Boolean isAlive { get; set; }


        Rectangle frameArea;
        Vector2 frameLocation;
        float frameTimer = 0.0f;         //A Timer variable
        float intervalTimer = 10f;
        bool aniOn = false;        
        int currentFrame = 1;
        int dCurrentFrame = 1;


        public explosion(Texture2D texture, Vector2 texSize, Vector2 texPos)//constructuor intitiases everything here
        {
            tex = texture;
            size = texSize;
            pos = texPos;
            isAlive = true;
            aniOn = true;
        }

        public void spriteDraw(SpriteBatch spBatch)
        {           
            spBatch.Draw(tex, pos, frameArea, Color.White, 0f, frameLocation, 1, SpriteEffects.None, 0);//Scales the image and adds effects  
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


                if (currentFrame == 5 && dCurrentFrame == 5)
                {
                    isAlive = false;
                }
                else if (currentFrame == 5)
                {
                    dCurrentFrame += 1;
                    currentFrame = 0;
                }
            }
            else
            {
                currentFrame = 0;
            }
            int x = (int)size.X;
            int y = (int)size.Y;
            frameArea = new Rectangle((currentFrame * x), (dCurrentFrame * y), x, y);
            frameLocation = new Vector2(frameArea.Width / 2, frameArea.Height / 2);
        }



    }
}
