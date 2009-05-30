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
        int healthPercentage;
        SpriteFont infoBarFont;
        SpriteFont initScreenFont;
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
            myTexture = CreateRectangle(640, 10);
            debug = Content.Load<SpriteFont>(@"fonts/debugInformation");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            infoBarFont = Content.Load<SpriteFont>(@"fonts/infoBar");
            initScreenFont = Content.Load<SpriteFont>(@"fonts/initScreen");

            player = new playerTank(Content.Load<Texture2D>(@"images/tank_body"), Content.Load<Texture2D>(@"images/tank_turret"), Content.Load<Texture2D>(@"images/bullet"), 10.0f, 3, new Vector2(800, 500), new Vector2(800, 500) + new Vector2(60, 60));
           // player.

            enemyTankTexture = Content.Load<Texture2D>(@"images/tank_body");
            enemyTurretTexture = Content.Load<Texture2D>(@"images/tank_turret");
            enemyShellTexture= Content.Load<Texture2D>(@"images/bullet");
            
            helpScreen = new help(Content.Load<SpriteFont>(@"fonts/help"), new Vector2(400, 200), 
                " TARGET SHOOTER GAME \n (Keyboard info) \n Up and Down arrows to move front and back \n Left arrow to turn the player tank left \n Right arrow to turn the player tank right \n 'a' to move the turret Right \n 's' to move the turret Left");
            info = new infoBar(0, 0, Content.Load<SpriteFont>(@"fonts/infoBar"), new Vector2(10, Window.ClientBounds.Y +650));

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

            // initialize window & tank width/height here so it can be accessed
            // else where
            GlobalClass.scrWidth = graphics.GraphicsDevice.Viewport.Width;
            GlobalClass.scrHeight = graphics.GraphicsDevice.Viewport.Height;
            GlobalClass.plHeight = player.tankImage.Height;
            GlobalClass.plWidth = player.tankImage.Width;

            if (gameFlag)
            {
                for (int i = 0; i < enemyList.Count(); i++)
                {

                    if (enemyList.ElementAt(i).Position.X < 0 || (enemyList.ElementAt(i).Position.X > Window.ClientBounds.Width) || (enemyList.ElementAt(i).Position.Y < 0) || (enemyList.ElementAt(i).Position.Y > Window.ClientBounds.Height))
                    {
                        enemyList.RemoveAt(i);
                        randomTank();
                    }
                /*    else if ( collide(enemyList.ElementAt(i).getHeight(), enemyList.ElementAt(i), player.getShellPosition(), player.getWidth(), player.getHeight()))
                    {

                        enemyList.RemoveAt(i);
                        randomTank();
                    }*/
                    
                }


                Rectangle tankRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.imageOfTurret.Width, player.imageOfTurret.Height);

                for (int i = 0; i < enemyList.Count; i++)
                {
                    NPCTank enemy = enemyList[i];

                    enemy.update(t, new Vector2( Window.ClientBounds.Width, Window.ClientBounds.Height), player.Position, new Vector2(tankRect.Left, tankRect.Bottom), new Vector2(tankRect.Right, tankRect.Top));
                }
                player.update(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));


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
                    foreach (NPCTank enemy in enemyList)
                    {
                        enemy.fireShell();
                    }
                    enemyShotCountDown = 2000;


                }
                else enemyShotCountDown = enemyShotCountDown - gameTime.ElapsedGameTime.Milliseconds;



                List<List<NPCTankShell>> enShellList = new List<List<NPCTankShell>>();
                foreach (NPCTank enemy in enemyList)
                {
                    enShellList.Add(enemy.getBulletList());
                    if (enShellList.Count > 0)

                        for (int i = 0; i < enShellList.Count; i++)
                        {
                            List<NPCTankShell> bullListForOneTank = new List<NPCTankShell>();
                            bullListForOneTank = enShellList[i];
                            for (int j = 0; j < bullListForOneTank.Count; j++)
                            {
                                NPCTankShell enemyShell = bullListForOneTank[j];

                                if (collide(player.Position, player.getWidth(), player.getHeight(), enemyShell.getShellPosition(), enemyShell.getWidth(), enemyShell.getHeight()))
                                {
                                    enemy.remoteSheelFromListAt(j);

                                    player.getHit(10);
                                    player.notifyAboutHit();

                                }

                            }
                        }
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

                spriteBatch.DrawString(initScreenFont, init.getMenu(), new Vector2(500, 500), Color.White);

            }
            else if (helpFlag)
            {

                spriteBatch.DrawString(helpScreen.helpSprite, helpScreen.HelpString, helpScreen.HelpPosition, Color.White);

            
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

                        spriteBatch.Draw(bull.getBulletImage(), bull.getShellPosition(), Color.White);

                    }
                }
                foreach (NPCTank enemy in enemyList)
                {

                    spriteBatch.Draw(enemy.tankImage, enemy.Position, null, Color.White, enemy.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(enemy.imageOfTurret, new Vector2(enemy.TurretPosition.X - 55, enemy.TurretPosition.Y - 55), null, Color.White, enemy.getTurretAngle(),
                new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);

                
                }



                spriteBatch.DrawString(info.fontSprite,info.getInfoBar(),info.position, Color.White);








            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private Texture2D CreateRectangle(int width, int height)
        {
            Texture2D rectangleTexture = new Texture2D(GraphicsDevice, width, height, 1, TextureUsage.None,
            SurfaceFormat.Color);// create the rectangle texture, ,but it will have no color! lets fix that
            Color[] color = new Color[width * height];//set the color to the amount of pixels in the textures

            for (int i = 0; i < color.Length; i++)//loop through all the colors setting them to whatever values we want
            {
                color[i] = new Color(0, 0, 0, 255);
            }
            rectangleTexture.SetData(color);//set the color data on the texture
            return rectangleTexture;//return the texture
        }



        bool collide(Vector2 object1Pos, int object1Width, int object1Height, Vector2 object2Pos, int object2Width, int object2Height)
        {

            Rectangle object1Rect = new Rectangle((int)object1Pos.X, (int)object1Pos.Y, object1Width, object1Height);
            Rectangle object2Rect = new Rectangle((int)object2Pos.X, (int)object2Pos.Y, object2Width, object2Height);

            return object1Rect.Intersects(object2Rect);
        }


    }
}
