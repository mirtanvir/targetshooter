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
using System;
using System.Collections.Generic;
using System.Linq;

namespace targetshooter
{
    class help
    {

        string helpMsg;
        Vector2 position;
        SpriteFont helpFont;

        public help(SpriteFont helpF, Vector2 pos, string helpString)
        {
            this.helpFont = helpF;
            this.position = pos;
            this.helpMsg = helpString;
        
        }

        public Vector2 HelpPosition {

            get {

                return position;
            
            }
            set {
                position = value;
            
            }
        
        }

        public SpriteFont helpSprite {

            get {

                return helpFont;
            
            }
            set {

                helpFont = value;
            
            }
        
        }

        public string HelpString
        {

            get
            {


                return helpMsg;
            }
            set {

                helpMsg = value;
            
            }
        }





    }
}
