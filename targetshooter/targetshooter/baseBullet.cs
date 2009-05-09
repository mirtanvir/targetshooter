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
    class baseBullet
    {
        private Vector2 position;
        private float speed;
        private int angleInDegree;
        private Texture2D bulletImage;


        /*
         *  move the bullet
         * 
         * @param bulletPosition -- Vector2
         * @return just adding something
        */
        protected void move(Vector2 bulletPositon)
        {
            position = bulletPositon;
        }

        /*
         * return the speed of the bullet
         * 
         * @param
         * @return speed
         * 
        */

        protected float getSpeed()
        {
            return this.speed;
        }

        /*
         * return the angle of the bullet
         * 
         * @param
         * @return angleInDegree
         * 
        */
        protected int getAngle()
        {
            return this.angleInDegree;
        }

        /*
         * return the postion of the bullet
         * 
         * @param
         * @return position
         * 
        */
        protected Vector2 getBulletPosition()
        {

            return position;
        
        }

        /*
         * return the image of the bullet
         * 
         * @param
         * @return bulletImage
         * 
        */
        protected Texture2D getBulletImage()
        {

            return bulletImage;
            
        }

        // Constructor
        public baseBullet(Texture2D bulletimg, float spd, Vector2 pos,int rotationAngleInDegree)
        {

            bulletImage = bulletimg;
            this.speed = spd;
            this.position = pos;
            this.angleInDegree = rotationAngleInDegree;
        }

        /*
         * set the speed of the bullet
         * 
         * @param spd -- float
         * @return 
         * 
        */
        protected void setSpeed(float spd)
        {

            speed = spd;

        }

        /*
         * return the bullet's position, speed, angle, and image
         * 
         * @param position -- Vector2
         * @param speed -- float
         * @param angleInDegree -- int
         * @param image -- Textture2D
         * @return 
         * 
        */
        protected void update(Vector2 position, float speed, int angleIndegree, Texture2D image)
        {
            this.position = position;
            this.speed = speed;
            this.angleInDegree = angleIndegree;
            this.bulletImage = image;

        }

    }
}
