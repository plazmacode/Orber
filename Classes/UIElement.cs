using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Orber
{
    public class UIElement
    {
        private Vector2 position;
        private Rectangle collisionBox;
        private string name;
        private string type;

        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public Rectangle CollisionBoxProp { get => collisionBox; set => collisionBox = value; }

        public UIElement(string name, string type)
        {
            this.name = name;
            this.type = type;
            UpdatePosition();
        }

        public void LoadContent(ContentManager content)
        {
            //This object is loads no sprites
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Arial, name, position, Color.White);
        }

        public void UpdatePosition()
        {
            switch (name)
            {
                case "Make White Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - GameWorld.Arial.MeasureString(name).X, 0);
                    collisionBox = new Rectangle((int)position.X, (int)position.Y, 200, 24);
                    break;
                case "Make Blue Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - GameWorld.Arial.MeasureString(name).X, 24);
                    collisionBox = new Rectangle((int)position.X, (int)position.Y, 200, 24);
                    break;
                case "Make Yellow Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - GameWorld.Arial.MeasureString(name).X, 48);
                    collisionBox = new Rectangle((int)position.X, (int)position.Y, 200, 24);
                    break;
                case "Make Orange Orbs":
                    position = new Vector2(GameWorld.ScreenSizeProp.X - GameWorld.Arial.MeasureString(name).X, 72);
                    collisionBox = new Rectangle((int)position.X, (int)position.Y, 200, 24);
                    break;
                default:
                    throw new Exception("wrong name");
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
