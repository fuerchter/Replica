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

        public Switch(List<Entity> entities, Transform transform)
            : base(entities, transform, EntityType.Switch)
        {
            collided = false;
            activated = false;

            boundsColor = Color.Green;
        }

        public override void Update(GameTime gameTime)
        {
            activated = collided;
            collided = false;
        }

        public override void OnCollision(Entity entity)
        {
            if (entity.GetEntityType() == EntityType.Player || entity.GetEntityType() == EntityType.Replicant) //TODO: Testing
            {
                collided = true;
            }
        }

        public bool isActivated()
        {
            return activated;
        }
    }
}
