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
using Replica.Drawables;

namespace Replica.Gamestates
{
    public class Ingame : Gamestate
    {
        List<Entity> entities;

        List<Drawable> drawables;

        Level lvl;
        GraphicsDevice gDevice;
        BasicEffect defaultEffect;

        AudioListener listener=new AudioListener();

        public void Init(GraphicsDevice gDevice)
        {
            this.gDevice = gDevice;
            defaultEffect = new BasicEffect(gDevice);
            defaultEffect.VertexColorEnabled = true;

            entities = new List<Entity>();

            drawables = new List<Drawable>();

            drawables.Add(new Skillbar());

            foreach (Drawable drawable in drawables)
            {
                drawable.Initialize();
            }

            lvl = new Level(entities);
        }

        public eGamestates Update(GameTime gameTime)
        {
            Transform playerTransform=lvl.GetPlayer().GetTransform();
            listener.Position = playerTransform.position;
            listener.Forward = playerTransform.Forward;
            listener.Up = playerTransform.Up;

            for (int i = 0; i < entities.Count; i++) //Certain entities will create/delete other entities in their Update, foreach does not work
            {
                entities[i].Update(gameTime, listener);
            }

            CollisionSystem.CheckCollisions(entities);


            if(Input.isClicked(Microsoft.Xna.Framework.Input.Keys.Escape))
                return eGamestates.MainMenu;

            if (Globals.reachedGoal == true)
            {
                Globals.reachedGoal = false;
                return eGamestates.Credits;
            }

            foreach (Drawable drawable in drawables)
            {
                drawable.Update();
            }

            return eGamestates.InGame;
        }

        public void Draw(GraphicsDevice graphicDevice, GameTime gameTime)
        {
            Camera camera = lvl.GetPlayer().GetCamera();
            defaultEffect.World = Matrix.Identity;
            defaultEffect.View = camera.GetView();
            defaultEffect.Projection = camera.GetProjection();

            

            foreach (Entity entity in entities)
            {
                entity.Draw(graphicDevice, gameTime, defaultEffect, camera);
            }

            

            foreach (Drawable drawable in drawables)
            {
                drawable.Draw();
            }
        }
    }
}
