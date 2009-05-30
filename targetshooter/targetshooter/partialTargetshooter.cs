﻿using System;
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
        bool counter = false;
        
        public void randomTank()
        {
            if (enemyList.Count() == 0 && totalNumOfEnemy > 0)
            {
                
                for (int l = 0; l < 8; l++)
                {
                    do
                    {
                        int tmp = random.Next(0, 2);
                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);

                        if (y >= 0 && y <= 10)
                            x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);

                        for (int i = 0; i < enemyList.Count; i++)
                        {
                            for (int j = 1; j < GlobalClass.plWidth / 2; i++)
                            {
                                if (x == enemyList.ElementAt(l).getWidth() + j)
                                {
                                    counter = true;
                                }
                                else if (x == enemyList.ElementAt(l).getWidth() - j)
                                {
                                    counter = true;
                                }
                                else
                                {
                                    for (int k = 1; k < GlobalClass.plHeight / 2; k++)
                                    {
                                        if (y == enemyList.ElementAt(l).getHeight() + j)
                                        {
                                            counter = true;
                                        }
                                        else if (y == enemyList.ElementAt(l).getHeight() - j)
                                        {
                                            counter = true;
                                        }
                                    }
                                }
                            }

                        }
                    } while (counter != true);

                    NPCTank en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));

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
                int tmp = random.Next(0, 2);
                y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);

                if (y >= 0 && y <= 10)
                    x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                if (tmp == 0)
                    x = 10;
                else
                    x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);

                NPCTank en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));

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

                enemyList.Add(en);
                totalNumOfEnemy--;

            }
            // create boss
            else if (totalNumOfEnemy == 0 && enemyList.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {

                    int tmp = random.Next(0, 2);
                    y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);

                    if (y >= 0 && y <= 10)
                        x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                    if (tmp == 0)
                        x = 10;
                    else
                        x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);

                    NPCTank en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));
                    en.setIsBoss(true);
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

                    enemyList.Add(en);
                    totalNumOfEnemy--;
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
        public bool isTankInScreen(Vector2 maxWindowPosition, NPCTank tank)
        {
            // for (int i = 0; i < enemyList.Count(); i++)
            //{
            if (tank.getWidth() < 0 || (tank.getWidth() > maxWindowPosition.X) || (tank.getHeight() < 0) || (tank.getHeight() > maxWindowPosition.Y))
                return false;
            else
                return true;
            // }
            //return true;


        }
        
          

    }

}
