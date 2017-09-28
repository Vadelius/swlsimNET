using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using swlsimNET.ServerApp.Models;

namespace swlsimNET.ServerApp.Utilities
{
    internal static class Helper
    {
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        public static double RNG()
        {
            lock (SyncLock)
            {
                // TODO: Not 100% accurate since it cant return 1 which would be 100%?
                return Random.NextDouble();
            }
        }

        public static bool IsHit(double targetdifficulty, double hitchance)
        {
            // TODO: Check if this is correct
            return RNG() + targetdifficulty < 1 + hitchance;
        }

        public static bool IsCrit(double critchance)
        {
            var rng = RNG();
            return rng > 1 - critchance;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static class Env
        {
            #if DEBUG
                public static readonly bool Debugging = true;
            #else
                public static readonly bool Debugging = false;
            #endif
        }
    }
}