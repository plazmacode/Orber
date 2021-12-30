using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    //TODO: make it so that orbs produce materials instead of other orbs
    //Add upgrades to autocraft orbs into better ones
    //Materials produced by orbs to be used for upgrades.

    /// <summary>
    /// Any orbers???
    /// </summary>
    public static class OrbSystem
    {
        private static float[] orbs = new float[5];
        private static float[] orbsPerSecond = new float[5] { 1, 0, 0, 0, 0 };
        private static float[] orbSpeed = new float[5] { 1, 10, 100, 1000, 10000 }; //Gain orbs every orbSpeed seconds.
        private static double nextOrbTime = 1000;

        private static List<string> totalStatsString = new List<string>();
        private static List<string> totalStatsHover = new List<string>();

        public static float[] Orbs { get => orbs; }
        public static List<string> TotalStatsString { get => totalStatsString; }
        public static List<string> TotalStatsHover { get => totalStatsHover; }

        public static void Update(GameTime gameTime)
        {
            UpdateStatsString();
            AddOrbs(gameTime);
            UpdateOrbs();
        }

        /// <summary>
        /// Updates how many orbs are added every loop
        /// </summary>
        public static void UpdateOrbs()
        {
            for (int i = 0; i < orbsPerSecond.Length -1; i++)
            {
                orbsPerSecond[i] = orbs[i+1] * 10;
            }
        }

        /// <summary>
        /// Adds new orbs every loop
        /// </summary>
        /// <param name="gameTime"></param>
        public static void AddOrbs(GameTime gameTime) {
            if (gameTime.TotalGameTime.TotalMilliseconds >= nextOrbTime)
            {
                for (int i = 0; i < orbs.Length; i++)
                {
                    orbs[i] += orbsPerSecond[i] / 100;
                }
                nextOrbTime = gameTime.TotalGameTime.TotalMilliseconds + 10;
            }
        }


        /// <summary>
        /// Creates a list of string of the chosen stats, to be displayed via for-loop in GameWorld.
        /// </summary>
        private static void UpdateStatsString()
        {
            //TODO: automate this?
            //Add strings in correct order, every strings has a hover string(fix this later)
            totalStatsString.Clear();
            totalStatsHover.Clear();

            totalStatsString.Add("Gray Orbs: " + Math.Round(Orbs[0] * 10) / 10);
            TotalStatsString.Add("White Orbs: " + Math.Round(Orbs[1] * 10) / 10);
            TotalStatsString.Add("Blue Orbs: " + Math.Round(Orbs[2] * 10) / 10);
            TotalStatsString.Add("Yellow Orbs: " + Math.Round(Orbs[3] * 10) / 10);
            TotalStatsString.Add("Orange Orbs: " + Math.Round(Orbs[4] * 10) / 10);

            totalStatsHover.Add("Orbs per/s: " + Math.Round(orbsPerSecond[0] * 10) / 10);
            totalStatsHover.Add("Orbs per/s: " + Math.Round(orbsPerSecond[1] * 10) / 10);
            totalStatsHover.Add("Orbs per/s: " + Math.Round(orbsPerSecond[2] * 10) / 10);
            totalStatsHover.Add("Orbs per/s: " + Math.Round(orbsPerSecond[3] * 10) / 10);
            totalStatsHover.Add("Orbs per/s: " + Math.Round(orbsPerSecond[4] * 10) / 10);


        }

    }
}
