using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    public static class OrbSystem
    {
        private static int[] orbs = new int[5];

        private static int[] orbSpeed = new int[5] { 1, 0, 0, 0, 0}; //Gain orbs every orbSpeed seconds.
        private static double[] nextOrbTime = new double[5]; //Time until next orb.
        private static double[] remainingOrbTime = new double[5];

        private static List<string> totalStatsString = new List<string>();
        private static List<string> totalStatsHover = new List<string>();

        public static int[] Orbs { get => orbs; }
        public static double[] RemainingOrbTime { get => remainingOrbTime; }
        public static List<string> TotalStatsString { get => totalStatsString; }
        public static List<string> TotalStatsHover { get => totalStatsHover; }

        public static void Update(GameTime gameTime)
        {
            UpdateStatsString();
            remainingOrbTime[0] = Math.Round(nextOrbTime[0] - gameTime.TotalGameTime.TotalMilliseconds);
            if (nextOrbTime[0] <= gameTime.TotalGameTime.TotalMilliseconds)
            {
                orbs[0]++;
                nextOrbTime[0] = gameTime.TotalGameTime.TotalMilliseconds + orbSpeed[0]*1000;
            }
        }

        /// <summary>
        /// Creates a list of string of the chosen stats, to be displayed via for-loop in GameWorld.
        /// </summary>
        private static void UpdateStatsString()
        {
            //Add strings in correct order, every strings has a hover string(fix this later)
            totalStatsString.Clear();
            totalStatsHover.Clear();

            totalStatsString.Add("Gray Orbs: " + OrbSystem.Orbs[0]);
            TotalStatsString.Add("White Orbs: " + OrbSystem.Orbs[1]);
            TotalStatsString.Add("Blue Orbs: " + OrbSystem.Orbs[2]);
            TotalStatsString.Add("Yellow Orbs: " + OrbSystem.Orbs[3]);
            TotalStatsString.Add("Orange Orbs: " + OrbSystem.Orbs[4]);

            totalStatsHover.Add("Remaining Orb Time: " + Math.Round(OrbSystem.RemainingOrbTime[0] / 100) / 10);
            totalStatsHover.Add("Remaining Orb Time: " + Math.Round(OrbSystem.RemainingOrbTime[1] / 100) / 10);
            totalStatsHover.Add("Remaining Orb Time: " + Math.Round(OrbSystem.RemainingOrbTime[2] / 100) / 10);
            totalStatsHover.Add("Remaining Orb Time: " + Math.Round(OrbSystem.RemainingOrbTime[3] / 100) / 10);
            totalStatsHover.Add("Remaining Orb Time: " + Math.Round(OrbSystem.RemainingOrbTime[4] / 100) / 10);

        }

    }
}
