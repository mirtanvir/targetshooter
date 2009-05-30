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



        bool collide(Vector2 object1Pos, int object1Width, int object1Height, Vector2 object2Pos, int object2Width, int object2Height)
        {

            Rectangle object1Rect = new Rectangle((int)object1Pos.X, (int)object1Pos.Y, object1Width, object1Height);
            Rectangle object2Rect = new Rectangle((int)object2Pos.X, (int)object2Pos.Y, object2Width, object2Height);

            return object1Rect.Intersects(object2Rect);
        }


    }
}
