using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using swlsimNET.ServerApp.Models;

namespace swlsimNET.ServerApp.Models
{
    public class TablePopulator 
    {
        // [spellName, DPS, DPS%, Executes, DPE, SpellType, Count, Avarage, Crit%]

        public string Name { get; set; }
        public int DamagePerSecond { get; set; }
        public double DpsPercentage { get; set; }
        public int Executes { get; set; }
        public int DamagePerExecution { get; set; }
        public string SpellType { get; set; }
        public int Amount {get; set; }
        public int Avarage { get; set; }
        public decimal CritChance { get; set; }

    }
}
