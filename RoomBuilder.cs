using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Orber
{
    static class RoomBuilder
    {
        private static Rectangle room;
        private static Vector2 roomSize = new Vector2(800, 800);
        private static Vector2 roomOffset = new Vector2(roomSize.X/2, -(roomSize.Y/2));

        private static Texture2D backgroundImage;
        private static Texture2D[] crateSprites = new Texture2D[5];

        private static List<Lootable> lootableList = new List<Lootable>();

        public static Rectangle Room { get => room; }
        public static Vector2 RoomOffset { get => roomOffset; set => roomOffset = value; }
        public static List<Lootable> LootableList { get => lootableList; set => lootableList = value; }

        public static void LoadContent(ContentManager content)
        {
            //backgroundImage = content.Load<Texture2D>("backgroundImage");
            for (int i = 0; i < 5; i++)
            {
                crateSprites[i] = content.Load<Texture2D>("crate" + i);
            }
        }

        /// <summary>
        /// Randomly generates a room with loot
        /// </summary>
        /// <param name="seed"></param>
        public static void GenerateRoom(string seed)
        {
            // TODO: make room generation, maybe add RoomLuck parameter
            //Should rooms be retraceable?
            //Generate entire map at start, or random rooms throughout?

            room = new Rectangle(
                (int)GameWorld.ScreenSizeProp.X / 2 - (int)roomOffset.X,
                (int)GameWorld.ScreenSizeProp.Y / 2 + (int)roomOffset.Y,
                (int)roomSize.X, (int)roomSize.Y);

            Lootable lootable = new Lootable(crateSprites);

            LootableList.Add(lootable);
            GameWorld.Instantiate(lootable); //Adds to GameWorld
        }

        /// <summary>
        /// Removes all the room related objects
        /// </summary>
        public static void DestroyRoom()
        {

        }
    }
}
