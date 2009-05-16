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
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
namespace targetshooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int shotCountDown;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 800;
            this.graphics.IsFullScreen = true;


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



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>

        playerTank player;

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.



            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            player = new playerTank(Content.Load<Texture2D>(@"images/tank_body"), Content.Load<Texture2D>(@"images/tank_turret"), Content.Load<Texture2D>(@"images/bullet"), 10.0f, new Vector2(10, 10), new Vector2(10, 10) + new Vector2(60, 60));

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

            if ((Keyboard.GetState().IsKeyDown(Keys.Escape)))
                this.Exit();
            //Putting the player on the bottom of the screen.



            player.update(new Vector2(Window.ClientBounds.Height, Window.ClientBounds.Width));


            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;



            if ((Keyboard.GetState().IsKeyDown(Keys.Down)))// && (player.Position.Y <= Window.ClientBounds.Height - texture.Height))
            {

                player.MovePlayerTankDown(t);




            }


            //trying to make the player go up
            if ((Keyboard.GetState().IsKeyDown(Keys.Up)))// && (pos.Y <= 0))
            {

                player.movePlayerTankUp(t);


            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (shotCountDown <= 0)
                {

                    player.fireShell();

                    shotCountDown = 100;


                }
                else shotCountDown = shotCountDown - gameTime.ElapsedGameTime.Milliseconds;
            }






            //Turrent rotation angle
    
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {


                player.rotateTurretClockwise();

            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.rotateTurretCounterClockwise();
            }




            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                player.rotateTankClockwise();

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {


                player.rotateTankCounterClockwise();
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {


            GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin();

            spriteBatch.Draw(player.tankImage, player.Position, null, Color.White, player.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);

            spriteBatch.Draw(player.imageOfTurret, new Vector2(player.TurretPosition.X - 55, player.TurretPosition.Y - 55), null, Color.White, player.getTurretAngle(),
        new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);

            List<playerTankShell> playerShellList = player.getBulletList();

            foreach (playerTankShell bull in playerShellList)
            {
    
                spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.White);
    
            }



            spriteBatch.End();


            base.Draw(gameTime);
        }


    }
}
