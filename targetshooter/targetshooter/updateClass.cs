﻿using System;
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
    static class updateClass
    {
        //to control different actions for player or enemy tank based upon this flag
        public static bool m_bIsPlayer = true;

        //public static float calculateSlope(float objectAngelInDegree)
        //{

        //    float slope = (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(objectAngelInDegree)));
        //    return slope;//returning the slope
        //    //test2
        //}


        // static string debugString;

        public static Vector2 UpdateTankPositionUp(bool isplayer, float tankAngleInDegree, Vector2 position, float tankSpeed, float gameTimeChanged)
        {/* This method is responsible for moving the tank forwwrd from the current position. 
          * 
          * 
          * 
          * 
          * 
          * 
          */ 

            Vector2 prevPosition = position;
            m_bIsPlayer = isplayer;
            //string dbg;
            double speed = tankSpeed;
            float radian = MathHelper.ToRadians(tankAngleInDegree);// Converting the degree to radian

            if (tankAngleInDegree > 0 && tankAngleInDegree < 90)
            {
                double y = (speed * Math.Cos(((double)radian)));// get deltay


                double x = (speed * Math.Sin((double)radian));// get deltax


                position.X = position.X + (float)x;// add to the prev position to move forward
                position.Y = position.Y - (float)y;// subtract from the prev position to move forward
            }
            else if (tankAngleInDegree < 360 && tankAngleInDegree > 270)
            {
                tankAngleInDegree = 360f - tankAngleInDegree;// make the angle localized

                radian = MathHelper.ToRadians(tankAngleInDegree);

                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X - (float)x;
                position.Y = position.Y - (float)y;


            }
            else if (tankAngleInDegree < 270 && tankAngleInDegree > 180)
            {
                tankAngleInDegree = 270f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double x = (speed * Math.Cos(((double)radian)));


                double y = (speed * Math.Sin((double)radian));


                position.X = position.X - (float)x;
                position.Y = position.Y + (float)y;


            }
            else if (tankAngleInDegree < 180 && tankAngleInDegree > 90)
            {
                tankAngleInDegree = 180f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X + (float)x;
                position.Y = position.Y + (float)y;



            }
            else if (tankAngleInDegree == 0)// if the angle is 0, we just need move up
            {
                position.Y = position.Y - (float)speed;

            }
            else if (tankAngleInDegree == 180)// we just need to move to the right
            {
                position.Y = position.Y + (float)speed;

            }
            else if (tankAngleInDegree == 270)// need to move to the left
            {
                position.X = position.X - (float)speed;

            }
            else if (tankAngleInDegree == 90)// need to move down
            {
                position.X = position.X + (float)speed;
            }



            // keep the player tank within the window boundary;
            // again do this only for player tank and not for npc tank
            if (m_bIsPlayer)
            {
                //dbg = "prev X " + prevPosition.X + " curr X " + position.X;
                //System.Console.WriteLine(dbg);

                //System.Console.WriteLine(position.X);
                //dbg = "dw " + GlobalClass.GlobalWidth + " dh " + GlobalClass.GlobalHeight + " plw " + GlobalClass.GloWidth + " plh " + GlobalClass.GloHeight;
                //System.Console.WriteLine(dbg);
                //dbg = "pos x " + position.X + " pos y " + position.Y;
                //System.Console.WriteLine(dbg);
                //System.Console.WriteLine("-------------------------------");

                int MaxX = GlobalClass.scrWidth - GlobalClass.plHeight / 2;
                int MaxY = GlobalClass.scrHeight - GlobalClass.plHeight / 2;
                int MinX = GlobalClass.plHeight / 2;
                int MinY = GlobalClass.plHeight / 2;

                if (position.X > MaxX)
                {
                    position.X = MaxX;
                    position.Y = prevPosition.Y;
                }
                else if (position.X < MinX)
                {
                    position.X = MinX;
                    position.Y = prevPosition.Y;
                }

                if (position.Y > MaxY)
                {
                    position.Y = MaxY;
                    position.X = prevPosition.X;
                }
                else if (position.Y < MinY)
                {
                    position.Y = MinY;
                    position.X = prevPosition.X;
                }

                //dbg = "new  X " + position.X;
                //System.Console.WriteLine(dbg);

            }

            return position;
        }



        public static Vector2 updateTankPositionDown(bool isplayer, float tankAngleInDegree, Vector2 position, float tankSpeed, float gameTimeChanged)
        {/* This method is necessary for moving the tank down.
          * Please take a look at UpdateTankPositionUp() method for detail comments. This method is doing almost the same thing.
          * This method just does the opposite interms of adding and subtracting the following two lines.
          * 
                position.X = position.X - (float)x;
                position.Y = position.Y + (float)y;

          * 
          * 
          * 
          * 
          */

            m_bIsPlayer = isplayer;
            Vector2 prevPosition = position;

            double speed = tankSpeed;// (double)tankSpeed;
            float radian = MathHelper.ToRadians(tankAngleInDegree);



            if (tankAngleInDegree > 0 && tankAngleInDegree < 90)
            {

                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X - (float)x;
                position.Y = position.Y + (float)y;
            }
            else if (tankAngleInDegree < 360 && tankAngleInDegree > 270)
            {
                tankAngleInDegree = 360f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X + (float)x;
                position.Y = position.Y + (float)y;


            }
            else if (tankAngleInDegree < 270 && tankAngleInDegree > 180)
            {
                tankAngleInDegree = 270f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double x = (speed * Math.Cos(((double)radian)));


                double y = (speed * Math.Sin((double)radian));


                position.X = position.X + (float)x;
                position.Y = position.Y - (float)y;


            }
            else if (tankAngleInDegree < 180 && tankAngleInDegree > 90)
            {

                tankAngleInDegree = 180f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X - (float)x;
                position.Y = position.Y - (float)y;



            }
            else if (tankAngleInDegree == 0)
            {

                position.Y = position.Y + (int)speed;



            }


            else if (tankAngleInDegree == 180)
            {

                position.Y = position.Y - (int)speed;



            }
            else if (tankAngleInDegree == 270)
            {

                position.X = position.X + (int)speed;



            }
            else if (tankAngleInDegree == 90)
            {

                position.X = position.X - (int)speed;



            }

            // keep the player tank within the window boundary;
            // again do this only for player tank and not for npc tank
            if (m_bIsPlayer)
            {
                int MaxX = GlobalClass.scrWidth - GlobalClass.plHeight / 2;
                int MaxY = GlobalClass.scrHeight - GlobalClass.plHeight / 2;
                int MinX = GlobalClass.plHeight / 2;
                int MinY = GlobalClass.plHeight / 2;

                if (position.X > MaxX)
                {
                    position.X = MaxX;
                    position.Y = prevPosition.Y;
                }
                else if (position.X < MinX)
                {
                    position.X = MinX;
                    position.Y = prevPosition.Y;
                }

                if (position.Y > MaxY)
                {
                    position.Y = MaxY;
                    position.X = prevPosition.X;

                }
                else if (position.Y < MinY)
                {
                    position.Y = MinY;
                    position.X = prevPosition.X;
                }
            }




            return position;

        }


        public static Vector2 updateBulletPosition(float _turretAngleInDegree, Vector2 _position, float speed) //Vector2 currentPlayerTankPosition,float currentTankAngle, float currentTankTurretAngleInDegree)
        {/* This method is for updating the bullet position. Please take a look at UpdateTankPositionUp() method for detail
          * comment.
          * 
          * 
          * 
          */ 

            Vector2 position = _position;
            float tankAngleInDegree = _turretAngleInDegree;
            //speed = 10f;// (double)tankSpeed;
            //tankAngleInDegree = MathHelper.ToDegrees(tankAngleInDegree);
            float radian = MathHelper.ToRadians(tankAngleInDegree);



            if (tankAngleInDegree > 0 && tankAngleInDegree < 90)
            {

                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X + (float)x;
                position.Y = position.Y - (float)y;
            }
            else if (tankAngleInDegree < 360 && tankAngleInDegree > 270)
            {
                tankAngleInDegree = 360f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X - (float)x;
                position.Y = position.Y - (float)y;


            }
            else if (tankAngleInDegree < 270 && tankAngleInDegree > 180)
            {
                tankAngleInDegree = 270f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double x = (speed * Math.Cos(((double)radian)));


                double y = (speed * Math.Sin((double)radian));


                position.X = position.X - (float)x;
                position.Y = position.Y + (float)y;


            }
            else if (tankAngleInDegree < 180 && tankAngleInDegree > 90)
            {

                tankAngleInDegree = 180f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                double y = (speed * Math.Cos(((double)radian)));


                double x = (speed * Math.Sin((double)radian));


                position.X = position.X + (float)x;
                position.Y = position.Y + (float)y;



            }
            else if (tankAngleInDegree == 0)
            {

                position.Y = position.Y - (int)speed;



            }


            else if (tankAngleInDegree == 180)
            {

                position.Y = position.Y + (int)speed;



            }
            else if (tankAngleInDegree == 270)
            {

                position.X = position.X - (int)speed;



            }
            else if (tankAngleInDegree == 90)
            {

                position.X = position.X + (int)speed;



            }


            return position;
            #region prevCode
            //    float _slope = calculateSlope(_turretAngleInDegree);
            //    //float speed = 10;
            //    if ((_turretAngleInDegree > 270) || (_turretAngleInDegree < 90))
            //    {
            //        _position.Y = _position.Y - speed;
            //        //_position.X++;
            //        float x;
            //        if (_slope == 0)
            //            x = 0;
            //        else x = (speed / _slope);

            //        _position.X = _position.X + (float) x;
            //    }
            //    else if ((_turretAngleInDegree < 270) && (_turretAngleInDegree > 90))
            //    {

            //        _position.Y = _position.Y + speed;
            //        //_position.X++;
            //        float x;
            //        if (_slope == 0)
            //            x = 0;
            //        else x = (speed / _slope);

            //        _position.X = _position.X - (float) x;

            //    }
            //    return _position;
            //}




            //}
            #endregion prevCode
        }
    }

}
