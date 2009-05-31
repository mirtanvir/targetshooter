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

    // global class to share window/player width/height
    // across files
    static  class GlobalClass
    {
        private static int screenHeight = 0;
        private static int screenWidth = 0;
        private static int playerHeight = 0;
        private static int playerWidth = 0;

        public static int scrHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }
        public static int scrWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }
        public static int plHeight
        {
            get { return playerHeight; }
            set { playerHeight = value; }
        }
        public static int plWidth
        {
            get { return playerWidth; }
            set { playerWidth = value; }
        }
    }

    public partial class TargetShooter : Microsoft.Xna.Framework.Game
    {
        Texture2D enemyTankTexture;
        Texture2D enemyTurretTexture;
        Texture2D enemyShellTexture;
        Texture2D backgroundTexture;
        
        SpriteFont debug;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int shotCountDown;
        int level2FontCountdown=5000;
        int healthPercentage;
        SpriteFont infoBarFont;
        SpriteFont initScreenFont;
        SpriteFont gameOverFont;
        SpriteFont nextLevelFont;
        initialScreen init = new initialScreen();
        bool initScrnFlag = true, gameFlag = false, helpFlag = false;
        playerTank player;
        List<NPCTank> enemyList = new List<NPCTank>();
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

        Texture2D myTexture;

        protected override void LoadContent()
        {
            backgroundTexture = Content.Load<Texture2D>(@"images/sand");

            // Create a new SpriteBatch, which can be used to draw textures.
           // myTexture = CreateRectangle(640, 10);
            debug = Content.Load<SpriteFont>(@"fonts/debugInformation");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            infoBarFont = Content.Load<SpriteFont>(@"fonts/infoBar");
            initScreenFont = Content.Load<SpriteFont>(@"fonts/initScreen");
            gameOverFont = Content.Load<SpriteFont>(@"fonts/gameOver");
            nextLevelFont = Content.Load<SpriteFont>(@"fonts/nextLevelFont");

            player = new playerTank(Content.Load<Texture2D>(@"images/tank_body"), Content.Load<Texture2D>(@"images/tank_turret"), Content.Load<Texture2D>(@"images/bullet"), 10.0f, 3, new Vector2(800, 500), new Vector2(800, 500) + new Vector2(60, 60));
           // player.

            enemyTankTexture = Content.Load<Texture2D>(@"images/tank_body");
            enemyTurretTexture = Content.Load<Texture2D>(@"images/tank_turret");
            enemyShellTexture= Content.Load<Texture2D>(@"images/bullet");
            
            helpScreen = new help(Content.Load<SpriteFont>(@"fonts/help"), new Vector2(400, 200), 
                " TARGET SHOOTER GAME \n (Keyboard info) \n Up and Down arrows to move front and back \n Left arrow to turn the player tank left \n Right arrow to turn the player tank right \n 'a' to move the turret Right \n 's' to move the turret Left");
            info = new infoBar(1, 0, Content.Load<SpriteFont>(@"fonts/infoBar"), new Vector2(10, Window.ClientBounds.Bottom-50));

            info.updateHealthAndLives(player.numberOflives, player.healthPercentages);

            //enemy.
            //NPCTank enemy;


            //enemy = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(100, 0), new Vector2(100, 0) + new Vector2(60, 60));

            //enemyList.Add(enemy);

            //enemy = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(400, 0), new Vector2(400, 0) + new Vector2(60, 60));

            //enemyList.Add(enemy);
            //enemy = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(700, 0), new Vector2(700, 0) + new Vector2(60, 60));

            //enemyList.Add(enemy);

            //enemy = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(1000, 0), new Vector2(1000, 0) + new Vector2(60, 60));

            //enemyList.Add(enemy);

            //enemy = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(1200, 0), new Vector2(1200, 0) + new Vector2(60, 60));

            //enemyList.Add(enemy);

            randomTank();

            

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





     //   partial  void Update(GameTime gameTime);
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
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
            level2FontFlag = false;
            
            }

            // initialize window & tank width/height here so it can be accessed
            // else where
            GlobalClass.scrWidth = graphics.GraphicsDevice.Viewport.Width;
            GlobalClass.scrHeight = graphics.GraphicsDevice.Viewport.Height;
            GlobalClass.plHeight = player.tankImage.Height;
            GlobalClass.plWidth = player.tankImage.Width;

            if (gameFlag)
            {
                if (enemyList.Count == 0 && isBoss == true)
                {
                    info.level = 2;
                    isBoss = false;
                    
                    
                    randomTank();
                    
                }
                    
                for (int i = 0; i < enemyList.Count(); i++)
                {

                    if (enemyList.ElementAt(i).Position.X < 0 || (enemyList.ElementAt(i).Position.X > Window.ClientBounds.Width) || (enemyList.ElementAt(i).Position.Y < 0) || (enemyList.ElementAt(i).Position.Y > Window.ClientBounds.Height))
                    {
                        enemyList.RemoveAt(i);
                        randomTank();
                    }    
                }


                Rectangle tankRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.imageOfTurret.Width, player.imageOfTurret.Height);

                for (int i = 0; i < enemyList.Count; i++)
                {
                    NPCTank enemy = enemyList[i];

                    enemy.update(t, new Vector2( Window.ClientBounds.Width, Window.ClientBounds.Height), player.Position, new Vector2(tankRect.Left, tankRect.Bottom), new Vector2(tankRect.Right, tankRect.Top));
                }
                


                if ((Keyboard.GetState().IsKeyDown(Keys.Down)))// && (player.Position.Y <= Window.ClientBounds.Height - texture.Height))
                {
                    if (playerStuck)
                    {
                        Vector2 currPos = player.Position;
                        Vector2 nextPos = updateClass.updateTankPositionDown(true, player.getTankAngle(), currPos, player.getTankSpeed(), t);
                        float currDistance = this.distance(currPos, enemyStuckPos);
                        float nextDistance = this.distance(nextPos, enemyStuckPos);
                        if (currDistance < nextDistance)
                        {
                            player.MovePlayerTankDown(t);
                            this.playerStuck = false;
                        }
                    }


                    else player.MovePlayerTankDown(t);


                }


                //trying to make the player go up
                if ((Keyboard.GetState().IsKeyDown(Keys.Up)))// && (pos.Y <= 0))
                {

                    if (playerStuck)
                    {
                        Vector2 currPos = player.Position;
                        Vector2 nextPos = updateClass.UpdateTankPositionUp(true, player.getTankAngle(), currPos, player.getTankSpeed(), t);
                        float currDistance = this.distance(currPos, enemyStuckPos);
                        float nextDistance = this.distance(nextPos, enemyStuckPos);
                        if (currDistance < nextDistance)
                        {
                            player.movePlayerTankUp(t);
                            this.playerStuck = false;
                        
                        }
                    }


                    else player.movePlayerTankUp(t);

                  


                }


                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (shotCountDown <= 0)
                    {

                        player.fireShell();

                        shotCountDown = 350;


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
                    foreach (NPCTank enemy in enemyList)
                    {
                        enemy.fireShell();
                    }
                    enemyShotCountDown = 2000;


                }
                else enemyShotCountDown = enemyShotCountDown - gameTime.ElapsedGameTime.Milliseconds;



                List<List<NPCTankShell>> enShellList = new List<List<NPCTankShell>>();
                
                for (int k=0;k<enemyList.Count();k++)
                //for (NPCTank enemy in enemyList)
                {
                    NPCTank enemy = enemyList[k];

                    List<playerTankShell> playerShellList = player.getBulletList();

                    for(int x=0;x<playerShellList.Count();x++)
                    //foreach (playerTankShell playerShell in playerShellList)
                    {
                        playerTankShell playerShell = playerShellList[x];

                        if (collide(enemy.tankImage, enemy.Position, enemy.getTankAngle(), playerShell.getBulletImage(), playerShell.getShellPosition()))
                        {
                            if (info.level == 2)
                            {
                                if (!isBoss)
                                    enemy.getHit(15);
                                else
                                    enemy.getHit(5);

                            }
                            else
                            {
                                if (!isBoss)
                                    enemy.getHit(20);
                                else
                                    enemy.getHit(10);
                            }
                            playerShellList[x].Dead = true;
                            //player.removeSheelFromListAt(k);
                        
                        }
                    
                    }


                    if (enemy.isDead())
                    {

                        if (enemyList.Count() >=k)
                        {
                            enemyList.RemoveAt(k);
                            randomTank();
                            this.info.score += 1000;
                            
                        
                        }
                    
                    }

                    enShellList.Add(enemy.getBulletList());
                    if (enShellList.Count > 0)

                        for (int i = 0; i < enShellList.Count; i++)
                        {
                            List<NPCTankShell> bullListForOneTank = new List<NPCTankShell>();
                            bullListForOneTank = enShellList[i];
                            for (int j = 0; j < bullListForOneTank.Count; j++)
                            {
                                //this portion of the code is taken from http://creators.xna.com/en-US/tutorial/collision2dperpixeltransformed. 
                                
                                NPCTankShell enemyShell = bullListForOneTank[j];

               //                 Vector2 tankOrigin = new Vector2(GlobalClass.plWidth / 2, GlobalClass.plHeight / 2);
                                
               //                 Matrix playerTransform =
               //                 Matrix.CreateTranslation(new Vector3(-tankOrigin, 0.0f)) *
               //                     // Matrix.CreateScale(block.Scale) *  would go here
               //                  Matrix.CreateRotationZ(player.getTankAngle()) *
               //                  Matrix.CreateTranslation(new Vector3(player.Position, 0.0f));

               //                 // Calculate the bounding rectangle of this block in world space
               //                 Rectangle playerRectangle = CalculateBoundingRectangle(
               //                          new Rectangle(0, 0, player.width, player.getHeight()),
               //                          playerTransform);

               //                 Rectangle bulletRectangle = new Rectangle((int)enemyShell.getShellPosition().X, (int)enemyShell.getShellPosition().Y,
               // enemyShell.getBulletImage().Width, enemyShell.getBulletImage().Height);

               //                 Color[] bulletTextureData;
               //                 Color[] playerTextureData;

               //                 playerTextureData =
               //                   new Color[player.tankImage.Width * player.tankImage.Height];
               //                 player.tankImage.GetData(playerTextureData);
               //                 bulletTextureData =
               //                     new Color[enemyShell.getBulletImage().Width * enemyShell.getBulletImage().Height];
               //                 enemyShell.getBulletImage().GetData(bulletTextureData);


               //                 Matrix bulletTransform =
               //Matrix.CreateTranslation(new Vector3(enemyShell.getShellPosition(), 0.0f));
                                
                                
               //                 if (bulletRectangle.Intersects(playerRectangle))
               //                 {
               //                     // Check collision with person
               //                     if (IntersectPixels(bulletTransform, enemyShell.getBulletImage().Width,
               //                                          enemyShell.getBulletImage().Height, bulletTextureData,
               //                                         playerTransform, player.tankImage.Width,
               //                                         player.tankImage.Height, playerTextureData))
               //                     {
               //                         enemy.remoteSheelFromListAt(j);

               //                         player.getHit(10);
               //                         player.notifyAboutHit();

               //                     }
               //                 }





                                if (collide(player.tankImage, player.Position, player.getTankAngle(), enemyShell.getBulletImage(), enemyShell.getShellPosition()))
                                {
                                    enemy.remoteSheelFromListAt(j);

                                    player.getHit(10);
                                    player.notifyAboutHit();

                                }

                            }
                        }
                }

                List<playerTankShell> playerBulList =  player.getBulletList();

                for (int y = 0; y <playerBulList.Count(); y++)
                //foreach (playerTankShell playerShell in playerShellList)
                {
                    playerTankShell s= playerBulList[y];


                    if(s.Dead)

                        player.removeShellFromListAt(y);




                }


            }



            player.update(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));

            //console output to debug
              //string dbg;
              //dbg = "position =" + player.Position + "player width = " + player.getWidth() +
              //    "player height =" + player.getHeight();
              //System.Console.WriteLine(dbg);

              //collision detection between player tank and NPCTanks
            for (int z = 0; z < enemyList.Count(); z++)
            {
                NPCTank enemy = enemyList[z];

                if (tankscollide(player.Position, player.getWidth(), player.getHeight(), enemy.Position, enemy.getWidth(), enemy.getHeight()))
                {
                    //string hello;
                    //hello = "COLLIDE FUNCTION CALLED";
                    //System.Console.WriteLine(hello);
                    enemy.setstop(true);
                    this.playerStuck = true;
                    this.enemyStuckPos= enemy.Position;



                }
                else
                {
                    //this.playerStuck = false;

                    enemy.setstop(false);
                }
               NPCTank enemy1 = enemyList[z];
               NPCTank enemy2 = enemyList[(z + 1) % enemyList.Count()];

               if (tankscollide(enemy1.Position, enemy1.getWidth(), enemy1.getHeight(),
                                 enemy2.Position, enemy2.getWidth(), enemy2.getHeight()))
               {
                   enemy1.rotateTankClockwise();
                   enemy2.setstop(true);
                   
               }

             
               
               
            }





            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        string debugString;
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            //debugString = "Debug: # of bullet= " + bulletList.Count().ToString() + "Turret Angle= " + turretAngleInDegree.ToString()
            //      + "Turret Slope: " + calculateTurretSlope().ToString() + "Turret Position= " + tankTurretPos.ToString() + "\n Turret Origin Rotation= " + new Vector2((texture.Width / 2) - 3, (texture.Height / 2) - 2).ToString();

                        // debugString ="Slope of enemy Turret"+ updateClass.calculateSlope((int)MathHelper.ToDegrees(enemy.getTurretAngle()))  + "Angle of the Enemy Turret: " + MathHelper.ToDegrees(enemy.getTurretAngle()).ToString()+ " Theta:" + enemy.theta()  ;   //"Firing position: " + calculateBulletFiringPos().ToString();
            //debugString = enemyList[0].getDebugString();
            
            if (initScrnFlag)
            {

                spriteBatch.DrawString(initScreenFont, init.getMenu(), new Vector2(500, 500), Color.Honeydew);

            }
            else if (helpFlag)
            {

                spriteBatch.DrawString(helpScreen.helpSprite, helpScreen.HelpString, helpScreen.HelpPosition, Color.White);

            
            }
            else if(isGameWon())
            {

                spriteBatch.DrawString(gameOverFont, "Congratulations! You have won. ", new Vector2(Window.ClientBounds.Center.X/2, Window.ClientBounds.Center.Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            
            }
            else if(isGameOver()){
                //int score = info.score;


                spriteBatch.DrawString(gameOverFont, "Game Over!!!\n Your Score:" + info.score.ToString(), new Vector2(Window.ClientBounds.Center.X / 2, Window.ClientBounds.Center.Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            
            }

            else if (level2FontFlag)
                {
                        spriteBatch.DrawString(nextLevelFont, "Well Done! You have completed level 1. \nPress p to start level 2", new Vector2(Window.ClientBounds.Center.X/2, Window.ClientBounds.Center.Y), Color.Honeydew, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    

                }


            else if (gameFlag)
            {

                


            //    spriteBatch.Draw(myTexture, Vector2.Zero, Color.White);

                debugString = "Tank Angle:" + MathHelper.ToDegrees(player.getTankAngle());
                spriteBatch.DrawString(debug, debugString, new Vector2(10, 40), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        


                spriteBatch.Draw(player.tankImage, player.Position, null, Color.White, player.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);

                spriteBatch.Draw(player.imageOfTurret, new Vector2(player.TurretPosition.X - 55, player.TurretPosition.Y - 55), null, Color.White, player.getTurretAngle(),
            new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);

               


                List<playerTankShell> playerShellList = player.getBulletList();

                foreach (playerTankShell bull in playerShellList)
                {

                    spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.White);

                }


                foreach (NPCTank enemy in enemyList)
                {
                    enemyShellList = enemy.getBulletList();

                    foreach (NPCTankShell bull in enemyShellList)
                    {
                        if (info.level == 2)
                        {
                            if (!isBoss)
                                spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.DarkGreen);
                            else
                                spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.Gold);
                        }
                        else
                        {
                            if (!isBoss)
                                spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.Blue);
                            else
                                spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.Red);
                        }
                        
                    }
                }
                foreach (NPCTank enemy in enemyList)
                {
                   
                    if (info.level == 2)
                    {
                        if (!isBoss)
                        {
                            spriteBatch.Draw(enemy.tankImage, enemy.Position, null, Color.DarkGreen, enemy.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(enemy.imageOfTurret, new Vector2(enemy.TurretPosition.X - 55, enemy.TurretPosition.Y - 55), null, Color.DarkGreen, enemy.getTurretAngle(),
                            new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(enemy.tankImage, enemy.Position, null, Color.Gold, enemy.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(enemy.imageOfTurret, new Vector2(enemy.TurretPosition.X - 55, enemy.TurretPosition.Y - 55), null, Color.Gold, enemy.getTurretAngle(),
                            new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);
                        }

                    }
                    else
                    {
                        if (!isBoss)
                        {
                            spriteBatch.Draw(enemy.tankImage, enemy.Position, null, Color.Blue, enemy.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(enemy.imageOfTurret, new Vector2(enemy.TurretPosition.X - 55, enemy.TurretPosition.Y - 55), null, Color.Blue, enemy.getTurretAngle(),
                            new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(enemy.tankImage, enemy.Position, null, Color.Red, enemy.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(enemy.imageOfTurret, new Vector2(enemy.TurretPosition.X - 55, enemy.TurretPosition.Y - 55), null, Color.Red, enemy.getTurretAngle(),
                            new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);
                        }
                    }

                
                }



                spriteBatch.DrawString(info.fontSprite,info.getInfoBar(),info.position, Color.Black);

                






            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

  
        bool tankscollide(Vector2 object1Pos, int object1Width, int object1Height, Vector2 object2Pos, int object2Width, int object2Height)
        {
   
            Rectangle object1Rect = new Rectangle((int)object1Pos.X, (int)object1Pos.Y, object1Width +10, object1Height +10);
            Rectangle object2Rect = new Rectangle((int)object2Pos.X, (int)object2Pos.Y, object2Width +10, object2Height +10);

            return object1Rect.Intersects(object2Rect);
        }




        


    }
}
