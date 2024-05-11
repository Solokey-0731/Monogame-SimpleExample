using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Final1
{
    
    public class BlockSprite
    {
        public Texture2D Texture;
        public int Rows { get; set; }
        public int Columns { get; set; }

        public int currentFrame;
        private int totalFrames;
        public Vector2 position;
        public BlockSprite(Texture2D texture, int rows, int cols) {
            Texture = texture;
            Rows = rows;
            Columns = cols;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            position = new Vector2(180, 200);

        }
        public void UpdateT()
        {
            currentFrame++;
            currentFrame %= totalFrames;
        }
        public void UpdateY()
        {
            currentFrame--;
            if(currentFrame < 0 ) { currentFrame += totalFrames; }
        }
        public void Update1()
        {
            currentFrame = 0;
        }
        public void Update2()
        {
            currentFrame = 1;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;  
            int row = currentFrame / Columns;
            int col = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * col, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int) location.X, (int) location.Y, width, height);
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
