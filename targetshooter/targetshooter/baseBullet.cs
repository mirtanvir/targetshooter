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
    public class baseBullet
    {
        private Vector2 position;// the current position of the bullet in the screen
        private float speed;// How far the bullet will go on each update
        private float angleInDegree;
        private Texture2D bulletImage;
        private int height;
        private int width;
        private bool dead = false;

        /*
         *  move the bullet
         * 
         * @param bulletPosition -- Vector2
         * @return just adding something
        */

        public bool isBulletInScreen(Vector2 maxWindowPosition)
        {/* This method determines whether the bullet is inside the game screen or not. If it is not
          * inside the game screen,than we will delete the bullet from bullet list to save
          * memory.
          * maxWindowPosition variable holds the maximum co-ordinates of the game screen.
          * 
          */

            if ((position.X < 0) || (position.X > maxWindowPosition.X) || (position.Y < 0) || (position.Y > maxWindowPosition.Y))
            {/* if the position of the bullet is less than zero for both X and Y co-ordinates, than we know
              * that that bullet is ouside the screen.
              * 
              */

                return false;
            }
            else return true;
            //return true;


        }

        //public void makeDead()
        //{

        //    this.dead = true;

        //}


        public bool Dead
        {

            get
            {
                return this.dead;
            }
            set
            {

                this.dead = value;
            }

        }

        protected void move(Vector2 bulletPositon)
        {
            position = bulletPositon;
        }

        protected void update()
        {/*This is the bullet update class. It needs to be called from the main update() method from the TargetShooter class.
          * This method is necessary to move the bullet across the screen
          * 
          */

            position = updateClass.updateBulletPosition(angleInDegree, position, speed);

        }

        /*
         * return the speed of the bullet
         * 
         * @param
         * @return speed
         * 
        */

        public float getSpeed()
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
        protected float getAngle()
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

        protected void setBulletImage(Texture2D bulletImg)
        {

            this.bulletImage = bulletImg;

        }
        protected void setBulletPosition(Vector2 pos)
        {

            position = pos;

        }

        // Constructor
        public baseBullet(Texture2D bulletimg, Vector2 pos, float spd, float rotationAngleInDegree)
        {

            bulletImage = bulletimg;
            this.speed = spd;
            this.position = pos;
            this.angleInDegree = rotationAngleInDegree;
            this.width = bulletImage.Width;
            this.height = bulletImage.Height;
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



        public int getHeight()
        {

            return height;

        }

        public int getWidth()
        {

            return width;


        }


    }
}
