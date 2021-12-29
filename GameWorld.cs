using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orber
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static SpriteFont arial;
        private static MouseState mouseState;
        private static KeyboardState keyState;

        private Texture2D collisionTexture;

        // Static fields allow the Instatiate and Destroy methods to be static
        private static List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> newGameObjects = new List<GameObject>();
        private static List<GameObject> removeGameObjects = new List<GameObject>();
        private static List<string> debugTexts = new List<string>();

        private static Vector2 screenSize;
        private static Vector2 oldScreenSize;
        private static Vector2 cameraPosition = new Vector2(800, 450);

        private SoundEffectInstance backgroundMusic;

        private static Player player = new Player();

        /// <summary>
        /// Moves with the player. Draws everything else in relation to this
        /// </summary>
        public static Vector2 CameraPositionProp { get => cameraPosition; set => cameraPosition = value; }

        /// <summary>
        /// Size of screen for positioning player in the middle
        /// </summary>
        public static Vector2 ScreenSizeProp { get => screenSize; }

        /// <summary>
        /// Easily accessable reference to the instatiated player
        /// </summary>
        public static Player PlayerProp { get => player; }
        public static MouseState MouseStateProp { get => mouseState; }
        public static SpriteFont Arial { get => arial; }
        public static KeyboardState KeyStateProp { get => keyState; }
        public static List<string> DebugTexts { get => debugTexts; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        /// <summary>
        /// Updates various variables on system resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnResize(Object sender, EventArgs e)
        {
            oldScreenSize = screenSize;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            //Keeps player position correct(cameraPosition)
            cameraPosition.X += screenSize.X/2 - oldScreenSize.X/2;
            cameraPosition.Y += screenSize.Y/2 - oldScreenSize.Y/2;

            //Updates position of UI elements after window Resize;
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is UIElement)
                {
                    //Updates the position of UI elements by calling the method of the subclass from the superclass
                    gameObject.GetType().InvokeMember("UpdatePosition", System.Reflection.BindingFlags.InvokeMethod, null, gameObject, null);
                }
            }

            //Updates room position
            RoomBuilder.ReloadRoom("noSeedImplemented");
        }

        protected override void Initialize()
        {
            gameObjects.Add(new UIElement("Make White Orbs", "button", new Vector2(screenSize.X - 200, 0)));
            gameObjects.Add(new UIElement("Make Blue Orbs", "button", new Vector2(screenSize.X - 200, 24)));
            gameObjects.Add(new UIElement("Make Yellow Orbs", "button", new Vector2(screenSize.X - 200, 48)));
            gameObjects.Add(new UIElement("Make Orange Orbs", "button", new Vector2(screenSize.X - 200, 72)));

            gameObjects.Add(PlayerProp);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            arial = Content.Load<SpriteFont>("arial");

            collisionTexture = Content.Load<Texture2D>("collisionTexture");

            RoomBuilder.LoadContent(Content);
            RoomBuilder.GenerateRoom("TODO,make rooms seed based");

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();
#if DEBUG
            debugTexts.Add("OldScreen:" + oldScreenSize.ToString());
            debugTexts.Add("screenSize:" + screenSize.ToString());
#endif
            OrbSystem.Update(gameTime);

            gameObjects.AddRange(newGameObjects);
            newGameObjects.Clear();

            foreach (GameObject gameObject in removeGameObjects)
            {
                gameObjects.Remove(gameObject);
            }
            removeGameObjects.Clear();

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
                foreach (GameObject other in gameObjects)
                {
                    gameObject.CheckCollision(other);
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);   // Makes layers work

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
#if DEBUG
                DrawCollisionBox(gameObject.CollisionBoxProp);
#endif
            }

            DrawWorldBoundary(RoomBuilder.Room);

            //Draw stats string
            for (int i = 0; i < OrbSystem.TotalStatsString.Count; i++)
            {
                _spriteBatch.DrawString(Arial, OrbSystem.TotalStatsString[i], new Vector2(0, i * 24), Color.White);
            }

            //Draw hover stats string
            for (int i = 0; i < OrbSystem.TotalStatsString.Count; i++)
            {
                Vector2 textSize = Arial.MeasureString(OrbSystem.TotalStatsString[i]);
                Rectangle text = new Rectangle(0, i * 24, (int)textSize.X, (int)textSize.Y);
                if (text.Contains(mouseState.X, mouseState.Y))
                {
                    _spriteBatch.DrawString(Arial, OrbSystem.TotalStatsHover[i], new Vector2(mouseState.X + 20, mouseState.Y + 10), Color.White);
                }
            }

            //Draw hover stats for loot
            if (RoomBuilder.LootableList[0].CollisionBoxProp.Contains(mouseState.X, mouseState.Y))
            {
                _spriteBatch.DrawString(Arial, RoomBuilder.LootableList[0].Rarity, new Vector2(mouseState.X + 20, mouseState.Y + 10), Color.White);
            }

            //DebugText constantly updates
            for (int i = 0; i < DebugTexts.Count; i++)
            {
                _spriteBatch.DrawString(Arial, DebugTexts[i], new Vector2(0, 524+i*24), Color.Gray);
            }
            GameWorld.DebugTexts.Clear();

            _spriteBatch.DrawString(GameWorld.Arial, mouseState.Position.ToString(), new Vector2(0, screenSize.Y-24), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds newly instantiated gameobjects to gameobject list
        /// </summary>
        /// <param name="gameObject">Gameobject to be added</param>
        public static void Instantiate(GameObject gameObject)
        {
            newGameObjects.Add(gameObject);
        }

        /// <summary>
        /// Removes gameobject from gameobject list
        /// </summary>
        /// <param name="gameObject">gameobject to be removed</param>
        public static void Destroy(GameObject gameObject)
        {
            removeGameObjects.Add(gameObject);
        }

        /// <summary>
        /// Runs the DrawBox code with the given Rectangle from its parameter
        /// </summary>
        private void DrawCollisionBox(Rectangle rect)
        {
            DrawBox(rect, Color.Red, 1);
        }

        /// <summary>
        /// Draws the World Boundary in DarkGray colour.
        /// </summary>
        /// <param name="rect">Worldsize</param>
        private void DrawWorldBoundary(Rectangle rect)
        {
            Rectangle collisionBox = rect;

            collisionBox.X = collisionBox.X - (int)CameraPositionProp.X + (int)ScreenSizeProp.X / 2;
            collisionBox.Y = collisionBox.Y - (int)CameraPositionProp.Y + (int)ScreenSizeProp.Y / 2;

            DrawBox(collisionBox, Color.DarkGray, 10);
        }

        /// <summary>
        /// Generic method for drawing a box around a rectangle.
        /// </summary>
        private void DrawBox(Rectangle rect, Color color, int lineWidth)
        {
            Rectangle topLine = new Rectangle(rect.X, rect.Y, rect.Width, lineWidth);
            Rectangle bottomLine = new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, lineWidth);
            Rectangle rightLine = new Rectangle(rect.X + rect.Width, rect.Y, lineWidth, rect.Height + lineWidth);
            Rectangle leftLine = new Rectangle(rect.X, rect.Y, lineWidth, rect.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            _spriteBatch.Draw(collisionTexture, rightLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            _spriteBatch.Draw(collisionTexture, leftLine, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
        }
    }
}
