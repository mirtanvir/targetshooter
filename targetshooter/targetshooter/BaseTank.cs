﻿using System;
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
    class BaseTank
    {
            /*

      Has 2D position
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
        private Vector2 position;

        private Texture2D imageOfTank;
        private Texture2D tankTurret;
        private int numberOfLives;
        private int healthPercentage;
        private float tankSpeed;
        private int tankAngleInDegree;
        private int turretAngleInDegree;
        private float firingRate;

        protected void MoveTankUp()
        {

            float slope = (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(tankAngleInDegree)));
            
            if ((tankAngleInDegree > 270) || (tankAngleInDegree < 90))
            {
                position.Y = position.Y + tankSpeed;
                //_position.X++;
                float x;

                if (slope == 0)
                    x = 0;
                else x = (tankSpeed / slope);

                position.X = position.X - x;
            }
            else if ((tankAngleInDegree < 270) && (tankAngleInDegree > 90))
            {

                position.Y = position.Y - tankSpeed;
                //_position.X++;
                float x;
                if (slope == 0)
                    x = 0;
                else x = (tankSpeed / slope);

                position.X = position.X + x;

            }




          
        
        }


        protected void MoveTankDown()
        {


        }

        protected Vector2 GetPosition() {

            return position;

        }



    }
}
