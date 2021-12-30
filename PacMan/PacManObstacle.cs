using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber.PacMan
{
    class PacManObstacle : GameObject
    {
        public PacManObstacle(Vector2 position, Texture2D sprite)
        {
            this.Sprite = sprite;
            this.position = position;
            layerDepth = 0.3f;
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
            spriteBatch.Draw(Sprite, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }
    }
}
