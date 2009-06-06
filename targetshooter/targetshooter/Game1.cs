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
        private static int screenHeight = 0; //initialize screen size variables.
        private static int screenWidth = 0;
        private static int playerHeight = 0;
        private static int playerWidth = 0;

        public static int scrHeight         //get the size of the screen for the machine.
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
        Texture2D enemyTankTexture;     //load tank texture
        Texture2D enemyTurretTexture;   //load turret texture
        Texture2D enemyShellTexture;    //load tank shell texture
        Texture2D backgroundTexture;    //load background for game screen 
        Texture2D initbackground;       //background for initial screen
        SoundEffect soundEffect;        //load sound effect for tank explosion
        SoundEffect hit;                //load sound effect for tank hit
        Song background;                //load gameplay background music
        Song intro;                     //load intro theme
        SpriteFont debug;               //load debug sprite
        GraphicsDeviceManager graphics; //load to manage graphics using XNA
        SpriteBatch spriteBatch;        //
        int shotCountDown;              //initialize timer for shooting
        int level2FontCountdown=5000;   //initialize level 2 introscreen timer
        int healthPercentage;           //initialize health
        SpriteFont infoBarFont;         //load HUD sprite
        SpriteFont initScreenFont;      //load intro sprite
        SpriteFont gameOverFont;        //load game over sprite
        SpriteFont nextLevelFont;       //load next level sprite
        initialScreen init = new initialScreen();       //create new initial screen
        bool initScrnFlag = true, gameFlag = false, helpFlag = false; //set initial screen flag, disable game and help flag
        playerTank player;              //create player tank
        List<NPCTank> enemyList = new List<NPCTank>();  //create NPC tank list
        help helpScreen;                //create help screen
        int enemyShotCountDown =500;    //set enemy tank fire rate.
        List<NPCTankShell> enemyShellList = new List<NPCTankShell>();   //create list to hold tank bullets
        int enemyTankID = 0;            //initialize tank ID
        infoBar info;                   //initialize info bar

        public void Subscribe(playerTank tank)//update variable tank life depending on event
        {

            tank.OnTankLiveAndHealthChange += new playerTank.TankLiveAndHealthChangeHandler(healthHasChanged);
        }

        public void healthHasChanged(object player, TankInfoEventArgs args)//reflect updated variable tank health
        {

            info.updateHealthAndLives(args.lives, args.health);
        }

        public TargetShooter()//Create gamescreen
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
        protected override void Initialize() //initialize the game
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>

        Texture2D myTexture;

        protected override void LoadContent()//load media such as graphics, audio, and imiages.
        {
            initbackground = Content.Load<Texture2D>(@"images/InitSand");
            backgroundTexture = Content.Load<Texture2D>(@"images/Sand");
            soundEffect = Content.Load<SoundEffect>(@"Audio/bomb");
            //SoundEffectInstance soundEffectInstance = soundEffect.Play();
            //SoundEffectInstance hitInstance = hit.Play();
            background = Content.Load<Song>(@"Audio/Background");
            hit = Content.Load<SoundEffect>(@"Audio/Hit");
            intro = Content.Load<Song>(@"Audio/Intro");
            MediaPlayer.Play(intro);
            MediaPlayer.IsRepeating = true;
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
                " TARGET SHOOTER GAME \n (Keyboard info) \n " + 
                "Up and Down arrows to move front and back \n " + 
                "Left arrow to turn the player tank left \n " +
                "Right arrow to turn the player tank right \n " +
                "'A' to move the turret Right \n " +
                "'S' to move the turret Left\n " +
                "Space bar to fire the bullets\n" +
                "\nPress 'P' to start game\n" +
                "Press 'ESC' to exit");
            info = new infoBar(1, 0, Content.Load<SpriteFont>(@"fonts/infoBar"), new Vector2(10, graphics.GraphicsDevice.Viewport.Height-50));

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
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape))) //If escape key is pushed, exit game
                this.Exit();
            //Putting the player on the bottom of the screen.
            if ((Keyboard.GetState().IsKeyDown(Keys.C)) && isGameOver())//If game is over and 'C' is pushed to continue. restart game
            {
                player.resetPlayer();
                player.numberOflives = 3;
                player.healthPercentages = 100;
                info.updateHealthAndLives(player.numberOflives, player.healthPercentages);
                isBoss = false;
                info.level = 1;
                info.score = 0;
                enemyList.Clear();        
                //info = new infoBar(1, 0, infoBarFont, player.Position);
                totalNumOfEnemy = 4;
                createTank = 3;
                randomTank();
                //player.resetPlayer();
                //info.updateHealthAndLives(player.numberOflives, player.healthPercentages);
                gameFlag = false;
                initScrnFlag = true;
                level2FontFlag = false;
                helpFlag = false;
            }
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyState = Keyboard.GetState(); //Get state of keyboard
            
            if( keyState.IsKeyDown(Keys.F1)) //If F1 is pressed, set help flag true
            {
                    helpFlag = true;
                    initScrnFlag = false;
                    gameFlag = false;
            }
            else if(keyState.IsKeyDown(Keys.P))//If P is pressed, set game flag true, start playing
            {
                gameFlag = true;
                initScrnFlag = false;
                level2FontFlag = false;
                helpFlag = false;
                MediaPlayer.Play(background);
                MediaPlayer.IsRepeating = true;
            }

            if (isGameWon())    //if game is won, set game flag false
            {
                gameFlag = false;
                

                info.updateHealthAndLives(0, 0);
            }
            // initialize window & tank width/height here so it can be accessed
            // else where
            GlobalClass.scrWidth = graphics.GraphicsDevice.Viewport.Width;
            GlobalClass.scrHeight = graphics.GraphicsDevice.Viewport.Height;
            GlobalClass.plHeight = player.tankImage.Height;
            GlobalClass.plWidth = player.tankImage.Width;

            if (gameFlag)   //if game flag is on...
            {
                if (enemyList.Count == 0 && isBoss == true)// keep spawning tanks if list is not 0
                {
                    info.level = 2;
                    isBoss = false;

                    level2Flag = true;
                    randomTank();                    
                }    
                for (int i = 0; i < enemyList.Count(); i++)//While list of tank is not 0, keep spawning tanks
                {

                    if (enemyList.ElementAt(i).Position.X < 0 || (enemyList.ElementAt(i).Position.X > Window.ClientBounds.Width) || (enemyList.ElementAt(i).Position.Y < 0) || (enemyList.ElementAt(i).Position.Y > Window.ClientBounds.Height))
                    {
                        enemyList.RemoveAt(i);
                        randomTank();
                    }    
                }

                Rectangle tankRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.imageOfTurret.Width, player.imageOfTurret.Height);

                for (int i = 0; i < enemyList.Count; i++) //if there is more than 0 enimies, keep updating movement and life of enemy tanks
                {
                    NPCTank enemy = enemyList[i];

                    enemy.update(t, new Vector2( Window.ClientBounds.Width, Window.ClientBounds.Height), player.Position, new Vector2(tankRect.Left, tankRect.Bottom), new Vector2(tankRect.Right, tankRect.Top));
                }
            
                //Move player backward if arrow down is pushed.
                //If player is stuck, do not allow move if the distance between colliding objects decreases.
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
                //Move player fowward if arrow up is pushed.
                //If player is stuck, do not allow move if the distance between colliding objects decreases.
                if ((Keyboard.GetState().IsKeyDown(Keys.Up)))// && (pos.Y <= 0))
                {
                    if ((!(isEnemyInTheSamePosition(stuckEnemyID))) &&(!isEnemyPresent(stuckEnemyID)))
                        playerStuck = false;

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

                //Fire gun if spacebar is pushed.  Calculate shot cooldown
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (shotCountDown <= 0)
                    {

                        player.fireShell();

                        shotCountDown = 300;
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
                
                for (int k=0;k<enemyList.Count();k++) //For every enemy tank on list
                //for (NPCTank enemy in enemyList)
                {
                    NPCTank enemy = enemyList[k];

                    List<playerTankShell> playerShellList = player.getBulletList();

                    for(int x=0;x<playerShellList.Count();x++) //track every bullet that tank has fired
                    //foreach (playerTankShell playerShell in playerShellList)
                    {
                        playerTankShell playerShell = playerShellList[x];
                        //calculate if it hit the enemy
                        if (collide(enemy.tankImage, enemy.Position, enemy.getTankAngle(), playerShell.getBulletImage(), playerShell.getShellPosition()))
                        {   //decrease life of enemy
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

                   //if enemy is dead, remove him from list
                    if (enemy.isDead())
                    {
                       // player.tanksDead() += 1;
                        
                        /*string ct;
                        ct = "deadcount = " + player.tanksDead();
                        System.Console.WriteLine(ct);*/

                        SoundEffectInstance soundEffectInstance = soundEffect.Play();

                        if (enemyList.Count() >=k)
                        {
                            enemyList.RemoveAt(k);
                            randomTank();
                            this.info.score += 1000;
                        }
                    }

                    enShellList.Add(enemy.getBulletList());
                    if (enShellList.Count > 0)//for every enemy tank...

                        for (int i = 0; i < enShellList.Count; i++) //for every bullet they fire...
                        {
                            List<NPCTankShell> bullListForOneTank = new List<NPCTankShell>();
                            bullListForOneTank = enShellList[i];
                            for (int j = 0; j < bullListForOneTank.Count; j++)
                            {
                                NPCTankShell enemyShell = bullListForOneTank[j];
                                //check to see if it hit the player
                                if (collide(player.tankImage, player.Position, player.getTankAngle(), enemyShell.getBulletImage(), enemyShell.getShellPosition()))
                                {
                                    enemy.removeShellFromListAt(j);
                                    SoundEffectInstance hitInstance = hit.Play();
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
                    playerTankShell s= playerBulList[y]; //remove shell from game
                    if(s.Dead)
                        player.removeShellFromListAt(y);
                }
            }
            player.update(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));  //update the player bounds

            //console output to debug
              //string dbg;
              //dbg = "position =" + player.Position + "player width = " + player.getWidth() +
              //    "player height =" + player.getHeight();
              //System.Console.WriteLine(dbg);

              //collision detection between player tank and NPCTanks
            for (int z = 0; z < enemyList.Count(); z++)
            {
                NPCTank enemy = enemyList[z];

               // if (tankscollide(player.Position, player.getWidth(), player.getHeight(), enemy.Position, enemy.getWidth(), enemy.getHeight()))
                if (tankCollide(player.tankImage,player.Position,player.getTankAngle(),enemy.tankImage,enemy.Position,enemy.getTankAngle()))
                {
                    //string hello;
                    //hello = "COLLIDE FUNCTION CALLED";
                    //System.Console.WriteLine(hello);
                    enemy.setstop(true);
                    this.playerStuck = true;
                    this.stuckEnemyID = enemy.getEnemyID();
                    this.enemyStuckPos= enemy.Position;
                }
                else
                {
                    //this.playerStuck = false;
                    enemy.setstop(false);  //do not stop
                }

                if (enemyList.Count() > 1)//if there is more than one enemy on the game screen
                {
                    NPCTank enemy1 = enemyList[z];
                    NPCTank enemy2 = enemyList[(z + 1) % enemyList.Count()]; 
                    // if (tankscollide(enemy1.Position, enemy1.getWidth(), enemy1.getHeight(),
                    //                 enemy2.Position, enemy2.getWidth(), enemy2.getHeight()))
                    //check if they are colliding
                    if (tankCollide(enemy1.tankImage, enemy1.Position, enemy1.getTankAngle(), enemy2.tankImage, enemy2.Position, enemy2.getTankAngle()))
                    {
                        enemy1.rotateTankClockwise();
                        enemy2.setstop(true);
                    }
                }
               // else enemy2.setstop(false);              
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

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White); //load background

            //debugString = "Debug: # of bullet= " + bulletList.Count().ToString() + "Turret Angle= " + turretAngleInDegree.ToString()
            //      + "Turret Slope: " + calculateTurretSlope().ToString() + "Turret Position= " + tankTurretPos.ToString() + "\n Turret Origin Rotation= " + new Vector2((texture.Width / 2) - 3, (texture.Height / 2) - 2).ToString();

                        // debugString ="Slope of enemy Turret"+ updateClass.calculateSlope((int)MathHelper.ToDegrees(enemy.getTurretAngle()))  + "Angle of the Enemy Turret: " + MathHelper.ToDegrees(enemy.getTurretAngle()).ToString()+ " Theta:" + enemy.theta()  ;   //"Firing position: " + calculateBulletFiringPos().ToString();
            //debugString = enemyList[0].getDebugString();
            
            if (initScrnFlag)  //set game screen with background
            {
                spriteBatch.Draw(initbackground, new Vector2(0, 0), Color.White);

                spriteBatch.DrawString(initScreenFont, init.getMenu(), 
                    new Vector2(GlobalClass.scrWidth/3, GlobalClass.scrHeight/3), Color.Black);

            }
            else if (helpFlag) //set help screen with background
            {
                spriteBatch.Draw(initbackground, new Vector2(0, 0), Color.White);

                spriteBatch.DrawString(helpScreen.helpSprite, helpScreen.HelpString, new Vector2(GlobalClass.scrWidth/4, GlobalClass.scrHeight/4), Color.Black);

            
            }
            else if(isGameWon()) //if game is won
            {
                //display congrats if won
                spriteBatch.DrawString(gameOverFont, "Congratulations! You have won. \n Press 'C' to continue", new Vector2(Window.ClientBounds.Center.X/2, Window.ClientBounds.Center.Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
            else if(isGameOver()){
                //int score = info.score;
                //display game lose
                spriteBatch.DrawString(gameOverFont, "Game Over!!!\n Your Score:" + info.score.ToString() + "\n\nPress 'C' to Continue", new Vector2(Window.ClientBounds.Center.X / 2, Window.ClientBounds.Center.Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);          
            }
            else if (level2FontFlag)
                {
                        //display level 2 transition
                        spriteBatch.DrawString(nextLevelFont, "Well Done! You have completed level 1. \nPress p to start level 2", new Vector2(Window.ClientBounds.Center.X/2, Window.ClientBounds.Center.Y), Color.Honeydew, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                }
            else if (gameFlag)//if game is not over, then place player on screen
            {
            //    spriteBatch.Draw(myTexture, Vector2.Zero, Color.White);

                //debugString = "Tank Angle:" + MathHelper.ToDegrees(player.getTankAngle()) + " Current Enemy ID: "+ enemyTankID.ToString() ;
                //spriteBatch.DrawString(debug, debugString, new Vector2(10, 40), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                spriteBatch.Draw(player.tankImage, player.Position, null, Color.White, player.getTankAngle(), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);

                spriteBatch.Draw(player.imageOfTurret, new Vector2(player.TurretPosition.X - 55, player.TurretPosition.Y - 55), null, Color.White, player.getTurretAngle(),
                // draw player and enemies on game screen
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
                //for each level, draw appropriate number of enemies
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

        //if tanks collide, resolve collision
        bool tankscollide(Vector2 object1Pos, int object1Width, int object1Height, Vector2 object2Pos, int object2Width, int object2Height)
        {
   
            Rectangle object1Rect = new Rectangle((int)object1Pos.X, (int)object1Pos.Y, object1Width +10, object1Height +10);
            Rectangle object2Rect = new Rectangle((int)object2Pos.X, (int)object2Pos.Y, object2Width +10, object2Height +10);

            return object1Rect.Intersects(object2Rect);
        }
    }
}
