using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace targetshooter
{
  static  class  updateClass
    {

      protected static float calculateSlope(int objectAngelInDegree)
      {
      


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




    }
}
