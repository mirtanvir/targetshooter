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
    class playerTank : BaseTank
    {

        public playerTank(Texture2D imgOfTank, Texture2D imgOfTankTurret, Vector2 firstPosition, Vector2 turretPos)
            : base(imgOfTank, imgOfTankTurret, firstPosition, turretPos)
        {


        }

        public float getTankAngle()
        {

            return MathHelper.ToRadians((float)base.tankAngle);

        }
        public float getTurretAngle()
        {

            return MathHelper.ToRadians((float)base.turretAngle);
        
        }

        public void rotateTankClockwise()
        {


            int currentTankAngle = tankAngle;
            int currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle + 1;
            base.tankAngle = currentTankAngle + 1;
            fixTankandTurret();


        }
        public void rotateTurretClockwise()
        {

            int currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle + 1;
            fixTankandTurret();
        
        }

        public void rotateTurretCounterClockwise()
        {
            int currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle - 1;
            fixTankandTurret();
            
        
        }
        public void rotateTankCounterClockwise()
        {

            int currentTankAngle = tankAngle;
            int currentTurretAngle = turretAngle;

            base.turretAngle = currentTurretAngle - 1;
            base.tankAngle = currentTankAngle - 1;
            fixTankandTurret();

        }

        private void fixTankandTurret()
        {
            if (tankAngle > 360)
                base.tankAngle = 0;
            else if (tankAngle < 0)
                base.tankAngle = 360 - tankAngle;
        }

        public void movePlayerTankUp()
        {
        
            Vector2 newPos= updateClass.UpdateTankPositionUp(tankAngle, Position,TankSpeed);
            base.MoveTank(newPos);
                base.TurretPosition = Position + new Vector2(60,60);
        
        }

        public void MovePlayerTankDown()
        {

            Vector2 newPos = updateClass.updateTankPositionDown(tankAngle, Position, TankSpeed);
            base.MoveTank(newPos);
            base.TurretPosition = Position + new Vector2(60, 60);
        
        }
        
    }
}
