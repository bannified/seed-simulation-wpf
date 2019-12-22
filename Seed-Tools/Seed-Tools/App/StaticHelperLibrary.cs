using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public static class StaticHelperLibrary
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            rng = new Random();

            DoShuffle(list);
        }

        public static void Shuffle<T>(this IList<T> list, int seed)
        {
            rng = new Random(seed);

            DoShuffle(list);
        }

        private static void DoShuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
