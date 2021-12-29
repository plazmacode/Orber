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
        private static Texture2D obstacleSprite;
        private static Texture2D pointSprite;
        private static Texture2D playerSprite;

        private static char[,] level = new char[5, 5] {
            { 'o','o','o','o','o' },
            { 'o',' ',' ','p','o' },
            { 'o','s','o',' ','o' },
            { 'o','p',' ','p','o' },
            { 'o','o','o','o','o' }
        };

        static PacMan()
        {
            pacManOffset = new Vector2(0,250);
        }

        public static void LoadContent(ContentManager content)
        {
            obstacleSprite = content.Load<Texture2D>("PacManObstacle");
            pointSprite = content.Load<Texture2D>("PacManPoint");
            playerSprite = content.Load<Texture2D>("PacMan");
        }

        public static void DrawLevel()
        {
            for (int i = 0; i < level.GetLength(1); i++) //array height
            {
                for (int j = 0; j < level.GetLength(0); j++) // Array width
                {
                    if (level[j, i] == 'o')
                    {
                        GameWorld.Instantiate(new PacManObstacle(new Vector2(32 * i+pacManOffset.X, 32 * j+pacManOffset.Y), obstacleSprite));
                    }
                    else if (level[j, i] == 'p')
                    {
                        GameWorld.Instantiate(new PacManPoint(new Vector2(32 * i + pacManOffset.X, 32 * j + pacManOffset.Y), pointSprite));
                    }
                    else if (level[j, i] == 's')
                    {
                        GameWorld.Instantiate(new PacManPlayer(new Vector2(32 * i + pacManOffset.X, 32 * j + pacManOffset.Y), playerSprite));
                    }
                }
            }
        }
    }
}
