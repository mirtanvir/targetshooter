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


        public void randomTank()
        {
            int x, y;
            Random random= new Random();
            if (enemyList.Count() == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    x = random.Next(0, graphics.GraphicsDevice.Viewport.Width);
                    y = random.Next(0, graphics.GraphicsDevice.Viewport.Height);
                    /*for (int i = 0; i < enemyList.Count; i++)
                    {
                        if (enemyList.ElementAt(i).Position.X = x && enemyList.ElementAt(i).Position.Y = y)


                    }*/
                    NPCTank en = new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60));
                    for (int j = 0; j < 50; j++)
                        //{

                        en.rotateTankClockwise();

                    //}

                    enemyList.Add(en);
                }
                //numOfEnemy = 3;
            }
            /*   else if (numOfEnemy < 3)
               {
                   x = random.Next(0, 1200);
                   y = random.Next(0, 600);
                   enemyList.Add(new NPCTank(enemyTankTexture, enemyTurretTexture, enemyShellTexture, 3f, 1, new Vector2(x, y), new Vector2(x, y) + new Vector2(60, 60)));
                   numOfEnemy++;         
               }
               else
               {
                   enemyList.RemoveAt(0);
                   numOfEnemy--;
               }*/

        
        }




    }
}
