using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    public abstract class CBTCharacter : GameObject
    {
        protected float maxHealth;
        protected float maxStamina;
        protected float maxMana;
        protected float health;
        protected float stamina;
        protected float mana;
        protected float criticalChance;
        protected float strength;
        protected float baseMagicDamage;
        protected bool IsStunned;
        protected bool IsAlive;


        public override void LoadContent(ContentManager content)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
    }
}
