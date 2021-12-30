using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber.PacMan
{
    public static class PacMan
    {
        private static Vector2 pacManOffset;
        private static Rectangle pacManArea;
        private static Texture2D obstacleSprite;
        private static Texture2D pointSprite;

        private static char[,] level = new char[5, 5] {
            { 'o','o','o','o','o' },
            { 'o',' ',' ','p','o' },
            { 'o','s','o',' ','o' },
            { 'o','p',' ','p','o' },
            { 'o','o','o','o','o' }
        };

        public static Rectangle PacManArea { get => pacManArea; set => pacManArea = value; }
        public static char[,] Level { get => level; set => level = value; }
        public static Vector2 PacManOffset { get => pacManOffset; set => pacManOffset = value; }

        static PacMan()
        {
            PacManOffset = new Vector2(0, 250);
            PacManArea = new Rectangle((int)PacManOffset.X, (int)PacManOffset.Y, 32 * Level.GetLength(0), 32 * Level.GetLength(1));
        }

        public static void LoadContent(ContentManager content)
        {
            obstacleSprite = content.Load<Texture2D>("PacManObstacle");
            pointSprite = content.Load<Texture2D>("crate0");
        }

        public static void DrawLevel()
        {
            for (int i = 0; i < Level.GetLength(1); i++) //array height
            {
                for (int j = 0; j < Level.GetLength(0); j++) // Array width
                {
                    if (Level[j, i] == 'o')
                    {
                        GameWorld.PacMan.Instantiate(new PacManObstacle(new Vector2(32 * i+PacManOffset.X, 32 * j+PacManOffset.Y), obstacleSprite));
                    }
                    else if (Level[j, i] == 'p')
                    {
                        GameWorld.PacMan.Instantiate(new PacManPoint(new Vector2(32 * i + PacManOffset.X, 32 * j + PacManOffset.Y), pointSprite));
                    }
                    else if (Level[j, i] == 's')
                    {
                        GameWorld.PacManPlayerProp.Position = new Vector2(32 * i + PacManOffset.X, 32 * j + PacManOffset.Y);
                        GameWorld.PacMan.GameObjectsProp.Add(GameWorld.PacManPlayerProp);
                        GameWorld.PacManPlayerProp.PlayerLevelPosition[0] = i;
                        GameWorld.PacManPlayerProp.PlayerLevelPosition[1] = j;

                    }
                }
            }
        }
    }
}
