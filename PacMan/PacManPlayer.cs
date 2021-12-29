using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber.PacMan
{
    class PacManPlayer : GameObject
    {
        public PacManPlayer(Vector2 position, Texture2D sprite) {
            this.position = position;
            this.sprite = sprite;

            layerDepth = 0.2f;
            scale = 1f;
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }
    }
}
