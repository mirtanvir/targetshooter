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
            /*

      Has 2D tanKposition
    *

      Image for the tank
    *

      Tank turret
    *

      Bullets images
    *

      Life
    *

      Health
    *

      Upgrade
    *

      Speed of tank
    *

      Speed of bullet
    *

      Turret angle in degree
    *

      Tank angle in degree
    *

      Moving sounds
    *

      Firing sounds
    *

      Turret sounds
    *

      firing frequency
    *

      armor level
    *

      damage modifier


        
       
         */
        private Vector2 tanKposition;
        private Vector2 turretPosition;
        private Texture2D imageOfTank;
        private Texture2D imageOfTankTurret;
        private int numberOfLives;
        private int healthPercentage;
        private float tankSpeed;
        private float tankAngleInDegree;
        private float turretAngleInDegree;
        private float firingRate;
        private int wide;
        private int high;
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
        {
            bool isDead = false;
            healthPercentage = healthPercentage - damageModifier;


            if (healthPercentage <= 0)
            {
                numberOflives = numberOflives - 1;
                healthPercentages = 100;
            }
          

        }

        public bool isDead()
        {

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
        {

            this.imageOfTank = imgOfTank;
            this.imageOfTankTurret = imgOfTankTurret;
            this.tanKposition = firstPosition;
            this.turretPosition=turretPos;
            this.turretAngleInDegree = 0;
            this.tankAngleInDegree = 0;
            this.tankSpeed = tankSpeed;
            this.healthPercentage = 100;
            //this.high = imageOfTank.Height;
            //this.wide = imageOfTank.Width;
        }

        //public float TankSpeed
        //{

        //    get
        //    {

        //        return tankSpeed;
        //    }
        //    set
        //    {
        //        tankSpeed = value;

        //    }

        //}

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
