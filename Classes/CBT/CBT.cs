using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    /// <summary>
    /// Turn-based combat? Combat Based on Turns!!!
    /// </summary>
    public static class CBT
    {
        private static CBTPlayer CBTPlayer = new CBTPlayer();
        private static CBTEnemy CBTEnemy = new CBTEnemy();
        private static Vector2 CBTOffset;
        private static Rectangle CBTArea;
        private static Texture2D playerSprite;
        private static Texture2D enemySprite;
        private static Vector2 playerPosition;
        private static Vector2 enemyPosition;

        public static CBTPlayer CBTPlayerProp { get => CBTPlayer; set => CBTPlayer = value; }
        public static CBTEnemy CBTEnemyProp { get => CBTEnemy; set => CBTEnemy = value; }
        public static Vector2 CBTOffsetProp { get => CBTOffset; set => CBTOffset = value; }
        public static Rectangle CBTAreaProp { get => CBTArea; set => CBTArea = value; }

        static CBT() //???? 🥺static CBT🥺 ????
        {
            CBTOffsetProp = new Vector2(150, GameWorld.ScreenSizeProp.Y - 300);
            CBTAreaProp = new Rectangle((int)CBTOffsetProp.X, (int)CBTOffsetProp.Y, 800, 300);
        }

        public static void ReloadArea() //Reloads offsets
        {
            CBTOffsetProp = new Vector2(150, GameWorld.ScreenSizeProp.Y - 300);
            CBTAreaProp = new Rectangle((int)CBTOffsetProp.X, (int)CBTOffsetProp.Y, 800, 300);
        }

        public static void LoadContent(ContentManager content)
        {
            CBTPlayerProp.Sprite = content.Load<Texture2D>("player");
        }

        public static void CreateArena()
        {
            playerPosition = new Vector2(CBTOffset.X, CBTOffset.Y+CBTArea.Height - CBTPlayerProp.Sprite.Height); //initial position of player
            CBTPlayerProp.Position = playerPosition;
            GameWorld.CBTProp.Instantiate(CBTPlayerProp);
            GameWorld.CBTProp.Instantiate(CBTEnemyProp);
        }

    }
}
