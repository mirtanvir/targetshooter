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

    public class TargetShooter : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int shotCountDown;
        int healthPercentage;
        SpriteFont infoBarFont;
        SpriteFont initScreenFont;
        initialScreen init = new initialScreen();
        bool initScrnFlag = true, gameFlag = false, helpFlag = false;
        playerTank player;
        NPCTank enemy;
        help helpScreen; 
        int enemyShotCountDown =500;
        List<NPCTankShell> enemyShellList = new List<NPCTankShell>();

        infoBar info;

        public void Subscribe(playerTank tank)
        {

            tank.OnTankLiveAndHealthChange += new playerTank.TankLiveAndHealthChangeHandler(healthHasChanged);
        }

        public void healthHasChanged(object player, TankInfoEventArgs args)
        {

            info.updateHealthAndLives(args.lives, args.health);
        }

        public TargetShooter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 800;
            this.graphics.IsFullScreen = false;


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

        

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.



            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            infoBarFont = Content.Load<SpriteFont>(@"fonts/infoBar");
            initScreenFont = Content.Load<SpriteFont>(@"fonts/initScreen");

            player = new playerTank(Content.Load<Texture2D>(@"images/tank_body"), Content.Load<Texture2D>(@"images/tank_turret"), Content.Load<Texture2D>(@"images/bullet"), 10.0f, 3, new Vector2(10, 10), new Vector2(10, 10) + new Vector2(60, 60));
            enemy = new NPCTank(Content.Load<Texture2D>(@"images/tank_body"), Content.Load<Texture2D>(@"images/tank_turret"), Content.Load<Texture2D>(@"images/bullet"), 10.0f, 0, new Vector2(100, 100), new Vector2(100, 100) + new Vector2(60, 60));
            helpScreen = new help(Content.Load<SpriteFont>(@"fonts/help"), new Vector2(400, 400), "Help goes here");
            info = new infoBar(0, 0, Content.Load<SpriteFont>(@"fonts/infoBar"), new Vector2(10, Window.ClientBounds.Y - 10));

            info.updateHealthAndLives(player.numberOflives, player.healthPercentages);

            //enemy.

            healthPercentage = 100;
            this.Subscribe(player);
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



           

            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;



            KeyboardState keyState = Keyboard.GetState();
            
            if( keyState.IsKeyDown(Keys.F1))
            {
                    helpFlag = true;
                    initScrnFlag = false;
                    gameFlag = false;
            
            }
            else if(keyState.IsKeyDown(Keys.P)){
            gameFlag = true;
            initScrnFlag = false;

            
            }

           
            if (gameFlag)
            {

                enemy.update(new Vector2(Window.ClientBounds.Height, Window.ClientBounds.Width));

                player.update(new Vector2(Window.ClientBounds.Height, Window.ClientBounds.Width));

            }


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


            //make the enemy tank fire every .5 seconds;


           
                if (enemyShotCountDown <= 0)
                {

                    enemy.fireShell();

                    enemyShotCountDown = 500;


                }
                else enemyShotCountDown = enemyShotCountDown - gameTime.ElapsedGameTime.Milliseconds;




                if (enemyShellList.Count > 0)

                    for (int i = 0; i < enemyShellList.Count; i++)
                    {
                        NPCTankShell enemyShell = enemyShellList[i];

                        if (collide(player.Position, player.getWidth(), player.getHeight(), enemyShell.getShellPosition(), enemyShell.getWidth(), enemyShell.getHeight()))
                        {

                            player.getHit(10);
                            player.notifyAboutHit();
                            enemyShellList.RemoveAt(i);
                        }

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
            if (initScrnFlag)
            {

                spriteBatch.DrawString(initScreenFont, init.getMenu(), new Vector2(500, 500), Color.White);

            }
            else if (helpFlag)
            {

                spriteBatch.DrawString(helpScreen.helpSprite, helpScreen.HelpString, helpScreen.HelpPosition, Color.White);

            
            }
            else if (gameFlag)
            {


                spriteBatch.Draw(player.tankImage, player.Position, null, Color.White, player.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);

                spriteBatch.Draw(player.imageOfTurret, new Vector2(player.TurretPosition.X - 55, player.TurretPosition.Y - 55), null, Color.White, player.getTurretAngle(),
            new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);

               spriteBatch.Draw(enemy.tankImage, enemy.Position, null, Color.White, enemy.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);
               spriteBatch.Draw(enemy.imageOfTurret, new Vector2(enemy.TurretPosition.X - 55, enemy.TurretPosition.Y - 55), null, Color.White, enemy.getTurretAngle(),
           new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);



                List<playerTankShell> playerShellList = player.getBulletList();

                foreach (playerTankShell bull in playerShellList)
                {

                    spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.White);

                }



                 enemyShellList = enemy.getBulletList();

                foreach (NPCTankShell bull in enemyShellList)
                {

                    spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.White);

                }




                spriteBatch.DrawString(info.fontSprite,info.getInfoBar(),info.position, Color.White);








            }

            spriteBatch.End();
            base.Draw(gameTime);
        }



        bool collide(Vector2 object1Pos, int object1Width, int object1Height, Vector2 object2Pos, int object2Width, int object2Height)
        {

            Rectangle object1Rect = new Rectangle((int)object1Pos.X, (int)object1Pos.Y, object1Width, object1Height);
            Rectangle object2Rect = new Rectangle((int)object2Pos.X, (int)object2Pos.Y, object2Width, object2Height);

            return object1Rect.Intersects(object2Rect);
        }


    }
}
