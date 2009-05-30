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
        int totalNumOfEnemy = 50;

        public void randomTank()
        {
            
            if (enemyList.Count() == 0 && totalNumOfEnemy > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    do
                    {
                        x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                        y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
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
               
            }
            else if (enemyList.Count() < 3 && totalNumOfEnemy > 0)
            {
                do
                {
                    x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                    y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
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
        }
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
