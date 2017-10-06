using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Models
{
    public interface ICombat
    {
        double CastTime { get; set; }
        double CurrentTimeSec { get; }
        double GCD { get; set; }
        int RepeatHits { get; }
        Spell CurrentSpell { get; }
        RoundResult NewRound(double currentSec, double intervalSec);
    }
}