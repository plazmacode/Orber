using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber.CBT
{
    /// <summary>
    /// Turn-based combat? Combat Based on Turns!!!
    /// </summary>
    public static class CBT
    {
        private static CBTPlayer CBTPlayer = new CBTPlayer();
        private static Vector2 CBTOffset;
        private static Rectangle CBTArea;
        private static Texture2D playerSprite;
        private static Texture2D enemySprite;
        private static Vector2 playerPosition;
        private static Vector2 enemyPosition;

        internal static CBTPlayer CBTPlayerProp { get => CBTPlayer; set => CBTPlayer = value; }

        public static void LoadContent(ContentManager content)
        {
            CBTPlayerProp.Sprite = content.Load<Texture2D>("player");
        }

        public static void CreateArena()
        {
            playerPosition = new Vector2(50, 50);
            CBTPlayerProp.Position = playerPosition;
            GameWorld.CBTProp.Instantiate(CBTPlayerProp);
        }

    }
}
