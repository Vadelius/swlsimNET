using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.Models;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;

namespace swlsimNET.ServerApp
{
    public class Engine
    {
        private const decimal Interval = 0.1m;
        private readonly Settings _settings;

        public Engine(Settings settings)
        {
            _settings = settings;
        }

        // TODO: @Grem fix progress
        public List<FightResult> StartIterations(IProgress<int> progress = null)
        {
            var iterationResults = new List<FightResult>();

            // Run iterations
            for (var i = 1; i <= _settings.Iterations; i++)
            {
                var player = new Player(_settings);
                var fightResult = StartFight(player);
                fightResult.Iteration = i;

                iterationResults.Add(fightResult);

                progress?.Report(i);
            }

            return iterationResults;
        }

        public FightResult StartFight(Player player)
        {
            var fightResult = new FightResult(player);

            // Run a complete fight
            // Check if we can do something 10 times a second (to account for lag)
            for (decimal sec = 0; sec <= _settings.FightLength; sec += Interval)
            {
                var rr = player.NewRound(sec, Interval);
                foreach (var a in rr.Attacks)
                {
                    // non damage attacks never added to report totals for hit/crits
                    if (a.IsHit && a.Damage > 0) rr.TotalHits++;
                    if (a.IsCrit && a.Damage > 0) rr.TotalCrits++;
                    rr.TotalDamage += a.Damage;
                }

                // Only save rounds with any actions
                if (rr.Attacks.Any())
                {
                    fightResult.RoundResults.Add(rr);
                    fightResult.TotalDamage += rr.TotalDamage;
                    fightResult.TotalHits += rr.TotalHits;
                    fightResult.TotalCrits += rr.TotalCrits;
                }
            }

            return fightResult;
        }
    }
}