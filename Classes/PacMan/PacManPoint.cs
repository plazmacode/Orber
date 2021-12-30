using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber.PacMan
{
    class PacManPoint : GameObject
    {
        public PacManPoint(Vector2 position, Texture2D sprite) {
            this.position = position;
            this.Sprite = sprite;

            layerDepth = 0.3f;
            scale = 1f;
        }

        public override void Update(GameTime gameTime)
        {
            collisionBox = new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, Sprite.Width, Sprite.Height);
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }
    }
}
