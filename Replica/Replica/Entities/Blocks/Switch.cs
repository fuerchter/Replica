﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Replica.Entities.Blocks
{
    class Switch : Block
    {
        bool collided;
        bool activated; //Not that easy since we can't check whether Switch is NOT colliding

        public Switch(List<Entity> entities, Level lvl, Transform transform, Vector3 boundsSize, String color)
            : base(entities, lvl, transform, boundsSize, EntityType.Switch)
        {
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
            solid = false;
        }

        public override void Update(GameTime gameTime)
        {
            activated = collided;
            collided = false;
        }

        public override void OnCollision(Entity entity)
        {
            //TODO 2: Need universal expression for this (see Door)
            if (entity.GetEntityType() == EntityType.Player || entity.GetEntityType() == EntityType.Replicant || entity.GetEntityType() == EntityType.ImitatingReplicant)
            {
                collided = true;
            }
        }

        public bool IsActivated()
        {
            return activated;
        }
    }
}
