using System.Collections.Generic;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Combat
{
    public class RoundResult
    {
        public RoundResult()
        {
            Attacks = new List<Attack>();
            Buffs = new List<IBuff>();
        }

        public decimal TimeSec { get; set; }
        public decimal Interval { get; set; }

        public List<Attack> Attacks { get; set; }
        public List<IBuff> Buffs { get; set; }

        public double TotalDamage { get; set; }
        public int TotalHits { get; set; }
        public int TotalCrits { get; set; }

        public int PrimaryEnergyStart { get; set; }
        public int PrimaryEnergyEnd { get; set; }
        public int SecondaryEnergyStart { get; set; }
        public int SecondaryEnergyEnd { get; set; }

        public double PrimaryGimmickStart { get; set; }
        public double PrimaryGimmickEnd { get; set; }
        public double SecondaryGimmickStart { get; set; }
        public double SecondaryGimmickEnd { get; set; }
    }
}