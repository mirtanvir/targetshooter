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
    class AI
    {
        Vector2 playerTankPosition;


        public bool AimTurret(Vector2 NPCTurretPosition) { 
        
           return true;



        //MathHelper.d
        
        }


        private int findCoOrdinate(Vector2 NPCTurretPosition)
        {



            //if ((NPCTurretPosition.X < playerTankPosition.X) && (NPCTurretPosition.Y > playerTankPosition.Y))
            //{ 
            
            //    return 4;

            
            //}
            //else if ((NPCTurretPosition.X < playerTankPosition.X) && (NPCTurretPosition.Y > playerTankPosition.Y))
            //{ 
            
            //    return 
            
            //}
            //    else if ((NPCTurretPosition.X < playerTankPosition.X) && (NPCTurretPosition.Y > playerTankPosition.Y))
            //{ 
            
            //    return 
            
            //}
            //        else return '

            return 0;
        
        }


    }

     public static class BresLine
    {
        /// <summary>
        /// This function chooses an appropriate private method for rendering the line
        /// based on the begin and end characteristics. These separate methods could be
        /// combined into a single method but I believe that runtime performance while
        /// enumerating through the points would suffer. 
        /// 
        /// (given the overhead involved with the LINQ calls it may not make much difference)
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<Point> RenderLine(Point begin, Point end)
        {
            if (Math.Abs(end.Y - begin.Y) < Math.Abs(end.X - begin.X))
            {
                // dX > dY... not steep
                if (end.X >= begin.X)
                {
                    return BresLineOrig(begin, end);
                }
                else
                {
                    return BresLineReverseOrig(begin, end);
                }
            }
            else // steep (dY > dX)
            {
                if (end.Y >= begin.Y)
                {
                    return BresLineSteep(begin, end);
                }
                else
                {
                    return BresLineReverseSteep(begin, end);
                }
            }
        }

        /// <summary>
        /// Creates a line from Begin to End starting at (x0,y0) and ending at (x1,y1)
        /// * where x0 less than x1 and y0 less than y1
        ///   AND line is less steep than it is wide (dx less than dy)
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static IEnumerable<Point> BresLineOrig(Point begin, Point end)
        {
            Point nextPoint = begin;
            int deltax = end.X - begin.X;
            int deltay = end.Y - begin.Y;
            int error = deltax / 2;
            int ystep = 1;
            if (end.Y < begin.Y)
            {
                ystep = -1;
            }
            else if (end.Y == begin.Y)
            {
                ystep = 0;
            }

            while (nextPoint.X < end.X)
            {
                if (nextPoint != begin) yield return nextPoint;
                nextPoint.X++;

                error -= deltay;
                if (error < 0)
                {
                    nextPoint.Y += ystep;
                    error += deltax;
                }
            }
        }

        /// <summary>
        /// Whenever dy > dx the line is considered steep and we have to change
        /// which variables we increment/decrement
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static IEnumerable<Point> BresLineSteep(Point begin, Point end)
        {
            Point nextPoint = begin;
            int deltax = Math.Abs(end.X - begin.X);
            int deltay = end.Y - begin.Y;
            int error = Math.Abs(deltax / 2);
            int xstep = 1;

            if (end.X < begin.X)
            {
                xstep = -1;
            }
            else if (end.X == begin.X)
            {
                xstep = 0;
            }

            while (nextPoint.Y < end.Y)
            {
                if (nextPoint != begin) yield return nextPoint;
                nextPoint.Y++;

                error -= deltax;
                if (error < 0)
                {
                    nextPoint.X += xstep;
                    error += deltay;
                }
            }
        }

        /// <summary>
        /// If x0 > x1 then we are going from right to left instead of left to right
        /// so we have to modify our routine slightly
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static IEnumerable<Point> BresLineReverseOrig(Point begin, Point end)
        {
            Point nextPoint = begin;
            int deltax = end.X - begin.X;
            int deltay = end.Y - begin.Y;
            int error = deltax / 2;
            int ystep = 1;

            if (end.Y < begin.Y)
            {
                ystep = -1;
            }
            else if (end.Y == begin.Y)
            {
                ystep = 0;
            }

            while (nextPoint.X > end.X)
            {
                if (nextPoint != begin) yield return nextPoint;
                nextPoint.X--;

                error += deltay;
                if (error < 0)
                {
                    nextPoint.Y += ystep;
                    error -= deltax;
                }
            }
        }

        /// <summary>
        /// If x0 > x1 and dy > dx we have to go from right to left and alter the routine
        /// for a steep line
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static IEnumerable<Point> BresLineReverseSteep(Point begin, Point end)
        {
            Point nextPoint = begin;
            int deltax = end.X - begin.X;
            int deltay = end.Y - begin.Y;
            int error = deltax / 2;
            int xstep = 1;

            if (end.X < begin.X)
            {
                xstep = -1;
            }
            else if (end.X == begin.X)
            {
                xstep = 0;
            }

            while (nextPoint.Y > end.Y)
            {
                if (nextPoint != begin) yield return nextPoint;
                nextPoint.Y--;

                error += deltax;
                if (error < 0)
                {
                    nextPoint.X += xstep;
                    error -= deltay;
                }
            }
        }
    }



  class XNAShape
  {
      Texture2D pixel;
      GraphicsDevice myDevice;

      public XNAShape(GraphicsDevice myDevice)
      {
          this.myDevice = myDevice;
          CreatePixelTexture();

          myDevice.DeviceReset += new EventHandler(myDevice_DeviceReset);
      }

      //Each time the device is reset, we need to recreate the Texture - otherwise it crashes in windowed mode
      void myDevice_DeviceReset(object sender, EventArgs e)
      {
          CreatePixelTexture();
      }

      //Creates a white 1*1 Texture that is used for the lines by scaling and rotating it
      public void CreatePixelTexture()
      {
          int TargetWidth = 1;
          int TargetHeight = 1;

          RenderTarget2D LevelRenderTarget = new RenderTarget2D(myDevice, TargetWidth, TargetHeight, 1,
              myDevice.PresentationParameters.BackBufferFormat, myDevice.PresentationParameters.MultiSampleType,
              myDevice.PresentationParameters.MultiSampleQuality, RenderTargetUsage.PreserveContents);

          DepthStencilBuffer stencilBuffer = new DepthStencilBuffer(myDevice, TargetWidth, TargetHeight,
              myDevice.DepthStencilBuffer.Format, myDevice.PresentationParameters.MultiSampleType,
              myDevice.PresentationParameters.MultiSampleQuality);

          myDevice.SetRenderTarget(0, LevelRenderTarget);

          // Cache the current depth buffer
          DepthStencilBuffer old = myDevice.DepthStencilBuffer;
          // Set our custom depth buffer
          myDevice.DepthStencilBuffer = stencilBuffer;

          myDevice.Clear(Color.White);

          myDevice.SetRenderTarget(0, null);

          // Reset the depth buffer
          myDevice.DepthStencilBuffer = old;
          pixel = LevelRenderTarget.GetTexture();
      }

      //Calculates the distances and the angle and than draws a line
      public void DrawLine(SpriteBatch sprite,Vector2 start, Vector2 end, Color color)
      {
          int distance = (int)Vector2.Distance(start, end);

          Vector2 connection = end - start;
          Vector2 baseVector = new Vector2(1, 0);

          float alpha = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

          if (pixel != null)
              sprite.Draw(pixel, new Rectangle((int)start.X, (int)start.Y, distance, 1),
                  null, color, alpha, new Vector2(0, 0),SpriteEffects.None, 0);
      }

      //Draws a rect with the help of DrawLine
      public void DrawRect(SpriteBatch sprite, Rectangle rect, Color color)
      {
          // | left
          DrawLine(sprite, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height), color);
          // - top
          DrawLine(sprite, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y ), color);
          // - bottom
          DrawLine(sprite, new Vector2(rect.X, rect.Y + rect.Height),
              new Vector2(rect.X + rect.Width, rect.Y + rect.Height), color);
          // | right
          DrawLine(sprite, new Vector2(rect.X + rect.Width, rect.Y),
              new Vector2(rect.X + rect.Width, rect.Y + rect.Height), color);

      }
  }
}



