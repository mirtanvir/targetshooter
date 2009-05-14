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
    class playerTankShell: baseBullet
    {

      


        public playerTankShell(Texture2D playerTankShellImage, Vector2 firingPosition,float speed,int turretAngle)
            : base(playerTankShellImage, firingPosition,speed, turretAngle)
        { 
        
        
        }

        public Texture2D getBulletImage()
        {

            return base.getBulletImage();
        
        }

        public void setBulletImage(Texture2D bulletImage)
        {

            base.setBulletImage(bulletImage);
        
        }

        public void setBulletSpeed(float speed)
        {

            base.setSpeed(speed);
        
        }

        public void updateBulletPosition()
        {

            base.update();

        
        }

        public Vector2 getShellPosition()
        {

            return base.getBulletPosition();
        
        }
    }
}
