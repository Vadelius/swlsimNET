using System.Collections.Generic;
using swlsimNET.Models;
using swlsimNET.ServerApp.Models;

namespace swlsimNET.ServerApp.Combat
{
    public class FightResult
    {
        private Settings _settings;

        public int Iteration { get; set; }
        public double TotalDamage { get; set; }
        public int TotalHits { get; set; }
        public int TotalCrits { get; set; }
        public double Dps => TotalDamage / (double)_settings.FightLength;

        // Here we want to store each rounds result
        public List<RoundResult> RoundResults { get; set; } = new List<RoundResult>();

        public FightResult(IPlayer player)
        {
            _settings = player.Settings;
        }
    }
}