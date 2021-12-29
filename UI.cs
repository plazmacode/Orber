using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Orber
{
    public class UIElement : GameObject
    {
        private string name;
        private string type;
        private string color;
        private Rectangle rect;
        private static MouseState oldState;


        public UIElement(string name, string type, Vector2 position)
        {
            this.name = name;
            this.type = type;
            this.position = position; ;
            this.rect = new Rectangle((int)position.X, (int)position.Y, 200, 40);
        }

        public UIElement(string type, Vector2 position, string color)
        {
            this.type = type;
            this.position = position; ;
            this.rect = new Rectangle((int)position.X, (int)position.Y, 200, 40);
        }

        public override void LoadContent(ContentManager content)
        {
            //This object is loads no sprites
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Arial, name, position, Color.White);
        }

        public override void OnCollision(GameObject other)
        {

        }

        public void UpdatePosition()
        {
            switch (name)
            {
                case "Make White Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - 200, 0);
                    break;
                case "Make Blue Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - 200, 24);
                    break;
                case "Make Yellow Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - 200, 48);
                    break;
                case "Make Orange Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - 200, 72);
                    break;
                default:
                    break;
            }
        }

        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (type == "button")
            {
                //If left button clicked while UI contains mouse
                if (oldState.LeftButton == ButtonState.Pressed &&
                    GameWorld.MouseStateProp.LeftButton == ButtonState.Released &&
                    rect.Contains(GameWorld.MouseStateProp.Position))
                {
                    switch (name)
                    {
                        case "Make White Orbs":
                            if (OrbSystem.Orbs[0] >= 10)
                            {
                                OrbSystem.Orbs[0] -= 10;
                                OrbSystem.Orbs[1] += 1;
                            }
                            break;
                        case "Make Blue Orbs":
                            if (OrbSystem.Orbs[1] >= 10)
                            {
                                OrbSystem.Orbs[1] -= 10;
                                OrbSystem.Orbs[2] += 1;
                            }
                            break;
                        case "Make Yellow Orbs":
                            if (OrbSystem.Orbs[2] >= 10)
                            {
                                OrbSystem.Orbs[2] -= 10;
                                OrbSystem.Orbs[3] += 1;
                            }
                            break;
                        case "Make Orange Orbs":
                            if (OrbSystem.Orbs[3] >= 10)
                            {
                                OrbSystem.Orbs[3] -= 10;
                                OrbSystem.Orbs[4] += 1;
                            }
                            break;
                        default:
                            break;
                    }
                }
                oldState = GameWorld.MouseStateProp;
            }
        }
    }
}
