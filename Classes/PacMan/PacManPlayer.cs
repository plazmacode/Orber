using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Orber.PacMan
{
    public class PacManPlayer : GameObject
    {
        private double nextInput;
        private double inputSpeed;
        private bool isCollided;

        private int[] playerLevelPosition = new int[2] { 0, 0 };

        public int[] PlayerLevelPosition { get => playerLevelPosition; set => playerLevelPosition = value; }

        public PacManPlayer() {
            layerDepth = 0.3f;
            scale = 1f;
            inputSpeed = 40.0f;
            isCollided = false;
        }

        public override void Update(GameTime gameTime)
        {
            screenPosition = position - GameWorld.CameraPositionProp;
            collisionBox = new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, Sprite.Width, Sprite.Height);
            HandleInput(gameTime);
            Move(gameTime);
        }

        public void Move(GameTime gameTime)
        {
            isCollided = false;
            int newX = (int)(GameWorld.PacManPlayerProp.playerLevelPosition[0] + velocity.X);
            int newY = (int)(GameWorld.PacManPlayerProp.playerLevelPosition[1] + velocity.Y);


            //Level Border collision
            if (newX < 0 || newX >= PacMan.Level.GetLength(0))
            {
                isCollided = true;
            }
            if (newY < 0 || newY >= PacMan.Level.GetLength(1))
            {
                isCollided = true;
            }

            //Level Obstacle collision
            if (PacMan.Level[newX,newY] == 'o')
            {
                isCollided = true;
            }

            //Move player
            if (!isCollided)
            {
                GameWorld.PacManPlayerProp.playerLevelPosition[0] += (int)velocity.X;
                GameWorld.PacManPlayerProp.playerLevelPosition[1] += (int)velocity.Y;
                position = new Vector2(32 * playerLevelPosition[0] + PacMan.PacManOffset.X, 32 * playerLevelPosition[1] + PacMan.PacManOffset.Y);
                velocity = Vector2.Zero;
            }
        }

        private void HandleInput(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= nextInput)
            {
                nextInput = gameTime.TotalGameTime.TotalMilliseconds + inputSpeed;
                if (PacMan.PacManArea.Contains(GameWorld.MouseStateProp.Position))
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
                    if (velocity != Vector2.Zero)
                    {
                        velocity.Normalize();
                    }
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>("PacMan");
        }

        public override void OnCollision(GameObject other)
        {
            if (other is PacManPoint)
            {
                GameWorld.PacMan.Destroy(other); //replace with respawn command, use system.reflection
                OrbSystem.Orbs[0]++;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }
    }
}
