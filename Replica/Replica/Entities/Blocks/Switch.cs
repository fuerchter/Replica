﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Replica.Statics;

namespace Replica.Entities.Blocks
{
    class Switch : Block
    {
        bool collided;
        bool activated; //Not that easy since we can't check whether Switch is NOT colliding
        public bool Activated { get { return activated; } }

        string color;

        public Switch(List<Entity> entities, Level lvl, Transform transform, Vector3 boundsSize, String color)
            : base(entities, lvl, transform, boundsSize, EntityType.Switch)
        {
            this.color = color;
            if (color == "green")
            {
                boundsColor = Color.Green;
            }
            if (color == "red")
            {
                boundsColor = Color.Red;
            }
            if (color == "blue")
            {
                boundsColor = Color.Blue;
            }
            drawBounds = true;
            solid = false;

            draw = false;
        }

        public override void Update(GameTime gameTime, AudioListener listener)
        {
            activated = collided;
            collided = false;
        }

        public override void Draw(GraphicsDevice graphics, GameTime gameTime, BasicEffect effect, Camera camera)
        {
            Globals.DrawModel(Assets.redSwitchModel, t, 1, 1, camera);
            //Identifying how many switches are activated
            List<Switch> switches=lvl.GetSwitches(color);
            int numActivated=0;
            foreach (Switch s in switches)
            {
                if (s.Activated)
                {
                    numActivated++;
                }
            }
            string text = numActivated + "/" + switches.Count;

            //Drawing 3D Text
            Matrix rotation = Matrix.Identity;
            rotation.Right = -lvl.P.T.Right;
            rotation.Up = lvl.P.T.Up;

            BasicEffect textEffect = new BasicEffect(Game1.graphics.GraphicsDevice);
            textEffect.World = rotation * Matrix.CreateScale(new Vector3(-0.075f)) * Matrix.CreateTranslation(t.position);
            textEffect.View = effect.View;
            textEffect.Projection = effect.Projection;
            textEffect.TextureEnabled = true;
            textEffect.VertexColorEnabled = true;

            //Having to end already open spriteBatch
            Game1.spriteBatch.End();
            Game1.graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Game1.spriteBatch.Begin(0, null, SamplerState.PointWrap, DepthStencilState.DepthRead, null, textEffect);

            Vector2 textSize = Assets.font1.MeasureString(text);
            Game1.spriteBatch.DrawString(Assets.font1, text, -textSize / 2.0f, Color.White);

            Game1.spriteBatch.End();
            Game1.graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Game1.spriteBatch.Begin();

            base.Draw(graphics, gameTime, effect, camera);
        }

        public override void OnCollision(Entity entity)
        {
            //TODO 2: Need universal expression for this (see Door)
            if (entity.Type == EntityType.Player || entity.Type == EntityType.Replicant || entity.Type == EntityType.ImitatingReplicant)
            {
                collided = true;
            }
        }
    }
}
