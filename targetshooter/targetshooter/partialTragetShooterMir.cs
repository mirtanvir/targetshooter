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
    public partial class TargetShooter
    {
        bool isGameWon()
        { 
        
           if((info.level==2) && (enemyList.Count()==0))
            return true;
            else return false;

        
        }

        bool isGameOver()
        {

            if (info.getLives() <= 0)
                return true;
            else return false;


        
        }

        float distance(Vector2 object1Pos, Vector2 object2Pos)
        {

            return Vector2.Distance(object1Pos, object2Pos);
        
        }



        bool tankCollide(Texture2D tankTexture, Vector2 tankPosition, float tankRotationInRadian, Texture2D tank2Texture, Vector2 tank2Position, float tank2RotationInRadian)//  Vector2 object1Pos, int object1Width, int object1Height, Vector2 object2Pos, int object2Width, int object2Height)
        {
            Vector2 tankOrigin = new Vector2(tankTexture.Width / 2, tankTexture.Height / 2);
            Vector2 tank2Origin = new Vector2(tank2Texture.Width / 2, tank2Texture.Height / 2);
            Matrix playerTransform =
            Matrix.CreateTranslation(new Vector3(-tankOrigin, 0.0f)) *
                // Matrix.CreateScale(block.Scale) *  would go here
             Matrix.CreateRotationZ(tankRotationInRadian) *
             Matrix.CreateTranslation(new Vector3(tankPosition, 0.0f));

            // Calculate the bounding rectangle of this block in world space
            Rectangle playerRectangle = CalculateBoundingRectangle(
                     new Rectangle(0, 0, tankTexture.Width, tankTexture.Height),
                     playerTransform);

            Rectangle tank2Rectangle = new Rectangle((int)tank2Position.X, (int)tank2Position.Y,
tank2Texture.Width, tank2Texture.Height);

            Color[] tank2TextureData;
            Color[] playerTextureData;

            playerTextureData =
              new Color[tankTexture.Width * tankTexture.Height];
            tankTexture.GetData(playerTextureData);
            tank2TextureData =
                new Color[tank2Texture.Width * tank2Texture.Height];
            tank2Texture.GetData(tank2TextureData);


           


            Matrix tank2Transform =
            Matrix.CreateTranslation(new Vector3(-tank2Origin, 0.0f)) *
                // Matrix.CreateScale(block.Scale) *  would go here
             Matrix.CreateRotationZ(tank2RotationInRadian) *
             Matrix.CreateTranslation(new Vector3(tank2Position, 0.0f));


            if (tank2Rectangle.Intersects(playerRectangle))
            {
                // Check collision with person
                if (IntersectPixels(tank2Transform, tank2Texture.Width,
                                     tank2Texture.Height, tank2TextureData,
                                    playerTransform, tankTexture.Width,
                                    tankTexture.Height, playerTextureData))
                {
                    return true;

                }

            }

            return false;




            //Rectangle object1Rect = new Rectangle((int)object1Pos.X, (int)object1Pos.Y, object1Width, object1Height);
            //Rectangle object2Rect = new Rectangle((int)object2Pos.X, (int)object2Pos.Y, object2Width, object2Height);

            //return object1Rect.Intersects(object2Rect);
        }




        bool collide(Texture2D tankTexture, Vector2 tankPosition, float tankRotationInRadian,Texture2D bulletTexture, Vector2 bulletPosition)//  Vector2 object1Pos, int object1Width, int object1Height, Vector2 object2Pos, int object2Width, int object2Height)
        {
            Vector2 tankOrigin = new Vector2(tankTexture.Width / 2, tankTexture.Height / 2);

            Matrix playerTransform =
            Matrix.CreateTranslation(new Vector3(-tankOrigin, 0.0f)) *
                // Matrix.CreateScale(block.Scale) *  would go here
             Matrix.CreateRotationZ(tankRotationInRadian) *
             Matrix.CreateTranslation(new Vector3(tankPosition, 0.0f));

            // Calculate the bounding rectangle of this block in world space
            Rectangle playerRectangle = CalculateBoundingRectangle(
                     new Rectangle(0, 0, tankTexture.Width, tankTexture.Height),
                     playerTransform);

            Rectangle bulletRectangle = new Rectangle((int)bulletPosition.X, (int)bulletPosition.Y,
bulletTexture.Width, bulletTexture.Height);

            Color[] bulletTextureData;
            Color[] playerTextureData;

            playerTextureData =
              new Color[tankTexture.Width * tankTexture.Height];
            tankTexture.GetData(playerTextureData);
            bulletTextureData =
                new Color[bulletTexture.Width * bulletTexture.Height];
            bulletTexture.GetData(bulletTextureData);


            Matrix bulletTransform =
Matrix.CreateTranslation(new Vector3(bulletPosition, 0.0f));


            if (bulletRectangle.Intersects(playerRectangle))
            {
                // Check collision with person
                if (IntersectPixels(bulletTransform, bulletTexture.Width,
                                     bulletTexture.Height, bulletTextureData,
                                    playerTransform, tankTexture.Width,
                                    tankTexture.Height, playerTextureData))
                {
                    return true;

                }

            }

            return false;




            //Rectangle object1Rect = new Rectangle((int)object1Pos.X, (int)object1Pos.Y, object1Width, object1Height);
            //Rectangle object2Rect = new Rectangle((int)object2Pos.X, (int)object2Pos.Y, object2Width, object2Height);

            //return object1Rect.Intersects(object2Rect);
        }


          /// <summary>
        /// Determines if there is overlap of the non-transparent pixels between two
        /// sprites.
        /// </summary>
        /// <param name="transformA">World transform of the first sprite.</param>
        /// <param name="widthA">Width of the first sprite's texture.</param>
        /// <param name="heightA">Height of the first sprite's texture.</param>
        /// <param name="dataA">Pixel color data of the first sprite.</param>
        /// <param name="transformB">World transform of the second sprite.</param>
        /// <param name="widthB">Width of the second sprite's texture.</param>
        /// <param name="heightB">Height of the second sprite's texture.</param>
        /// <param name="dataB">Pixel color data of the second sprite.</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(
                            Matrix transformA, int widthA, int heightA, Color[] dataA,
                            Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }


        /// <summary>
        /// Calculates an axis aligned rectangle which fully contains an arbitrarily
        /// transformed axis aligned rectangle.
        /// </summary>
        /// <param name="rectangle">Original bounding rectangle.</param>
        /// <param name="transform">World transform of the rectangle.</param>
        /// <returns>A new rectangle which contains the trasnformed rectangle.</returns>
        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                           Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }


    }

