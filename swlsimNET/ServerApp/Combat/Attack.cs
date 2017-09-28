using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Combat
{
    public enum TargetType
    {
        Champion = 3,
        Dungeon = 5,
        Lair = 18,
        Regional = 31,
        MegaLairs = 35
    }

    public class Attack
    {
        public string Name => this.Spell.Name;
        public double Damage { get; set; } = 0;
        public bool IsCrit { get; set; } = false;
        public bool IsHit { get; set; } = false;
        public ISpell Spell { get; set; }
    }
}