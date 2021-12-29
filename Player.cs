using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Orber
{
    public class Player : Character
    {
        private bool isAutoMoving;
        private float autoLootSpeed = 0.2f;
        private float lootX;
        private float lootY;

        public Player()
        {
            layerDepth = 0.4f;
            scale = 1f;
            speed = 400;
            isAutoMoving = true;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("player");
            CreateOrigin();
            position = new Vector2(GameWorld.ScreenSizeProp.X / 2, GameWorld.ScreenSizeProp.Y / 2);
            //GameWorld.CameraPositionProp = position;
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            collisionBox = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), sprite.Width, sprite.Height);
            HandleInput();
            Move(gameTime);
            AutoMove(gameTime);
            GameWorld.DebugTexts.Add("CameraPosition: " + GameWorld.CameraPositionProp.X.ToString() + ": " + GameWorld.CameraPositionProp.Y);
            GameWorld.DebugTexts.Add("Lootable: " + lootX.ToString() + ": " + lootY.ToString());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, scale, SpriteEffects.None, layerDepth);
        }

        private void HandleInput()
        {
            velocity = Vector2.Zero;
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }
            if (GameWorld.KeyStateProp.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }
        }

        /// <summary>
        /// Creates a rectangle in the future position of the player, and checks if that rectangle is inside the world and doesn't collide with another object.
        /// <para>If no collisions are found, the players position will be updated</para> 
        /// <para>Updates the position of the playerTarget, which is where the enemies can see the player. When the enemies are attacking the target will freeze position.</para> 
        /// </summary>
        /// <param name="gameTime">Time reference for running update code at a fixed interval</param>
        protected override void Move(GameTime gameTime)
        {
            // If the window is resized, the player will remain in the middle of the screen.
            position = new Vector2(GameWorld.ScreenSizeProp.X / 2, GameWorld.ScreenSizeProp.Y / 2);

            // Save position from before move.
            initialPosition = GameWorld.CameraPositionProp;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            bool isColliding = false;

            // Create future player position(collision with objects)
            // Unused. However, it can be used for collision with obstacles in the future.
            int newX = (int)(position.X - origin.X + velocity.X * speed * deltaTime);
            int newY = (int)(position.Y - origin.Y + velocity.Y * speed * deltaTime);

            // Future Camera position (collision with RoomSize)
            int cameraX = (int)(GameWorld.CameraPositionProp.X - origin.X + velocity.X * speed * deltaTime);
            int cameraY = (int)(GameWorld.CameraPositionProp.Y - origin.Y + velocity.Y * speed * deltaTime);

            // Future player collision
            //Rectangle futurePosition = new Rectangle(newX, newY, sprite.Width, sprite.Height);
            Rectangle futureCamera = new Rectangle(cameraX, cameraY, sprite.Width, sprite.Height);

            // For collision with worldSize use future camera position
            // For collision with objects use futurePosition
            // This is because the futurePosition uses the player position, which is always in the middle of the screen.
            if (!RoomBuilder.Room.Contains(futureCamera))
            {
                isColliding = true;
            }

            if (isColliding)
            {
                GameWorld.CameraPositionProp = initialPosition;
            } else
            {
                GameWorld.CameraPositionProp += velocity * speed * deltaTime;
            }
        }

        /// <summary>
        /// Automatically moves the player towards crates
        /// </summary>
        /// <param name="gameTime"></param>
        private void AutoMove(GameTime gameTime)
        {
            if (isAutoMoving)
            {
                lootX = RoomBuilder.LootableList[0].ScreenPosition.X;
                lootY = RoomBuilder.LootableList[0].ScreenPosition.Y;

                Vector2 moveDirection = new Vector2(lootX, lootY) - position;
                velocity = moveDirection;
                velocity.Normalize();

                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                GameWorld.CameraPositionProp += velocity * speed * autoLootSpeed * deltaTime;
            }
        }
    }


}
