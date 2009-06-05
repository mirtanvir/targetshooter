using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class BaseTank
    {
        private Vector2 tanKposition;// the current position of the tank in the screen
        private Vector2 turretPosition; // The turret position in relation to the tank body
        private Texture2D imageOfTank;// Image of the tank body
        private Texture2D imageOfTankTurret;// image of the turret body
        private int numberOfLives;// how many lives will there be?
        //private int tanksDead;//to  keep count of tanks dead
        private int healthPercentage;
        private float tankSpeed;// How far tank will go on each move command
        private float tankAngleInDegree;// the angle of the tank rotation in the screen
        private float turretAngleInDegree;// the angle of the turret rotation in the screen
        private float firingRate;
        private int wide;// The width of the tank image
        private int high;// height of the tank image
       // updateClass update;

        
        public float getTurretAngle()
        {

            return turretAngleInDegree;
        
        }

        
        public int healthPercentages
        {


            get
            {

                return healthPercentage;
            }
            set
            {

                healthPercentage = value;

            }

        }

        public void getHit(int damageModifier)
        {/*This method is used when the tank is get hit by an enemy shell. Depending on the damage
          * modifier, health of the tank will be reduced.
          * 
          * 
          */ 
            
            healthPercentage = healthPercentage - damageModifier;


            if (healthPercentage <= 0)// checking whether we need to decrement a life
            {
                numberOflives = numberOflives - 1;
                healthPercentages = 100;
            }
        }

        public bool isDead()
        {/*This method is to check for whether the tank is dead or not
          * 
          * 
          */ 

            if (numberOflives > 0)
                return false;
            else return true;
        
        }


        public Vector2 TurretPosition{
        
            get{
            
            
                return turretPosition;
            }
            set{
            
                turretPosition=value;
            }
        
        }


        protected float tankAngle {

            get {

                return tankAngleInDegree;

            }
            set{
            tankAngleInDegree=value;
            }
        
        }

        protected float turretAngle {

            get {
                return turretAngleInDegree;
            }
            set {

                turretAngleInDegree = value;
            }
        
        }


        public BaseTank(Texture2D imgOfTank, Texture2D imgOfTankTurret ,Vector2 firstPosition, Vector2 turretPos, float tankSpeed)
        {/* This is the constructor of this class. it will take all the above variables and initialize the variable
          * inside the class
          * 
          * 
          * 
          * 
          */ 

            this.imageOfTank = imgOfTank;
            this.imageOfTankTurret = imgOfTankTurret;
            this.tanKposition = firstPosition;
            this.turretPosition=turretPos;
            this.turretAngleInDegree = 0;
            this.tankAngleInDegree = 0;
            this.tankSpeed = tankSpeed;
            this.healthPercentage = 100;
            this.high = imageOfTank.Height;
            this.wide = imageOfTank.Width;
        }


        public Texture2D imageOfTurret{
    
        get
        {
            return imageOfTankTurret;
    
        }
            set {

                imageOfTankTurret = value;
            
            }
    
    }


        protected float RateOfFire
        {

            get{
            
                return firingRate;
            }
            set{

                firingRate=value;
            
            }
            


        }

        //to keep count of enemy tanks killed
        public int tanksDead
        {
            get
            {
                return tanksDead;
            }
            set
            {
                tanksDead = value;
            }
        }

        public int numberOflives
        
        {

            get {

                return numberOfLives;
            }
            set {

                numberOfLives=value;
            
            }

        }

        public Texture2D tankImage {

            get {

                return imageOfTank;
            }
            set {
                imageOfTank=value;
            
            }
        
        }


        protected void MoveTank(Vector2 newPos)
        {

            tanKposition = newPos; 
            
        }

        protected float TankSpeed
        {

            get {


                return tankSpeed;
            }
            set {

                tankSpeed = value;
            }


        }

        public Vector2 Position {

            get
            {
                return tanKposition;
            }

            set {


                tanKposition=value;


            }
        }


        public int width {

            get {

                return wide;
            }
            set {

                wide = value;
            
            }
        
        }


        public int getHeight() {

                return high;
            
        
        }

        public int getWidth()
        {

            return wide;
        
        }



    }
}
