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

using Replica.Entities;
using Replica.Entities.Blocks;
using Replica.Statics;

namespace Replica
{
    public class Mainmenu  : Gamestate 

    {
        Button playbutton;
        Button exitbutton;
        Button loadbutton;

        public void init()
        {           
            playbutton = new Button(Assets.play, Game1.graphics.GraphicsDevice);
            playbutton.setPosition(new Vector2(350, 100));

            exitbutton = new Button(Assets.exit, Game1.graphics.GraphicsDevice);
            exitbutton.setPosition(new Vector2(350, 200));

            loadbutton = new Button(Assets.levelselection, Game1.graphics.GraphicsDevice);
            loadbutton.setPosition(new Vector2(350, 300));
        }
        
        public eGamestates update()
        {        
            playbutton.Update(Mouse.GetState());
            exitbutton.Update(Mouse.GetState());
            loadbutton.Update(Mouse.GetState());

            if (playbutton.isClicked)
            {
                return eGamestates.InGame;
            }
            if (exitbutton.isClicked || Input.isClicked(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                return eGamestates.LeaveGame;
            }
            if (loadbutton.isClicked)
            {
                return eGamestates.Levelselection;
            }          
            return eGamestates.MainMenu;
        }

        public void draw()
        {
            Game1.spriteBatch.Draw(Assets.dna, new Rectangle(0, 0, Assets.dna.Width, Assets.dna.Height), Color.White);

            playbutton.Draw(Game1.spriteBatch);           
            exitbutton.Draw(Game1.spriteBatch);
            loadbutton.Draw(Game1.spriteBatch);
        }
         

    }
}
