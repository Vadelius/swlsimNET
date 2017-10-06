using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swlsimNET.ServerApp.Combat
{
    public class RoundResult
    {
        public RoundResult()
        {
            Attacks = new List<Attack>();
        }

        public double TimeSec { get; set; }
        public double Interval { get; set; }

        public List<Attack> Attacks { get; set; }

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
