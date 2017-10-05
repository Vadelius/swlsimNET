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
        private const int Interval = 100;
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
            for (var ms = 0; ms < _settings.FightLength * 1000; ms += Interval)
            {
                var roundResult = player.NewRound(ms, Interval);

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