﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Replica.Entities
{
    class PlayerBase : Entity
    {
        protected Vector3 movementBoundsSize;
        protected List<Trigger> movementBounds;

        protected Vector3 prevMovement;

        protected float yVelocity;
        protected float gravity;
        protected float jumpVelocity;

        public PlayerBase(List<Entity> entities, Level lvl, EntityType type, Transform transform)
            : base(entities, lvl, type, transform, new Vector3(2, 2, 2))
        {
            movementBoundsSize = new Vector3(1.75f, 0.2f, 1.75f); //in Y direction

            Transform t = new Transform();
            movementBounds = new List<Trigger>();
            //Y-, Y+
            Vector3 nextBoundsSize = movementBoundsSize;
            t.position = new Vector3(transform.position.X, bounds.Min.Y - nextBoundsSize.Y / 2.0f, transform.position.Z);
            Trigger trigger = new Trigger(entities, lvl, t, nextBoundsSize);
            movementBounds.Add(trigger);
            entities.Add(trigger);

            t.position = new Vector3(transform.position.X, bounds.Max.Y + nextBoundsSize.Y / 2.0f, transform.position.Z);
            trigger = new Trigger(entities, lvl, t, nextBoundsSize);
            movementBounds.Add(trigger);
            entities.Add(trigger);

            //X-, X+
            nextBoundsSize = new Vector3(movementBoundsSize.Y, movementBoundsSize.X, movementBoundsSize.Z);
            t.position = new Vector3(bounds.Min.X - nextBoundsSize.X / 2.0f, transform.position.Y, transform.position.Z);
            trigger = new Trigger(entities, lvl, t, nextBoundsSize);
            movementBounds.Add(trigger);
            entities.Add(trigger);

            t.position = new Vector3(bounds.Max.X + nextBoundsSize.X / 2.0f, transform.position.Y, transform.position.Z);
            trigger = new Trigger(entities, lvl, t, nextBoundsSize);
            movementBounds.Add(trigger);
            entities.Add(trigger);

            //Z-, Z+
            nextBoundsSize = new Vector3(movementBoundsSize.X, movementBoundsSize.Z, movementBoundsSize.Y);
            t.position = new Vector3(transform.position.X, transform.position.Y, bounds.Min.Z - nextBoundsSize.Z / 2.0f);
            trigger = new Trigger(entities, lvl, t, nextBoundsSize);
            movementBounds.Add(trigger);
            entities.Add(trigger);

            t.position = new Vector3(transform.position.X, transform.position.Y, bounds.Max.Z + nextBoundsSize.Z / 2.0f);
            trigger = new Trigger(entities, lvl, t, nextBoundsSize);
            movementBounds.Add(trigger);
            entities.Add(trigger);

            prevMovement = Vector3.Zero;

            yVelocity = -20;
            gravity = -0.25f;
            jumpVelocity = 10;
        }

        public override void Update(GameTime gameTime)
        {
            MoveY(gameTime);
            HandleCollisions();
        }

        public override void Move(Vector3 velocity)
        {
            base.Move(velocity);
            foreach (Trigger trigger in movementBounds)
            {
                trigger.Move(velocity);
            }
        }

        void HandleCollisions()
        {
            for (int i = 0; i < movementBounds.Count; i++)
            {
                if (movementBounds[i].IsActivated())
                {
                    float offset = 0.05f;
                    Entity collider = movementBounds[i].GetCollider();
                    Vector3 newPosition = transform.position;
                    switch (i)
                    {
                        case 0:
                            if (prevMovement.Y < 0.0f)
                            {
                                newPosition.Y = collider.GetTransform().position.Y + collider.GetBoundsSize().Y / 2 + boundsSize.Y / 2 + offset;
                                yVelocity = 0;
                            }
                            break;
                        case 1:
                            if (prevMovement.Y > 0.0f)
                            {
                                newPosition.Y = collider.GetTransform().position.Y - collider.GetBoundsSize().Y / 2 - boundsSize.Y / 2 - offset;
                                yVelocity = 0;
                            }
                            break;
                        case 2:
                            if (prevMovement.X < 0.0f)
                            {
                                newPosition.X = collider.GetTransform().position.X + collider.GetBoundsSize().X / 2 + boundsSize.X / 2 + offset;
                            }
                            break;
                        case 3:
                            if (prevMovement.X > 0.0f)
                            {
                                newPosition.X = collider.GetTransform().position.X - collider.GetBoundsSize().X / 2 - boundsSize.X / 2 - offset;
                            }
                            break;
                        case 4:
                            if (prevMovement.Z < 0.0f)
                            {
                                newPosition.Z = collider.GetTransform().position.Z + collider.GetBoundsSize().Z / 2 + boundsSize.Z / 2 + offset;
                            }
                            break;
                        case 5:
                            if (prevMovement.Z > 0.0f)
                            {
                                newPosition.Z = collider.GetTransform().position.Z - collider.GetBoundsSize().Z / 2 - boundsSize.Z / 2 - offset;
                            }
                            break;
                        default:
                            break;
                    };
                    Move(newPosition - transform.position);
                }
            }
        }

        void MoveY(GameTime gameTime)
        {
            yVelocity = yVelocity + gravity;
            Vector3 yVector = new Vector3(0, yVelocity, 0);
            yVector *= (float)gameTime.ElapsedGameTime.TotalSeconds;

            Move(yVector);
            prevMovement.Y = yVector.Y;
        }
    }
}
