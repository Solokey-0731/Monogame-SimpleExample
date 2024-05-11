using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final1
{
    public class NPCSprite
    {
        public Texture2D Texture;
        public int Rows { get; set; }
        public int Columns { get; set; }

        public Vector2 idx = new Vector2(0, 0);
        public Vector2 position = new Vector2(0, 0);
        public int npcHP;
        public int currentFrame;
        private int totalFrames;
        public NPCSprite(Texture2D texture, int rows, int cols)
        {
            Texture = texture;
            Rows = rows;
            Columns = cols;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            position = new Vector2(20, 200); 
            npcHP = 5;
        }

        public void Update()
        {
            if(Columns > 1) idx.X = 1 - idx.X;
            currentFrame = (int)idx.Y * Columns + (int)idx.X;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
