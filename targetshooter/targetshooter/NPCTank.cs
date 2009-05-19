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
    class NPCTank : BaseTank
    {
        private List<NPCTankShell> shellList = new List<NPCTankShell>();
        private Texture2D bulletImage;
        private float tankShellSpeed;
        public NPCTank(Texture2D imgOfTank, Texture2D imgOfTankTurret, Texture2D imgOfTheShell, float shellSpeed,int numberOfLives, Vector2 firstPosition, Vector2 turretPos)
            : base(imgOfTank, imgOfTankTurret, firstPosition, turretPos)
        {
            bulletImage = imgOfTheShell;
            //shellList  = new playerTankShell(imgOfTheShell,
            base.numberOflives = numberOfLives;

            base.tankAngle = 180;
            base.turretAngle = 180;

            tankShellSpeed = shellSpeed;
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
        {

            NPCTankShell shell = new NPCTankShell(bulletImage,this.calculateBulletFiringPos(base.TurretPosition), tankShellSpeed, base.turretAngle);
            shellList.Add(shell);

        }

        public void rotateTankClockwise()
        {


            int currentTankAngle = tankAngle;
            int currentTurretAngle = turretAngle;
            base.turretAngle = currentTurretAngle + 1;
            base.tankAngle = currentTankAngle + 1;
            fixTank();


        }
        protected Vector2 calculateBulletFiringPos(Vector2 tankTurretPos)
        {

            // Rectangle turretRect = new Rectangle((int)tankTurretPos.X, (int)tankTurretPos.Y, tankTurret.Width, tankTurret.Height);
            float factor = 140.0f;
            Vector2 turPos = new Vector2(tankTurretPos.X - 60, tankTurretPos.Y - 60);
            //turPos.Normalize();

            return new Vector2(turPos.X, turPos.Y);
            // return new Vector2((tankTurretPos.X-60)+ (factor*(float)Math.Cos(Convert.ToDouble(MathHelper.ToRadians(90) - turretRotationAngle))), (tankTurretPos.Y-30) -factor*(float)Math.Sin(Convert.ToDouble(MathHelper.ToRadians(90) - turretRotationAngle)) );

        }



        public void rotateTurretClockwise()
        {

            int currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle + 1;
            fixTurret();

        }

        public void rotateTurretCounterClockwise()
        {
            int currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle - 1;
            fixTurret();


        }
        public void rotateTankCounterClockwise()
        {

            int currentTankAngle = tankAngle;
            int currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle - 1;
            base.tankAngle = currentTankAngle - 1;
            fixTank();

        }

        private void fixTurret() {
            //if ((turretAngle == 270) || (turretAngle == 90) || (turretAngle == 0))
            //{
            //    int currentTurretAngle = turretAngle;

            //    base.turretAngle = currentTurretAngle + 1;
            //}
            //if (updateClass.calculateSlope((int)MathHelper.ToDegrees(this.getTurretAngle())) == 0)
            //{
            //    int currentTurretAngle = turretAngle;

            //    base.turretAngle = currentTurretAngle + 1;
            
            
            //}

            if (turretAngle > 360)
                base.turretAngle = 0;
            else if (turretAngle < 0)
                base.turretAngle = 360 - turretAngle;
        
        }

        private void fixTank()
        {

            if (tankAngle > 360)
                base.tankAngle = 0;
            else if (tankAngle < 0)
                base.tankAngle = 360 - tankAngle;
        }

        public void movePlayerTankUp(float timeChangedSinceLastUpdate)
        {

            Vector2 newPos = updateClass.UpdateTankPositionUp(tankAngle, Position, TankSpeed, timeChangedSinceLastUpdate);
            base.MoveTank(newPos);
            base.TurretPosition = Position + new Vector2(60, 60);

        }

        public void MovePlayerTankDown(float timeChangedSinceLastUpdate)
        {

            Vector2 newPos = updateClass.updateTankPositionDown(tankAngle, Position, TankSpeed, timeChangedSinceLastUpdate);
            base.MoveTank(newPos);
            base.TurretPosition = Position + new Vector2(60, 60);

        }
        public void update(Vector2 MaxWindow, Vector2 playerPos, Vector2 tankRightTip, Vector2 tankLeftTip)
        {

            aim(playerPos,tankRightTip,  tankLeftTip);
            fixTurret();

            for (int i = 0; i < shellList.Count(); i++)
            {
                NPCTankShell b = shellList[i];
               


                b.updateBulletPosition();

                if (!b.isBulletInScreen(MaxWindow))
                {
                    shellList.RemoveAt(i);

                }

                //    if (collide(b.getBulletPosition()))
                //    {
                //        float x = rnd.Next(0, Window.ClientBounds.Width - texture.Width);
                //        enemyPos.X = x;
                //        bulletList.RemoveAt(i);
                //        numberOfEnemyLife--;
                //    }

                //}



            }

        }
        public List<NPCTankShell> getBulletList()
        {

            return shellList;
        }

        public Vector2 findTipOfTheTurret(Vector2 pos)
        {
            Vector2 posOfTip=pos;
            for (int i = 0; i < 20; i++)
            {

                posOfTip = updateClass.updateBulletPosition(base.turretAngle, posOfTip, .01f);
            
            }

            return posOfTip;
        }

        public void remoteSheelFromListAt(int index)
        {

            shellList.RemoveAt(index);

        
        }
        private bool negate = false;
        private double angle=0;
        float maxSlope=9999;
        private void maxTurretSlope(float slop)
        {

            if ((this.maxSlope > slop))
                maxSlope = slop;

            
        }
        bool collide(Vector2 positionOfPlayer, int playerWidth, int playerHeight, Vector2 turretPos)
        {

            Rectangle object1Rect = new Rectangle((int)positionOfPlayer.X, (int)positionOfPlayer.Y, playerWidth, playerHeight);
            Rectangle object2Rect = new Rectangle((int)turretPos.X, (int)turretPos.Y, 10000,5);
            bool ret =object1Rect.Intersects(object2Rect);
            return ret;
        }
        public void aim2(Vector2 positionOfPlayer, int playerWidth, int playerHeight)
        {
            this.rotateTurretClockwise();
            

        
        }

        public void aim(Vector2 positionOfPlayer, Vector2 tankRightTip, Vector2 tankLeftTip)
        {

            

            Vector2 turretPosition = this.calculateBulletFiringPos(base.TurretPosition);
            Vector2 anotherPoint = this.findTipOfTheTurret(turretPosition);

            int currTurretAngle = base.turretAngle;
            //int TempAngle = currTurretAngle - 90;

            float turretSlope = updateClass.calculateSlope(currTurretAngle);


            float anotherSlope = (anotherPoint.Y - turretPosition.Y) / (anotherPoint.X-turretPosition.X);
            float slopeOfThePlayer = (positionOfPlayer.Y - turretPosition.Y) / (positionOfPlayer.X - turretPosition.X);
            float slopeOfThePlayerRight = (tankRightTip.Y - turretPosition.Y) / (tankRightTip.X - turretPosition.X);
            float slopeOfThePlayerLeft = (tankLeftTip.Y - turretPosition.Y) / (tankLeftTip.X - turretPosition.X);
            //double TanTheta = (updateClass.calculateSlope(0) - slopeOfThePlayer) / (1 + updateClass.calculateSlope(0) * slopeOfThePlayer);
            //double theta = Math.Atan(TanTheta);
            //theta = MathHelper.ToDegrees((float)theta);
            // angle = theta;

            double distanceFromTurret= this.distance(turretPosition,positionOfPlayer);
            double distanceFromAnotherPoint=this.distance(anotherPoint,positionOfPlayer);


            debug = "Turret Angle:" + currTurretAngle + "anotherSlope: " + anotherSlope.ToString() + "\nslopeOfThePlayer="
                + slopeOfThePlayer.ToString();
                //+ " Same Sign: " + sameSign(anotherSlope, slopeOfThePlayer).ToString()
                //+ " Maximum turret slope:" + maxSlope +" " + Math.Abs(anotherSlope - slopeOfThePlayer); 

            //debug += "Collide:" + collide(positionOfPlayer, playerWidth, playerHeight, anotherPoint);
            maxTurretSlope(anotherSlope);
            //debug = "Targeting Slope: " + anotherSlope.ToString() + "Left Tank Slope: " + slopeOfThePlayerLeft
            //    + " Right Tank Slope: " + slopeOfThePlayerRight;
            //if (!((anotherSlope > slopeOfThePlayerLeft) && (anotherSlope < slopeOfThePlayerRight)))
            //{

            //    rotateTurretClockwise();
            //}
            //else if ((distanceFromTurret < distanceFromAnotherPoint))
            //   rotateTurretClockwise();


            
            if ((Math.Abs(slopeOfThePlayer-anotherSlope) > .01))
            {//

                //if (!negate)
                //    currTurretAngle++;
                //else currTurretAngle--;
                // float tempSlope = updateClass.calculateSlope(currTurretAngle);
                //if ((slopeOfThePlayer - tempSlope) < slopeOfThePlayer)
                //  negate = true;
                //else negate = false;//currTurretAngle = currTurretAngle-2;

                if (Math.Abs(anotherSlope + .766666) > .01)
                {
                    rotateTurretClockwise();
                }

                 

            }
            else if (!sameSign(anotherSlope, slopeOfThePlayer))
            {

                rotateTurretClockwise();
            }
            else if ((distanceFromTurret < distanceFromAnotherPoint))
                rotateTurretClockwise();

        }
        string debug;

        private bool sameSign(float v1, float v2)
        {

            if (((v1 < 0) && (v2 < 0)) || ((v1 > 0) && (v2 > 0)))
                return true;
            else return false;
               
        
        }
        private double distance(Vector2 point1, Vector2 point2)
        {
            angle = Math.Sqrt((Math.Pow((point2.X - point1.X), 2)) + (Math.Pow((point2.Y - point1.Y), 2)));
            return Math.Sqrt((Math.Pow((point2.X - point1.X), 2))+ (Math.Pow((point2.Y - point1.Y), 2)));
        
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