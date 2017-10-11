using swlSimulator.api.Combat;
using swlSimulator.api.Spells;

namespace swlSimulator.api.Models
{
    public interface ICombat
    {
        decimal CastTime { get; set; }
        decimal CurrentTimeSec { get; }
        decimal GCD { get; set; }
        Spell CurrentSpell { get; }
        RoundResult NewRound(decimal currentSec, decimal intervalSec);
    }
}