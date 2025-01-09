using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace J_RPG.Utils
{
    public static class RandomGenerator
    {
        private static readonly Random rng = new Random();

        public static int Next(int minValue, int maxValue)
        {
            return rng.Next(minValue, maxValue);
        }

        public static double NextDouble()
        {
            return rng.NextDouble();
        }
    }
}
