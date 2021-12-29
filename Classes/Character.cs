using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    public abstract class Character : GameObject
    {
        protected Vector2 initialPosition;

        protected abstract void Move(GameTime gameTime);
    }
}
