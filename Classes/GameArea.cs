using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    public class GameArea
    {
        private string name;
        private static Vector2 offset;
        private static Rectangle areaRect;

        private  List<GameObject> gameObjects = new List<GameObject>();
        private  List<GameObject> removeGameObjects = new List<GameObject>();
        private  List<GameObject> newGameObjects = new List<GameObject>();

        public List<GameObject> GameObjectsProp { get => gameObjects; set => gameObjects = value; }
        public static Vector2 Offset { get => offset; set => offset = value; }
        public static Rectangle AreaRect { get => areaRect; set => areaRect = value; }

        public void LoadContent(ContentManager content)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            GameObjectsProp.AddRange(newGameObjects);
            newGameObjects.Clear();

            foreach (GameObject gameObject in removeGameObjects)
            {
                GameObjectsProp.Remove(gameObject);
            }
            removeGameObjects.Clear();

            foreach (GameObject gameObject in GameObjectsProp)
            {
                gameObject.Update(gameTime);
                foreach (GameObject other in GameObjectsProp)
                {
                    gameObject.CheckCollision(other);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
#if DEBUG
                if (gameObject is Lootable)
                {
                    Rectangle col = gameObject.CollisionBoxProp;
                    col.Inflate(-gameObject.Sprite.Width, -gameObject.Sprite.Height);
                    if (RoomBuilder.RoomScreenRect.Contains(col))
                    {
                        GameWorld.DrawCollisionBox(gameObject.CollisionBoxProp);
                    }
                }
                else
                {
                    GameWorld.DrawCollisionBox(gameObject.CollisionBoxProp);
                }
#endif
            }
        }

        public void Instantiate(GameObject gameObject)
        {
            newGameObjects.Add(gameObject);
        }

        /// <summary>
        /// Removes gameobject from gameobject list
        /// </summary>
        /// <param name="gameObject">gameobject to be removed</param>
        public void Destroy(GameObject gameObject)
        {
            removeGameObjects.Add(gameObject);
        }
    }
}
