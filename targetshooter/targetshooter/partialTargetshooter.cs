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
        int totalNumOfEnemy = 4;
        bool counter ;
        NPCTank en;
        bool isBoss = false;
        int createTank = 3;
        bool level2Flag = true;
        bool level2FontFlag = false;
        bool playerStuck = false;
        Vector2 enemyStuckPos;
        int stuckEnemyID;
        
        /**
         * Create enemy tanks at random location
         * 
         * @param
         * @return
         * 
         * */
        public void randomTank()
        {
            // check if it is level 2
            // then change appropriate variable for level 2
            if ((info.level == 2) && (level2Flag))
            {
                createTank = 5;
                totalNumOfEnemy = 7;
                level2Flag = false;
                level2FontFlag = true;
                gameFlag = false;
                player.resetPlayer();
            }

            // Create the first tanks and add to the list
            if (enemyList.Count() == 0 && totalNumOfEnemy > 0)
            {
                
                for (int l = 0; l < createTank; l++)
                {
                    
                    do
                    {
                        counter = false;
                        // get random side for tank
                        // 0 for left and 1 for right
                        int tmp = random.Next(0, 2);

                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                        if (y >= 0 && y <= 10)
                            x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; //random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        enemyTankID++;
                        en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60), enemyTankID);

                        // create a rectangle with the same size as the tank
                        Rectangle nt = new Rectangle((int)en.Position.X, (int)en.Position.Y, en.tankImage.Width, en.tankImage.Height);

                        // check if the new created tanks is overlapping with the others
                        for (int k = 0; k < enemyList.Count; k++)
                        {
                            Rectangle e = new Rectangle((int)enemyList.ElementAt(k).Position.X, (int)enemyList.ElementAt(k).Position.Y,enemyList.ElementAt(k).tankImage.Width,enemyList.ElementAt(k).tankImage.Height);
                            
                            if (nt.Intersects(e))
                                counter = true;
                        }
                      
                    } while (counter == true);

                    // Turn the tank so the tank stay in screen
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
                        enemyTankID++;    
                    en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60),enemyTankID);

                        Rectangle nt = new Rectangle((int)en.Position.X, (int)en.Position.Y, en.tankImage.Width, en.tankImage.Height);

                        // check if the new created tanks is overlapping with the others
                        for (int k = 0; k < enemyList.Count; k++)
                        {
                            // create a rectangle with the same size as the tank
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
                for (int i = 0; i < createTank; i++)
                {

                    do
                    {
                        counter = false;
                        // get random side for tank
                        // 0 for left and 1 for right
                        int tmp = random.Next(0, 2);

                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                        if (y >= 0 && y <= 10)
                            x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        if (tmp == 0)
                            x = 10;
                        else
                            x = Window.ClientBounds.Width - 10; 
                        enemyTankID++;
                        en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60),enemyTankID);

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
                    isBoss = true;
                    enemyList.Add(en);
                    totalNumOfEnemy--;
                }

            }

        }
            
    }

}
