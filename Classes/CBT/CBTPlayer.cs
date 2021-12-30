using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber.CBT
{
    class CBTPlayer : CBTCharacter
    {
        public CBTPlayer()
        {
            maxHealth = 100f;
            maxStamina = 100f;
            maxMana = 0f;
            health = 100f;
            stamina = 100f;
            mana = 0f;
            layerDepth = 0.3f;
            scale = 1f;

            strength = 10f;
            baseMagicDamage = 50f;
            criticalChance = 0.01f;
            IsStunned = false;
            IsAlive = true;
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            screenPosition = position; //TODO: don't rely on screenPosition to draw collision
            collisionBox = new Rectangle((int)screenPosition.X - (int)origin.X, (int)screenPosition.Y - (int)origin.Y, Sprite.Width, Sprite.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, scale, SpriteEffects.None, layerDepth);
        }

        public override void OnCollision(GameObject other)
        {

        }
    }
}
