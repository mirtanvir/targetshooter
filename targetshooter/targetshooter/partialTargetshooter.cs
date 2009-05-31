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
        int x, y;
        Random random = new Random();
        int totalNumOfEnemy = 10;
        bool counter ;
        NPCTank en;
        
        public void randomTank()
        {
            if (enemyList.Count() == 0 && totalNumOfEnemy > 0)
            {
                
                for (int l = 0; l < 8; l++)
                {
                    
                    do
                    {
                        counter = false;
                        int tmp = random.Next(0, 2);

                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                        if (y >= 0 && y <= 10)
                            x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                         en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));

                        Rectangle nt = new Rectangle((int)en.Position.X, (int)en.Position.Y, en.tankImage.Width, en.tankImage.Height);

                        for (int k = 0; k < enemyList.Count; k++)
                        {
                            Rectangle e = new Rectangle((int)enemyList.ElementAt(k).Position.X, (int)enemyList.ElementAt(k).Position.Y,enemyList.ElementAt(k).tankImage.Width,enemyList.ElementAt(k).tankImage.Height);

                            if (nt.Intersects(e))
                                counter = true;
                        }
                      
                    } while (counter == true);

 
                    if (x == 10 && y < Window.ClientBounds.Height/2)
                        for (int j = 0; j < 300; j++) //320
                            en.rotateTankClockwise();
                    else if (x == 10 && y > Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 240; j++) //225
                            en.rotateTankClockwise();
                    else if (x == Window.ClientBounds.Width -10 && y < Window.ClientBounds.Height/2)
                        for (int j = 0; j < 60; j++)  //45
                            en.rotateTankClockwise();
                    else if (x == Window.ClientBounds.Width - 10 && y > Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 112; j++)  //135
                            en.rotateTankClockwise();

                    enemyList.Add(en);
                    totalNumOfEnemy--;
                }
            }
            else if (enemyList.Count() < 3 && totalNumOfEnemy > 0)
            {
                do
                    {
                        counter = false;
                        int tmp = random.Next(0, 2);

                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                        if (y >= 0 && y <= 10)
                            x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                         en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));

                        Rectangle nt = new Rectangle((int)en.Position.X, (int)en.Position.Y, en.tankImage.Width, en.tankImage.Height);

                        for (int k = 0; k < enemyList.Count; k++)
                        {
                            Rectangle e = new Rectangle((int)enemyList.ElementAt(k).Position.X, (int)enemyList.ElementAt(k).Position.Y,enemyList.ElementAt(k).tankImage.Width,enemyList.ElementAt(k).tankImage.Height);

                            if (nt.Intersects(e))
                                counter = true;
                        }
                      
                    } while (counter == true);

 
                    if (x == 10 && y < Window.ClientBounds.Height/2)
                        for (int j = 0; j < 300; j++) //320
                            en.rotateTankClockwise();
                    else if (x == 10 && y > Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 240; j++) //225
                            en.rotateTankClockwise();
                    else if (x == Window.ClientBounds.Width -10 && y < Window.ClientBounds.Height/2)
                        for (int j = 0; j < 60; j++)  //45
                            en.rotateTankClockwise();
                    else if (x == Window.ClientBounds.Width - 10 && y > Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 112; j++)  //135
                            en.rotateTankClockwise();

                    enemyList.Add(en);
                    totalNumOfEnemy--;
            }
            // create boss
            else if (totalNumOfEnemy == 0 && enemyList.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {

                    do
                    {
                        counter = false;
                        int tmp = random.Next(0, 2);

                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                        if (y >= 0 && y <= 10)
                            x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));

                        Rectangle nt = new Rectangle((int)en.Position.X, (int)en.Position.Y, en.tankImage.Width, en.tankImage.Height);

                        for (int k = 0; k < enemyList.Count; k++)
                        {
                            Rectangle e = new Rectangle((int)enemyList.ElementAt(k).Position.X, (int)enemyList.ElementAt(k).Position.Y, enemyList.ElementAt(k).tankImage.Width, enemyList.ElementAt(k).tankImage.Height);

                            if (nt.Intersects(e))
                                counter = true;
                        }

                    } while (counter == true);


                    if (x == 10 && y < Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 300; j++) //320
                            en.rotateTankClockwise();
                    else if (x == 10 && y > Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 240; j++) //225
                            en.rotateTankClockwise();
                    else if (x == Window.ClientBounds.Width - 10 && y < Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 60; j++)  //45
                            en.rotateTankClockwise();
                    else if (x == Window.ClientBounds.Width - 10 && y > Window.ClientBounds.Height / 2)
                        for (int j = 0; j < 112; j++)  //135
                            en.rotateTankClockwise();
                    en.setIsBoss(true);
                    enemyList.Add(en);
                    totalNumOfEnemy--;
                }

            }

        }
            
    }

}
/*
            if (enemyList.Count() == 0 && totalNumOfEnemy > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    do
                    {
                        int tmp = random.Next(0, 2);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                    } while (x == player.Position.X && y == player.Position.Y);
                 
                    NPCTank en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));

                    int rot = random.Next(0, 360);
                    for (int j = 0; j < rot; j++)
                    {

                        en.rotateTankClockwise();

                    }

                    enemyList.Add(en);
                    totalNumOfEnemy--;
                }
               
            }
            else if (enemyList.Count() < 3 && totalNumOfEnemy > 0)
            {
                do
                {
                    int tmp = random.Next(0, 2);
                    y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                    
                    if (tmp == 0)
                        x = 10;
                    else
                        x = Window.ClientBounds.Width -  10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                    
                } while (x == player.Position.X || y == player.Position.Y);
                NPCTank en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));

                int rot = random.Next(0, 360);
                for (int j = 0; j < rot; j++)
                {

                    en.rotateTankClockwise();

                }

                enemyList.Add(en);
                totalNumOfEnemy--;

            }
                // create boss
            else if (totalNumOfEnemy == 0)
            {
                

            }
        }*/
/*  counter = false;
                        int tmp = random.Next(0, 2);
                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);

                        if (y >= 0 && y <= 10)
                            x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);

                        if (enemyList.Count == 0 || enemyList.Count == 1)
                        {
                            counter = false;
                        }
                        else
                        {
                            for (int i = 0; i < enemyList.Count; i++)
                            {
                                for (int k=0; k < 30; k++)
                                {
                                    if (x == enemyList.ElementAt(i).Position.X + k)
                                    {
                                        for (int j = 0; j < 45; j++)
                                        {
                                            if (y == enemyList.ElementAt(i).Position.Y + j)
                                                counter = true;
                                            else if (y == enemyList.ElementAt(i).Position.Y - j)
                                                counter = true;
                                        }
                                     }
                                    else if (x == enemyList.ElementAt(i).Position.X - k)
                                    {
                                        for (int j = 0; j < 45; j++)
                                        {
                                            if (y == enemyList.ElementAt(i).Position.Y - j)
                                                counter = true;
                                            else if (y == enemyList.ElementAt(i).Position.Y + j)
                                                counter = true;

                                        }
                                    }

                                }

                            }

                        }
                        
                        */