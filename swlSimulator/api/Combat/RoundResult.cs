using System.Collections.Generic;
using swlSimulator.api.Spells;

namespace swlSimulator.api.Combat
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

        public decimal PrimaryGimmickStart { get; set; }
        public decimal PrimaryGimmickEnd { get; set; }
        public decimal SecondaryGimmickStart { get; set; }
        public decimal SecondaryGimmickEnd { get; set; }
    }
}