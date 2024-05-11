using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace Final1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 npcPosition;
        private Vector2 enemyPosition;
        private Vector2 blockPosition;
        private Vector2 firePosition;
        private Vector2 heartPosition;
        private Vector2 itemPosition;
        private Vector2 item123Position;
        private Vector2 item1Position;

        private Texture2D item123Texture;
        private Texture2D item1Texture;
        private Texture2D backgroundTexture;
        private Texture2D links1;
        private Texture2D links2;
        private NPCSprite npcSprite;
        private NPCSprite enemySprite;
        private BlockSprite blockSprite;
        private BlockSprite fireSprite;
        private BlockSprite heartSprite;
        private BlockSprite itemSprite;
        private BlockSprite item1Sprite;

        private SpriteFont font;
        private int npcOrEnemy = 0;
        // private Command IC;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
           
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here    
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("infos");

            item123Texture = Content.Load<Texture2D>("item123");
            item1Texture = Content.Load<Texture2D>("item1");
            backgroundTexture = Content.Load<Texture2D>("background");
            links1 = Content.Load<Texture2D>("linksword");
            links2 = Content.Load<Texture2D>("stlink");

            npcSprite = new NPCSprite(Content.Load<Texture2D>("stlink"), 4, 2);
            enemySprite = new NPCSprite(Content.Load<Texture2D>("enemy"), 4, 2);
            blockSprite = new BlockSprite(Content.Load<Texture2D>("block"), 1, 4);
            fireSprite = new BlockSprite(Content.Load<Texture2D>("fire"), 1, 2);
            heartSprite = new BlockSprite(Content.Load<Texture2D>("heart"), 1, 1);
            itemSprite = new BlockSprite(Content.Load<Texture2D>("items"), 1, 3);
            item1Sprite = new BlockSprite(Content.Load<Texture2D>("item123"), 1, 3);

            enemySprite.position = new Vector2(500, 200);
            blockSprite.position = new Vector2(180, 200);
            fireSprite.position = new Vector2(200, 300);
            heartSprite.position = new Vector2(60, 410);
            itemSprite.position = new Vector2(240, 40);
            item123Position = new Vector2(180, 410);
            item1Position = new Vector2(0, 0);
            // IC = new Command(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, Content.Load<Texture2D>("linksword"),Content.Load<Texture2D>("stlink"));
            // TODO: use this.Content to load your game content here
        }

        private bool itemExist = false;
        private bool TKeyDown = false;
        private bool YKeyDown = false;
        private bool EKeyDown = false;
        private bool ZNKeyDown = false;
        private bool UKeyDown = false;
        private bool IKeyDown = false;
        private bool OPKeyDown = false;
        private bool NumDown = false;
        private Vector2 windowSize;
        private double Etime = 0;
        private Vector2 itemDirection = new Vector2(0, 0);
        private float speed = 50f;

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Q))
                Exit();

            if (kstate.IsKeyDown(Keys.R))
                Initialize();

            // IC.Execute(kstate, ref npcOrEnemy, ref itemExist, gameTime, ref npcSprite, ref enemySprite, ref blockSprite, ref itemSprite, ref item1Sprite, ref fireSprite);

            // 方块切换
            if (kstate.IsKeyDown(Keys.T))
            {
                if (!TKeyDown) blockSprite.UpdateT();
                TKeyDown = true;
            }
            else TKeyDown = false;

            if (kstate.IsKeyDown(Keys.Y))
            {
                if (!YKeyDown) blockSprite.UpdateY();
                YKeyDown = true;
            }
            else YKeyDown = false;

            // 物品切换
            if (kstate.IsKeyDown(Keys.U))
            {
                if (!UKeyDown) itemSprite.UpdateT();
                UKeyDown = true;
            }
            else UKeyDown = false;

            if (kstate.IsKeyDown(Keys.I))
            {
                if (!IKeyDown) itemSprite.UpdateY();
                IKeyDown = true;
            }
            else IKeyDown = false;

            if (kstate.IsKeyDown(Keys.O) || kstate.IsKeyDown(Keys.P))
            {
                if (!OPKeyDown) npcOrEnemy = 1 - npcOrEnemy;
                OPKeyDown = true;
            }
            else OPKeyDown = false;

            // NPC移动、出剑等
            if (npcOrEnemy == 0)
            {
                if (kstate.IsKeyDown(Keys.W) && checkEdge(new Vector2(npcSprite.position.X, npcSprite.position.Y - 1), blockSprite.position))
                {
                    npcSprite.idx.Y = 1;
                    npcSprite.position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) npcSprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.S) && checkEdge(new Vector2(npcSprite.position.X, npcSprite.position.Y + 1), blockSprite.position))
                {
                    npcSprite.idx.Y = 0;
                    npcSprite.position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) npcSprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.A) && checkEdge(new Vector2(npcSprite.position.X - 1, npcSprite.position.Y), blockSprite.position))
                {
                    npcSprite.idx.Y = 2;
                    npcSprite.position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) npcSprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.D) && checkEdge(new Vector2(npcSprite.position.X + 1, npcSprite.position.Y), blockSprite.position))
                {
                    npcSprite.idx.Y = 3;
                    npcSprite.position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) npcSprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.E) && !EKeyDown)
                {
                    if (npcSprite.npcHP > 0)
                    {
                        npcSprite.npcHP -= 1;
                        fireSprite.position.X = npcSprite.position.X + 10;
                        fireSprite.position.Y = npcSprite.position.Y + 20;
                        EKeyDown = true;
                        Etime = gameTime.TotalGameTime.TotalSeconds;
                    }
                    else npcSprite.npcHP = 5;
                }
                else if ((kstate.IsKeyDown(Keys.Z) || kstate.IsKeyDown(Keys.N)) && !ZNKeyDown)
                {
                    ZNKeyDown = true;
                    Etime = gameTime.TotalGameTime.TotalSeconds;
                    int npcy = (int)npcSprite.idx.Y;
                    Vector2 npcp = npcSprite.position;
                    npcSprite.Texture = links1;
                    npcSprite.Columns = 1;
                    npcSprite.position = npcp;
                    npcSprite.idx.Y = npcy;
                    npcSprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.D1) && !NumDown)
                {
                    NumDown = true;
                    Etime = gameTime.TotalGameTime.TotalSeconds;
                    itemExist = true;
                    item1Sprite.Update1();
                    if (npcSprite.idx.Y == 0) { itemDirection.X = 0; itemDirection.Y = 1; }
                    else if (npcSprite.idx.Y == 1) { itemDirection.X = 0; itemDirection.Y = -1; }
                    else if (npcSprite.idx.Y == 2) { itemDirection.X = -1; itemDirection.Y = 0; }
                    else { itemDirection.X = 1; itemDirection.Y = 0; }
                    item1Sprite.position.X = npcSprite.position.X;
                    item1Sprite.position.Y = npcSprite.position.Y;

                }
                else if (kstate.IsKeyDown(Keys.D2) && !NumDown)
                {
                    NumDown = true;
                    Etime = gameTime.TotalGameTime.TotalSeconds;
                    itemExist = true;
                    item1Sprite.Update2();
                    if (npcSprite.idx.Y == 0) { itemDirection.X = 0; itemDirection.Y = 1; }
                    else if (npcSprite.idx.Y == 1) { itemDirection.X = 0; itemDirection.Y = -1; }
                    else if (npcSprite.idx.Y == 2) { itemDirection.X = -1; itemDirection.Y = 0; }
                    else { itemDirection.X = 1; itemDirection.Y = 0; }
                    item1Sprite.position.X = npcSprite.position.X;
                    item1Sprite.position.Y = npcSprite.position.Y;
                }
                else if (kstate.IsKeyDown(Keys.D3) && !NumDown)
                {
                    NumDown = true;
                    Etime = gameTime.TotalGameTime.TotalSeconds;
                    if (npcSprite.npcHP < 7) npcSprite.npcHP++;
                }
            }

            if (npcOrEnemy == 1)
            {
                if (kstate.IsKeyDown(Keys.W) && checkEdge(new Vector2(enemySprite.position.X, enemySprite.position.Y - 1), blockSprite.position))
                {
                    enemySprite.idx.Y = 1;
                    enemySprite.position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) enemySprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.S) && checkEdge(new Vector2(enemySprite.position.X, enemySprite.position.Y + 1), blockSprite.position))
                {
                    enemySprite.idx.Y = 0;
                    enemySprite.position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) enemySprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.A) && checkEdge(new Vector2(enemySprite.position.X - 1, enemySprite.position.Y), blockSprite.position))
                {
                    enemySprite.idx.Y = 2;
                    enemySprite.position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) enemySprite.Update();
                }
                else if (kstate.IsKeyDown(Keys.D) && checkEdge(new Vector2(enemySprite.position.X + 1, enemySprite.position.Y), blockSprite.position))
                {
                    enemySprite.idx.Y = 3;
                    enemySprite.position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 200 == 0) enemySprite.Update();
                }
                else if ((kstate.IsKeyDown(Keys.D1) || kstate.IsKeyDown(Keys.D2)) && !NumDown)
                {
                    NumDown = true;
                    Etime = gameTime.TotalGameTime.TotalSeconds;
                    itemExist = true;
                    item1Sprite.Update2();
                    if (enemySprite.idx.Y == 0) { itemDirection.X = 0; itemDirection.Y = 1; }
                    else if (enemySprite.idx.Y == 1) { itemDirection.X = 0; itemDirection.Y = -1; }
                    else if (enemySprite.idx.Y == 2) { itemDirection.X = -1; itemDirection.Y = 0; }
                    else { itemDirection.X = 1; itemDirection.Y = 0; }
                    item1Sprite.position.X = enemySprite.position.X;
                    item1Sprite.position.Y = enemySprite.position.Y;
                }
            }
            // e: HP - 1
            if (EKeyDown)
            {
                if (gameTime.TotalGameTime.TotalSeconds - Etime > 2)
                {
                    EKeyDown = false;
                }
                if ((int)gameTime.TotalGameTime.TotalMilliseconds % 20 == 0)
                {
                    // 20ms 更新一次火焰 🔥
                    fireSprite.UpdateT();
                }
            }
            // zn: sword
            if (ZNKeyDown)
            {
                if (gameTime.TotalGameTime.TotalSeconds - Etime > 0.5)
                {
                    ZNKeyDown = false;
                    int npcy = (int)npcSprite.idx.Y;
                    Vector2 npcp = npcSprite.position;
                    npcSprite.Texture = links2;
                    npcSprite.Columns = 2;
                    npcSprite.position = npcp;
                    npcSprite.idx.Y = npcy;
                    npcSprite.Update();
                }

            }
            // num 1, 2, 3:
            if (NumDown)
            {
                if (gameTime.TotalGameTime.TotalSeconds - Etime > 1)
                {
                    NumDown = false;
                }
            }
            if (itemExist)
            {
                if ((int)gameTime.TotalGameTime.TotalMilliseconds % 20 == 0)
                {
                    if (checkEdge(item1Sprite.position, blockSprite.position) == false)
                    {
                        itemExist = false;
                    }
                    else
                    {
                        item1Sprite.position.X += itemDirection.X * speed / 2;
                        item1Sprite.position.Y += itemDirection.Y * speed / 2;
                    }
                }
            }
            base.Update(gameTime);

        }

        public bool checkEdge(Vector2 nextP, Vector2 blockP)
        {
            if (nextP.X <= 0 || nextP.Y <= 0 || nextP.X >= _graphics.PreferredBackBufferWidth - 32 || nextP.Y >= _graphics.PreferredBackBufferHeight - 32 - 80) 
                return false;
            if (nextP.X >= blockP.X - 32 && nextP.Y >= blockP.Y - 32 && nextP.X <= blockP.X + 32 && nextP.Y <= blockP.Y + 32)
                return false;
            return true;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            // 各种文字和静图纹理
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            _spriteBatch.Draw(item123Texture, item123Position, Color.White);
            _spriteBatch.DrawString(font, "HP: " + npcSprite.npcHP, new Vector2(10, 407), Color.Black);
            _spriteBatch.DrawString(font, "1     2     3", item123Position, Color.Black);
            _spriteBatch.DrawString(font, "T/ Y", new Vector2(blockSprite.position.X, blockSprite.position.Y - 20), Color.White);
            _spriteBatch.DrawString(font, "U/ I", new Vector2(itemSprite.position.X, itemSprite.position.Y - 20), Color.White);
            if (npcOrEnemy == 0)
            {
                _spriteBatch.DrawString(font, "Now : NPC", new Vector2(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferHeight - 70), Color.White);
                _spriteBatch.DrawString(font, "NPC", new Vector2(npcSprite.position.X, npcSprite.position.Y - 20), Color.Orange);
            }
            if (npcOrEnemy == 1) 
            {
                _spriteBatch.DrawString(font, "Now : Enemy", new Vector2(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferHeight - 70), Color.Black);
                _spriteBatch.DrawString(font, "Enemy", new Vector2(enemySprite.position.X, enemySprite.position.Y - 20), Color.Black);
            }
            _spriteBatch.End();
            
            blockSprite.Draw(_spriteBatch, blockSprite.position);
            if (npcOrEnemy == 0) 
            {   
                enemySprite.Draw(_spriteBatch, enemySprite.position);
                npcSprite.Draw(_spriteBatch, npcSprite.position);
            }
            else if (npcOrEnemy == 1) 
            {
                npcSprite.Draw(_spriteBatch, npcSprite.position);
                enemySprite.Draw(_spriteBatch, enemySprite.position);
            }
            
            itemSprite.Draw(_spriteBatch, itemSprite.position);
            if (itemExist) item1Sprite.Draw(_spriteBatch, item1Sprite.position);
            if (EKeyDown) fireSprite.Draw(_spriteBatch, fireSprite.position);
            for (int i = 0; i < npcSprite.npcHP; i++) { heartSprite.Draw(_spriteBatch, new Vector2(heartSprite.position.X + i * 16, heartSprite.position.Y)); }

            base.Draw(gameTime);
        }
    }
}
