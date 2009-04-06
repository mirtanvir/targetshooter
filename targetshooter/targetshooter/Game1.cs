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
    public class bullet
    {
        public bool isInScreen;
        private Vector2 _position;
        private Texture2D _bulletSprite;
        private float _slope;
        private float _turretAngleInDegree;
        public bullet(Vector2 position, Texture2D bulletSprite, float slope, float turretAngleInDegree)
        {
            isInScreen = true;
            _slope = slope;
           
            _position = position;

            _bulletSprite = bulletSprite;
            _turretAngleInDegree = turretAngleInDegree;
        }
        public Texture2D getBulletTexture()
        {

            return _bulletSprite;
        
        }
        public Vector2 getBulletPosition()
        {

            return _position;
        
        }
    public void updateBulletPosition() //Vector2 currentPlayerTankPosition,float currentTankAngle, float currentTankTurretAngleInDegree)
        {
           
            float speed = 10;
            if ((_turretAngleInDegree > 270) || (_turretAngleInDegree < 90))
            {
                _position.Y = _position.Y - speed;
                //_position.X++;
                float x;
                if (_slope == 0)
                    x = 0;
                else x = (speed / _slope);

                _position.X = _position.X + x;
            }
            else if ((_turretAngleInDegree < 270) && (_turretAngleInDegree > 90))
               { 

                 _position.Y = _position.Y + speed;
                //_position.X++;
                float x;
                if (_slope == 0)
                    x = 0;
                else x = (speed / _slope);

                _position.X = _position.X - x;
            
            }
    }
        public bool isBulletInScreen(Vector2 maxWindowPosition)
        {

            if ((_position.X < 0) || (_position.X > maxWindowPosition.X) || (_position.Y < 0) || (_position.Y > maxWindowPosition.Y))
                return false;
            else return true;
                


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
        Texture2D tankTurret;

        Vector2 pos = new Vector2(0, 0);
        Vector2 enemyPos = new Vector2(0, 0);
        Vector2 tankTurretPos = new Vector2(0, 0);
        int shotCountDown = 100;
        int turretRotationDelayInMilliSec = 400;
        static Random rnd = new Random();
        float enemySpeed = 2f;
        bool isRight = true;
        int enemyDirectionCheckDelay = 1000;
        float turretRotationAngle=0f;
        float tankRotationAngle = 0f;
        float turretAngleInDegree = 0f;
        SpriteFont enemyLife;
        SpriteFont gameO;
        SpriteFont debug;
        string debugString;

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
            texture = Content.Load<Texture2D>(@"images/tank_body");
            tankTurret = Content.Load<Texture2D>(@"images/tank_turret");
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

        bool collisionCheckWithTurret(Vector2 bullPos)
        {

            Rectangle turretRect = new Rectangle((int)tankTurretPos.X - 60, (int)tankTurretPos.Y - 70, tankTurret.Width, tankTurret.Height);
            Rectangle bulletRect = new Rectangle((int)bullPos.X, (int)bullPos.Y, bulletTexture.Width, bulletTexture.Height);
                 bool tr = bulletRect.Intersects(turretRect);
                return tr;
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected Vector2 calculateBulletFiringPos()
        { 
         
           // Rectangle turretRect = new Rectangle((int)tankTurretPos.X, (int)tankTurretPos.Y, tankTurret.Width, tankTurret.Height);
            float factor = 140.0f;
            return new Vector2(tankTurretPos.X-60, tankTurretPos.Y-60);
           // return new Vector2((tankTurretPos.X-60)+ (factor*(float)Math.Cos(Convert.ToDouble(MathHelper.ToRadians(90) - turretRotationAngle))), (tankTurretPos.Y-30) -factor*(float)Math.Sin(Convert.ToDouble(MathHelper.ToRadians(90) - turretRotationAngle)) );
         
        }


      
        float calculateTurretSlope()
        {

            return (float)  Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90)- turretRotationAngle));
        
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //turretAngleInDegree =turretRotationAngle- tankRotationAngle;
            tankTurretPos= pos + new Vector2(60, 60);
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

            //if ((Keyboard.GetState().IsKeyDown(Keys.Right)) && (pos.X < Window.ClientBounds.Width - texture.Width))
            //    pos.X++;

            //if ((Keyboard.GetState().IsKeyDown(Keys.Left)) && (pos.X != 0))
            //    pos.X--;
            if ((Keyboard.GetState().IsKeyDown(Keys.Down)) && (pos.Y <= Window.ClientBounds.Height - texture.Height))
            // pos.Y++;
            {
                float slope = (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(tankRotationAngle)));
                float speed = 1;
                if ((tankRotationAngle > 270) || (tankRotationAngle < 90))
                {
                    pos.Y = pos.Y + speed;
                    //_position.X++;
                    float x;

                    if (slope == 0)
                        x = 0;
                    else x = (speed / slope);

                    pos.X = pos.X - x;
                }
                else if ((tankRotationAngle < 270) && (tankRotationAngle > 90))
                {

                    pos.Y = pos.Y - speed;
                    //_position.X++;
                    float x;
                    if (slope == 0)
                        x = 0;
                    else x = (speed / slope);

                    pos.X = pos.X + x;

                }

            
            }


            //trying to make the player go up
            if ((Keyboard.GetState().IsKeyDown(Keys.Up)))// && (pos.Y <= 0))
            {
                float slope = (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians( tankRotationAngle)));
                float speed = 1;
                if ((tankRotationAngle > 270) || (tankRotationAngle < 90))
                {
                    pos.Y = pos.Y - speed;
                    //_position.X++;
                    float x;
                   
                    if (slope == 0)
                        x = 0;
                    else x = (speed / slope);

                    pos.X = pos.X + x;
                }
                else if ((tankRotationAngle < 270) && (tankRotationAngle > 90))
                {

                    pos.Y = pos.Y + speed;
                    //_position.X++;
                    float x;
                    if (slope == 0)
                        x = 0;
                    else x = (speed / slope);

                    pos.X = pos.X - x;

                }

                //pos.Y--;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (shotCountDown <= 0)
                {
                    Vector2 bulletFiringPos = calculateBulletFiringPos(); //(tankTurretPos - new Vector2(5, 58));
                   
                    bullet b = new bullet(bulletFiringPos, bulletTexture,calculateTurretSlope(),turretAngleInDegree);


                    shotCountDown = 100;
                    bulletList.Add(b);

                }
                else shotCountDown = shotCountDown - gameTime.ElapsedGameTime.Milliseconds;
            }


            for (int i = 0; i < bulletList.Count(); i++)
            {
                bullet b = bulletList[i];
              

                b.updateBulletPosition();

                if (!b.isBulletInScreen( new Vector2( Window.ClientBounds.Height, Window.ClientBounds.Width)))
                {
                    bulletList.RemoveAt(i);

                }



                if (collide(b.getBulletPosition()))
                {
                    float x = rnd.Next(0, Window.ClientBounds.Width - texture.Width);
                    enemyPos.X = x;
                    bulletList.RemoveAt(i);
                    numberOfEnemyLife--;
                }

            }




           
            //Turrent rotation angle

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (turretRotationDelayInMilliSec <= 0)
                {
                    turretAngleInDegree += 1;
                    turretRotationDelayInMilliSec =0;
                }
                else turretRotationDelayInMilliSec -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                turretAngleInDegree -= 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (turretRotationDelayInMilliSec <= 0)
                {
                    turretAngleInDegree += 1; 
                    tankRotationAngle += 1;
                    turretRotationDelayInMilliSec = 0;
                }
                else turretRotationDelayInMilliSec -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                turretAngleInDegree -= 1; 
                tankRotationAngle -= 1;
            }





            if (turretAngleInDegree > 360)
                turretAngleInDegree = 0;
            else if (turretAngleInDegree < 0)
                turretAngleInDegree = 360 - turretAngleInDegree;
                

            //float c = MathHelper.Pi * 2;

            turretRotationAngle = MathHelper.ToRadians(turretAngleInDegree);
            
            


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

            if (numberOfEnemyLife == 10)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(enemyLife, "Congratulation you won",
    new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), Color.DarkBlue, 0, Vector2.Zero,
    1, SpriteEffects.None, 1);



                spriteBatch.End();


            }
            else
            {
                 debugString="Debug: # of bullet= " + bulletList.Count().ToString() + "Turret Angle= " + turretAngleInDegree.ToString()
                   + "Turret Slope: " + calculateTurretSlope().ToString() + "Turret Position= " + tankTurretPos.ToString() +"\n Turret Origin Rotation= "+ new Vector2((texture.Width / 2)-3,(texture.Height / 2)-2).ToString();

                 debugString += "Firing position: " + calculateBulletFiringPos().ToString();
                spriteBatch.Begin();

                spriteBatch.DrawString(enemyLife, getEnemyLifeString(),
    new Vector2(10, 10), Color.DarkBlue, 0, Vector2.Zero,
    1, SpriteEffects.None, 1);

                spriteBatch.DrawString(debug,debugString ,
                    new Vector2(10, 40), Color.DarkBlue, 0, Vector2.Zero,
                    1, SpriteEffects.None, 1);

                //  Drawing the player
                spriteBatch.Draw(texture, pos, null, Color.White, MathHelper.ToRadians(tankRotationAngle), new Vector2(40, 70), 1.0f, SpriteEffects.None, 0f);

                //spriteBatch.Draw(tankTurret, pos+ new Vector2(1,0), Color.White);
               // spriteBatch.Draw(tankTurret,tankTurretPos, null, Color.White, turretRotationAngle,
        //new Vector2(((texture.Width/2)+3), (texture.Width/2)+3), 1.0f, SpriteEffects.None, 0f);

                spriteBatch.Draw(tankTurret, new Vector2(tankTurretPos.X-55,tankTurretPos.Y-55), null, Color.White, turretRotationAngle,
        new Vector2(25, 70), 1.0f, SpriteEffects.None, 0f);

                //  Drawing the enemy
                spriteBatch.Draw(enemy, enemyPos, Color.White);

                foreach (bullet bull in bulletList)
                {
                    if (!(collisionCheckWithTurret(bull.getBulletPosition())))
                        spriteBatch.Draw(bull.getBulletTexture(), bull.getBulletPosition(), Color.White);
                    else ;
                }

                spriteBatch.End();



                // TODO: Add your drawing code here


            }
            base.Draw(gameTime);
        }


    }
}
