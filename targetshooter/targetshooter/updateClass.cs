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
  static  class  updateClass
    {

      public static float calculateSlope(int objectAngelInDegree)
      {

          float slope = (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(objectAngelInDegree)));
          return slope;//returning the slope
          //test2
      }



      public static Vector2 UpdateTankPositionUp(int tankAngleInDegree, Vector2 position, float tankSpeed)
      {

          float slope = calculateSlope(tankAngleInDegree);// (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(tankAngleInDegree)));

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



          return position;


      }



      public static Vector2 updateTankPositionDown(int tankAngleInDegree, Vector2 position, float tankSpeed)
      {
          float slope = calculateSlope(tankAngleInDegree);// (float)Math.Tan(Convert.ToDouble(MathHelper.ToRadians(90) - MathHelper.ToRadians(tankAngleInDegree)));
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

          return position;
      
      }


      public static Vector2 updateBulletPosition(int _turretAngleInDegree, Vector2 _position, int speed) //Vector2 currentPlayerTankPosition,float currentTankAngle, float currentTankTurretAngleInDegree)
      {
          float _slope = calculateSlope(_turretAngleInDegree);
          //float speed = 10;
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
          return _position;
      }




    }
}
