using Newtonsoft.Json;
using swlSimulator.api.Spells;

namespace swlSimulator.api.Combat
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
        public string Name => Spell.Name;
        public double Damage { get; set; } = 0;
        public bool IsCrit { get; set; } = false;
        public bool IsHit { get; set; } = false;

        [JsonIgnore]
        public ISpell Spell { get; set; }
    }
}