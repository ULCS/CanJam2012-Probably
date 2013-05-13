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
    class rock
    {

        public Texture2D tex { get; set; } //is used for the texture
        public Vector2 size { get; set; } //is used for the texture size
        public Vector2 pos { get; set; } //is used for the texture position 
        public int speed { get; set; } //is used for the speed of the sheep.
        public static Boolean inUse = false;
        public static int useCounter = 0;
        private float scale;

        public Boolean isAlive { get; set; }

        public rock()
        {
            //For placeholder.
        }

        public rock(Texture2D texture, Vector2 texSize, Vector2 texPos, int mainSpeed)//constructuor intitiases everything here
        {
            tex = texture;
            size = texSize;
            pos = texPos;
            speed = mainSpeed;
            isAlive = true;
        }

        public void spriteDraw(SpriteBatch spBatch)
        {
            Rectangle temp = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            spBatch.Draw(tex, pos, temp, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);//Scales the image and adds effects

        }

        public void move()
        {
            //Moves the sheep towards the tower.
            pos = new Vector2(pos.X + speed, pos.Y);

            //This makes the sheep get bigger as they get closer.
            if (pos.Y >= 800 && pos.Y <= 900)
            {
                scale = 1.0f;
            }
            else if (pos.Y >= 700 && pos.Y <= 799)
            {
                scale = 0.96f;
            }
            else if (pos.Y >= 600 && pos.Y <= 699)
            {
                scale = 0.90f;
            }
            else if (pos.Y >= 500 && pos.Y <= 599)
            {
                scale = 0.84f;
            }
            else if (pos.Y >= 400 && pos.Y <= 499)
            {
                scale = 0.78f;
            }
            else if (pos.Y >= 300 && pos.Y <= 399)
            {
                scale = 0.72f;
            }
            else if (pos.Y >= 200 && pos.Y <= 299)
            {
                scale = 0.66f;
            }
            else if (pos.Y >= 100 && pos.Y <= 199)
            {
                scale = 0.60f;
            }
            else if (pos.Y >= 0 && pos.Y <= 99)
            {
                scale = 0.54f;
            }
        }

        public void useSet(Boolean Used)
        {
            inUse = Used;
        }

        public Boolean useGet()
        {
            return inUse;
        }

        public void useDelay()
        {
            if (inUse)
            {
                useCounter += 1;
                if (useCounter >= 30)
                {
                    inUse = false;
                    useCounter = 0;
                }
            }
        }


    }
}
