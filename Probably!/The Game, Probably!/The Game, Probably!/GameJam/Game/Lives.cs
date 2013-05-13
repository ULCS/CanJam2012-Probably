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
    class Lives
    {
        public Texture2D tex0 { get; set; } //is used for the texture
        public Texture2D tex1 { get; set; } //is used for the texture
        public Texture2D tex2 { get; set; } //is used for the texture
        public Texture2D tex3 { get; set; } //is used for the texture
        public Texture2D tex4 { get; set; } //is used for the texture


        public Vector2 size { get; set; } //is used for the texture size
        public Vector2 pos { get; set; } //is used for the texture position        
        public int liveCount { get; set; }


        public Lives(Texture2D texture0, Texture2D texture1,Texture2D texture2,Texture2D texture3,Texture2D texture4,
            Vector2 texSize, Vector2 texPos, int heartNum)//constructuor intitiases everything here
        {
            tex0 = texture0;
            tex1 = texture1;
            tex2 = texture2;
            tex3 = texture3;
            tex4 = texture4;

            size = texSize;
            pos = texPos;
            liveCount = heartNum;

        }

        public void spriteDraw(SpriteBatch spBatch)
        {
            Rectangle temp = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            Texture2D temp2 = tex1;//Needed for assigning

            if (liveCount == 4)
                temp2 = tex4;
            else if (liveCount == 3)
                temp2 = tex3;
            else if (liveCount == 2)
                temp2 = tex2;
            else if (liveCount == 1)
                temp2 = tex1;
            else if (liveCount == 0)
                temp2 = tex0;

            spBatch.Draw(temp2, pos, temp, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);//Scales the image and adds effects            
        }


    }
}
