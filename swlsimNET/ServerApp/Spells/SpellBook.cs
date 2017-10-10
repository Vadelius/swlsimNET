namespace swlsimNET.ServerApp.Spells
{
    public class OpeningShot : Buff
    {
        public OpeningShot()
        {
            MaxDuration = 8;
            MaxCooldown = 20;
            MaxBonusCritMultiplier = 0.3;
        }
    }

    public class Savagery : Buff
    {
        public Savagery()
        {
            MaxDuration = 6;
            MaxCooldown = 20;
            MaxBonusBaseDamageMultiplier = 0.15;
        }
    }

    public class Exposed : Debuff
    {
        public Exposed()
        {
            MaxDuration = 0; // uptime 100%
            MaxCooldown = 0;
            MaxBonusDamageMultiplier = 0.1;
        }
    }

// TODO: Add Glaciate 
}