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
using System;
using System.Collections.Generic;
using System.Linq;

namespace targetshooter
{
    public class NPCTank : BaseTank
    {
        private List<NPCTankShell> shellList = new List<NPCTankShell>();// All the bullets that this tank has fired
        private Texture2D bulletImage;
        private float tankShellSpeed;
        private bool isBoss = false;// is this a boss tank?
        private bool stop = false; //to stop npc tank after collision with player
        private int tankID;// What is the id of tank? This is necessary to resolve the bug of player being stuck at the end of the screen.
        private double angle = 0;

        public NPCTank(Texture2D imgOfTank, Texture2D imgOfTankTurret, Texture2D imgOfTheShell, float shellSpeed, int numberOfLives, Vector2 firstPosition, Vector2 turretPos, int tankID)
            : base(imgOfTank, imgOfTankTurret, firstPosition, turretPos, 0)
        {
            bulletImage = imgOfTheShell;
            
            base.numberOflives = numberOfLives;
            base.TankSpeed = 1.5f;
            base.tankAngle = 180;
            base.turretAngle = 180;
            this.tankID = tankID;
            tankShellSpeed = shellSpeed;
        }

        public void setIsBoss(bool value)
        {

            isBoss = value;

        }
        public int getEnemyID()
        {

            return tankID;

        }
        public bool getIsBoss()
        {
            return isBoss;
        }

        //collision detection btwn player and tank
        public void setstop(bool st)
        {
            stop = st;
        }

        public bool getstop()
        {
            return stop;
        }

        public float getTankAngle()
        {

            return MathHelper.ToRadians((float)base.tankAngle);

        }
        public float getTurretAngle()
        {

            return MathHelper.ToRadians((float)base.turretAngle);

        }

        public void fireShell()
        {/* This method handles the firing of the bullet.
          * 
          * 
          * 
          */ 

            NPCTankShell shell = new NPCTankShell(bulletImage, this.calculateBulletFiringPos(base.TurretPosition), tankShellSpeed, base.turretAngle); // Create a new enemy tank shell
            float tempSpd = shell.getSpeed();// preserve the current shell speed
            shell.setBulletSpeed(25f);// this speed is the honeyspot to move the shell to the turret
            for (int i = 0; i < 3; i++)
            {// do an internal update to the bullet which no one can see.
                // This is the sweet HACK!
            
                shell.updateBulletPosition();

            }
            shell.setBulletSpeed(tempSpd);// Set the bullet speed to whatever it was before.



            shellList.Add(shell);// Add the newly created bullet to the shellList associated with this tank.

        }

        public void rotateTankClockwise()
        {

            /*When tank rotates, turret needs to be rotating as well. So we need
             * to add 1 to both tank and turret rotation angle
             * 
             */ 
            float currentTankAngle = tankAngle;// get the current angle
            float currentTurretAngle = turretAngle;
            base.turretAngle = currentTurretAngle + 1;// Add one to the current angle
            base.tankAngle = currentTankAngle + 1;
            fixTank();// fixing angle incase it is greater than 360


        }
        protected Vector2 calculateBulletFiringPos(Vector2 tankTurretPos)
        { /* This metod is to determine the firing position. We need the hack in fireShell() method
           * to correct it even more.
           * 
           * 
           */ 

            
            
            Vector2 turPos = new Vector2(tankTurretPos.X - 60, tankTurretPos.Y - 60);
            

            return new Vector2(turPos.X, turPos.Y);
            

        }



        public void rotateTurretClockwise()
        {// rotate the Turrent Clock wise

            float currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle + .5f;
            fixTurret();// fix the turret in case greater than 360

        }

        public void rotateTurretCounterClockwise()
        {
            float currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle - .5f;// subtract from the current angle to rotate counter clock wise
            fixTurret();


        }
        public void rotateTankCounterClockwise()
        {

            float currentTankAngle = tankAngle;
            float currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle - .5f;
            base.tankAngle = currentTankAngle - .5f;
            fixTank();

        }

        private void fixTurret()
        {// Making sure that Turret angle is not greater than 360 or less than 0
            

            if (turretAngle > 360)
                base.turretAngle = 0;
            else if (turretAngle < 0)
                base.turretAngle = 360 + turretAngle;

        }

        private void fixTank()
        {// Making sure that tank angle is not greater than 360 or less than 0

            if (tankAngle > 360)
                base.tankAngle = 0;
            else if (tankAngle < 0)
                base.tankAngle = 360 - tankAngle;
        }

        public void moveNPCTankUp(float timeChangedSinceLastUpdate)
        {//  This method is responsible for moving the enemy tank forward

            // Calculate the new position
            Vector2 newPos = updateClass.UpdateTankPositionUp(false, tankAngle, Position, TankSpeed, timeChangedSinceLastUpdate);
            base.MoveTank(newPos);// replace the prev position with new position
            base.TurretPosition = Position + new Vector2(60, 60);// fix the turret position too to keep it on top of the tank

        }

        private bool OnTheScreenBoundary = false;
        private int OnTheScreenBoundaryCounter = 0;

        public void MoveNPCTankDown(float timeChangedSinceLastUpdate)
        {
            Vector2 prevpos = base.Position;
            Vector2 newPos;

            if (!isBoss)
            {
                newPos = updateClass.UpdateTankPositionUp(false, tankAngle, Position, TankSpeed, timeChangedSinceLastUpdate);
                if (this.getstop())
                    base.MoveTank(prevpos);
                else
                    base.MoveTank(newPos);

                base.TurretPosition = Position + new Vector2(60, 60);
            }
            else
            {

                newPos = updateClass.UpdateTankPositionUp(true, tankAngle, Position, TankSpeed, timeChangedSinceLastUpdate);
                if (base.Position.X == newPos.X && base.Position.Y == newPos.Y)
                {
                    OnTheScreenBoundaryCounter = 1;
                    OnTheScreenBoundary = true;
                }
                if (this.OnTheScreenBoundaryCounter >= 45)
                {
                    this.OnTheScreenBoundaryCounter = 0;
                    this.OnTheScreenBoundary = false;

                }
                if (this.OnTheScreenBoundary && (this.OnTheScreenBoundaryCounter > 0))
                {
                    this.OnTheScreenBoundaryCounter++;

                    rotateTankClockwise();
                    newPos = updateClass.UpdateTankPositionUp(true, tankAngle, Position, TankSpeed, timeChangedSinceLastUpdate);

                }
                else
                {
                    if (this.getstop())
                        base.MoveTank(prevpos);
                    else
                        base.MoveTank(newPos);

                    base.TurretPosition = Position + new Vector2(60, 60);
                }
            }

        }
        public void update(float timeChanged, Vector2 MaxWindow, Vector2 playerPos, Vector2 tankRightTip, Vector2 tankLeftTip)
        {/* This method is responsible for Aiming the enemy tank turret to the player,
          * Moving the tank down, updating all the shells that this tank fired,
          * 
          * 
          * 
          * 
          * 
          */ 


            aim2(playerPos);// Aim the turret toward the player
            
            fixTurret();// fix the turret


            this.MoveNPCTankDown(timeChanged);// move the tank down



            for (int i = 0; i < shellList.Count(); i++)
            {// for each shell in the shellList update it's position

                NPCTankShell b = shellList[i];



                b.updateBulletPosition();

                if (!b.isBulletInScreen(MaxWindow))
                {
                    shellList.RemoveAt(i);// if bullet is out of the screen, remove it

                }

                
            }

        }
        public List<NPCTankShell> getBulletList()
        {

            return shellList;
        }

        

        public void removeShellFromListAt(int index)
        {// Remove the bullet at that index
            if (shellList.Count != 0)// make sure that shell count is greater than zero
                shellList.RemoveAt(index);


        }
        

        





        public void aim2(Vector2 positionOfPlayer)
        {// This method is responsible for Aiming the enemy turret toward player.


            
            if (getCoordinate(positionOfPlayer, this.Position) == 0)
            { // if player tank is in co-ordinate 0
               
                
                // get the tan theta manually. using y2-y1/x2-x1

                double TanTheta = (this.Position.Y - positionOfPlayer.Y) / (positionOfPlayer.X - this.Position.X);
                double theta = Math.Atan(TanTheta);//
                theta = MathHelper.ToDegrees((float)theta);// convert it to degrees
                theta = 90f - theta;

                float targetTheta = (float)theta;


            
                if (this.turretAngle > 270)
                    this.rotateTurretClockwise();
                else if (((targetTheta > 90)) || (this.turretAngle > targetTheta))
                {

                    this.rotateTurretCounterClockwise();

                }

                else this.rotateTurretClockwise();

             
            }


            if (getCoordinate(positionOfPlayer, this.Position) == 3)
            {
                double TanTheta = (this.Position.Y - positionOfPlayer.Y) / (this.Position.X - positionOfPlayer.X);
                double theta = Math.Atan(TanTheta);
                theta = MathHelper.ToDegrees((float)theta);
                theta = 270f + theta;

                float targetTheta = (float)theta;
                targetTheta = targetTheta;

                debug += "Turret Angle" + this.turretAngle + " \nTarget angle:" + targetTheta.ToString() + "\nTan Theta:" + TanTheta.ToString()
                + "\nTheta: " + theta.ToString();



                if (((this.turretAngle < 90) /*&& (targetTheta > 270))*/) || (this.turretAngle > targetTheta))
                {

                    this.rotateTurretCounterClockwise();

                }
                else this.rotateTurretClockwise();


            }



            if (getCoordinate(positionOfPlayer, this.Position) == 2)
            {
                double TanTheta = (positionOfPlayer.Y - this.Position.Y) / (this.Position.X - positionOfPlayer.X);
                double theta = Math.Atan(TanTheta);
                theta = MathHelper.ToDegrees((float)theta);
                theta = 270f - theta;

                float targetTheta = (float)theta;
                targetTheta = targetTheta;

                debug += "Turret Angle" + this.turretAngle + " \nTarget angle:" + targetTheta.ToString() + "\nTan Theta:" + TanTheta.ToString()
                + "\nTheta: " + theta.ToString();



                if (!(this.turretAngle > targetTheta))
                    rotateTurretClockwise();
                else rotateTurretCounterClockwise();

            }
            if ((getCoordinate(positionOfPlayer, this.Position) == 1))
            {
                double TanTheta = (positionOfPlayer.Y - this.Position.Y) / (positionOfPlayer.X - this.Position.X);
                double theta = Math.Atan(TanTheta);
                theta = MathHelper.ToDegrees((float)theta);
                theta = 90f + theta;

                float targetTheta = (float)theta;
                //targetTheta = targetTheta;

                debug += "Turret Angle" + this.turretAngle + " \nTarget angle:" + targetTheta.ToString() + "\nTan Theta:" + TanTheta.ToString()
                + "\nTheta: " + theta.ToString();



                if (!(this.turretAngle > targetTheta))
                    rotateTurretClockwise();
                else rotateTurretCounterClockwise();


            }

        }



        private int getCoordinate(Vector2 Playerposition, Vector2 enemyPosition)
        {/* Get the current co-ordinate in terms of the enemy tank
          * 
          * 
          */ 

            if ((Playerposition.X > enemyPosition.X) && (Playerposition.Y < enemyPosition.Y))
            {

                return 0;

            }
            else if ((Playerposition.X > enemyPosition.X) && (Playerposition.Y > enemyPosition.Y))
            {

                return 1;

            }
            else if ((Playerposition.X < enemyPosition.X) && (Playerposition.Y > enemyPosition.Y))
            {

                return 2;

            }
            else return 3;
        }


        string debug;

        private bool sameSign(float v1, float v2)
        {

            if (((v1 < 0) && (v2 < 0)) || ((v1 > 0) && (v2 > 0)))
                return true;
            else return false;


        }
        private double distance(Vector2 point1, Vector2 point2)
        {// Return the distance between two points
            angle = Math.Sqrt((Math.Pow((point2.X - point1.X), 2)) + (Math.Pow((point2.Y - point1.Y), 2)));
            return Math.Sqrt((Math.Pow((point2.X - point1.X), 2)) + (Math.Pow((point2.Y - point1.Y), 2)));

        }

        public string getDebugString()
        {

            return debug;
        }
        public string theta()
        {

            return angle.ToString();

        }


    }
}