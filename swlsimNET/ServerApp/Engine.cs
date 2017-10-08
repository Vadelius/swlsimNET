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
        private Settings _settings;

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
                // Decimal needed since double is to inaccurate, castint do double afterwards seems fine
                var roundResult = player.NewRound(sec, Interval);

                // Only save rounds with any actions
                if (roundResult.Attacks.Any())
                {
                    fightResult.RoundResults.Add(roundResult);
                    fightResult.TotalDamage += roundResult.TotalDamage;
                    fightResult.TotalHits += roundResult.TotalHits;
                    fightResult.TotalCrits += roundResult.TotalCrits;
                }
            }

            return fightResult;
        }
    }
}