using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swlsimNET.ServerApp.Combat
{
    public class FightResult
    {
        public int Iteration { get; set; }
        public double TotalDamage { get; set; }
        public int TotalHits { get; set; }
        public int TotalCrits { get; set; }
        public double Dps => TotalDamage / 240; //TODO: Update.

        // Here we want to store each rounds result
        public List<RoundResult> RoundResults { get; set; } = new List<RoundResult>();
    }
}