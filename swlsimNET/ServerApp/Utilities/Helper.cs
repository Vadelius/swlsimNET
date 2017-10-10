using System;
using System.Collections.Generic;
using Expressions;
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

        public static bool EvaluateArgs(string args, Player player,
            ExpressionLanguage language = ExpressionLanguage.Csharp)
        {
            var expression = new DynamicExpression(args, language);

            // Only create context once
            if (player.Context == null)
            {
                player.Context = new ExpressionContext(null, player, true);
                player.Context.Variables.Add("Player", player);

                foreach (var spell in player.Spells.DistinctBy(s => s.Name))
                    player.Context.Variables.Add(spell.Name, spell);
            }

            var res = expression.Invoke(player.Context);

            return (bool) res;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
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