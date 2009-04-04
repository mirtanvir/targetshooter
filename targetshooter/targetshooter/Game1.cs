using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace targetshooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public struct bullet
    {
        public Vector2 _position;

        public Texture2D _bulletSprite;

        public bullet(Vector2 position, Texture2D bulletSprite)
        {


            _position = position;

            _bulletSprite = bulletSprite;
        }
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        Texture2D texture;
        Texture2D bulletTexture;
        Texture2D enemy;

        Vector2 pos = new Vector2(0, 0);
        Vector2 enemyPos = new Vector2(0, 0);
        int shotCountDown = 100;
        static Random rnd = new Random();
        float enemySpeed = 2f;
        bool isRight = true;
        int enemyDirectionCheckDelay = 1000;

        SpriteFont enemyLife;
        SpriteFont gameO;
        SpriteFont debug;


        List<bullet> bulletList = new List<bullet>();

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debug = Content.Load<SpriteFont>(@"fonts/debugInformation");
            enemy = Content.Load<Texture2D>(@"images/enemy");
            texture = Content.Load<Texture2D>(@"images/rect");
            bulletTexture = Content.Load<Texture2D>(@"images/bullet");
            gameO = Content.Load<SpriteFont>(@"fonts/gameOver");
            enemyLife = Content.Load<SpriteFont>(@"fonts/font");
            pos.Y = Window.ClientBounds.Height - texture.Height;
            // TODO: use this.Content to load your game content here
        }

        bool collide(Vector2 bullPos)
        {

            Rectangle enemyRect = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, enemy.Width, enemy.Height);
            Rectangle bulletRect = new Rectangle((int)bullPos.X, (int)bullPos.Y, bulletTexture.Width, bulletTexture.Height);
            return enemyRect.Intersects(bulletRect);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //Putting the player on the bottom of the screen.


            if (enemyDirectionCheckDelay <= 0)
            {
                if (randToBool(rnd.Next(0, 2)))
                {
                    isRight = false;

                }
                else isRight = true;
                enemyDirectionCheckDelay = 3000;
            }
            else enemyDirectionCheckDelay -= gameTime.ElapsedGameTime.Milliseconds;

            if (isRight)
                enemyPos.X += enemySpeed;
            else
                enemyPos.X -= enemySpeed;

            if (enemyPos.X >= Window.ClientBounds.Width)
                enemyPos.X = 0;
            if (enemyPos.X < 0)
                enemyPos.X = Window.ClientBounds.Width;

            if ((Keyboard.GetState().IsKeyDown(Keys.Right)) && (pos.X < Window.ClientBounds.Width - texture.Width))
                pos.X++;

            if ((Keyboard.GetState().IsKeyDown(Keys.Left)) && (pos.X != 0))
                pos.X--;
            if ((Keyboard.GetState().IsKeyDown(Keys.Down)) && (pos.Y <= Window.ClientBounds.Height - texture.Height))
                pos.Y++;
            //trying to make the player go up
            if ((Keyboard.GetState().IsKeyDown(Keys.Up)))// && (pos.Y <= 0))
                pos.Y--;


            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (shotCountDown <= 0)
                {
                    Vector2 bulletFiringPos = pos + new Vector2(texture.Width / 2, 5);
                    bullet b = new bullet(bulletFiringPos, bulletTexture);


                    shotCountDown = 100;
                    bulletList.Add(b);

                }
                else shotCountDown = shotCountDown - gameTime.ElapsedGameTime.Milliseconds;
            }


            for (int i = 0; i < bulletList.Count(); i++)
            {
                bullet b = bulletList[i];
                float y = b._position.Y;
                y--;
                if (y < 0)
                {
                    bulletList.RemoveAt(i);

                }
                else
                {
                    b._position.Y = y;
                    bulletList[i] = b;
                }

                if (collide(b._position))
                {
                    float x = rnd.Next(0, Window.ClientBounds.Width - texture.Width);
                    enemyPos.X = x;
                    bulletList.RemoveAt(i);
                    numberOfEnemyLife--;
                }

            }




            base.Update(gameTime);
        }
        public bool randToBool(int x)
        {

            if (x >= 1)
                return true;
            else return false;

        }
        string getEnemyLifeString()
        {

            if (numberOfEnemyLife == 3)
                return "***";
            else if (numberOfEnemyLife == 2)
                return "**";
            else if (numberOfEnemyLife == 1)
                return "*";
            else return "";
        }
        int numberOfEnemyLife = 3;
        long counter = 0;
        bool isBulletFired = false;
        Vector2 bulletPos;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {


            GraphicsDevice.Clear(Color.Red);

            if (numberOfEnemyLife <= 0)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(enemyLife, "Congratulation you won",
    new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), Color.DarkBlue, 0, Vector2.Zero,
    1, SpriteEffects.None, 1);



                spriteBatch.End();


            }
            else
            {



                spriteBatch.Begin();

                spriteBatch.DrawString(enemyLife, getEnemyLifeString(),
    new Vector2(10, 10), Color.DarkBlue, 0, Vector2.Zero,
    1, SpriteEffects.None, 1);

                spriteBatch.DrawString(debug,"Debug: # of bullet= " + bulletList.Count().ToString(),
                    new Vector2(10, 40), Color.DarkBlue, 0, Vector2.Zero,
                    1, SpriteEffects.None, 1);

                //  Drawing the player
                spriteBatch.Draw(texture, pos, Color.White);

                //  Drawing the enemy
                spriteBatch.Draw(enemy, enemyPos, Color.White);

                foreach (bullet bull in bulletList)
                {

                    spriteBatch.Draw(bull._bulletSprite, bull._position, Color.White);

                }

                spriteBatch.End();



                // TODO: Add your drawing code here


            }
            base.Draw(gameTime);
        }


    }
}
