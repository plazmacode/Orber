using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    public class CBTEnemy : CBTCharacter
    {
        public CBTEnemy()
        {
            maxHealth = 50f;
            maxStamina = 100f;
            maxMana = 0f;
            health = 100f;
            stamina = 100f;
            mana = 0f;
            layerDepth = 0.3f;
            scale = 1f;

            strength = 1f;
            baseMagicDamage = 50f;
            criticalChance = 0.01f;
            IsStunned = false;
            IsAlive = true;
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
    }
}
