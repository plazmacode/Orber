using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Orber
{
    public class Lootable : GameObject
    {
        private int pickupRange;
        private int crateRNG;
        private string rarity;
        private Random random;

        public string Rarity { get => rarity; set => rarity = value; }

        public Lootable(Texture2D[] crateSprites)
        {
            random = new Random();
            this.sprites = crateSprites;


            this.pickupRange = 20;
            scale = 1f;
            layerDepth = 0.1f;
            Spawn();
        }

        public Lootable(int x, int y, Texture2D[] crateSprites)
        {
            random = new Random();
            this.position.X = x;
            this.position.Y = y;
            this.sprites = crateSprites;
            
            this.pickupRange = 20;
            scale = 1f;
            layerDepth = 0.1f;
            Spawn();
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            screenPosition = position - GameWorld.CameraPositionProp + GameWorld.ScreenSizeProp + new Vector2(-RoomBuilder.Room.Width / 2, RoomBuilder.RoomOffset.Y);
            collisionBox = new Rectangle((int)screenPosition.X - (int)origin.X, (int)screenPosition.Y - (int)origin.Y, Sprite.Width, Sprite.Height);


            GameWorld.DebugTexts.Add("CrateRNG: " + crateRNG.ToString());
            //GameWorld.DebugTexts.Add("CratePosition: " + position.ToString());
            //GameWorld.DebugTexts.Add("CameraPosition: " + GameWorld.CameraPositionProp.X.ToString() + ", " + GameWorld.CameraPositionProp.Y.ToString());
            //GameWorld.DebugTexts.Add("ScreenPosition: " + screenPosition.ToString());
            //GameWorld.DebugTexts.Add("PlayerPosition: " + GameWorld.PlayerProp.Position.ToString());

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle col = collisionBox;
            col.Inflate(-Sprite.Width, -Sprite.Height);
            if (RoomBuilder.Room.Contains(col))
            {
                spriteBatch.Draw(Sprite, screenPosition, null, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
            }
        }

        /// <summary>
        /// Determines the rarity of crates
        /// </summary>
        private void SetRarity()
        {
            crateRNG = random.Next(0, 1000);
            if (crateRNG < 1) // 0.1%
            {
                Rarity = "orange";
            } else if (crateRNG < 10) // 1%
            {
                Rarity = "yellow";
            }
            else if (crateRNG < 60) // 5%
            {
                Rarity = "blue";
            }
            else if (crateRNG < 240) //18%
            {
                Rarity = "white";
            }
            else if (crateRNG < 1000) //76%
            {
                Rarity = "gray";
            }

            switch (Rarity)
            {
                case "gray":
                    Sprite = sprites[0];
                    break;
                case "white":
                    Sprite = sprites[1];
                    break;
                case "blue":
                    Sprite = sprites[2];
                    break;
                case "yellow":
                    Sprite = sprites[3];
                    break;
                case "orange":
                    Sprite = sprites[4];
                    break;
                default:
                    Sprite = sprites[4]; //ERROR
                    rarity = "Error" + crateRNG.ToString();
                    GameWorld.DebugTexts.Add("ERROR SPRITE MISSING RARITY: " + crateRNG.ToString());
                    break;
            }
        }

        public void Spawn()
        {
            SetRarity();
            int spawnPoint = random.Next(0, 4);
            int rngWidth = random.Next(25, RoomBuilder.Room.Width -25);
            int rngHeight = random.Next(25, RoomBuilder.Room.Height -25);

            switch (spawnPoint) //Top left is 0, 0
            {
                case 0: //top
                    position = new Vector2(rngWidth - Sprite.Width / 2, 50 - Sprite.Height / 2);
                    break;
                case 1: //bottom
                    position = new Vector2(rngWidth - Sprite.Width / 2, RoomBuilder.Room.Height - 50 - Sprite.Height / 2);
                    break;
                case 2: //left
                    position = new Vector2(50 - Sprite.Width / 2, rngHeight - Sprite.Height / 2);
                    break;
                case 3: //right
                    position = new Vector2(RoomBuilder.Room.Width - 50 - Sprite.Width / 2, rngHeight - Sprite.Height / 2);
                    break;
                default:
                    break;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                GameWorld.CameraPositionProp = new Vector2(GameWorld.ScreenSizeProp.X / 2, GameWorld.ScreenSizeProp.Y / 2);
                switch (Rarity)
                {
                    case "gray":
                        OrbSystem.Orbs[0]++;
                        break;
                    case "white":
                        OrbSystem.Orbs[1]++;
                        break;
                    case "blue":
                        OrbSystem.Orbs[2]++;
                        break;
                    case "yellow":
                        OrbSystem.Orbs[3]++;
                        break;
                    case "orange":
                        OrbSystem.Orbs[4]++;
                        break;
                    default:
                        break;
                }
                Spawn();
            }
        }
    }
}
