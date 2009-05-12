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
        private Microsoft.Xna.Framework.Vector2 position = new Microsoft.Xna.Framework.Vector2(0, 0);

        private Texture2D imageOfTank;
        private Texture2D tankTurret;
        private int numberOfLives;
        private int healthPercentage;
        private float tankSpeed;
        private int tankAngleInDegree;
        private int turretAngleInDegree;
        private float firingRate;

       // updateClass update;

        protected void MoveTankUp()
        {

            position = updateClass.UpdateTankPositionUp(tankAngleInDegree, position, tankSpeed);
        }

        protected void MoveTankDown()
        {
            position = updateClass.updateTankPositionDown(tankAngleInDegree, position, tankSpeed);
            
        }

        protected Vector2 GetPosition() {

            return position;

        }



    }
}
