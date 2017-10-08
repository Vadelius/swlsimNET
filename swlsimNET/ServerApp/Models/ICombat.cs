using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Models
{
    public interface ICombat
    {
        decimal CastTime { get; set; }
        decimal CurrentTimeSec { get; }
        decimal GCD { get; set; }
        int RepeatHits { get; }
        Spell CurrentSpell { get; }
        RoundResult NewRound(decimal currentSec, decimal intervalSec);
    }
}