using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace targetshooter
{
    class infoBar
    {
        int currentLevel;
        int currentScore;
        int lives;

        int health;
        Vector2 currentPosition;
        SpriteFont infoBarFont;
        public infoBar(int level, int score, SpriteFont fontImage, Vector2 pos)
        {

            this.level = level;
            this.score = score;
            this.infoBarFont = fontImage;
            this.currentPosition = pos;

            //this.lives = lives;
            //this.health = health;

        }
        public void updateHealthAndLives(int lives, int health)
        {

            this.health = health;
            this.lives = lives;
        

        }

        public string getInfoBar()
        {
            string info = "Current Level = " + currentLevel + "  Current Score = " + currentScore + "  Number of Lives Remaining = " + lives + "  Current Health = " + health;
            return info;
        }

        public int getHealh()
        {

            return health;
        

        }

        public int getLives()
        {

            return lives;
        
        }

        public int level {


            get {

                return currentLevel;
            
            }
            set {

                currentLevel = value;
            
            }
        
        }

        public int score {

            get {

                return currentScore;
            
            }
            set {

                currentScore = value;

            
            }
        
        }

        public Vector2 position
        {

            get
            {

                return currentPosition;

            }
            set
            {

                currentPosition = value;


            }

        }


        public SpriteFont fontSprite {


            get {

                return infoBarFont;
            
            }
            set {

                infoBarFont = value;
            
            }

        
        }




    }
}
