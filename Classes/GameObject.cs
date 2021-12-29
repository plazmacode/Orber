using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    public abstract class GameObject
    {
        protected Vector2 screenPosition;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 origin;

        protected float speed;
        protected float rotation;

        protected Texture2D sprite;
        protected Texture2D[] sprites;
        protected float scale;
        protected float layerDepth;

        protected Rectangle collisionBox;

        /// <summary>
        /// The position of the object
        /// <para>For pathfinding use the screenPosition</para>
        /// </summary>
        public Vector2 Position { get => position; set => position = value; }
        /// <summary>
        /// The Screen position is different dependent on the object.
        /// <para>Lootable screenposition uses the RoomBuilder.Room for different positioning</para>
        /// </summary>
        public Vector2 ScreenPosition { get => screenPosition; set => screenPosition = value; }

        public virtual Rectangle CollisionBoxProp { get => collisionBox; }

        public abstract void LoadContent(ContentManager content);

        public virtual void Update(GameTime gameTime)
        {
            screenPosition = position - GameWorld.CameraPositionProp;
            collisionBox = new Rectangle((int)screenPosition.X - (int)origin.X, (int)screenPosition.Y - (int)origin.Y, sprite.Width, sprite.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, screenPosition, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }

        /// <summary>
        /// Is executed when a collision occurs
        /// </summary>
        /// <param name="other">The object we collided with</param>
        public abstract void OnCollision(GameObject other);

        /// <summary>
        /// Check if this GameObject has collided with another GameObject
        /// </summary>
        /// <param name="other">The Object we collided with</param>
        public void CheckCollision(GameObject other)
        {
            if (CollisionBoxProp.Intersects(other.CollisionBoxProp))
            {
                OnCollision(other);
            }
        }
        
        protected virtual void CreateOrigin()
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

    }
}
