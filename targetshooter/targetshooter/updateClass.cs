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
    static class updateClass
    {

        public static float calculateSlope(float objectAngelInDegree)
        {

            float slope = (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(objectAngelInDegree)));
            return slope;//returning the slope
            //test2
        }


        static string debugString;

        public static Vector2 UpdateTankPositionUp(float tankAngleInDegree, Vector2 position, float tankSpeed, float gameTimeChanged)
        {
            Vector2 prevPosition = position;

            double speed = tankSpeed;// (double)tankSpeed;
            float radian = MathHelper.ToRadians(tankAngleInDegree);



            if (tankAngleInDegree > 0 && tankAngleInDegree < 90)
            {

                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X + x;
                position.Y = position.Y - y;
            }
            else if (tankAngleInDegree < 360 && tankAngleInDegree > 270)
            {
                tankAngleInDegree = 360f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X - x;
                position.Y = position.Y - y;


            }
            else if (tankAngleInDegree < 270 && tankAngleInDegree > 180)
            {
                tankAngleInDegree = 270f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int x = (int)(speed * Math.Cos(((double)radian)));


                int y = (int)(speed * Math.Sin((double)radian));


                position.X = position.X - x;
                position.Y = position.Y + y;


            }
            else if (tankAngleInDegree < 180 && tankAngleInDegree > 90)
            {

                tankAngleInDegree = 180f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X + x;
                position.Y = position.Y + y;



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









            #region prevCode
            //float slope = calculateSlope(tankAngleInDegree);// (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(tankAngleInDegree)));






            //if ((tankAngleInDegree > 270) || (tankAngleInDegree < 90))
            //{
            //    position.Y = position.Y - speed*gameTimeChanged;
            //    //_positionition.X++;
            //    float x;

            //    if (slope == 0)
            //        x = 0;
            //    else x = ((speed* gameTimeChanged) / slope );

            //    position.X = position.X + x ;
            //}
            //else if ((tankAngleInDegree < 270) && (tankAngleInDegree > 90))
            //{

            //    position.Y = position.Y + speed * gameTimeChanged;
            //    //_position.X++;
            //    float x;
            //    if (slope == 0)
            //        x = 0;
            //    else x = ((speed * gameTimeChanged) / slope);

            //    position.X = position.X - x * gameTimeChanged;

            //}

            #endregion prevCode

            return position;


        }



        public static Vector2 updateTankPositionDown(float tankAngleInDegree, Vector2 position, float tankSpeed, float gameTimeChanged)
        {
            Vector2 prevPosition = position;

            double speed =tankSpeed;// (double)tankSpeed;
            float radian = MathHelper.ToRadians(tankAngleInDegree);



            if (tankAngleInDegree > 0 && tankAngleInDegree < 90)
            {

                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X - x;
                position.Y = position.Y + y;
            }
            else if (tankAngleInDegree < 360 && tankAngleInDegree > 270)
            {
                tankAngleInDegree = 360f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X + x;
                position.Y = position.Y + y;


            }
            else if (tankAngleInDegree < 270 && tankAngleInDegree > 180)
            {
                tankAngleInDegree = 270f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int x = (int)(speed * Math.Cos(((double)radian)));


                int y = (int)(speed * Math.Sin((double)radian));


                position.X = position.X + x;
                position.Y = position.Y - y;


            }
            else if (tankAngleInDegree < 180 && tankAngleInDegree > 90)
            {

                tankAngleInDegree = 180f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X - x;
                position.Y = position.Y - y;



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




            return position;

            #region prevCode
            //float slope = calculateSlope(tankAngleInDegree);// (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(tankAngleInDegree)));
            //if ((tankAngleInDegree > 270) || (tankAngleInDegree < 90))
            //{
            //    position.Y = position.Y + tankSpeed * gameTimeChanged;
            //    //_position.X++;
            //    float x;

            //    if (slope == 0)
            //        x = 0;
            //    else x = (tankSpeed  / slope);

            //    position.X = position.X - x * gameTimeChanged;
            //}
            //else if ((tankAngleInDegree < 270) && (tankAngleInDegree > 90))
            //{

            //    position.Y = position.Y - tankSpeed * gameTimeChanged;
            //    //_position.X++;
            //    float x;
            //    if (slope == 0)
            //        x = 0;
            //    else x = (tankSpeed  / slope);

            //    position.X = position.X + x * gameTimeChanged;

            //}

            //return position;
            #endregion prevCode
        }


        public static Vector2 updateBulletPosition(float _turretAngleInDegree, Vector2 _position, float speed) //Vector2 currentPlayerTankPosition,float currentTankAngle, float currentTankTurretAngleInDegree)
        {
            Vector2 position = _position;
            float tankAngleInDegree = _turretAngleInDegree;
             //speed = 10f;// (double)tankSpeed;
             //tankAngleInDegree = MathHelper.ToDegrees(tankAngleInDegree);
            float radian = MathHelper.ToRadians(tankAngleInDegree);

            

            if (tankAngleInDegree > 0 && tankAngleInDegree < 90)
            {

                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X + x;
                position.Y = position.Y - y;
            }
            else if (tankAngleInDegree < 360 && tankAngleInDegree > 270)
            {
                tankAngleInDegree = 360f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X - x;
                position.Y = position.Y - y;


            }
            else if (tankAngleInDegree < 270 && tankAngleInDegree > 180)
            {
                tankAngleInDegree = 270f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int x = (int)(speed * Math.Cos(((double)radian)));


                int y = (int)(speed * Math.Sin((double)radian));


                position.X = position.X - x;
                position.Y = position.Y + y;


            }
            else if (tankAngleInDegree < 180 && tankAngleInDegree > 90)
            {

                tankAngleInDegree = 180f - tankAngleInDegree;
                radian = MathHelper.ToRadians(tankAngleInDegree);
                int y = (int)(speed * Math.Cos(((double)radian)));


                int x = (int)(speed * Math.Sin((double)radian));


                position.X = position.X + x;
                position.Y = position.Y + y;



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

            //        _position.X = _position.X + x;
            //    }
            //    else if ((_turretAngleInDegree < 270) && (_turretAngleInDegree > 90))
            //    {

            //        _position.Y = _position.Y + speed;
            //        //_position.X++;
            //        float x;
            //        if (_slope == 0)
            //            x = 0;
            //        else x = (speed / _slope);

            //        _position.X = _position.X - x;

            //    }
            //    return _position;
            //}




            //}
            #endregion prevCode
        }
    }

}
