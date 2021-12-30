using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Orber
{
    /// <summary>
    /// Used for the dungeon minigame found in the center of the screen.
    /// </summary>
    static class RoomBuilder
    {
        private static Rectangle room;
        private static Vector2 roomSize = new Vector2(1200, 1200); //Size of the generated room
        private static Vector2 roomScreenSize = new Vector2(200, 200); //Size of the dungeon games screen

        private static Rectangle roomScreenRect;
        private static Vector2 roomOffset = new Vector2(RoomSize.X / 2, -(RoomSize.Y / 2));
        private static Vector2 roomScreenOffset = new Vector2(RoomScreenSize.X / 2, -(RoomScreenSize.Y / 2));

        private static Texture2D backgroundImage;
        private static Texture2D[] crateSprites = new Texture2D[5];

        private static List<Lootable> lootableList = new List<Lootable>();

        /// <summary>
        /// Rectangle where the rooms x, y, width and height is stored.
        /// </summary>
        public static Rectangle Room { get => room; }
        public static Vector2 RoomOffset { get => roomOffset; set => roomOffset = value; }
        public static List<Lootable> LootableList { get => lootableList; set => lootableList = value; }
        public static Vector2 RoomSize { get => roomSize; set => roomSize = value; }
        public static Rectangle RoomScreenRect { get => roomScreenRect; set => roomScreenRect = value; }
        public static Vector2 RoomScreenSize { get => roomScreenSize; set => roomScreenSize = value; }
        public static Vector2 RoomScreenOffset { get => roomScreenOffset; set => roomScreenOffset = value; }

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
                (int)RoomSize.X, (int)RoomSize.Y);

            roomScreenRect = new Rectangle(
                (int)GameWorld.ScreenSizeProp.X / 2 - (int)roomScreenOffset.X,
                (int)GameWorld.ScreenSizeProp.Y / 2 + (int)roomScreenOffset.Y,
                (int)RoomScreenSize.X, (int)RoomScreenSize.Y);

            Lootable lootable = new Lootable(crateSprites);

            LootableList.Add(lootable);
            GameWorld.Dungeon.Instantiate(lootable); //Adds to GameWorld
            OrbSystem.Orbs[0] = 100;
            OrbSystem.Orbs[1] = 100;
        }

        public static void ReloadRoom(string seed)
        {
            room = new Rectangle(
                (int)GameWorld.ScreenSizeProp.X / 2 - (int)roomOffset.X,
                (int)GameWorld.ScreenSizeProp.Y / 2 + (int)roomOffset.Y,
                (int)RoomSize.X, (int)RoomSize.Y);
        }

        /// <summary>
        /// Removes all the room related objects
        /// </summary>
        public static void DestroyRoom()
        {

        }
    }
}
